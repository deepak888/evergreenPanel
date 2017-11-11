using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class StoreMasterModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }

    public class StoreMasterModelRootObject
    {
        public StoreMasterModel data { get; set; }
        public Response response { get; set; }
    }
}