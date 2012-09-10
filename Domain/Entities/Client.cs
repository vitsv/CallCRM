using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        public int? ContactStageID { get; set; }
        public int? CompanyID { get; set; }
        public int? EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public virtual ContactStage ContactStage { get; set; }
        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
