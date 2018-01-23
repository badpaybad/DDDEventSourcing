using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.Domain.Commands
{
    public class CreateCheckin : ICommand
    {
        public CreateCheckin(Guid checkinId, int createdBy, int duration, DateTime startDate)
        {
            CheckinId = checkinId;
            CreatedBy = createdBy;
            Duration = duration;
            StartDate = startDate;
        }

        public Guid CheckinId { get; private set; }
        public int CreatedBy { get; private set; }
        public int Duration { get; private set; }
        public DateTime StartDate { get; private set; }
    }
}
