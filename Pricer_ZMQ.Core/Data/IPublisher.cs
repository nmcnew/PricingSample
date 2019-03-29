using System.Collections.Generic;
using System.Threading.Tasks;
using Pricer_ZMQ.Core.Data.Model;

namespace Pricer_ZMQ.Core.Data
{

    public interface IPublisher
    {
        void Save(NewPrice price);
        Task SaveAsync(NewPrice price);
        void Save(IEnumerable<NewPrice> price);

    }
}
