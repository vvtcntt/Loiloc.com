using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loiloc.Models;
namespace Loiloc.Controllers.Display.Section.services
{
    public class servicesController : Controller
    {
        // GET: services
        public ActionResult Index()
        {
            return View();
        }
        private LoilocContext db = new LoilocContext();
        public ActionResult servicesDetail(string tag)
        {
            var tblservices = db.tblServices.First(p => p.Tag == tag);
             ViewBag.Title = "<title>" + tblservices.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblservices.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblservices.Keyword + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblservices.Title + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblservices.Description + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Loiloc.com/dich-vu/" + StringClass.NameToTag(tag) + "\" />";

            meta += "<meta itemprop=\"name\" content=\"" + tblservices.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblservices.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Loiloc.com" + tblservices.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblservices.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"news\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Loiloc.com" + tblservices.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Loiloc.com\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblservices.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Descriptionss = tblservices.Description;
            ViewBag.Meta = meta;
            string chuoitag = "";
            if (tblservices.Keyword != null)
            {
                string Chuoi = tblservices.Keyword;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    chuoitag += "<h2><a href=\"/tagServices/" + StringClass.NameToTag(Mang[i]) + "\" title=\"" + Mang[i] + "\"> " + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> > " + tblservices.Name;

            return View(tblservices);
        }
    }
}