using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;
using System.Web.Mvc;

namespace Store.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext conrtollerContext,
            ModelBindingContext bildingContext)
        {
            Cart cart = null;
            if (conrtollerContext.HttpContext.Session != null)
            {
                cart = (Cart)conrtollerContext.HttpContext.Session[sessionKey];
            }
            if (cart == null)
            {
                cart = new Cart();
                if (conrtollerContext.HttpContext.Session != null)
                {
                    conrtollerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            
            return cart;
        }
    }
}