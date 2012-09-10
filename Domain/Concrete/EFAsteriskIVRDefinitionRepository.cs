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
    public class EFAsteriskIVRDefinitionRepository : IRepository<AsteriskIVRDefinition>
    {
         private EFDbContext context;

        public EFAsteriskIVRDefinitionRepository()
        {
            context = new EFDbContext();
        }

        public EFAsteriskIVRDefinitionRepository(string connectionString)
        {
            context = new EFDbContext(connectionString);
        }

        public void Insert(AsteriskIVRDefinition entity)
        {
            context.AsteriskIVRDefinitions.Add(entity);
            context.SaveChanges();
        }

        public void Delete(AsteriskIVRDefinition entity)
        {
            context.AsteriskIVRDefinitions.Remove(entity);
            context.SaveChanges();
        }

        public void Update(AsteriskIVRDefinition entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<AsteriskIVRDefinition> SearchFor(Expression<Func<AsteriskIVRDefinition, bool>> predicate)
        {
            return context.AsteriskIVRDefinitions.Where(predicate);
        }

        public IQueryable<AsteriskIVRDefinition> GetAll()
        {
            return context.AsteriskIVRDefinitions;
        }

        public AsteriskIVRDefinition GetById(int id)
        {
            return context.AsteriskIVRDefinitions.Where(p => p.AsteriskIVRDefinitionID == id).FirstOrDefault();
        }

    }
}
