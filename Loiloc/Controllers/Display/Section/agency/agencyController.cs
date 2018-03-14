using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loiloc.Models;
using PagedList;
using PagedList.Mvc;
namespace Loiloc.Controllers.Display.Section.agency
{
    public class agencyController : Controller
    {
        // GET: agency
        public ActionResult Index()
        {
            return View();
        }
        LoilocContext db = new LoilocContext();

        public ActionResult listAgency(  int? page)
        {
           
            var listAgency = db.tblAgencies.Where(p =>p.Active == true).OrderByDescending(p => p.Ord).ToList();
             
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = "Danh sách đại lý lõi lọc nước";
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> / Đại lý lõi lọc nước";
            ViewBag.Title = "<title>Danh sách đại lý lõi lọc nước</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"Danh sách đại lý lõi lọc nước chính hãng thuộc Loiloc.com. Hệ thống Lõi lọc nước có mặt tại 63 tỉnh thành trên toàn quốc \"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"Đại lý lõi lọc nước\" /> ";
            return View(listAgency.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult tagAgency(string tag, int? page)
        {

            string[] Mang1 = StringClass.COnvertToUnSign1(tag).Split('-');
            string chuoitag = "";
            for (int i = 0; i < Mang1.Length; i++)
            {
                if (i == 0)
                    chuoitag += Mang1[i];
                else
                    chuoitag += " " + Mang1[i];
            }
            int dem = 1;
            string name = "";
            List<tblAgency> ListNew = (from c in db.tblAgencies where c.Active == true select c).ToList();
            List<tblAgency> listnews = ListNew.FindAll(delegate (tblAgency math)
            {
                if (StringClass.COnvertToUnSign1(math.Tabs.ToUpper()).Contains(chuoitag.ToUpper()))
                {

                    string[] Manghienthi = math.Tabs.Split(',');
                    foreach (var item in Manghienthi)
                    {
                        if (dem == 1)
                        {
                            var kiemtra = StringClass.COnvertToUnSign1(item.ToUpper()).Contains(chuoitag.ToUpper());
                            if (kiemtra == true)
                            {
                                name = item;
                                dem = 0;
                            }
                        }
                    }

                    return true;
                }

                else
                    return false;
            }
            );

            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> / "+ name + "";
            ViewBag.Title = "<title>"+ name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\""+ name + " \"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\""+ name + "\" /> ";
            return View(ListNew.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult agencyDetail(string tag)
        {

            var tblagency = db.tblAgencies.First(p => p.Tag == tag);
             ViewBag.Title = "<title>" + tblagency.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblagency.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblagency.Tabs + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblagency.Name + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblagency.Description + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Loiloc.com/dai-ly" + StringClass.NameToTag(tag) + "\" />";

            meta += "<meta itemprop=\"name\" content=\"" + tblagency.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblagency.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Loiloc.com" + tblagency.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblagency.Name + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Loiloc.com" + tblagency.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Loiloc.com\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblagency.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Descriptionss = tblagency.Description;
            ViewBag.Meta = meta;
     

            //load tag
            string chuoitag = "";
            if (tblagency.Tabs != null)
            {
                string Chuoi = tblagency.Tabs;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    chuoitag += "<h2><a href=\"/tagsAgency/" + StringClass.NameToTag(Mang[i]) + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;

            //Load root

            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> / "+ tblagency.Name+ "";
            return View(tblagency);
        }
     }
}