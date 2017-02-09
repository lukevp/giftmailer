using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace GiftMailer
{
    class GiftRecipient
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            { 
                Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} username \"friendly name\" password");
                return;
            }
            var bodyTextTemplate = File.ReadAllText("GiftEmail.txt");
            // count, toName, 
            
            List<List<string>> giftData = JsonConvert.DeserializeObject<List<List<string>>>(File.ReadAllText("GiftRecipients.json"));
            
            List<int> indexes = new List<int>();
            int counter = 0;
            foreach (var i in giftData)
            {
                indexes.Add(counter++);
            }

            bool noDupes = false;
            while (!noDupes)
            {
                noDupes = true;
                indexes.Shuffle();
                for (int j = 0; j < indexes.Count; j++)
                {
                    if (j == indexes[j])
                    {
                        noDupes = false;
                        break;
                    }
                }
            }
            int source = 0;
            foreach (var index in indexes)
            { 
                var body = string.Format(bodyTextTemplate, indexes.Count, giftData[index][1], giftData[index][2]);
                var handler = new GmailHandler(args[0], args[1], args[2]);
                handler.SendEmail(new MailAddress(giftData[source][0], giftData[source][1]), "Gift Assignment Notice!", body);
                source += 1;
            }

        }
    }
}
