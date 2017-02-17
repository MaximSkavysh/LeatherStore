using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
    public class ThingController : Controller
    {
        private IThingRepository repository;
        public int pageSize = 4;
        public ThingController(IThingRepository repo)
        {
            repository = repo;
        }

        public FileContentResult GetImage(int thingId)
        {
            Thing thing = repository.Things
                .FirstOrDefault(g => g.ThingId == thingId);

            if (thing != null)
            {
                return File(thing.ImageData, thing.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ViewResult List(string category, int page = 1)
        {
            ThingsListViewModel model = new ThingsListViewModel
            {
                Things = repository.Things
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(thing => thing.ThingId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        repository.Things.Count() :
                        repository.Things.Where(thing => thing.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }


        
    }
}

    

