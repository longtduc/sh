using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderMeeting.Web.Models;
using System.Drawing;
using ShareHolderMeeting.Web.Services;
using System.Drawing.Imaging;

namespace ShareHolderMeeting.Web.Controllers
{
    public class CandidateFileController : Controller
    {
        private readonly ShareHolderContext _ctx;       

        public CandidateFileController(ShareHolderContext context)
        {
            _ctx = context;
        }
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {

            if (upload != null && upload.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(upload.FileName));
                    upload.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

            }

            return RedirectToAction("Upload");
        }

        public ActionResult SaveAFileToDB()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveAFileToDB(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                try
                {
                    var file = new CandidateFile
                    {
                        FileType = Models.FileType.Picture,
                        FileName = upload.FileName,
                        CandidateId = 1,
                        ContentType = ""
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        file.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    _ctx.CandidateFiles.Add(file);
                    _ctx.SaveChanges();
                }
                catch (Exception)
                {


                }
            }

            return RedirectToAction("SaveAFileToDB");
        }

        public ActionResult ShowImages_1()
        {
            var files = _ctx.CandidateFiles;
            return View(files);
        }

        public ActionResult ShowImages_2()
        {
            var files = _ctx.CandidateFiles;
            return View(files);
        }

        public void Show(int id)
        {
            var row = _ctx.CandidateFiles.Where(m => m.Id == id).FirstOrDefault();

            byte[] image = row.Content;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(image);
            Response.End();

        }

        public ActionResult UploadAndResize()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadAndResize(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {   
                try
                {
                   Image bmpImg = BhvImageLib.ResizeByWidth(upload.InputStream, 1024);

                    string path = Path.Combine(Server.MapPath("~/Images"),
                                      Path.GetFileName(upload.FileName));                   
               
                    bmpImg.Save(path, ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    Response.Write("Error occured: " + ex.Message.ToString());
                }
            }


            return RedirectToAction("UploadAndResize");
        }

        public ActionResult UploadResizeSaveAFileToDB()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadResizeSaveAFileToDB(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                try
                {
                    var file = new CandidateFile
                    {
                        FileType = Models.FileType.Picture,
                        FileName = upload.FileName,
                        CandidateId = 1,
                        ContentType = "image/jpeg"
                    };
                    
                    var bmpImg = BhvImageLib.ResizeByWidth(upload.InputStream, 1024);

                    bmpImg.Save(@"D:\pic1Resized-XYZ.jpg", ImageFormat.Jpeg);

                    file.Content = BhvImageLib.ImageToByte(bmpImg);

                    _ctx.CandidateFiles.Add(file);
                    _ctx.SaveChanges();

                }
                catch (Exception ex)
                {
                    

                }
            }

            return RedirectToAction("UploadResizeSaveAFileToDB");
        }


    }
}