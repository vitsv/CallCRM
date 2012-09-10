using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ContactField
    {
        public int ContactFieldID { get; set; }
        public int ContactFieldTypeID { get; set; }
        public string Value { get; set; }
        public int? RelateToID { get; set; }

        public virtual ContactFieldType ContactFieldType { get; set; }

    }
}
