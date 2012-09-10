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
    public class EFEventCategoryRepository : IRepository<EventCategory>
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(EventCategory entity)
        {
            context.EventCategories.Add(entity);
            context.SaveChanges();
        }

        public void Delete(EventCategory entity)
        {
            context.EventCategories.Remove(entity);
            context.SaveChanges();
        }

        public void Update(EventCategory entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<EventCategory> SearchFor(Expression<Func<EventCategory, bool>> predicate)
        {
            return context.EventCategories.Where(predicate);
        }

        public IQueryable<EventCategory> GetAll()
        {
            return context.EventCategories;
        }

        public EventCategory GetById(int id)
        {
            return context.EventCategories.Where(p => p.EventCategoryID == id).FirstOrDefault();
        }

    }
}
