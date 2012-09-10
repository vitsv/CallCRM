using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventStatus
    {
        #region Contstants

        public const int ID_STATUS_NONASWER = 1;
        public const int ID_STATUS_NASWER = 2;
        public const int ID_STATUS_BUSY = 3;
        public const int ID_STATUS_CANCEL = 4;
        public const int ID_STATUS_RINGING = 5;

        #endregion

        public int EventStatusID { get; set; }
        public int EventCategoryID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }

        public virtual EventCategory EventCategory { get; set; }
    }
}
