using Microsoft.ServiceBus.Messaging;

namespace TestQueueApp
{
    public class QueueConnection
    { 
        private QueueClient client;
        public QueueConnection()
        {
            client = QueueClient.CreateFromConnectionString("Endpoint=sb://testnayan.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/kr73MZ6aqFSSeH7naEYJf2bJhDbdXHgCJBYwhk1+ik=", "testquueue");
        }
        
        public void SendMessage(BrokeredMessage message)
        {
            client.SendAsync(message).Wait();
        }

        public void DeadLetter(BrokeredMessage message)
        {
            message.DeadLetter("", "");
        }

        public void Abandon(BrokeredMessage message)
        {
            message.Abandon();
        }

        public BrokeredMessage Receive()
        {
            
            return client.ReceiveAsync().Result;
        }

        public int CountMessages()
        {
            var ret = 0;
            BrokeredMessage m;
            while((m=client.PeekAsync().Result)!=null)
            {
                ret++;
            }
            return ret;
        }

        public void Delete(BrokeredMessage message)
        {
            message.Complete();
        }
    }
}
