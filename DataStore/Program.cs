using System;
using System.Threading;
using MarketPriceGenerator_ZMQ;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;

namespace DataStore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var request = new RequestSocket())
            {
                request.Connect("tcp://localhost:5555");
                while (true)
                {
                    request.SendFrame("GetNewPrice");
                    var message = request.ReceiveMultipartStrings(2);
                    if (message[0].Equals("Accepted"))
                    {
                        var price = JsonConvert.DeserializeObject<NewPrice>(message[1]);
                        if(price != null)
                            Console.WriteLine(price);
                    }
                    Thread.Sleep(100);
                }
            }
        }
    }
}
