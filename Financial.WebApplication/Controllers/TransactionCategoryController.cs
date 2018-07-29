using Financial.Business;
using Financial.Data;
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
        private IUnitOfWork _unitOfWork;
        private IBusinessService _businessService;


        public TransactionCategoryController(IUnitOfWork unitOfWork, IBusinessService businessService)
            : base()
        {
            _unitOfWork = unitOfWork;
            _businessService = businessService;
        }


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
            var vmIndex = _unitOfWork.TransactionCategories.GetAll()
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
            _unitOfWork.TransactionCategories.Add(new Core.Models.TransactionCategory()
            {
                Name = vmCreate.Name,
                IsActive = true
            });

            // update db
            _unitOfWork.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "New Category Added";
            return RedirectToAction("Index", "TransactionCategory");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // transfer id to dto 
            var dtoTransactionCategory = _unitOfWork.TransactionCategories.Get(id);

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
            var dtoTransactionCategory = _unitOfWork.TransactionCategories.Get(vmEdit.Id);
            dtoTransactionCategory.Name = vmEdit.Name;

            // update db
            _unitOfWork.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record updated.";
            return RedirectToAction("Index", "TransactionCategory");
        }
    }
}