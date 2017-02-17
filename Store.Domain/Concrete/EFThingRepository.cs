using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Domain.Entities;
using Store.Domain.Abstract;

namespace Store.Domain.Concrete
{
    public class EFThingRepository : IThingRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Thing> Things
        {
            get { return context.Things; }
        }

        public void SaveThing(Thing thing)
        {
            if (thing.ThingId == 0)
                context.Things.Add(thing);
            else
            {
                Thing dbEntry = context.Things.Find(thing.ThingId);
                if (dbEntry != null)
                {
                    dbEntry.Name = thing.Name;
                    dbEntry.Description = thing.Description;
                    dbEntry.Price = thing.Price;
                    dbEntry.Category = thing.Category;
                    dbEntry.ImageData = thing.ImageData;
                    dbEntry.ImageMimeType = thing.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public void DeleteThing(Thing thing)
        {
            context.Things.Remove(thing);
            context.SaveChanges();
        }
    }
}
