using System;

using Asterisk.NET.FastAGI;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asterisk.IVR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Asterisk AGI agent started.\nPress CTRL+C to stop agent.");

            AsteriskFastAGI agi = new AsteriskFastAGI();
            agi.Start();
        }
    }
}
