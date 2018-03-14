using Loiloc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loiloc.Controllers.Display
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        private LoilocContext db = new LoilocContext();
        public ActionResult Index()
        {
            tblConfig config = db.tblConfigs.First();
            ViewBag.Title = "<title>" + config.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + config.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + config.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + config.Keywords + "\" /> ";
            ViewBag.h1 = "<h1 class=\"h1\">" + config.Title + "</h1>";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Loiloc.com\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + config.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + config.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://loiloc.com" + config.Logo + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + config.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Loiloc.com" + config.Logo + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Loiloc\" />";
            meta += "<meta property=\"og:description\" content=\"" + config.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.chuoi = config.Content;
            return View();
        }
        public PartialViewResult partialdefault()
        {
            return PartialView(db.tblConfigs.First());
        }
        public PartialViewResult transportPartial()
        {
            return PartialView();
        }
         public PartialViewResult tuVanDangKyPartial()
        {
            return PartialView(db.tblConfigs.First());
        }
    }
}