using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestQueueApp
{
    class Program
    {
        static BrokeredMessage GetBrokeredMeaagse(string s)
        {
            return new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(s)));
        }
        static QueueConnection conn = new QueueConnection();

        static void Main(string[] args)
        {
            //for (var i = 0; i < 50; i++)
            //{
            //    var message = GetBrokeredMeaagse($"This is a test message {i+1}");
            //    message.Properties.Add("MessageId", Guid.NewGuid().ToString());
            //    message.Properties.Add("MessageType", "WOSegmentCreatedInDbs");
            //    conn.SendMessage(message);
            //}

            Console.WriteLine($"No Of Messgaes: {conn.CountMessages()}");

            Console.ReadLine();

        }

        static void Receive()
        {
            BrokeredMessage message;
            var icount = 1;
            while ((message = conn.Receive()) != null)
            {
                Console.WriteLine($"Message {icount++}:");
                message.Properties.ToList().ForEach(x =>
                {
                    Console.WriteLine($"{x.Key}: {x.Value}");
                });

                string body = "";

                using (var stream = new StreamReader(message.GetBody<Stream>()))
                {
                    body = stream.ReadToEnd();
                }
                Console.WriteLine($"====================================================");
                conn.Delete(message);
            }

        }
    }
}
