using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loiloc.Models;
using System.Text;

namespace Loiloc.Controllers.Display.Header
{
    public class headerController : Controller
    {
        //
        // GET: /header/
        LoilocContext db = new LoilocContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult headerPartial()
        {
            var tblConfig = db.tblConfigs.FirstOrDefault();
            var listMenu = db.tblGroupProducts.Where(p => p.ParentID == null && p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for(int i=0;i<listMenu.Count;i++)
            {
                result.Append("<div class=\"tearMenuTop\">");
                result.Append("<a class=\"nameMenu\" href=\"/"+listMenu[i].Tag+".html\" title=\""+listMenu[i].Name+ "\">" + listMenu[i].Name + "</a>");
                int id = listMenu[i].id;
                var listChild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == id).OrderBy(p => p.Ord).ToList();
               if(listChild.Count>0)
                {
                    result.Append("<ul class=\"ul2\">");
                    for (int j = 0; j < listChild.Count; j++)
                    {
                        result.Append("<li class=\"li2\"><a href = \"/"+listChild[j].Tag+".html\" title=\""+listChild[j].Name+ "\">" + listChild[j].Name + "</a></li>");
                    }                    
                    result.Append(" </ul>");
                }                
                result.Append("</div>");
            }
            ViewBag.result = result.ToString();
            var listService = db.tblServices.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder resultService = new StringBuilder();
            for(int i=0;i<listService.Count;i++)
            {
                resultService.Append("<li><a href =\"/dich-vu/"+listService[i].Tag+"\" rel=\"nofollow\" title = \""+listService[i].Name+ "\" >" + listService[i].Name + " </a></li>");
            }
            ViewBag.resultService = resultService.ToString();
            var imagesTop = db.tblImages.FirstOrDefault(p => p.idCate == 2);
            if(imagesTop!=null)
            {
                ViewBag.images = "<a href=\"" + imagesTop.Url + "\" title=\"" + imagesTop.Name + "\"><img src=\"" + imagesTop.Images + "\" alt=\"" + imagesTop.Name + "\" /></a>";

            }
            var listBaoGia = db.tblGroupProducts.Where(p => p.Active == true && p.Baogia == true).OrderBy(p => p.Ord).ToList();
            StringBuilder resultBaoGia = new StringBuilder();
            for (int i = 0; i < listBaoGia.Count; i++)
            {
                resultBaoGia.Append("<li><a href=\"/bao-gia/" + listBaoGia[i].Tag + "\" rel=\"nofollow\" title=\"Báo giá " + listBaoGia[i].Name + "\">Báo giá " + listBaoGia[i].Name + "</a></li>");
            }
            ViewBag.resultBaoGia = resultBaoGia.ToString();
            return PartialView(tblConfig);
        }
 	}
}