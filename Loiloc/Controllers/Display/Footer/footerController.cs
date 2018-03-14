using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loiloc.Models;
using System.Text;

namespace Loiloc.Controllers.Display.Footer
{
   
    public class footerController : Controller
    {
        private LoilocContext db = new LoilocContext();
        //
        // GET: /footer/
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult footerPartial()
        {
            var listBaoGia = db.tblGroupProducts.Where(p => p.Active == true && p.Baogia == true).OrderBy(p => p.Ord).Take(5).ToList();
            StringBuilder resultBaoGia = new StringBuilder();
            for(int i=0;i<listBaoGia.Count;i++)
            {
                resultBaoGia.Append("<li><a href=\"/bao-gia/" + listBaoGia[i].Tag + "\" title=\"Báo giá " + listBaoGia[i].Name+ "\">Báo giá " + listBaoGia[i].Name + "</a></li>");
            }
            ViewBag.resultBaoGia = resultBaoGia.ToString();
            var listService = db.tblServices.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder resultService = new StringBuilder();
            for (int i = 0; i < listService.Count; i++)
            {
                resultService.Append("<li><a href =\"/dich-vu/" + listService[i].Tag + "\" rel=\"nofollow\" title = \"" + listService[i].Name + "\" >" + listService[i].Name + " </a></li>");
            }
            ViewBag.resultService = resultService.ToString();
            var listDichVu = db.tblNews.Where(p => p.Active == true && p.idCate==3).OrderBy(p => p.Ord).ToList();
            StringBuilder resultDichvu = new StringBuilder();
            for (int i = 0; i < listDichVu.Count; i++)
            {
                resultDichvu.Append("<li><a href =\"/tin-tuc/" + listDichVu[i].Tag + "\" rel=\"nofollow\" title = \"" + listDichVu[i].Name + "\" >" + listDichVu[i].Name + " </a></li>");
            }
            ViewBag.resultDichvu = resultDichvu.ToString();
            StringBuilder resultAngecy = new StringBuilder();
            var listAngecy = db.tblHotlines.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            for(int i=0;i<listAngecy.Count;i++)
            {
                resultAngecy.Append("<li>");
                resultAngecy.Append(" <span> Chi nhánh "+listAngecy[i].Name+":</span>");
                resultAngecy.Append("<div class=\"desc\">");
                resultAngecy.Append("<p>" + listAngecy[i].Address + "(<a class=\"map -hcm pointer\" title=\"Bản đồ\">xem bản đồ</a>)</p>");
                resultAngecy.Append("<p>Tel: " + listAngecy[i].Mobile + " - " + listAngecy[i].Hotline + "  -  E-mail: " + listAngecy[i].Email + "</p>");
                resultAngecy.Append("</div>");
                resultAngecy.Append("</li>");
            }
            ViewBag.resultAgency = resultAngecy.ToString();
            StringBuilder resultSupport = new StringBuilder();
            var listSupport = db.tblSupports.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            for(int i=0;i<listSupport.Count;i++)
            {
                resultSupport.Append("<p>Hỗ trợ khách hàng "+(i+1)+" <a class=\"icon\" href=\"Skype:"+listSupport[i].Skype+ "?chat\"></a> <span class=\"email\">" + listSupport[i].Email + "</span></p>");
            }
            ViewBag.resultSupport = resultSupport.ToString();
            return PartialView(db.tblConfigs.First());
        }
	}
}