using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class CategoryMasterModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
    }

    public class CategoryMasterModelRootObject
    {
        public List<CategoryMasterModel> data { get; set; }
    }
}