using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IClientRepository
    {
        void Insert(Client entity);
        void Update(Client entity);
        void Delete(Client entity);
        IQueryable<Client> SearchFor(Expression<Func<Client, bool>> predicate);
        IQueryable<Client> GetAll();
        Client GetById(int id);
        List<ContactFieldType> GetContactFieldType();
        IQueryable<ContactField> GetContactFields();
        List<Company> GetCompanies();
        List<ContactStage> GetContactStages();
        List<Employee> GetEmployees();
    }
}
