using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomField
    {
        public int CustomFieldID { get; set; }
        public int CustomFieldTypeID { get; set; }
        public string Value { get; set; }
        public int RelateToID { get; set; }

        public virtual CustomFieldType CustomFieldType { get; set; }
    }
}
