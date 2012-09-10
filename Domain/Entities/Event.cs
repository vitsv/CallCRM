using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event
    {
        public int EventID { get; set; }
        public int? ClientID { get; set; }
        public int? EventStatusID { get; set; }
        public int EventCategoryID { get; set; }
        public int? EmployeeID { get; set; }
        public bool Inbound { get; set; }
        public bool IsComplete { get; set; }
        public string Description { get; set; }
        public bool IsPlanned { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsServedBy { get; set; }
        public string AsteriskEventUniqueId { get; set; }

        public virtual Client Client { get; set; }
        public virtual EventStatus EventStatus { get; set; }
        public virtual EventCategory EventCategory { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
