using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFEmployeeRepository : IRepository<Employee>
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(Employee entity)
        {
            context.Employees.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Employee entity)
        {
            context.Employees.Remove(entity);
            context.SaveChanges();
        }

        public void Update(Employee entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<Employee> SearchFor(Expression<Func<Employee, bool>> predicate)
        {
            return context.Employees.Where(predicate);
        }

        public IQueryable<Employee> GetAll()
        {
            return context.Employees;
        }

        public Employee GetById(int id)
        {
            return context.Employees.Where(p => p.EmployeeID == id).FirstOrDefault();
        }

        public List<AgentStats> GetStatistics(DateTime? startDate, DateTime? endDate)
        {
            var list = from agent in this.context.Employees
                       select new AgentStats
                       {
                           Person = agent.LastName + " " + agent.FirstName,

                           Column1 = context.Events.Where(a => a.EmployeeID == agent.EmployeeID && a.EventCategoryID == 1 && a.Inbound == true && a.CreationDate > startDate && a.CreationDate < endDate).Count(),
                           Column2 = context.Events.Where(a => a.EmployeeID == agent.EmployeeID && a.EventCategoryID == 1 && a.Inbound == false && a.CreationDate > startDate && a.CreationDate < endDate).Count(),
                           Column3 = context.Events.Where(a => a.EmployeeID == agent.EmployeeID && a.EventCategoryID == 2 && a.CreationDate > startDate && a.CreationDate < endDate).Count(),
                           Column4 = context.Events.Where(a => a.EmployeeID == agent.EmployeeID && a.EventCategoryID == 3 && a.CreationDate > startDate && a.CreationDate < endDate).Count()
                       };

            return list.ToList();
        }

    }
}
