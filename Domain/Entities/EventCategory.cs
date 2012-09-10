using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventCategory
    {
        #region Contstants

        public const int ID_CATEGORY_PHONE_CALL = 1;
        public const int ID_CATEGORY_EMAIL = 2;
        public const int ID_CATEGORY_MEETING = 3;

        #endregion

        public int EventCategoryID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsTask { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
    }
}
