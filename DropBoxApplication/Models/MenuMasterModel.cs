using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class MenuMasterModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
        public string ImageUrl { get; set; }

    }
    public class MenuMasterModelRootObject
    {
        public List<MenuMasterModel> DataList { get; set; }
        public MenuMasterModel Data { get; set; }
        public Response response { get; set; }
    }
}