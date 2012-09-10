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
    public class EFEventRepository : IEventRepository
    {
        private EFDbContext context = new EFDbContext();

        public void Insert(Event entity)
        {
            context.Events.Add(entity);
            context.SaveChanges();
        }

        public void Update(Event entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Event entity)
        {
            context.Events.Remove(entity);
            context.SaveChanges();
        }
        public IQueryable<Event> SearchFor(Expression<Func<Event, bool>> predicate)
        {
            return context.Events.Where(predicate);
        }
        public IQueryable<Event> GetAll()
        {
            return context.Events;
        }
        public Event GetById(int id)
        {
            return context.Events.Where(p => p.EventID == id).FirstOrDefault();
        }

        public List<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public List<EventCategory> GetEventCategories()
        {
            return context.EventCategories.ToList();
        }

        public List<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public IQueryable<EventStatus> GetEventStatuses()
        {
            return context.EventStatuses;
        }

        public Employee GetEmployee(Guid userID)
        {
            return context.Employees.Where(e => e.UserID == userID).FirstOrDefault();
        }

        public EventCategory GetEventCategory(int id)
        {
            return context.EventCategories.Where(ec => ec.EventCategoryID == id).FirstOrDefault();
        }

    }
}
