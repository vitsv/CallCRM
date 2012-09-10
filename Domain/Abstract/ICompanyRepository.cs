using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface ICompanyRepository
    {
        void Insert(Company entity);
        void Update(Company entity);
        void Delete(Company entity);
        IQueryable<Company> SearchFor(Expression<Func<Company, bool>> predicate);
        IQueryable<Company> GetAll();
        Company GetById(int id);
        List<ContactStage> GetContactStages();
    }
}
