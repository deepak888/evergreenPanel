﻿using DropBoxApplication.App_Start;
using DropBoxApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DropBoxApplication.Controllers
{
    public class MenuMasterController : BaseController
    {
        // GET: MenuMaster
        public ActionResult Index()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = "Your application Daily Activity page.";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewMenu(FormCollection fc, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                MenuMasterModel model = new MenuMasterModel();
                string menuname = fc["MenuName"];
                string menuprice = fc["MenuPrice"];
                string url = GetUrl(2);
                url = url + "MenuMaster/AddNewMenu?menuname=" + menuname + "&menuprice=" + menuprice + "";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(url);
                    MenuMasterModelRootObject result = new MenuMasterModelRootObject();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var response = responseMessage.Content.ReadAsStringAsync().Result;
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        result = JsonConvert.DeserializeObject<MenuMasterModelRootObject>(response);
                        int menuid = result.Data.MenuId;
                        if (menuid > 0)
                        {
                            var allowedExtensions = new[]
                            {
                                 ".Jpg", ".png", ".jpg", "jpeg",".JPG",
                            };
                            //string imagepath = "http://103.233.79.234/Data/EverGreen_Android/LocalityPictures/";
                            model.ImageUrl = file.ToString(); //getting complete url                         
                            var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                            var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                            if (allowedExtensions.Contains(ext)) //check what type of extension  
                            {
                                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                                string myfile = +menuid + ext; //appending the name with id  
                                                               // store the file inside ~/project folder(Img)  
                                                               //var path = Path.Combine(imagepath, myfile);
                                string path = @"C:\inetpub\wwwroot\Data\EverGreen_Android\\MenuPictures\" + Server.HtmlEncode(myfile);
                                model.ImageUrl = path;
                                file.SaveAs(path);
                            }
                        }
                        else
                        {
                            ViewBag.message = "Please choose only Image file";
                        }
                    }
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }
    }
}