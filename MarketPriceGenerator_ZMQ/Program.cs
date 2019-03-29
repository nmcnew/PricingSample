using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MarketPriceGenerator_ZMQ
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var queue = new ConcurrentQueue<NewPrice>();
            var subscriber = ReceivePrices(queue);
            var receiver = ReceiveRequests(queue);

            await Task.WhenAll(subscriber, receiver);
        }

        private static async Task ReceiveRequests(ConcurrentQueue<NewPrice> queue)
        {
            var task = new Task(() =>
            {
                using (var response = new ResponseSocket())
                {
                    Console.WriteLine("Binding Request Receiver...");
                    response.Bind("tcp://*:5555");
                    Console.WriteLine("Bound");
                    while (true)
                    {

                        var message = response.ReceiveFrameString();
                        Console.WriteLine("Received request: {0}", message);
                        if (!message.Equals("GetNewPrice")) continue;
                        queue.TryDequeue(out var price);
                        response.SendMoreFrame("Accepted").SendFrame(JsonConvert.SerializeObject(price));

                    }
                }
            });

            task.Start();
            await task;
        }

        private static async Task ReceivePrices(ConcurrentQueue<NewPrice> queue)
        {
            var task =  new Task(() =>
            {
                using (var sub = new SubscriberSocket())
                {
                    Console.Write("Binding Subscriber...");
                    sub.Bind("tcp://*:5000");
                    sub.Subscribe("NewPrice");
                    Console.WriteLine("Bound");
                    while (true)
                    {
                        var message = sub.ReceiveMultipartMessage(2);
                        var price = JsonConvert.DeserializeObject<NewPrice>(message[1].ConvertToString());
                        Console.WriteLine("Received Price");
                        queue.Enqueue(price);
                    }
                }
            });

            task.Start();
            await task;
        }
    }
}
