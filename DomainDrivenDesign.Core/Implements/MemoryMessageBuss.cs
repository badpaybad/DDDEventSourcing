using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DomainDrivenDesign.Core.Commands;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.Reflection;

namespace DomainDrivenDesign.Core.Implements
{
    public static class MemoryMessageBuss
    {

        static readonly Dictionary<Type, List<Action<IEvent>>> _eventHandler = new Dictionary<Type, List<Action<IEvent>>>();
        static readonly Dictionary<Type, Action<ICommand>> _commandHandler = new Dictionary<Type, Action<ICommand>>();

        static object _eventLocker = new object();
        static object _commandLocker = new object();


        static MemoryMessageBuss()
        {

        }

        public static void AutoRegisterExecutingAssembly()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            List<string> allAssemblies = new List<string>();

            string path = Path.GetDirectoryName(executingAssembly.Location);

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
                allAssemblies.Add(dll);


            foreach (var assembly in allAssemblies)
            {
                MemoryMessageBuss.RegisterAssembly(assembly);
            }
        }

        /// <summary>
        /// Can load dynamic assembly (dll file) and register Handle
        /// can load dll from folder Bin eg:
        ///   var executingAssembly = Assembly.GetExecutingAssembly();
        ///   List<string> allAssemblies = new List<string>();
        ///   string path = Path.GetDirectoryName(executingAssembly.Location);
        ///   foreach (string dll in Directory.GetFiles(path, "*.dll"))allAssemblies.Add(dll);
        /// </summary>
        /// <param name="pathFile"></param>
        public static void RegisterAssembly(string pathFile)
        {

            var executingAssembly = Assembly.LoadFile(pathFile);
            var allTypes = executingAssembly.GetTypes();
            var listHandler = allTypes.Where(t => typeof(ICqrsHandle).IsAssignableFrom(t)
                                                  && t.IsClass && !t.IsAbstract).ToList();
            
            Console.WriteLine(pathFile);
            Console.WriteLine($"Found {listHandler.Count} handle(s) to register to message buss");

            foreach (var handlerType in listHandler)
            {
                var cqrsHandler = (ICqrsHandle)Activator.CreateInstance(handlerType);
                if (cqrsHandler == null) continue;

                Console.WriteLine($"Found Handle type: {cqrsHandler.GetType()}");

                MethodInfo[] allMethod = cqrsHandler.GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach (var mi in allMethod)
                {
                    if (!mi.Name.Equals("handle", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var pParameterType = mi.GetParameters().SingleOrDefault().ParameterType;

                    if (typeof(IEvent).IsAssignableFrom(pParameterType))
                    {
                        lock (_eventLocker)
                        {
                            var t = pParameterType;

                            List<Action<IEvent>> ax;

                            if (_eventHandler.TryGetValue(t, out ax))
                            {
                                ax.Add(p =>
                                {
                                    mi.Invoke(cqrsHandler, new object[] { p });
                                });
                            }
                            else
                            {
                                ax = new List<Action<IEvent>>() {
                                    p =>
                                    {
                                        mi.Invoke(cqrsHandler, new object[] { p });
                                    } };
                            }

                            _eventHandler[t] = ax;
                        }
                        Console.WriteLine($"Regsitered method to process event type: {pParameterType}");
                    }

                    if (typeof(ICommand).IsAssignableFrom(pParameterType))
                    {
                        lock (_commandLocker)
                        {
                            var t = pParameterType;

                            Action<ICommand> ax;

                            if (_commandHandler.TryGetValue(t, out ax))
                            {
                                throw new Exception($"Should only one handle to cover type: {t}. Check DomainEngine.Boot");
                            }

                            _commandHandler[t] = (p) =>
                            {
                                mi.Invoke(cqrsHandler, new object[] { p });
                            };
                        }
                        Console.WriteLine($"Regsitered method to process command type: {pParameterType}");

                    }
                }
            }
        }

        public static void RegisterEvent<T>(Action<T> handle) where T : IEvent
        {
            lock (_eventLocker)
            {
                var t = typeof(T);

                List<Action<IEvent>> ax;

                if (_eventHandler.TryGetValue(t, out ax))
                {
                    ax.Add(p => handle((T)p));
                }
                else
                {
                    ax = new List<Action<IEvent>>() { p => handle((T)p) };
                }

                _eventHandler[t] = ax;
            }
        }

        internal static void Push(IEvent e)
        {
            var t = e.GetType();
            List<Action<IEvent>> listAction;
            lock (_eventLocker)
            {
                if (!_eventHandler.TryGetValue(t, out listAction) || listAction == null || listAction.Count == 0)
                {
                    throw new EntryPointNotFoundException($"Not found type: {t}");
                }
            }

            foreach (var a in listAction)
            {
                a(e);
            }
        }



        public static void RegisterCommand<T>(Action<T> handle) where T : ICommand
        {
            lock (_commandLocker)
            {
                var t = typeof(T);

                Action<ICommand> ax;

                if (_commandHandler.TryGetValue(t, out ax))
                {
                    throw new Exception($"Should only one handle to cover type: {t}. Check DomainEngine.Boot");
                }

                _commandHandler[t] = (p) => handle((T)p);
            }
        }

        public static void PushCommand(ICommand e)
        {
            var t = e.GetType();
            Action<ICommand> a;
            lock (_commandLocker)
            {
                if (!_commandHandler.TryGetValue(t, out a) || a == null)
                {
                    throw new EntryPointNotFoundException($"Not found type: {t}. Check DomainEngine.Boot");
                }
            }

            a(e);

        }


    }
}