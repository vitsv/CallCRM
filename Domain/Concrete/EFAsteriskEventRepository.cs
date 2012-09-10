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
    public class EFAsteriskEventRepository : IRepository<AsteriskEvent>
    {
        private EFDbContext context;

        public EFAsteriskEventRepository()
        {
            context = new EFDbContext();
        }

        public EFAsteriskEventRepository(string connectionString)
        {
            context = new EFDbContext(connectionString);
        }

        public void Insert(AsteriskEvent entity)
        {
            context.AsteriskEvents.Add(entity);
            context.SaveChanges();
        }

        public void Delete(AsteriskEvent entity)
        {
            context.AsteriskEvents.Remove(entity);
            context.SaveChanges();
        }

        public void Update(AsteriskEvent entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<AsteriskEvent> SearchFor(Expression<Func<AsteriskEvent, bool>> predicate)
        {
            return context.AsteriskEvents.Where(predicate);
        }

        public IQueryable<AsteriskEvent> GetAll()
        {
            return context.AsteriskEvents;
        }

        public AsteriskEvent GetById(int id)
        {
            return context.AsteriskEvents.Where(p => p.AsteriskEventID == id).FirstOrDefault();
        }

        private Client GetOrCreateClient(string phoneNumber)
        {
            Client client = context.Clients.Where(c => c.PhoneNumber == phoneNumber).FirstOrDefault();

            if (client == null)
            {
                client = new Client();
                client.PhoneNumber = phoneNumber;
                client.CreationDate = DateTime.Now;
                context.Clients.Add(client);
                context.SaveChanges();
            }

            return client;
        }

        public void UpdateEventStatus(AsteriskEvent astEvent)
        {
            switch (astEvent.Event)
            {
                case AsteriskEvent.DIAL_EVENT_CODE:
                    var updEvent = context.Events.Where(e => e.AsteriskEventUniqueId == astEvent.UniqueId).FirstOrDefault();
                    var status = context.EventStatuses.Where(s => s.Code == astEvent.State).FirstOrDefault();
                    if (status != null)
                        updEvent.EventStatusID = status.EventStatusID;
                    else
                        updEvent.EventStatusID = null;

                    //TODO Tutaj powinien byc tylko czas rozmowy
                    var duration = astEvent.ReceivedTime - updEvent.CreationDate;

                    updEvent.Description = string.Format("Duration: {0}", duration);
                    updEvent.IsComplete = true;

                    context.Entry(updEvent).State = EntityState.Modified;
                    context.SaveChanges();

                    break;
                case AsteriskEvent.NEW_EVENT_STATE_EVENT_CODE:
                    //wedlug destination wykryc kto odebral telefon, i zmienic Employee v Event
                    //Dodac do Event uniqueId, zeby wiedziec ktory Event zmieniac
                    var dialEvent = context.AsteriskEvents.Where(ae => ae.Destination.Contains(astEvent.Channel)).FirstOrDefault();

                    if (dialEvent != null)
                    {
                        var _event = context.Events.Where(e => e.AsteriskEventUniqueId == dialEvent.UniqueId).FirstOrDefault();

                        string AsteriskName = astEvent.Channel.Split('-')[0];
                        AsteriskName = AsteriskName.Split('/')[1];

                        var Employee = context.Employees.Where(e => e.AsteriskName == AsteriskName).FirstOrDefault();

                        if (Employee != null)
                        {
                            _event.EmployeeID = Employee.EmployeeID;
                            context.Entry(_event).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                    break;
            }
        }

        public void AddEvent(AsteriskEvent astEvent)
        {
            Event _event = new Event();

            string number = "";

            if (astEvent.Destination.StartsWith("Local"))
            {
                _event.Inbound = false;
                number = astEvent.Destination.Split('@')[0];
                number = number.Split('/')[1];

                string AsteriskName = astEvent.Channel.Split('-')[0];
                AsteriskName = AsteriskName.Split('/')[1];

                var Employee = context.Employees.Where(e => e.AsteriskName == AsteriskName).FirstOrDefault();

                if (Employee != null)
                    _event.EmployeeID = Employee.EmployeeID;
            }
            else
            {
                _event.Inbound = true;
                number = astEvent.CallerIdNum;
            }

            var client = GetOrCreateClient(number);

            _event.ClientID = client.ClientID;

            _event.EventStatusID = EventStatus.ID_STATUS_RINGING;
            _event.EventCategoryID = EventCategory.ID_CATEGORY_PHONE_CALL;
            _event.IsComplete = false;
            _event.CreationDate = DateTime.Now;
            _event.AsteriskEventUniqueId = astEvent.UniqueId;

            context.Events.Add(_event);
            context.SaveChanges();
        }

    }
}
