using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;


namespace Store.WebUI.Models
{
    public class ThingsListViewModel
    {
        public IEnumerable<Thing> Things { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}