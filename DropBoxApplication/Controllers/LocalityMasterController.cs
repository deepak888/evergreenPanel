using DropBoxApplication.App_Start;
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
    public class LocalityMasterController : BaseController
    {
        // GET: LocalityMaster
        public ActionResult Index()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = "Your application Daily Activity page.";
            return View();            
        }

        [HttpPost]
        public async Task<ActionResult> AddNewLocalityName(FormCollection fc, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                LocalityMasterModel model = new LocalityMasterModel();
                string localityname = fc["LocalityName"];
                string url = GetUrl(2);
                url = url + "Localities/AddNewLocalityName?localityname=" + localityname + "";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(url);
                    LocalityMasterModelRootObject result = new LocalityMasterModelRootObject();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var response = responseMessage.Content.ReadAsStringAsync().Result;
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        result = JsonConvert.DeserializeObject<LocalityMasterModelRootObject>(response);
                        int localityId = result.data.LocalityId;
                        if (localityId > 0)
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
                                string myfile = +localityId + ext; //appending the name with id  
                                                                   // store the file inside ~/project folder(Img)  
                                                                   //var path = Path.Combine(imagepath, myfile);
                                string path = @"C:\inetpub\wwwroot\Data\EverGreen_Android\LocalityPictures\" + Server.HtmlEncode(myfile);
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