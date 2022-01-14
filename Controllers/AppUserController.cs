using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication.Data;
using WebApplication.HelperFunctions;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AppUserController : Controller
    {
        private MyDbContext context = new MyDbContext();
        public IActionResult Index()
        {
            return View(context.AppUser);
        }

        [HttpGet]
        public IActionResult CreateOrEdit(int id)
        {
            AppUser obj = new AppUser();

            if (id > 0) //edit
            {
                obj = context.AppUser.Find(id);
            }

            //Dropdown List --> City List
            obj.CityList = new SelectList(context.Cities.ToList(), "CityId", "Name");
            //ViewBag.CityList = list;


            //Radio Buttons --> Gender
            obj.GenderList = new SelectList(context.Gender.ToList(), "GenderId", "Name");

            //CheckBox --> Interest List
            //obj.InterestList = new SelectList(context.Interest.ToList(), "InterestId", "Name");

            List<SelectListItem> list = new List<SelectListItem>();
            var interestList = context.Interest.ToList();

            var userInterest = context.UserInterest.Where(x => x.UserId == id).ToList();

            //context.Interest.ToList().ForEach(x => { list.Add(new SelectListItem() { Text = x.Name.ToString(), Value = x.InterestId.ToString() });});

            foreach (var item in interestList)
            {
                SelectListItem element = new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.InterestId.ToString(),
                    Selected = userInterest.Where(x => x.InterestId == item.InterestId).Count() > 0 ? true : false
                };

                list.Add(element);
            }


            obj.InterestList = list;
            //Insert,Edit --> Get

            return View(obj);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(AppUser model)
        {
            if (ModelState.IsValid)
            {
                model.InterestList = model.InterestList.Where(x => x.Selected == true).ToList();

                if (model.UserId > 0) //Edit
                {
                    context.AppUser.Update(model);
                    context.SaveChanges();

                    //remove
                    var list = context.UserInterest.Where(x => x.UserId == model.UserId).ToList();
                    context.UserInterest.RemoveRange(list);
                    context.SaveChanges();

                    //insert
                    List<UserInterest> userInterestList = new List<UserInterest>();
                    model.InterestList.ForEach(x => { userInterestList.Add(new UserInterest() { UserId = model.UserId, InterestId = Convert.ToInt32(x.Value) }); });

                    context.UserInterest.AddRange(userInterestList);
                    context.SaveChanges();
                }
                else
                {
                    context.AppUser.Add(model);
                    context.SaveChanges();

                    List<UserInterest> userInterestList = new List<UserInterest>();
                    model.InterestList.ForEach(x =>{ userInterestList.Add(new UserInterest() { UserId = model.UserId, InterestId = Convert.ToInt32(x.Value) });});

                    //foreach (var item in model.InterestList)
                    //{
                    //    userInterestList.Add(new UserInterest() { UserId = model.UserId, InterestId = Convert.ToInt32(item.Value)});
                    //}

                    context.UserInterest.AddRange(userInterestList);
                    context.SaveChanges();

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult BulkSaveUpload()
        {
            string[] validImageExtensions = { ".png",".jpeg",".jpg"};
            string[] validPdfExtensions = { ".pdf"};

            int imageId = 0;
            
            List<ObjectModel> returnModelList = new List<ObjectModel>();

            var file = Request.Form.Files;
            List<Images> imgList = new List<Images>();
            for (int i = 0; i < file.Count; i++)
            {
                var filename = file[i].FileName;
                var fileSize = file[i].Length;

                var extension = Path.GetExtension(filename);
                bool isImageExtensionValid = true;

                if (!validImageExtensions.Contains(extension)) { isImageExtensionValid = false; }

                string filePath = "2/1/" + (isImageExtensionValid ? "Images/" : "Pdfs/") + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-ff") + "_" + filename;
                string fullPath = Path.Combine("C://ObjectStorage/" + filePath);

                //Save in folder
                bool isLocalSaves = ObjectStorageHelper.PutObject(filePath, file[i]);
                if (isLocalSaves)
                {
                    Images img = new Images() { Name = filename, Extension = extension, FileSize = fileSize, FileType = (isImageExtensionValid ? "Images" : "Pdfs"), FilePath = filePath };
                   context.Images.Add(img);
                   context.SaveChanges();
                   imageId = img.ImageId;

                    var obj = ObjectStorageHelper.GetObject(filePath);

                    ObjectModel returnModel = new ObjectModel();
                    returnModel.ImageId = imageId;
                    returnModel.FilePath = Convert.ToBase64String(obj.fileBytes);

                    returnModelList.Add(returnModel);
                }
            }
            
            //Save in database
            return  Json(returnModelList);
        }
    }
}
