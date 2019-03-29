using System.Collections.Generic;
using System.Threading.Tasks;
using Pricer_ZMQ.Core.Data.Model;

namespace Pricer_ZMQ.Core.Data
{
    public class DatabasePublisher : IPublisher
    {
        public void Save(NewPrice price)
        {
            throw new System.NotImplementedException();
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
