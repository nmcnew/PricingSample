using System;
using System.Net.Sockets;
using System.Threading;
using Ninject;
using Pricer_ZMQ.Core.Data;
using Pricer_ZMQ.Core.Data.Model;

namespace QPricer_ZMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please specify a unique instance identifier");
            }

            var kernel = new StandardKernel();
            kernel.Bind<IPublisher>().To<QueuePublisher>().InSingletonScope()
                .WithConstructorArgument("socket", 5000);
            BuildAndSavePrices(kernel.Get<IPublisher>(), args[0]);
        }

        private static void BuildAndSavePrices(IPublisher publisher, string name)
        {
            var r = new Random();
            while (true)
            {
                Console.WriteLine("Sending new Price");
                publisher.Save(new NewPrice()
                {
                    Id = Guid.NewGuid().ToString(),
                    Price = (decimal) (r.Next() + r.Next() * 0.01),
                    Source = name
                });
                Thread.Sleep(500);
            }
        }
    }
}