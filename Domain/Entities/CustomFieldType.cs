using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomFieldType
    {
        public int CustomFieldTypeID { get; set; }
        public string Name { get; set; }
        public int Lenght { get; set; }
        public string AddTo { get; set; }
    }
}
