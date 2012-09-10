using System;

using Asterisk.NET.Manager;
using Asterisk.NET.Manager.Action;
using Asterisk.NET.Manager.Response;
using Asterisk.NET.FastAGI;
using Asterisk.NET.Manager.Event;
using Domain.Entities;
using Domain.Concrete;
using System.Linq;

namespace Asterisk.NET.Test
{
    class Program
    {
        const int ASTERISK_PORT = 5038;
        const string ASTERISK_HOST = "192.168.0.100";
        const string ASTERISK_LOGINNAME = "manager";
        const string ASTERISK_LOGINPWD = "password";

        private static ManagerConnection manager;

        [STAThread]
        static void Main()
        {
            checkManagerAPI();
        }

        #region CheckForNewEvents

        private static void CheckForNewEvents()
        {
            var repository = new EFAsteriskEventRepository("Data Source=.\\SQLEXPRESS; Initial Catalog=ccrm_db; Integrated Security=True;");

            var newEvent = repository.SearchFor(e => e.IsServedBy == false && e.IsOutgoing == true).FirstOrDefault();

            if (newEvent != null)
            {
                newEvent.IsServedBy = true;
                repository.Update(newEvent);
              

            }
        }

        #endregion

        #region displayQueue()
        private static void displayQueue()
        {
            manager = new ManagerConnection(ASTERISK_HOST, ASTERISK_PORT, ASTERISK_LOGINNAME, ASTERISK_LOGINPWD);

            try
            {
#if NOTIMEOUT
				manager.Connection.DefaultTimeout = 0;
				manager.Connection.DefaultEventTimeout = 0;
#endif
                manager.Login();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press ENTER to next test or CTRL-C to exit.");
                Console.ReadLine();
                return;
            }

            ResponseEvents re;
            try
            {
                re = manager.SendEventGeneratingAction(new QueueStatusAction());
            }
            catch (EventTimeoutException e)
            {
                // this happens with Asterisk 1.0.x as it doesn't send a QueueStatusCompleteEvent
                re = e.PartialResult;
            }

            foreach (ManagerEvent e in re.Events)
            {
                if (e is QueueParamsEvent)
                {
                    QueueParamsEvent qe = (QueueParamsEvent)e;
                    Console.WriteLine("QueueParamsEvent" + "\n\tQueue:\t\t" + qe.Queue + "\n\tServiceLevel:\t" + qe.ServiceLevel);
                }
                else if (e is QueueMemberEvent)
                {
                    QueueMemberEvent qme = (QueueMemberEvent)e;
                    Console.WriteLine("QueueMemberEvent" + "\n\tQueue:\t\t" + qme.Queue + "\n\tLocation:\t" + qme.Location);
                }
                else if (e is QueueEntryEvent)
                {
                    QueueEntryEvent qee = (QueueEntryEvent)e;
                    Console.WriteLine("QueueEntryEvent" + "\n\tQueue:\t\t" + qee.Queue + "\n\tChannel:\t" + qee.Channel + "\n\tPosition:\t" + qee.Position);
                }
            }
            Console.WriteLine("Press ENTER to next test or CTRL-C to exit.");
            Console.ReadLine();
        }
        #endregion

        #region checkManagerAPI()
        private static void checkManagerAPI()
        {
            manager = new ManagerConnection(ASTERISK_HOST, ASTERISK_PORT, ASTERISK_LOGINNAME, ASTERISK_LOGINPWD);

            manager.NewState += new NewStateEventHandler(dam_NewStateEvent);

            manager.Dial += new DialEventHandler(dam_Dial);



            manager.PingInterval = 0;
            // +++
            try
            {
                manager.Login();			// Login only (fast)

                Console.WriteLine("Asterisk version : " + manager.Version);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                manager.Logoff();
                return;
            }

            Console.ReadLine();

            manager.Logoff();
        }

        static void manager_IgnoreEvent(object sender, NewExtenEvent e)
        {
            // Ignore this event.
        }

        #endregion

        #region Event handlers

        static void dam_Dial(object sender, DialEvent e)
        {
            Console.WriteLine(
                "Dial Event"
                + "\n\tCallerId\t" + e.CallerId
                + "\n\tCallerIdName\t" + e.CallerIdName
                + "\n\tDestination\t" + e.Destination
                + "\n\tDestUniqueId\t" + e.DestUniqueId
                + "\n\tSrc\t\t" + e.Src
                + "\n\tSrcUniqueId\t" + e.SrcUniqueId
                + "\n\tStatus\t" + e.DialStatus
                );
            if(!e.Channel.StartsWith("Local"))
            FillAndAddEventToDB(AsteriskEvent.DIAL_EVENT_CODE, e.Channel, e.DialStatus, e.CallerIdName, e.CallerIdNum, e.UniqueId, e.DateReceived, e.Destination);
        }

        static void dam_NewStateEvent(object sender, NewStateEvent e)
        {
            Console.WriteLine("New State Event"
                + "\n\tChannel\t\t" + e.Channel
                + "\n\tUniqueId\t" + e.UniqueId
                + "\n\tCallerId\t" + e.CallerId
                + "\n\tCallerIdName\t" + e.CallerIdName
                + "\n\tState\t\t" + e.ChannelStateDesc
                + "\n\tDateReceived\t" + e.DateReceived.ToString()
                );
            FillAndAddEventToDB(AsteriskEvent.NEW_EVENT_STATE_EVENT_CODE, e.Channel, e.ChannelStateDesc, e.CallerIdName, e.CallerIdNum, e.UniqueId, e.DateReceived, null);
        }

        #endregion

        #region Data base methods

        static void FillAndAddEventToDB(string eventType, string channel, string state, string callerId, string callerNum, string uniqueId, DateTime receivedTime, string destination)
        {
            AsteriskEvent astEvent = new AsteriskEvent();

            astEvent.Event = eventType;
            astEvent.Channel = channel;
            astEvent.State = state;
            astEvent.CallerIdName = callerId;
            astEvent.CallerIdNum = callerNum;
            astEvent.UniqueId = uniqueId;
            astEvent.ReceivedTime = receivedTime;
            astEvent.Destination = destination;
            astEvent.IsServedBy = false;

            AddEventToDB(astEvent);
        }

        static void AddEventToDB(AsteriskEvent asteriskEvent)
        {

            var repository = new EFAsteriskEventRepository("Data Source=.\\SQLEXPRESS; Initial Catalog=ccrm_db; Integrated Security=True;");

            switch (asteriskEvent.Event)
            {
                case AsteriskEvent.DIAL_EVENT_CODE:
                    repository.Insert(asteriskEvent);
                    if (string.IsNullOrEmpty(asteriskEvent.State))
                        repository.AddEvent(asteriskEvent);
                    else
                        repository.UpdateEventStatus(asteriskEvent);
                    break;
                case AsteriskEvent.NEW_EVENT_STATE_EVENT_CODE:
                    repository.Insert(asteriskEvent);
                    if (asteriskEvent.State == "Up")
                        repository.UpdateEventStatus(asteriskEvent);
                    break;
            }

        }

        #endregion
    }
}