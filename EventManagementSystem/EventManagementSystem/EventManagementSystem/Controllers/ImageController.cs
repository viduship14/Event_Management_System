using EventManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
namespace EventManagementSystem.Controllers
{
    public class ImageController : Controller
    {
        EventMS1Entities db = new EventMS1Entities();
        // GET: Image
        /*THis method will run when admin will click the upload image in event addition
             
             */
        [HttpGet]
        public ActionResult UploadImage()
        {
            List<Event> list = db.Events.ToList();
            ViewBag.EventsList = new SelectList(list, "E_id", "E_name");
            return View();
        }
        /*THis is a overloaded method which will run when user will select the file from view
         *submit the file in the form and will add the file to the server and save the file path to the 
         * database */
        [HttpPost]
        public ActionResult UploadImage(ImageVM imageModel1)
        {
            try
            {
                List<Event> list = db.Events.ToList();
                ViewBag.EventsList = new SelectList(list, "E_id", "E_name");
                string dname = "" + imageModel1.E_id;
                Directory.CreateDirectory(Server.MapPath("~/Content/Images/" + dname));
                string fileName = Path.GetFileNameWithoutExtension(imageModel1.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel1.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel1.Path = "~/Content/Images/" + dname + "/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Images/" + dname + "/"), fileName);
                imageModel1.ImageFile.SaveAs(fileName); //file added to the directory
                Image i = new Image();
                i.E_id = imageModel1.E_id;
                i.Path = imageModel1.Path;
                db.Images.Add(i);             //image path and details added to the database
                db.SaveChanges();                       //to reflect the changes in database
                ModelState.Clear();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View();
        }


        public ActionResult PersonalizeCarousel(int? lo)
        {
            try
            {

                if (lo != null)
                {
                    Image i1 = db.Images.Find(lo);
                    db.Images.Remove(i1);
                    db.SaveChanges();
                }
                Image[] ar = db.Images.ToArray();
                List<String> lPath = new List<String>();
                List<int> lid = new List<int>();
                foreach (Image i in ar)
                {
                    if (i.Event.E_name.Equals("carousel"))
                        lPath.Add(i.Path);
                    lid.Add(i.Image_id);
                }
                ViewBag.lpath = lPath;
                ViewBag.lid = lid;
                return View(ViewBag);


            }
            catch (Exception e)
            {
                return View("Error");
            }
        }



        public ActionResult DisplayEventImages()
        {
            return View();
        }

        public ActionResult AddEventImages()
        {

            return View();
        }
        public ActionResult DeleteEventImages(int E_id)
        {
            try
            {
                Image[] li = db.Images.ToArray();
                Image temp;
                for (int i = 0; i < li.Count(); i++)
                {
                    if (li[i].E_id == E_id)
                    {
                        temp = li[i];
                        db.Images.Remove(temp);
                        db.SaveChanges();
                    }
                }
                var dir = new DirectoryInfo(Server.MapPath("~/Content/Images/" + E_id));
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
            }
            catch (Exception e)
            {
                return View("Error");
            }

            return RedirectToRoute("");
        }
        public ActionResult AddEventZipFolder()
        {

            return View();
        }
        public JsonResult Upload()
        {
            int E_id = 1;
            string isValid = checkIsValid(Request.Files, E_id);
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        private string checkIsValid(HttpFileCollectionBase files, int E_id)
        {
            string isValid = string.Empty;
            try
            {
                foreach (string upload in Request.Files)
                {
                    if (Request.Files[upload].FileName != "")
                    {
                        Directory.CreateDirectory(Server.MapPath("~/App_Data/zip"));
                        string path = Server.MapPath("~/App_Data/zip/");
                        string filename = Path.GetFileName(Request.Files[upload].FileName);
                        string pth = Path.Combine(path, filename);
                        Request.Files[upload].SaveAs(pth);
                        isValid = CheckForTheIcon(pth, E_id); ;
                    }

                }
                var dir = new DirectoryInfo(Server.MapPath("~/App_Data/zip"));
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);
                return isValid;
            }
            catch (Exception e)
            {
                return isValid;
            }
        }
        private string CheckForTheIcon(string strPath, int E_id)
        {
            string result = string.Empty;
            try
            {
                using (ZipArchive za = ZipFile.OpenRead(strPath))
                {
                    Image im = new Image();
                    foreach (ZipArchiveEntry zaItem in za.Entries)
                    {
                        string path = "~/Content/Images/" + E_id + zaItem.FullName;
                        string impath = Path.Combine(Server.MapPath("~/Content/Images/" + E_id), zaItem.FullName);
                        zaItem.ExtractToFile(impath);
                        im.E_id = E_id;
                        im.Path = path;
                        db.Images.Add(im);
                        db.SaveChanges();
                        if (zaItem.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                        {
                            result = "Success";
                        }
                        else
                        {
                            result = "No Image files has been found";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return "Same File";
            }
            return result;
        }
    }
}