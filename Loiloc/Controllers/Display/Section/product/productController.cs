using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loiloc.Models;
using System.Text;
using PagedList;
using PagedList.Mvc;
namespace Loiloc.Controllers.Display.Header.product
{
    public class productController : Controller
    {
        private LoilocContext db = new LoilocContext();
        //
        // GET: /product/
        public ActionResult Index()
        {
            return View();
        }
        List<string> Mangphantu = new List<string>();
        public List<string> Arrayid(int idParent)
        {

            var ListMenu = db.tblGroupProducts.Where(p => p.ParentID == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].id.ToString());
                int id = int.Parse(ListMenu[i].id.ToString());
                Arrayid(id);

            }

            return Mangphantu;
        }
        public List<string> ArrayidParent(int idParent)
        {

            var ListMenu = db.tblGroupProducts.FirstOrDefault(p => p.id == idParent);
            if(ListMenu!=null)
            {
                Mangphantu.Add(ListMenu.id.ToString());
                string Parentid = ListMenu.ParentID.ToString();
                if(Parentid != null && Parentid!="")
                {
                    int id = int.Parse(Parentid);
                    ArrayidParent(id);
                }
                
            }             

         

            return Mangphantu;
        }
        string nUrl = "";
        public string UrlProduct(int idCate)
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = "<li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" title=\"" + ListMenu[i].Title + "\" href=\"/" + ListMenu[i].Tag + ".html\"> <span itemprop=\"name\">" + ListMenu[i].Name + "</span></a> <meta itemprop=\"position\" content=\"" + (ListMenu[i].Level + 2) + "\" /> </li> ›" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }
        public PartialViewResult productHomesPartial()
        {
            var listCapacity = db.tblCapacities.Where(p => p.Active == true && p.Priority == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listCapacity.Count;i++ )
            {
                result.Append(" <div class=\"product\">");
                result.Append(" <div class=\"nvarProduct\">");
                result.Append("<div class=\"leftNvarProduct\">");
                result.Append("<h2><a href=\"/loi-loc-nuoc/" + listCapacity[i].Tag + "\" title=\"" + listCapacity[i].Name + "\">" + listCapacity[i].Name + "</a></h2>");
                result.Append("</div>");
                result.Append(" <div class=\"rightNvarProduct\">");
                result.Append("<a href=\"/loi-loc-nuoc/" + listCapacity[i].Tag + "\" title=\"" + listCapacity[i].Name + "\">Xem thêm <span></span></a>");
                result.Append("</div>");
                result.Append(" </div>");
                result.Append("<div class=\"contentProduct\"> ");
                int idCap = listCapacity[i].id;
                var listProduct = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap).OrderBy(p => p.idCate).ToList();
                for (int j = 0; j < listProduct.Count;j++ )
                {
                    result.Append("<div class=\"tear1\">");
                    if(listProduct[j].New==true)
                    {
                        result.Append("<div class=\"new\"><span>New</span></div>");
                    }

                    result.Append("<div class=\"compare\">");
                    result.Append("<a href=\"#\" title=\"" + listProduct[j].Name + "\"><i class=\"fa fa-balance-scale\" aria-hidden=\"true\"> </i><span>So sánh</span></a>");
                    result.Append(" </div>");
                    result.Append(" <div class=\"img\">");
                    result.Append("<a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                    result.Append("</div>");
                    result.Append("<h4><a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a></h4>");
                    result.Append("<span class=\"note\">" + listProduct[j].Func + "</span>");
                    result.Append("<div class=\"boxPrice\">");
                    result.Append("<div class=\"price\">");
                    result.Append("<span class=\"sp1\">"+string.Format("{0:#,#}",listProduct[j].PriceSale)+"đ</span>");
                    result.Append("<span class=\"sp2\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                    result.Append("</div>");
                    result.Append("<div class=\"orders\">");
                    result.Append("<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"CreateOrder("+ listProduct[j].id + ")\" title=\"Đặt hàng\"> Đặt hàng  <i class=\"fa fa-heart-o\" aria-hidden=\"true\"></i> </a>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append(" </div>");
                }
                    

                //sdsdsd
                result.Append("</div>");
                result.Append("</div>");

            }

            ViewBag.result = result.ToString();
                return PartialView();
        }
        public ActionResult productDetail(string tag)
        {
            var tblproduct = db.tblProducts.First(p => p.Tag == tag);
            ViewBag.Title = "<title>" + tblproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblproduct.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Loiloc.com/" + StringClass.NameToTag(tag) + ".htm\" />";
            string meta = "";
            tblproduct.Visit = tblproduct.Visit + 1;
            db.SaveChanges();
            ViewBag.visit = tblproduct.Visit;
            meta += "<meta itemprop=\"name\" content=\"" + tblproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Loiloc.com" + tblproduct.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Loiloc.com" + tblproduct.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Loiloc.com\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; int idcate = int.Parse(tblproduct.idCate.ToString());
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›" + UrlProduct(idcate) + "</ol><h1>" + tblproduct.Title + "</h1>";
            var tblManu = (from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b).Take(1).ToList();
            int idManu = 0;
            if (tblManu.Count > 0)
            {
                ViewBag.manu = tblManu[0].Name;
                ViewBag.urlmanu = tblManu[0].Images;
                idManu = tblManu[0].id;
                ViewBag.Baohanh = tblManu[0].Tag;

            }
            string chuoitag = "";
            if (tblproduct.Keyword != null)
            {
                string Chuoi = tblproduct.Keyword;
                string[] Mang = Chuoi.Split(',');
                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tagsp = StringClass.NameToTag(Mang[i]);
                    chuoitag += "<h2><a href=\"/tags/" + tagsp + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;
            int idcap = 0;
            if (tblproduct.Capacity.ToString() != null && tblproduct.Capacity.ToString() != "")
            {
                idcap = int.Parse(tblproduct.Capacity.ToString());
                var tblcapacity = db.tblCapacities.Find(idcap);
                ViewBag.capa = tblcapacity.Capacity;
                ViewBag.cappacity = tblcapacity.Capacity;
                ViewBag.songuoisd = tblcapacity.Note;
            }
            float Price = float.Parse(tblproduct.Price.ToString());
            float PriceSale = float.Parse(tblproduct.PriceSale.ToString());
            ViewBag.Diemthuong = string.Format("{0:#,#}", PriceSale / 100);
            ViewBag.tietkiem = string.Format("{0:#,#}", Convert.ToInt32(Price - PriceSale));
            //Load tính năng
            ViewBag.tendanhmuc = db.tblGroupProducts.Find(idcate).Name;
            var ListGroupCri = db.tblGroupCriterias.Where(p => p.idCate == idcate).ToList();
            List<int> Mang1 = new List<int>();
            for (int i = 0; i < ListGroupCri.Count; i++)
            {
                Mang1.Add(int.Parse(ListGroupCri[i].idCri.ToString()));
            }

            int idp = int.Parse(tblproduct.id.ToString());
            var ListCri = db.tblCriterias.Where(p => Mang1.Contains(p.id) && p.Active == true).ToList();
            string chuoi = "";
            #region[Lọc thuộc tính]
            for (int i = 0; i < ListCri.Count; i++)
            {
                int idCre = int.Parse(ListCri[i].id.ToString());
                var ListCr = (from a in db.tblConnectCriterias
                              join b in db.tblInfoCriterias on a.idCre equals b.id
                              where a.idpd == idp && b.idCri == idCre && b.Active == true
                              select new
                              {
                                  b.Name,
                                  b.Url,
                                  b.Ord
                              }).OrderBy(p => p.Ord).ToList();
                if (ListCr.Count > 0)
                {
                    chuoi += "<tr>";
                    chuoi += "<td>" + ListCri[i].Name + "</td>";
                    chuoi += "<td>";
                    int dem = 0;
                    string num = "";
                    if (ListCr.Count > 1)
                        num = "⊹ ";
                    foreach (var item in ListCr)
                        if (item.Url != null && item.Url != "")
                        {
                            chuoi += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\">";
                            if (dem == 1)
                                chuoi += num + item.Name;
                            else
                                chuoi += num + item.Name;
                            dem = 1;
                            chuoi += "</a>";
                        }
                        else
                        {
                            if (dem == 1)
                                chuoi += num + item.Name + "</br> ";
                            else
                                chuoi += num + item.Name + "</br> "; ;
                            dem = 1;
                        }
                    chuoi += "</td>";
                    chuoi += " </tr>";
                }
            }
            #endregion
            ViewBag.chuoi = chuoi;
            ViewBag.hotline = db.tblConfigs.First().HotlineIN;
            var listImages = db.tblImages.Where(p => p.Active == true && p.idCate == 11).OrderByDescending(p => p.Ord).ToList();
            StringBuilder chuoiimage = new StringBuilder();
            for (int i = 0; i < listImages.Count; i++)
            {
                chuoiimage.Append("<a href=\"" + listImages[i].Url + "\" title=\"" + listImages[i].Name + "\"><img src=\"" + listImages[i].Images + "\" alt=\"" + listImages[i].Name + "\" /></a>");
            }
            ViewBag.chuoiimages = chuoiimage;
            return View(tblproduct);
        }
        public ActionResult productList(string tag, int? page)
        {
            var Groupproduct = db.tblGroupProducts.First(p => p.Tag == tag);
            int id = Groupproduct.id;
            ViewBag.content = Groupproduct.Content;
            ViewBag.Headshort = Groupproduct.Content;
            ViewBag.h1 = Groupproduct.Name;
            ViewBag.Title = "<title>" + Groupproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + Groupproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + Groupproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + Groupproduct.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Loiloc.com/" + StringClass.NameToTag(tag) + ".html\" />";
            meta += "<meta itemprop=\"name\" content=\"" + Groupproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + Groupproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + Groupproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Loiloc.com\" />";
            meta += "<meta property=\"og:description\" content=\"" + Groupproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›" + UrlProduct(id) + "</ol><h2>" + Groupproduct.Title + "</h2>";
            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == id).OrderBy(p => p.Ord).ToList();
            //Phân trang
            const int pageSize = 16;
            var pageNumber = (page ?? 1);
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

            // kết thúcphân trang
            StringBuilder result = new StringBuilder();
        
            List<string> Mang = new List<string>();
            Mang = Arrayid(id);
            Mang.Add(id.ToString());
            var listIdImages = db.tblConnectImages.Where(p => Mang.Contains(p.idCate.ToString())).Select(p => p.idImg).ToList();
            var listImages = db.tblImages.Where(p => p.Active == true && listIdImages.Contains(p.id) && p.idCate == 10).OrderBy(p => p.Ord).ToList();
            if (listImages.Count>0)
            {
                result.Append(" <div id = \"SlideInPoduct\" >");
                result.Append("<div id=\"captioned-gallery\">");
                result.Append("<figure class=\"slider\">");
                for (int i = 0; i < listImages.Count; i++)
                {
                    result.Append(" <figure>");
                    result.Append("<a href=\"" + listImages[i].Url + "\" title=\"" + listImages[i].Name + "\"><img src = \"" + listImages[i].Images + "\" alt=\"" + listImages[i].Images + "\"></a>");
                    result.Append("</figure> ");
                }

                result.Append("</figure>");
                result.Append("</div>");
                result.Append("</div>");
            }
       
            var listProducty = db.tblProducts.Take(0).ToList();
            if (ListMenu.Count > 0)
            {
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    result.Append(" <div class=\"product\">");
                    result.Append(" <div class=\"nvarProduct\">");
                    result.Append("<div class=\"leftNvarProduct\">");
                    result.Append("<h2><a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\">" + ListMenu[i].Name + "</a></h2>");
                    result.Append("</div>");
                    result.Append(" <div class=\"rightNvarProduct\">");
                    result.Append("<a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\">Xem thêm <span></span></a>");
                    result.Append("</div>");
                    result.Append(" </div>");
                    result.Append("<div class=\"contentProduct\"> ");
                    int idCate = ListMenu[i].id;
                    List<string> mangId = new List<string>();
                    mangId = Arrayid(idCate);
                    mangId.Add(idCate.ToString());
                   var listProduct = db.tblProducts.Where(p => p.Active == true && mangId.Contains(p.idCate.ToString())).OrderBy(p => p.idCate).Take(12).ToList();
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        result.Append("<div class=\"tear1\">");
                        if (listProduct[j].New == true)
                        {
                            result.Append("<div class=\"new\"><span>New</span></div>");
                        }

                        result.Append("<div class=\"compare\">");
                        result.Append("<a href=\"#\" title=\"" + listProduct[j].Name + "\"><i class=\"fa fa-balance-scale\" aria-hidden=\"true\"> </i><span>So sánh</span></a>");
                        result.Append(" </div>");
                        result.Append(" <div class=\"img\">");
                        result.Append("<a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                        result.Append("</div>");
                        result.Append("<h4><a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a></h4>");
                        result.Append("<span class=\"note\">" + listProduct[j].Func + "</span>");
                        result.Append("<div class=\"boxPrice\">");
                        result.Append("<div class=\"price\">");
                        result.Append("<span class=\"sp1\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                        result.Append("<span class=\"sp2\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                        result.Append("</div>");
                        result.Append("<div class=\"orders\">");
                        result.Append("<a  rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"CreateOrder(" + listProduct[j].id + ")\" title=\"Đặt hàng\"> Đặt hàng  <i class=\"fa fa-heart-o\" aria-hidden=\"true\"></i> </a>");
                        result.Append("</div>");
                        result.Append("</div>");
                        result.Append(" </div>");
                    }


                    //sdsdsd
                    result.Append("</div>");
                    result.Append("</div>");
                    Mangphantu.Clear();

                }
            }
            else
            {
                result.Append(" <div class=\"product\">");
                result.Append(" <div class=\"nvarProduct\">");
                result.Append("<div class=\"leftNvarProduct\">");
                result.Append("<h2><a href=\"/" + Groupproduct.Tag + ".html\" title=\"" + Groupproduct.Name + "\">" + Groupproduct.Name + "</a></h2>");
                result.Append("</div>");
                result.Append(" <div class=\"rightNvarProduct\">");
                result.Append("<a href=\"/" + Groupproduct.Tag + ".html\" title=\"" + Groupproduct.Name + "\">Xem thêm <span></span></a>");
                result.Append("</div>");
                result.Append(" </div>");
                result.Append("<div class=\"contentProduct\"> ");
                int idCate = Groupproduct.id;
               var listProduct = db.tblProducts.Where(p => p.Active == true && p.idCate == idCate).OrderBy(p => p.idCate).ToList();


                var listProducts = listProduct.ToPagedList(pageNumber, pageSize);

                for (int j = 0; j < listProducts.Count; j++)
                {
                    result.Append("<div class=\"tear1\">");
                    if (listProducts[j].New == true)
                    {
                        result.Append("<div class=\"new\"><span>New</span></div>");
                    }

                    result.Append("<div class=\"compare\">");
                    result.Append("<a href=\"#\" title=\"" + listProducts[j].Name + "\"><i class=\"fa fa-balance-scale\" aria-hidden=\"true\"> </i><span>So sánh</span></a>");
                    result.Append(" </div>");
                    result.Append(" <div class=\"img\">");
                    result.Append("<a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkThumb + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                    result.Append("</div>");
                    result.Append("<h4><a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\" class=\"name\">" + listProducts[j].Name + "</a></h4>");
                    result.Append("<span class=\"note\">" + listProducts[j].Func + "</span>");
                    result.Append("<div class=\"boxPrice\">");
                    result.Append("<div class=\"price\">");
                    result.Append("<span class=\"sp1\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                    result.Append("<span class=\"sp2\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                    result.Append("</div>");
                    result.Append("<div class=\"orders\">");
                    result.Append("<a  rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"CreateOrder(" + listProduct[j].id + ")\" title=\"Đặt hàng\"> Đặt hàng  <i class=\"fa fa-heart-o\" aria-hidden=\"true\"></i> </a>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append(" </div>");
                }


                //sdsdsd
                result.Append("</div>");
                result.Append("</div>"); ViewBag.result = result.ToString();

                return View(listProducts);
            }
            ViewBag.result = result.ToString();
            return View(listProducty.ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult productLeftPartial(string tag)
        {
            var listGroup = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            var Groupproduct = db.tblGroupProducts.FirstOrDefault(p => p.Tag == tag);
            int id = Groupproduct.id;
            StringBuilder result = new StringBuilder();
            List<string> Mang = new List<string>();
            Mang = ArrayidParent(id);
            Mang.Add(id.ToString());
            for(int i=0;i<listGroup.Count;i++)
            {
                result.Append("<li class=\"li1\">");

                int id1 = listGroup[i].id;
                string kiemtra = Mang.Contains(id1.ToString()).ToString();
                if (Mang.Contains(id1.ToString()))
                {
                    result.Append("<a href = \"/"+listGroup[i].Tag+ ".html\" title=\"" + listGroup[i].id + "\" class=\"Name1 red\"> " + listGroup[i].Name + "</a>");
                }
                else
                {
                    result.Append("<a href = \"/" + listGroup[i].Tag + ".html\" title=\"" + listGroup[i].id + "\" class=\"Name1\"> " + listGroup[i].Name + "</a>");
                }

                var listChild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == id1).OrderBy(p => p.Ord).ToList();
                if(listChild.Count>0)
                {
                    result.Append("<ul class=\"ul2\">");
                  
                    for(int j=0;j<listChild.Count;j++)
                    {
                        int id2 = listChild[j].id;
                        if (Mang.Contains(id2.ToString()))
                        {
                            result.Append("<li class=\"li2\"><a href = \"/"+listChild[j].Tag+ ".html\" title=\"" + listChild[j].Name + "\" class=\"red\">› " + listChild[j].Name + "</a></li> ");

                        }
                        else
                        {
                            result.Append("<li class=\"li2\"><a href = \"/" + listChild[j].Tag + ".html\" title=\"" + listChild[j].Name + "\" >› " + listChild[j].Name + "</a></li> ");

                        }
                    }
                    
                    result.Append("</ul>");
                }
                
                result.Append("</li>");
            }
            ViewBag.result = result.ToString();
            return PartialView();
        }
        public ActionResult capacityList(string tag, int? page)
        {
            var capacitys = db.tblCapacities.First(p => p.Tag == tag);
            int id = capacitys.id;
            ViewBag.content = capacitys.Content;
            ViewBag.h1 = capacitys.Name;
            ViewBag.Headshort = capacitys.Content;
            ViewBag.Title = "<title>" + capacitys.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + capacitys.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + capacitys.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + capacitys.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Loiloc.com/Loi-loc-nuoc/" + StringClass.NameToTag(tag) + "\" />";
            meta += "<meta itemprop=\"name\" content=\"" + capacitys.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + capacitys.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + capacitys.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Loiloc.com\" />";
            meta += "<meta property=\"og:description\" content=\"" + capacitys.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›</ol><h1>" + capacitys.Title + "</h1>";
            //Phân trang
            const int pageSize = 20;
            var pageNumber = (page ?? 1);
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

            // kết thúcphân trang
            StringBuilder result = new StringBuilder();
            result.Append(" <div class=\"product\">");
            result.Append(" <div class=\"nvarProduct\">");
            result.Append("<div class=\"leftNvarProduct\">");
            result.Append("<h2><a href=\"/loi-loc-nuoc/" + capacitys.Tag + "\" title=\"" + capacitys.Name + "\">" + capacitys.Name + "</a></h2>");
            result.Append("</div>");
            result.Append(" <div class=\"rightNvarProduct\">");
            result.Append("<a href=\"/" + capacitys.Tag + "\" title=\"" + capacitys.Name + "\">Xem thêm <span></span></a>");
            result.Append("</div>");
            result.Append(" </div>");
            result.Append("<div class=\"contentProduct\"> ");
            int idCap = capacitys.id;
            var listProduct = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap).OrderBy(p => p.idCate).ToList();
            var listProducts = listProduct.ToPagedList(pageNumber, pageSize);
            for (int j = 0; j < listProducts.Count; j++)
            {
                result.Append("<div class=\"tear1\">");
                if (listProducts[j].New == true)
                {
                    result.Append("<div class=\"new\"><span>New</span></div>");
                }

                result.Append("<div class=\"compare\">");
                result.Append("<a href=\"#\" title=\"" + listProducts[j].Name + "\"><i class=\"fa fa-balance-scale\" aria-hidden=\"true\"> </i><span>So sánh</span></a>");
                result.Append(" </div>");
                result.Append(" <div class=\"img\">");
                result.Append("<a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkThumb + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                result.Append("</div>");
                result.Append("<h4><a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\" class=\"name\">" + listProducts[j].Name + "</a></h4>");
                result.Append("<span class=\"note\">" + listProducts[j].Func + "</span>");
                result.Append("<div class=\"boxPrice\">");
                result.Append("<div class=\"price\">");
                result.Append("<span class=\"sp1\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                result.Append("<span class=\"sp2\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                result.Append("</div>");
                result.Append("<div class=\"orders\">");
                result.Append("<a  rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"CreateOrder(" + listProduct[j].id + ")\" title=\"Đặt hàng\"> Đặt hàng  <i class=\"fa fa-heart-o\" aria-hidden=\"true\"></i> </a>");
                result.Append("</div>");
                result.Append("</div>");
                result.Append(" </div>");
            }
            //sdsdsd
            result.Append("</div>");
            result.Append("</div>"); ViewBag.result = result.ToString();
            ViewBag.result = result.ToString();
            return View(listProducts);
        }
        public ActionResult productTag(string tag, int? page)
        {
            string name = db.tblProductTags.Where(p => p.Tag == tag).Take(1).ToList()[0].Name;
            const int pageSize = 20;
            var pageNumber = (page ?? 1);
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
            var tbltags = db.tblTags.Where(p => p.Tag == tag && p.Active == true).ToList();
            if (tbltags.Count > 0)
            {
                ViewBag.Title = "<title>" + tbltags[0].Name + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tbltags[0].Name + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"" + tbltags[0].Description + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tbltags[0].Keyword + "\" /> ";
                string meta = "";
                meta += "<meta itemprop=\"name\" content=\"" + name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + tbltags[0].Description + "\" />";
                meta += "<meta itemprop=\"image\" content=\"\" />";
                meta += "<meta property=\"og:title\" content=\"" + tbltags[0].Name + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://Loiloc.com\" />";
                meta += "<meta property=\"og:description\" content=\"" + tbltags[0].Description + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />"; ViewBag.Meta = meta;
                ViewBag.Headshort = tbltags[0].Content;
            }
            else
            {
                ViewBag.Title = "<title>" + name + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + name + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
                string meta = "";
                meta += "<meta itemprop=\"name\" content=\"" + name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + name + "\" />";
                meta += "<meta itemprop=\"image\" content=\"\" />";
                meta += "<meta property=\"og:title\" content=\"" + name + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://Loiloc.com\" />";
                meta += "<meta property=\"og:description\" content=\"" + name + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />"; ViewBag.Meta = meta;
            }
           

            var listIdProduct = db.tblProductTags.Where(p => p.Tag == tag).Select(p => p.idp).ToList();

             var listProduct = db.tblProducts.Where(p => p.Active == true && listIdProduct.Contains(p.id)).OrderBy(p => p.PriceSale).Take(10).ToList();
            ViewBag.h1 = name;
            StringBuilder result = new StringBuilder();
            result.Append(" <div class=\"product\">");
            result.Append(" <div class=\"nvarProduct\">");
            result.Append("<div class=\"leftNvarProduct\">");
            result.Append("<h2><a href=\"/tags/" + tag + "\" title=\"" + name + "\">" + name + "</a></h2>");
            result.Append("</div>");
            result.Append(" <div class=\"rightNvarProduct\">");
            result.Append("<a href=\"/tags/" + tag + "\" title=\"" + name + "\">Xem thêm <span></span></a>");
            result.Append("</div>");
            result.Append(" </div>");
            result.Append("<div class=\"contentProduct\"> ");
           
            var listProducts = listProduct.ToPagedList(pageNumber, pageSize);
            for (int j = 0; j < listProducts.Count; j++)
            {
                result.Append("<div class=\"tear1\">");
                if (listProducts[j].New == true)
                {
                    result.Append("<div class=\"new\"><span>New</span></div>");
                }

                result.Append("<div class=\"compare\">");
                result.Append("<a href=\"#\" title=\"" + listProducts[j].Name + "\"><i class=\"fa fa-balance-scale\" aria-hidden=\"true\"> </i><span>So sánh</span></a>");
                result.Append(" </div>");
                result.Append(" <div class=\"img\">");
                result.Append("<a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkThumb + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                result.Append("</div>");
                result.Append("<h4><a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\" class=\"name\">" + listProducts[j].Name + "</a></h4>");
                result.Append("<span class=\"note\">" + listProducts[j].Func + "</span>");
                result.Append("<div class=\"boxPrice\">");
                result.Append("<div class=\"price\">");
                result.Append("<span class=\"sp1\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                result.Append("<span class=\"sp2\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                result.Append("</div>");
                result.Append("<div class=\"orders\">");
                result.Append("<a  rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"CreateOrder(" + listProduct[j].id + ")\" title=\"Đặt hàng\"> Đặt hàng  <i class=\"fa fa-heart-o\" aria-hidden=\"true\"></i> </a>");
                result.Append("</div>");
                result.Append("</div>");
                result.Append(" </div>");
            }
            //sdsdsd
            result.Append("</div>");
            result.Append("</div>"); ViewBag.result = result.ToString();
            ViewBag.result = result.ToString();
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›</ol><h1>" + name+ "</h1>";
            return View(listProduct.ToPagedList(pageNumber, pageSize) );
        }

    }
}