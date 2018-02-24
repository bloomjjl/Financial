using Financial.Core.ViewModels.TransactionCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class TransactionCategoryController : BaseController
    {
        [HttpGet]
        public ActionResult Create()
        {
            // display view
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", vmCreate);
            }

            // transfer vm to dto
            UOW.TransactionCategories.Add(new Core.Models.TransactionCategory()
            {
                Name = vmCreate.CategoryName,
                IsActive = true
            });

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "New Category Added";
            return RedirectToAction("Index", "AssetTransaction");
        }
    }
}