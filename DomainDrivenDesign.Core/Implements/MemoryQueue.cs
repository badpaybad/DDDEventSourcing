using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Commands;
using DomainDrivenDesign.Core.Events;
using Newtonsoft.Json;

namespace DomainDrivenDesign.Core.Implements
{
    /// <summary>
    /// fake redis queue, azzure buss, rabitmq ...
    /// </summary>
    public static class MemoryQueue
    {
        private static readonly ConcurrentQueue<ICommand> _cmdQueue = new ConcurrentQueue<ICommand>();
        private static readonly ConcurrentQueue<IEvent> _eventQueue = new ConcurrentQueue<IEvent>();

        private static readonly Thread _cmdThread;
        private static readonly Thread _eventThread;

        private static bool _cmdStoped = false;
        private static bool _eventStoped = false;

        private static bool _cmdDone = false;
        private static bool _eventDone = false;

        static MemoryQueue()
        {
            _cmdThread = new Thread(() => LoopTryExecCommand());
            _eventThread = new Thread(() => LoopTryExecEvent());
        }

        public static void Boot()
        {
            _cmdStoped = false;
            _eventStoped = false;
            _cmdThread.Start();
            _eventThread.Start();
        }

        public static void Stop()
        {
            _eventStoped = true;
            _cmdStoped = true;

            //make sure all commands, events processced.
            while (!_cmdDone)
            {
                Thread.Sleep(1);
            }

            while (!_eventDone)
            {
                Thread.Sleep(1);
            }
        }

        private static void LoopTryExecEvent()
        {
            while (!_cmdStoped)
            {
                try
                {
                    _cmdDone = false;
                    ICommand cmd;
                    var batch = new List<ICommand>();
                    while (_cmdQueue.TryDequeue(out cmd) && cmd!=null)
                    {
                        batch.Add(cmd);
                    }
                    if (batch.Count > 0)
                    {
                        foreach (var c in batch)
                        {
                            //try
                            //{
                            //    MemoryMessageBuss.ExecCommand(c);
                            //}
                            //catch (Exception e)
                            //{
                            //    //Consider handle error, error may enqueue or log to other db to process late
                            //    //var cmd = JsonConvert.SerializeObject(cmd)
                            //    //_cmdQueue.Enqueue(c);
                            //}
                            MemoryMessageBuss.ExecCommand(c);
                        }
                    }
                    _cmdDone = true;
                }
                catch (Exception ex)
                {
                    _cmdDone = true;
                    throw new Exception("Error when dequeue " + ex);
                }
                finally
                {
                    Thread.Sleep(1);
                }
            }
        }

        private static void LoopTryExecCommand()
        {
            while (!_eventStoped)
            {
                try
                {
                    _eventDone = false;
                    IEvent evt;
                    var batch = new List<IEvent>();
                    while (_eventQueue.TryDequeue(out evt) && evt!=null)
                    {
                        batch.Add(evt);
                    }
                    if (batch.Count > 0)
                    {
                        foreach (var e in batch)
                        {
                            //try
                            //{
                            //    MemoryMessageBuss.ExecCommand(c);
                            //}
                            //catch (Exception e)
                            //{
                            //    //Consider handle error, error may enqueue or log to other db to process late
                            //    //var cmd = JsonConvert.SerializeObject(cmd)
                            //    //_cmdQueue.Enqueue(c);
                            //}
                            MemoryMessageBuss.ExecEvent(e);
                        }
                    }
                    _eventDone = true;
                }
                catch (Exception ex)
                {
                    _eventDone = true;
                    throw new Exception("Error when dequeue " + ex);
                }
                finally
                {
                    Thread.Sleep(1);
                }
            }
        }

        internal static void PushCommand(ICommand c)
        {
            if (_cmdStoped) throw new Exception("Stoped process command");

            _cmdQueue.Enqueue(c);
        }

        internal static void PushEvent(IEvent e)
        {
            if (_eventStoped) throw new Exception("Stoped process event");

            _eventQueue.Enqueue(e);
        }
    }
}
