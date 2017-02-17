using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IThingRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IThingRepository repo,IOrderProcessor processor)
        {

            repository = repo;
            orderProcessor = processor;
        }

        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            return View(new ShippingDetails());
        }


        [HttpPost]
        public ViewResult CheckOut(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your's cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        

        public RedirectToRouteResult AddToCart(Cart cart,int thingId, string returnUrl)
        {
            Thing thing = repository.Things
                .FirstOrDefault(g => g.ThingId == thingId);

            if (thing != null)
            {
                cart.AddItem(thing, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int thingId, string returnUrl)
        {
            Thing thing = repository.Things
                .FirstOrDefault(g => g.ThingId == thingId);

            if (thing != null)
            {
                cart.RemoveLine(thing);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

       
    }
}
