using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Domain.Entities;

namespace Store.Domain.Abstract
{
    public interface IThingRepository
    {
        IEnumerable<Thing> Things { get; }
        void SaveThing(Thing thing);
        void DeleteThing(Thing thing);
    }


}
