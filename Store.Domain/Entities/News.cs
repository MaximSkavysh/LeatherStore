using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Domain.Entities
{
    public class News
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageType { get; set; }
    }
}
