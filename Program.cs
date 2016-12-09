using System;
using System.Net.Mail;

namespace GiftMailer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            { 
                Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} username \"friendly name\" password inputfile");
                return;
            }

            var handler = new GmailHandler(args[0], args[1], args[2]);
            handler.SendEmail(new MailAddress("someaddress", "Some Name"), "Gift Assignment!", "You are assigned Some Name.");
            
        }
    }
}
