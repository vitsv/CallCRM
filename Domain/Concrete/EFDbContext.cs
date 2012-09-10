using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {

        public EFDbContext() : base() { }

        public EFDbContext(string stringNameOrConnectionString) : base(stringNameOrConnectionString) { }

        /// <summary>
        /// Clients
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Companies, where clients works
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Events
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Call centre agents
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Addition contact fileds
        /// </summary>
        public DbSet<ContactField> ContactFields { get; set; }

        /// <summary>
        /// Type of contact fields
        /// </summary>
        public DbSet<ContactFieldType> ContactFieldTypes { get; set; }

        /// <summary>
        /// Stages for contacts
        /// </summary>
        public DbSet<ContactStage> ContactStages { get; set; }

        /// <summary>
        /// Defined by user fields
        /// </summary>
        public DbSet<CustomField> CustomFields { get; set; }


        /// <summary>
        /// Custom fields types
        /// </summary>
        public DbSet<CustomFieldType> CustomFieldTypes { get; set; }


        /// <summary>
        /// Category of event
        /// </summary>
        public DbSet<EventCategory> EventCategories { get; set; }


        /// <summary>
        /// Event status
        /// </summary>
        public DbSet<EventStatus> EventStatuses { get; set; }

        /// <summary>
        /// Asterisk events received from IPPBX
        /// </summary>
        public DbSet<AsteriskEvent> AsteriskEvents { get; set; }

        /// <summary>
        /// Definitions for IVR programm
        /// </summary>
        public DbSet<AsteriskIVRDefinition> AsteriskIVRDefinitions { get; set; }

        /// <summary>
        /// Steps for IVR programms
        /// </summary>
        public DbSet<AsteriskIVRStep> AsteriskIVRSteps { get; set; }

    }
}
