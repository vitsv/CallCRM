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
    public class EFContactStageRepository : IRepository<ContactStage>
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(ContactStage entity)
        {
            context.ContactStages.Add(entity);
            context.SaveChanges();
        }

        public void Delete(ContactStage entity)
        {
            context.ContactStages.Remove(entity);
            context.SaveChanges();
        }

        public void Update(ContactStage entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<ContactStage> SearchFor(Expression<Func<ContactStage, bool>> predicate)
        {
            return context.ContactStages.Where(predicate);
        }

        public IQueryable<ContactStage> GetAll()
        {
            return context.ContactStages;
        }

        public ContactStage GetById(int id)
        {
            return context.ContactStages.Where(p => p.ContactStageID == id).FirstOrDefault();
        }

    }
}
