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
    public class EFCompanyRepository : ICompanyRepository
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(Company entity)
        {
            context.Companies.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Company entity)
        {
            context.Companies.Remove(entity);
            context.SaveChanges();
        }

        public void Update(Company entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<Company> SearchFor(Expression<Func<Company, bool>> predicate)
        {
            return context.Companies.Where(predicate);
        }

        public IQueryable<Company> GetAll()
        {
            return context.Companies;
        }

        public Company GetById(int id)
        {
            return context.Companies.Where(p => p.CompanyID == id).FirstOrDefault();
        }

        public List<ContactStage> GetContactStages()
        {
            return context.ContactStages.ToList();
        }

    }
}
