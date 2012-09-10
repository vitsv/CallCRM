using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company
    {
        public int CompanyID { get; set; }
        public int? ContactStageID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ContactStage ContactStage { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
