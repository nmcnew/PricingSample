using System.Collections.Generic;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using Pricer_ZMQ.Core.Data.Model;

namespace Pricer_ZMQ.Core.Data
{
    public class QueuePublisher : IPublisher
    {
        private readonly PublisherSocket _pub;

        public QueuePublisher(int socket)
        {
            _pub = new PublisherSocket();
            _pub.Connect($"tcp://localhost:{socket}");
        }

        public void Save(NewPrice price)
        {
            var priceString = JsonConvert.SerializeObject(price);
            _pub.SendMoreFrame("NewPrice").SendFrame(priceString);
        }

        public Task SaveAsync(NewPrice price)
        {
            throw new System.NotImplementedException();
        }

        public void Save(IEnumerable<NewPrice> price)
        {
            throw new System.NotImplementedException();
        }
    }
}