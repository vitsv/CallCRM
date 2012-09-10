using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AsteriskEvent
    {
        #region Contstants

        public const string DIAL_EVENT_CODE = "DIAL";
        public const string NEW_EVENT_STATE_EVENT_CODE = "NEW_EVENT_STATE";

        public const string DIAL_STATUS_NONASWER = "NOANSWER";
        public const string DIAL_STATUS_NASWER = "ANSWER";
        public const string DIAL_STATUS_BUSY = "BUSY";
        public const string DIAL_STATUS_CANCEL = "CANCEL";

        #endregion

        public int AsteriskEventID { get; set; }
        public string Event { get; set; }
        public string Channel { get; set; }
        public string State { get; set; }
        public string CallerIdName { get; set; }
        public string CallerIdNum { get; set; }
        public string UniqueId { get; set; }
        public bool IsServedBy { get; set; }
        public DateTime ReceivedTime { get; set; }
        public string Destination { get; set; }
        public bool IsOutgoing { get; set; }
    }
}
