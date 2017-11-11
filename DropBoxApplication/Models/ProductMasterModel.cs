using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropBoxApplication.Models;

namespace DropBoxApplication.Models
{
    public class ProductMasterModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
    }

    public class ProductMasterModelRootObject
    {
        public List<ProductMasterModel> data { get; set; }
    }
}