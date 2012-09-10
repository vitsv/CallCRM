using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IEventRepository
    {
        void Insert(Event entity);
        void Update(Event entity);
        void Delete(Event entity);
        IQueryable<Event> SearchFor(Expression<Func<Event, bool>> predicate);
        IQueryable<Event> GetAll();
        Event GetById(int id);
        List<Employee> GetEmployees();
        List<EventCategory> GetEventCategories();
        List<Client> GetClients();
        IQueryable<EventStatus> GetEventStatuses();
        Employee GetEmployee(Guid userID);
        EventCategory GetEventCategory(int id);
    }
}
