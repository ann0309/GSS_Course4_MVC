using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookMaintainingSystem.Controllers
{

    //資料的增刪改查
    public class BookController : Controller
    {
        Models.CodeService codeService = new Models.CodeService();
        Models.BookService BookService = new Models.BookService();
        Models.BookEdit BookEdit = new Models.BookEdit();
        Models.Detail BookDetail = new Models.Detail();
        // GET: Book
        public ActionResult Index()//inital
        {
            ViewBag.BookCategory = this.codeService.GetBookCategory();
            ViewBag.BookKeeper = this.codeService.GetBookKeeper();
            ViewBag.BookStatus = this.codeService.GetBookStatus();
            return View();
        }
        [HttpPost()]
        public ActionResult Index(Models.BookSearchArg arg)
        {
            ViewBag.BookCategoryData = this.codeService.GetBookCategory();
            ViewBag.BookKeeperData = this.codeService.GetBookKeeper();
            ViewBag.BookStatusData = this.codeService.GetBookStatus();
            ViewBag.SearchResult = BookService.GetBookByCondtioin(arg);
            return View("Index");
        }

        public ActionResult Edit(int id)//按下編輯按鈕連到這邊,傳入book id
        {
            var result = BookEdit.GetBookByID(id);
            ViewBag.BookCategoryData = this.codeService.GetBookCategory();
            ViewBag.BookStatusData = this.codeService.GetBookStatus();
            ViewBag.BookKeeperData = this.codeService.GetBookKeeper();
            return View(result);
        }

        [HttpPost()]
        public ActionResult Edit(Models.BookEditArg arg)
        {
            ViewBag.BookCategoryData = this.codeService.GetBookCategory();
            ViewBag.BookKeeperData = this.codeService.GetBookKeeper();
            ViewBag.BookStatusData = this.codeService.GetBookStatus();
            if (ModelState.IsValid)
            {
                BookEdit.UpdateBook(arg);
                TempData["message"] = "修改成功";
            }
            return View(arg);
        }
        public ActionResult Insert()
        {
            ViewBag.BookCategory = this.codeService.GetBookCategory();//下拉式選單的值
            ViewBag.BookKeeper = this.codeService.GetBookKeeper();//下拉式選單的值
            ViewBag.BookStatus = this.codeService.GetBookStatus();//下拉式選單的值
            return View();
        }
        [HttpPost()]
        public ActionResult Insert(Models.BookEditArg arg)
        {
            ViewBag.BookCategory = this.codeService.GetBookCategory();
            ViewBag.EditKeeper = this.codeService.GetBookKeeper();
            ViewBag.BookStatus = this.codeService.GetBookStatus();

            if (ModelState.IsValid)
            {
                BookService.InsertBook(arg);
                TempData["message"] = "存檔成功";
            }
            return View(arg);
        }
        public ActionResult Detail(string id)
        {
            var result = this.BookDetail.GetBookByID(id);
            return View(result);
        }

        [HttpPost()]
        public JsonResult Delete(int id)
        {
            try
            {
                BookEdit.DeleteBookById(id);
                return this.Json(true);
            }
            catch (Exception ex)
            {
                return this.Json(false);
            }
        }
        public ActionResult LendRecord(int id)
        {
            ViewBag.Record = BookEdit.GetRecordByID(id);
            return View("LendRecord");
        }
    }
}
