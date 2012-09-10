using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class AgentStats
    {
        public string Person { get; set; }
        public int Column1 { get; set; }
        public int Column2 { get; set; }
        public int Column3 { get; set; }
        public int Column4 { get; set; }

        public int Sum
        {
            get { return Column1 + Column2 + Column3 + Column4; }
        }
    }
}
