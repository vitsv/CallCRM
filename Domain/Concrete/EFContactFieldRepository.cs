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
    public class EFContactFieldRepository : IRepository<ContactField>
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(ContactField entity)
        {
            context.ContactFields.Add(entity);
            context.SaveChanges();
        }

        public void Delete(ContactField entity)
        {
            context.ContactFields.Remove(entity);
            context.SaveChanges();
        }

        public void Update(ContactField entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<ContactField> SearchFor(Expression<Func<ContactField, bool>> predicate)
        {
            return context.ContactFields.Where(predicate);
        }

        public IQueryable<ContactField> GetAll()
        {
            return context.ContactFields;
        }

        public ContactField GetById(int id)
        {
            return context.ContactFields.Where(p => p.ContactFieldID == id).FirstOrDefault();
        }

    }
}
