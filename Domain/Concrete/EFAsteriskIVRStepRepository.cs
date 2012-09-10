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
    public class EFAsteriskIVRStepRepository : IRepository<AsteriskIVRStep>
    {
        private EFDbContext context;

        public EFAsteriskIVRStepRepository()
        {
            context = new EFDbContext();
        }

        public EFAsteriskIVRStepRepository(string connectionString)
        {
            context = new EFDbContext(connectionString);
        }

        public void Insert(AsteriskIVRStep entity)
        {
            context.AsteriskIVRSteps.Add(entity);
            context.SaveChanges();
        }

        public void Delete(AsteriskIVRStep entity)
        {
            context.AsteriskIVRSteps.Remove(entity);
            context.SaveChanges();
        }

        public void Update(AsteriskIVRStep entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<AsteriskIVRStep> SearchFor(Expression<Func<AsteriskIVRStep, bool>> predicate)
        {
            return context.AsteriskIVRSteps.Where(predicate);
        }

        public IQueryable<AsteriskIVRStep> GetAll()
        {
            return context.AsteriskIVRSteps;
        }

        public AsteriskIVRStep GetById(int id)
        {
            return context.AsteriskIVRSteps.Where(p => p.AsteriskIVRStepID == id).FirstOrDefault();
        }

    }
}
