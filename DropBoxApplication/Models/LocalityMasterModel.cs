using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{

    public class LocalityMasterModel
    {
        public int LocalityId { get; set; }
        public string LocalityName { get; set; }
        public string ImageUrl { get; set; }
        //public HttpPostedFileBase file { get; set; }
    }

    public class LocalityMasterModelRootObject
    {
        public LocalityMasterModel data { get; set; }
        public Response response { get; set; }
    }
}