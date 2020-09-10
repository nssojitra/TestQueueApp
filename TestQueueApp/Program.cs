using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
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

        static  void Main(string[] args)
        {
                              Console.WriteLine(new DbConnection().GetEmployyeeCount());
                              Console.ReadLine();

        }

          static async void GetSecret()
                    {
                              AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

                              try
                              {
                                        var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                                        var secret = await keyVaultClient.GetSecretAsync("https://testnss.vault.azure.net/secrets/password").ConfigureAwait(false);

                                        Console.WriteLine($"Secret: {secret.Value}");

                                        Console.WriteLine(azureServiceTokenProvider.PrincipalUsed.Type);

                              }
                              catch (Exception exp)
                              {
                                        Console.WriteLine($"Something went wrong: {exp}");
                              }
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
