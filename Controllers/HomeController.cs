using Library.Models;
using Library.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        LibraryDB db= new LibraryDB();
        // GET: Home
        public ActionResult Index(int? id)
        {
           VmBook  oBook = null;
            var oBM = db.BookMasters.Where(x => x.BookId == id).FirstOrDefault();
            if (oBM != null)
            {
                oBook = new VmBook();
                oBook.BookId = oBM.BookId;
                oBook.BookName = oBM.BookName;
                oBook.PublishDate = oBM.PublishDate;
                oBook.Available = oBM.Available;
                oBook.Image = oBM.Image;
                var listBookDetail = new List<VmBookDetail>();
                var listBD = db.BookDetails.Where(x => x.BookId == id).ToList();
                foreach (var item in listBD)
                {
                    var oBookDetail = new VmBookDetail();
                    oBookDetail.BookDetailId = item.BookDetailId;
                    oBookDetail.AuthorName = item.AuthorName;
                    oBookDetail.Price = item.Price;
                    listBookDetail.Add(oBookDetail);
                }
                oBook.LBookDetails=listBookDetail;
            }
            oBook = oBook == null ? new VmBook() : oBook;
            ViewData["List"] = db.BookMasters.ToList();
            return View(oBook);
        }
        [HttpPost]
        public ActionResult Index(VmBook model, HttpPostedFileBase img)
        {
            var oBookMaster = db.BookMasters.Find(model.BookId);
            string filename = img?.FileName;
            if (img != null)
            {
                string path = Path.Combine(Server.MapPath("~/Pictures/"), filename);
                img.SaveAs(path);
                model.Image = "/Pictures/" + filename;
            }
            if (oBookMaster == null)
            {
                oBookMaster = new BookMaster();
                oBookMaster.BookName = model.BookName;
                oBookMaster.PublishDate = model.PublishDate;
                oBookMaster.Available = model.Available;
                oBookMaster.Image = "/Pictures/" + filename;
                db.BookMasters.Add(oBookMaster);
                var listBookDetail = new List<BookDetail>();
                for (var i = 0; i < model.AuthorName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(model.AuthorName[i]))
                    {
                        var oBookDetail = new BookDetail();
                        oBookDetail.BookId = oBookMaster.BookId;
                        oBookDetail.AuthorName = model.AuthorName[i];
                        oBookDetail.Price = model.Price[i];
                        listBookDetail.Add(oBookDetail);
                    }
                }
                db.BookMasters.Add(oBookMaster);
                db.BookDetails.AddRange(listBookDetail);
                db.SaveChanges();
            }
            else
            {
                oBookMaster.BookName = model.BookName;
                oBookMaster.PublishDate = model.PublishDate;
                oBookMaster.Available = model.Available;
                oBookMaster.Image = (filename != null) ? "/Pictures/" + filename : oBookMaster.Image;
                db.BookMasters.Add(oBookMaster);
                var oBookMasterRem = db.BookMasters.Find(model.BookId);
                var oBookDetailRem = db.BookDetails.Where(x => x.BookId == model.BookId).ToList();
                db.BookMasters.Remove(oBookMasterRem);
                db.BookDetails.RemoveRange(oBookDetailRem);
                db.SaveChanges();
                var listBookDetail = new List<BookDetail>();
                for (var i = 0; i < model.AuthorName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(model.AuthorName[i]))
                    {
                        var oBookDetail = new BookDetail();
                        oBookDetail.BookId = oBookMaster.BookId;
                        oBookDetail.AuthorName = model.AuthorName[i];
                        oBookDetail.Price = model.Price[i];
                        listBookDetail.Add(oBookDetail);
                    }
                }
                db.BookMasters.Add(oBookMaster);
                db.BookDetails.AddRange(listBookDetail);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult IndexDelete(int id)
        {
            var oBookMaster = (from o in db.BookMasters where o.BookId == id select o).FirstOrDefault();
            var oBookDetail = (from o in db.BookDetails where o.BookId == id select o).ToList();
            if (oBookMaster != null && oBookDetail != null)
                db.BookMasters.Remove(oBookMaster);
            db.BookDetails.RemoveRange(oBookDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}