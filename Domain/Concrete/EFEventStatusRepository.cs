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
    public class EFEventStatusRepository : IEventStatusRepository
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(EventStatus entity)
        {
            context.EventStatuses.Add(entity);
            context.SaveChanges();
        }

        public void Delete(EventStatus entity)
        {
            context.EventStatuses.Remove(entity);
            context.SaveChanges();
        }

        public void Update(EventStatus entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<EventStatus> SearchFor(Expression<Func<EventStatus, bool>> predicate)
        {
            return context.EventStatuses.Where(predicate);
        }

        public IQueryable<EventStatus> GetAll()
        {
            return context.EventStatuses;
        }

        public EventStatus GetById(int id)
        {
            return context.EventStatuses.Where(p => p.EventStatusID == id).FirstOrDefault();
        }

        public List<EventCategory> GetEventCategories()
        {
            return context.EventCategories.ToList();
        }

    }
}
