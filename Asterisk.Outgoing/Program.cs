using System;

using Asterisk.NET.Manager;
using Asterisk.NET.Manager.Action;
using Asterisk.NET.Manager.Response;
using Asterisk.NET.FastAGI;
using Asterisk.NET.Manager.Event;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asterisk.Outgoing
{
    class Program
    {
        const string DEV_HOST = "192.168.0.100";
        const int ASTERISK_PORT = 5038;
        const string ASTERISK_HOST = "192.168.0.101";
        const string ASTERISK_LOGINNAME = "manager";
        const string ASTERISK_LOGINPWD = "password";

        private static ManagerConnection manager;
        private static string monitorChannel = null;
        private static string transferChannel = null;

        static void Main(string[] args)
        {
            manager = new ManagerConnection(ASTERISK_HOST, ASTERISK_PORT, ASTERISK_LOGINNAME, ASTERISK_LOGINPWD);

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

            if (args.Count() == 4)
            {
                string originateChannel = args[0];
                string originateCallerId = args[1];
                string originateExten = args[2];
                string originateContext = args[3];
                //string originateChannel = "SIP/billy";
                //string originateCallerId = "<1001>";
                //string originateExten = "1002";
                //string originateContext = "phones";

                OriginateAction oc = new OriginateAction();

                //oc.Channel = originateChannel;
                //oc.CallerId = originateCallerId;
                //oc.Context = originateContext;
                //oc.Exten = originateExten;
                //oc.Priority = 2;
                //oc.Timeout = 15000;

                oc.Channel = originateChannel;
                oc.CallerId = originateCallerId;
                oc.Application = "Dial";
                oc.Data = "Local/" + originateExten + "@" + originateContext;
                oc.Timeout = 15000;

                try
                {
                    ManagerResponse originateResponse = manager.SendAction(oc, oc.Timeout);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: {0}", e.Message);
                }

            }
            manager.Logoff();
        }
    }
}
