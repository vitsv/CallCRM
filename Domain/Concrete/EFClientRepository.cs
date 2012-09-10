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
    public class EFClientRepository : IClientRepository
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(Client entity)
        {
            context.Clients.Add(entity);
            context.SaveChanges();
        }

        public void Update(Client entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Client entity)
        {
            context.Clients.Remove(entity);
            context.SaveChanges();
        }
        public IQueryable<Client> SearchFor(Expression<Func<Client, bool>> predicate)
        {
            return context.Clients.Where(predicate);
        }
        public IQueryable<Client> GetAll()
        {
            return context.Clients;
        }
        public Client GetById(int id)
        {
            return context.Clients.Where(p => p.ClientID == id).FirstOrDefault();
        }

        public List<ContactFieldType> GetContactFieldType()
        {
            return context.ContactFieldTypes.ToList();
        }

        public IQueryable<ContactField> GetContactFields()
        {
            return context.ContactFields;
        }

        public List<Company> GetCompanies()
        {
            return context.Companies.ToList();
        }

        public List<ContactStage> GetContactStages()
        {
            return context.ContactStages.ToList();
        }

        public List<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

    }
}
