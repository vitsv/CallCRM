using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ContactStage
    {
        public int ContactStageID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }

    }
}
