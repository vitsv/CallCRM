using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AsteriskIVRDefinition
    {

        public int AsteriskIVRDefinitionID { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<AsteriskIVRStep> AsteriskIVRSteps { get; set; }

    }
}
