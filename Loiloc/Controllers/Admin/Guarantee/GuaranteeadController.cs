using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loiloc.Models;
using PagedList;
using PagedList.Mvc;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
namespace Loiloc.Views.Guarantee
{
    public class GuaranteeadController : Controller
    {
        //
        // GET: /Guaranteead/
        private LoilocContext db = new LoilocContext();
        public ActionResult Index(FormCollection collection,string idCate)
        {
            List<SelectListItem> carlist = new List<SelectListItem>();

            var menuModel = db.tblManufactures.Where(m => m.Active == true).OrderBy(m => m.id).ToList();
            carlist.Clear();
            foreach (var item in menuModel)
            {
                carlist.Add(new SelectListItem { Text = item.Name, Value = item.id.ToString() });
            }
            if(idCate!=null && idCate!="")
            {
                ViewBag.drMenu = new SelectList(carlist, "Value", "Text",idCate);
            }
            else
            {
                ViewBag.drMenu = new SelectList(carlist, "Value", "Text");
            }
                 
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(14, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
              
                if (Session["Thongbao"] != null && Session["Thongbao"] != "")
                {

                    ViewBag.thongbao = Session["Thongbao"].ToString();
                    Session["Thongbao"] = "";
                }
                if (collection["btnDelete"] != null)
                {
                    foreach (string key in Request.Form.Keys)
                    {
                        var checkbox = "";
                        if (key.StartsWith("chk_"))
                        {
                            checkbox = Request.Form["" + key];
                            if (checkbox != "false")
                            {
                                if (ClsCheckRole.CheckQuyen(14, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
                                {
                                    int ids = Convert.ToInt32(key.Remove(0, 4));
                                    tblGuarantee tblGuarantee = db.tblGuarantees.Find(ids);
                                    db.tblGuarantees.Remove(tblGuarantee);
                                    db.SaveChanges();
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    return Redirect("/Users/Erro");

                                }
                            }
                        }
                    }
                }
                return View();
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }
        public PartialViewResult partialGuarantee(int? page, string id,string idCate)
        {
            int ids = int.Parse(id);

            var ListQuarantee = db.tblGuarantees.Where(p => p.idDistrict == ids).ToList();
            if (idCate != null && idCate != "")
            {
                ViewBag.idCate = idCate;
                int idCates = int.Parse(idCate);
                ListQuarantee = ListQuarantee.Where(p => p.idManu == idCates).ToList();
            }

            const int pageSize = 20;
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
            if (id != "")
            {
                ViewBag.idCate = id;
                ViewBag.idMenu = id;
            }
            else
            {
                
            }
            return PartialView(ListQuarantee.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateGuarantee(string id, string Active, string Ord)
        {
            if (ClsCheckRole.CheckQuyen(14, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {

                int ids = int.Parse(id);
                var tblGuarantee = db.tblGuarantees.Find(ids);
                tblGuarantee.Active = bool.Parse(Active);
                tblGuarantee.Ord = int.Parse(Ord);
                db.SaveChanges();
                var result = string.Empty;
                result = "Thành công";
                return Json(new { result = result });
            }
            else
            {
                var result = string.Empty;
                result = "Bạn không có quyền thay đổi tính năng này";
                return Json(new { result = result });

            }

        }
        public ActionResult DeleteGuarantee(int id)
        {
            if (ClsCheckRole.CheckQuyen(14, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                tblGuarantee tblGuarantee = db.tblGuarantees.Find(id);
                var result = string.Empty;
                db.tblGuarantees.Remove(tblGuarantee);
                db.SaveChanges();
                result = "Bạn đã xóa thành công.";
                return Json(new { result = result });
            }
            else
            {
                var result = string.Empty;
                result = "Bạn không có quyền thay đổi tính năng này";
                return Json(new { result = result });

            }
        }
        public ActionResult Create(int id,string idCate)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (Session["Thongbao"] != null && Session["Thongbao"] != "")
            {

                ViewBag.thongbao = Session["Thongbao"].ToString();
                Session["Thongbao"] = "";
            }
            if (ClsCheckRole.CheckQuyen(14, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var pro = db.tblGuarantees.OrderByDescending(p => p.Ord).ToList();
                if (pro.Count > 0)
                    ViewBag.Ord = pro[0].Ord + 1;
                else
                { ViewBag.Ord = "0"; }
                List<SelectListItem> carlistDistrict = new List<SelectListItem>();
                var ListDistrict = db.tblDistricts.Where(m => m.Active == true).OrderBy(m => m.id).ToList();

                carlistDistrict.Clear();
                foreach (var item in ListDistrict)
                {
                    carlistDistrict.Add(new SelectListItem { Text = item.Name, Value = item.id.ToString() });
                }


                ViewBag.drDistrict = new SelectList(carlistDistrict, "Value", "Text", id);
                List<SelectListItem> carlistManu= new List<SelectListItem>();
                var ListManu = db.tblManufactures.Where(m => m.Active == true).OrderBy(m => m.id).ToList();

                carlistManu.Clear();
                foreach (var item in ListManu)
                {
                    carlistManu.Add(new SelectListItem { Text = item.Name, Value = item.id.ToString() });
                }
                if(idCate!=null && idCate !="")
                {
                    ViewBag.drManu = new SelectList(carlistManu, "Value", "Text", idCate);
                }
                else
                ViewBag.drManu = new SelectList(carlistManu, "Value", "Text");
                return View();
            }
            else
            {
                return Redirect("/Users/Erro");
            }
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(tblGuarantee tblGuarantee, FormCollection collection,int id, int idCate)
        {
            int ids = int.Parse(collection["drDistrict"].ToString());
            int idCates = int.Parse(collection["drManu"].ToString());
            tblGuarantee.idDistrict = id;
            tblGuarantee.idManu = idCates;
            tblGuarantee.DateCreate = DateTime.Now;
            tblGuarantee.Tag = StringClass.NameToTag(tblGuarantee.Name);
            db.tblGuarantees.Add(tblGuarantee);
            db.SaveChanges();
            #region[Updatehistory]
            Updatehistoty.UpdateHistory("Add tblGuarantee", Request.Cookies["Username"].Values["FullName"].ToString(), Request.Cookies["Username"].Values["UserID"].ToString());
            #endregion
            if (collection["btnSave"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                return Redirect("/Guaranteead/Index?id="+ids+"&idCate="+idCates+"");
            }
            if (collection["btnSaveCreate"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm danh mục  mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Guaranteead/Create?id=" + ids + "&idCate=" + idCates + "");
            }
            return Redirect("Index");


        }
        public ActionResult Edit(int id = 0)
        {

            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(14, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                tblGuarantee tblGuarantee = db.tblGuarantees.Find(id);
                if (tblGuarantee == null)
                {
                    return HttpNotFound();
                }
                int ids = int.Parse(tblGuarantee.idDistrict.ToString());
                int idcate = int.Parse(tblGuarantee.idManu.ToString());
                List<SelectListItem> carlistDistrict = new List<SelectListItem>();
                var ListDistrict = db.tblDistricts.Where(m => m.Active == true).OrderBy(m => m.id).ToList();

                carlistDistrict.Clear();
                foreach (var item in ListDistrict)
                {
                    carlistDistrict.Add(new SelectListItem { Text = item.Name, Value = item.id.ToString() });
                }


                ViewBag.drDistrict = new SelectList(carlistDistrict, "Value", "Text", ids);
                List<SelectListItem> carlistManu = new List<SelectListItem>();
                var ListManu = db.tblManufactures.Where(m => m.Active == true).OrderBy(m => m.id).ToList();

                carlistManu.Clear();
                foreach (var item in ListManu)
                {
                    carlistManu.Add(new SelectListItem { Text = item.Name, Value = item.id.ToString() });
                }
                ViewBag.drManu = new SelectList(carlistManu, "Value", "Text", idcate);
                return View(tblGuarantee);
            }
            else
            {
                return Redirect("/Users/Erro");


            }
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Edit(tblGuarantee tblGuarantee, int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                bool URL = (collection["URL"] == "false") ? false : true;
                int ids = int.Parse(collection["drDistrict"].ToString());
                int idCates = int.Parse(collection["drManu"].ToString());
                tblGuarantee.idDistrict = ids; tblGuarantee.DateCreate = DateTime.Now;
                tblGuarantee.idManu = idCates;
                if (URL == true)
                {
                    tblGuarantee.Tag = StringClass.NameToTag(tblGuarantee.Name);
                }
                else
                {
                    tblGuarantee.Tag = collection["NameURL"];
                }
                tblGuarantee.Tag = StringClass.NameToTag(tblGuarantee.Name);
                db.Entry(tblGuarantee).State = EntityState.Modified;

                db.SaveChanges();
                #region[Updatehistory]
                Updatehistoty.UpdateHistory("Edit tblGuarantee", Request.Cookies["Username"].Values["FullName"].ToString(), Request.Cookies["Username"].Values["UserID"].ToString());
                #endregion
                if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa  thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/Guaranteead/Index?id=" + ids + "&idCate=" + idCates + "");
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Guaranteead/Create?id=" + ids + "&idCate=" + idCates + "");
                }
            }
            return View(tblGuarantee);
        }
	}
}