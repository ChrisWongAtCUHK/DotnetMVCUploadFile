﻿using Microsoft.AspNetCore.Mvc;
using DotnetMVC.Models;

namespace DotnetMVC.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            SingleFileModel model = new();
            return View(model);
        }

        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new(model.File!.FileName);
                string fileName = model.FileName + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                model.IsSuccess = true;
                model.Message = "File upload successfully";

            }
            return View("Index", model);
        }

        public IActionResult MultiFile()
        {
            MultipleFilesModel model = new();
            return View(model);
        }


        [HttpPost]
        public IActionResult MultiUpload(MultipleFilesModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;
                if (model.Files!.Count > 0)
                {
                    foreach (var file in model.Files)
                    {

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        string fileNameWithPath = Path.Combine(path, file.FileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                    }
                    model.IsSuccess = true;
                    model.Message = "Files upload successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Please select files";
                }
            }

            return View("MultiFile", model);
        }


    }
}
