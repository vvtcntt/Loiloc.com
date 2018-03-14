using Loiloc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Loiloc.Controllers.Display.Section.baogia
{
    public class baogiaController : Controller
    {
        private LoilocContext db = new LoilocContext();

        // GET: baogia
        public ActionResult Index()
        {
            var listBaoGia = db.tblGroupProducts.Where(p => p.Active == true && p.Baogia == true).OrderBy(p => p.Ord).ToList();
            StringBuilder resultBaoGia = new StringBuilder();
            for (int i = 0; i < listBaoGia.Count; i++)
            {
                resultBaoGia.Append("<div class=\"tear_Bg\">");
                resultBaoGia.Append("<a href=\"/bao-gia/" + listBaoGia[i].Tag + "\" title=\"Báo giá " + listBaoGia[i].Name + "\"><img src = \"/Content/Display/Icon/bao-gia-loi-loc-nuoc.jpg\" alt=\"Báo giá " + listBaoGia[i].Name + "\" /></a>");
                resultBaoGia.Append("<a href=\"/bao-gia/" + listBaoGia[i].Tag + "\" title=\"Báo giá " + listBaoGia[i].Name + "\">" + listBaoGia[i].Name + "</a>");
                resultBaoGia.Append("</div>");
            }
            ViewBag.resultBaoGia = resultBaoGia.ToString();
            ViewBag.Title = "<title>Bảng báo giá lõi lọc nước Kangaroo, Karofi, Geyser... mới nhất năm "+DateTime.Now.Year+"</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"Bảng giá lõi lọc nước tổng hợp các thương hiệu nổi tiếng như Kangaroo, karofi,geyser... chính hãng cam kết sản phẩm lõi lọc nước giá rẻ nhất.Báo giá được câp nhật liên tục\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"Bảng Báo giá sản phẩm lõi lọc nước\" /> ";
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> Báo giá lõi lọc nước";

            return View();
        }
       
        public ActionResult baoGiaList(string tag)
        {
            tblConfig tblconfig = db.tblConfigs.First();

            tblGroupProduct groupproduct = db.tblGroupProducts.FirstOrDefault(p => p.Tag == tag);
            int idmenu = int.Parse(groupproduct.id.ToString());
            int idManu = int.Parse(db.tblConnectManuProducts.FirstOrDefault(p=>p.idCate==idmenu).idManu.ToString());
            tblManufacture manufacture = db.tblManufactures.Find(idManu);
            ViewBag.Namemanu = manufacture.Name;
            ViewBag.name = groupproduct.Name; ViewBag.favicon = " <link href=\"" + manufacture.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";
            ViewBag.imagemanu = manufacture.Images;
            string moth = "";
            int moths = int.Parse(DateTime.Now.Month.ToString());
          if(moths<=3)
            {
                moth = "Tháng 1,2,3 ";
            }
            else if (moths>3 && moths <= 6)
            {
                moth = "Tháng 4,5,6 ";
            }
            else if (moths > 6 && moths<=9)
            {
                moth = "Tháng 7,8,9 ";
            }
            else if (moths >=9 && moths<=12)
            {
                moth = "Tháng 10,11,12 ";
            }
            ViewBag.Title = "<title>Bảng báo giá " + groupproduct.Name + " "+ moth + "năm " + DateTime.Now.Year + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblconfig.Name + " là nhà phân phối chính thức của " + manufacture.Name + " . Chúng tôi xin gửi tới quý khách hàng bảng báo giá  " + groupproduct.Name + ".\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"Bảng Báo giá sản phẩm " + groupproduct.Name + "\" /> ";
            string chuoi = "";
            var listproduct = db.tblProducts.Where(p => p.Active == true && p.idCate == idmenu).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listproduct.Count; i++)
            {

                chuoi += "<tr>";
                chuoi += "<td class=\"Ords\">" + (i + 1) + "</td>";
                chuoi += "<td class=\"Names\">";
                chuoi += "<a href=\"/" + listproduct[i].Tag + ".htm\" title=\"" + listproduct[i].Name + "\">" + listproduct[i].Name + "</a></span>";
                chuoi += "<span class=\"n2\">Chức năng : " + listproduct[i].Func + "</span>";
                chuoi += " </td>";
                chuoi += "<td class=\"Codes\"> " + listproduct[i].Code + " </td>";
                chuoi += "<td class=\"Prices\">" + string.Format("{0:#,#}", listproduct[i].PriceSale) + "đ</td>";
                chuoi += "<td class=\"Qualitys\">01</td>";
                chuoi += "<td class=\"SumPrices\">" + string.Format("{0:#,#}", listproduct[i].PriceSale) + "đ</td>";
                chuoi += "<td class=\"Images\"><a href=\"/" + listproduct[i].Tag + ".htm\" title=\"" + listproduct[i].Name + "\"><img src=\"" + listproduct[i].ImageLinkThumb + "\" alt=\"" + listproduct[i].Name + "\" title=\"" + listproduct[i].Name + "\"/></a<</td>";
                chuoi += "</tr>";
            }
            ViewBag.chuoi = chuoi;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> Báo giá " + groupproduct.Name + "";

            return View(tblconfig);
        }
    }
}