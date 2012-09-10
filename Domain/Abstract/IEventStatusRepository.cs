using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IEventStatusRepository
    {
        void Insert(EventStatus entity);
        void Update(EventStatus entity);
        void Delete(EventStatus entity);
        IQueryable<EventStatus> SearchFor(Expression<Func<EventStatus, bool>> predicate);
        IQueryable<EventStatus> GetAll();
        EventStatus GetById(int id);
        List<EventCategory> GetEventCategories();
    }
}
