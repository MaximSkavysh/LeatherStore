using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;

namespace Store.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IThingRepository repository;

        public AdminController (IThingRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Things);
        }
        public ViewResult Edit(int thingId)
        {
            Thing thing = repository.Things
                .FirstOrDefault(g => g.ThingId == thingId);
             
           return View(thing);
        }
        [HttpPost]
        public ActionResult Edit(Thing thing, HttpPostedFileBase image=null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    thing.ImageMimeType = image.ContentType;
                    thing.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(thing.ImageData, 0, image.ContentLength);

                }

                
                    repository.SaveThing(thing);
                    TempData["message"] = string.Format("Changing \"{0}\" was saved", thing.Name);
                    return RedirectToAction("Index");

                
            }
            else
            {
                return View(thing);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Thing());
        
        }

       

       
        [HttpPost]
        public ActionResult Delete(int thingId)
        {
            var product = repository.Things
                .Where(prod => prod.ThingId == thingId)
                .FirstOrDefault();

            if (product != null)
            {
                repository.DeleteThing(product);
                TempData["message"] = string.Format("{0} has been deleted", product.Name);
            }

            return RedirectToAction("Index");
        }
        
        



    }
}
