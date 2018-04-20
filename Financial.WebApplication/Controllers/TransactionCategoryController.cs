using Financial.WebApplication.Models.ViewModels.TransactionCategory;
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
        public ActionResult Index()
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            // transfer dto to vm
            var vmIndex = UOW.TransactionCategories.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new IndexViewModel(r))
                .OrderBy(r => r.Name)
                .ToList();

            // display view
            return View("Index", vmIndex);
        }

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
                Name = vmCreate.Name,
                IsActive = true
            });

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "New Category Added";
            return RedirectToAction("Index", "TransactionCategory");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // transfer id to dto 
            var dtoTransactionCategory = UOW.TransactionCategories.Get(id);

            // validate dto
            if(dtoTransactionCategory == null)
            {
                TempData["ErrorMessage"] = "Encountered problem updated transaction category. Try again.";
                return RedirectToAction("Index", "TransactionCategory");
            }

            // display view
            return View("Edit", new EditViewModel(dtoTransactionCategory));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // transfer vm to dto
            var dtoTransactionCategory = UOW.TransactionCategories.Get(vmEdit.Id);
            dtoTransactionCategory.Name = vmEdit.Name;

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record updated.";
            return RedirectToAction("Index", "TransactionCategory");
        }
    }
}