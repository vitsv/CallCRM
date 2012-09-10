using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        public string Solt { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid UserID { get; set; }
        public string AsteriskName { get; set; }

    }
}
