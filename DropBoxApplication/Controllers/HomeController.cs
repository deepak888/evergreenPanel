using DropBoxApplication.App_Start;
using DropBoxApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DropBoxApplication.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //    string preurl = GetUrl(2);
            //    preurl = preurl + "Product/GetAllProductList";
            //    ProductMasterModelRootObject pobj = new ProductMasterModelRootObject();
            //    List<ProductMasterModel> Prlist = new List<ProductMasterModel>();
            //    using (HttpClient prclient = new HttpClient())
            //    {
            //        HttpResponseMessage responseMessage = await prclient.GetAsync(preurl);
            //          if (responseMessage.IsSuccessStatusCode)
            //        {
            //        var result = responseMessage.Content.ReadAsStringAsync().Result;
            //        pobj = JsonConvert.DeserializeObject<ProductMasterModelRootObject>(result);
            //        IList<SelectListItem> ProSelectList = new List<SelectListItem>();
            //        foreach (var item in pobj.data)
            //        {
            //            ProSelectList.Add(new SelectListItem { Text = item.ProductName, Value = item.ProductId.ToString() });
            //        }
            //        ProSelectList.Insert(0, new SelectListItem() { Value = "", Text = "Select Product" });
            //        ProSelectList.Insert(1, new SelectListItem() { Value = "0", Text = "All" });
            //        ViewBag.ProductList = ProSelectList;
            //    }
            //}
            //    return View(new MenuSetupMasterModel());
            return View();
        }

    public ActionResult About()
    {
        ViewBag.Message = "Your application description page.";

        return View();
    }

    public ActionResult Contact()
    {
        ViewBag.Message = "Your contact page.";

        return View();
    }

    public ActionResult Logout()
    {
        Session.Abandon();
        Session.RemoveAll();
        return RedirectToAction("Index", "Home");
    }
}
}