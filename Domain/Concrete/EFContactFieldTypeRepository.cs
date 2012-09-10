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
    public class EFContactFieldTypeRepository : IRepository<ContactFieldType>
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(ContactFieldType entity)
        {
            context.ContactFieldTypes.Add(entity);
            context.SaveChanges();
        }

        public void Delete(ContactFieldType entity)
        {
            context.ContactFieldTypes.Remove(entity);
            context.SaveChanges();
        }

        public void Update(ContactFieldType entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<ContactFieldType> SearchFor(Expression<Func<ContactFieldType, bool>> predicate)
        {
            return context.ContactFieldTypes.Where(predicate);
        }

        public IQueryable<ContactFieldType> GetAll()
        {
            return context.ContactFieldTypes;
        }

        public ContactFieldType GetById(int id)
        {
            return context.ContactFieldTypes.Where(p => p.ContactFieldTypeID == id).FirstOrDefault();
        }

    }
}
