﻿using DropBoxApplication.App_Start;
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
    public class MenuSetupMasterController : BaseController
    {
        // GET: MenuSetupMaster
        public async Task<ActionResult> Index()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = "Your application Daily Activity page.";

            //string url = GetUrl(2);
            //url = url + "Localities/GetAllLocalitiesWithOutPagging";

            //LocalityMasterModelRootObject obj = new LocalityMasterModelRootObject();
            //using (HttpClient client = new HttpClient())
            //{
            //    HttpResponseMessage responseMessage = await client.GetAsync(url);
            //    if (responseMessage.IsSuccessStatusCode)
            //    {
            //        var result = responseMessage.Content.ReadAsStringAsync().Result;
            //        obj = JsonConvert.DeserializeObject<LocalityMasterModelRootObject>(result);

            //        IList<SelectListItem> LSelectList = new List<SelectListItem>();
            //        foreach (var item in obj.data)
            //        {
            //            LSelectList.Add(new SelectListItem { Text = item.LocalityName, Value = item.LocalityId.ToString() });
            //        }
            //        LSelectList.Insert(0, new SelectListItem() { Value = "", Text = "Select Locality" });

            //        LSelectList.Insert(1, new SelectListItem() { Value = "0", Text = "All" });

            //        ViewBag.LocalityList = LSelectList;
            //    }
            //}

            string murl = GetUrl(2);
            murl = murl + "MenuMaster/GetAllMenuList";
            MenuMasterModelRootObject mobj = new MenuMasterModelRootObject();
            //List<MenuMasterModel> mlist = new List<MenuMasterModel>();
            using (HttpClient cclient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await cclient.GetAsync(murl);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    mobj = JsonConvert.DeserializeObject<MenuMasterModelRootObject>(result);
                    IList<SelectListItem> mSelectList = new List<SelectListItem>();
                    foreach (var item in mobj.DataList)
                    {
                        mSelectList.Add(new SelectListItem { Text = item.MenuName, Value = item.MenuId.ToString() });
                    }
                    mSelectList.Insert(0, new SelectListItem() { Value = "", Text = "Select Menu" });
                    mSelectList.Insert(1, new SelectListItem() { Value = "0", Text = "All" });
                    ViewBag.MenuList = mSelectList;
                }
            }

            string Clienturl = GetUrl(2);
            Clienturl = Clienturl + "Category/GetAllCategoryList";
            CategoryMasterModelRootObject cobj = new CategoryMasterModelRootObject();
            //List<CategoryMasterModel> clist = new List<CategoryMasterModel>();
            using (HttpClient cclient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await cclient.GetAsync(Clienturl);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    cobj = JsonConvert.DeserializeObject<CategoryMasterModelRootObject>(result);
                    IList<SelectListItem> CSelectList = new List<SelectListItem>();
                    foreach (var item in cobj.data)
                    {
                        CSelectList.Add(new SelectListItem { Text = item.CategoryName, Value = item.CategoryId.ToString() });
                    }
                    CSelectList.Insert(0, new SelectListItem() { Value = "", Text = "Select Category" });
                    CSelectList.Insert(1, new SelectListItem() { Value = "0", Text = "All" });
                    ViewBag.CategoryList = CSelectList;
                }
            }
            string preurl = GetUrl(2);
            preurl = preurl + "Product/GetAllProductList";
            ProductMasterModelRootObject pobj = new ProductMasterModelRootObject();
            //List<ProductMasterModel> Prlist = new List<ProductMasterModel>();
            using (HttpClient prclient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await prclient.GetAsync(preurl);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    pobj = JsonConvert.DeserializeObject<ProductMasterModelRootObject>(result);
                    IList<SelectListItem> ProSelectList = new List<SelectListItem>();
                    foreach (var item in pobj.data)
                    {
                        ProSelectList.Add(new SelectListItem { Text = item.ProductName, Value = item.ProductId.ToString() });
                    }
                    ProSelectList.Insert(0, new SelectListItem() { Value = "", Text = "Select Product" });
                    ProSelectList.Insert(1, new SelectListItem() { Value = "0", Text = "All" });
                    ViewBag.ProductList = ProSelectList;
                }
            }
            return View(new MenuSetupMasterModel());
        }

        [HttpPost]
        public async Task<PartialViewResult> SetupMenu(MenuSetupMasterModel model)
        {
            List<MenuSetUpViewModel> olist = new List<MenuSetUpViewModel>();

            MenuSetUpViewModelObjectRootModel obj = new MenuSetUpViewModelObjectRootModel();

            string url = GetUrl(2);           
            url = url + "MenuSetup/SetupMenu?MenuId=" + Convert.ToInt32(model.MenuName) + "&CategoryId=" + Convert.ToInt32(model.CategoryName) + "&ProductId=" + Convert.ToInt32(model.ProductName) + "";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    obj = JsonConvert.DeserializeObject<MenuSetUpViewModelObjectRootModel>(response, settings);
                    olist = obj.data;
                    ViewBag.TransactionList = olist;
                }
            }
            return PartialView("_MenuSetUplist", olist);           
        }
    }
}