using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.AssetType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeController : BaseController
    {
        public AssetTypeController()
            : base()
        {
        }

        public AssetTypeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public ViewResult Index()
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
            var vmIndex = UOW.AssetTypes.GetAll()
                .Select(r => new IndexViewModel(r))
                .OrderBy(r => r.Name)
                .ToList();

            // display view
            return View("Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create()
        {
            // display view
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // validation
            if (!ModelState.IsValid)
            {
                return View("Index", vmCreate);
            }

            // check for duplicate
            var count = UOW.AssetTypes.GetAll()
                .Count(r => r.Name == vmCreate.Name);
            if (count > 0)
            {
                // display view with message
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Create", vmCreate);
            }

            // transfer vm to dto
            var dtoAssetType = new AssetType()
            {
                Name = vmCreate.Name,
                IsActive = true
            };

            // update db
            UOW.AssetTypes.Add(dtoAssetType);
            UOW.CommitTrans();

            // display View with message
            TempData["SuccessMessage"] = "Asset Type Created";
            return RedirectToAction("CreateLinkedSettingTypes", "AssetTypeSettingType", new { assetTypeId = dtoAssetType.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // transfer dto to vm
            var vmEdit = UOW.AssetTypes.GetAll()
                .Select(r => new EditViewModel(r))
                .FirstOrDefault(r => r.Id == id);

            // display view
            return View("Edit", vmEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // validation
            if (!ModelState.IsValid)
            {
                return View("Edit", vmEdit);
            }

            // check for duplicate
            var count = UOW.AssetTypes.GetAll()
                .Where(r => r.Name == vmEdit.Name)
                .Count(r => r.Id != vmEdit.Id);
            if (count > 0)
            {
                // display view with message
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Edit", vmEdit);
            }

            // transfer vm to dto
            var dtoAssetType = UOW.AssetTypes.Get(vmEdit.Id);
            dtoAssetType.Name = vmEdit.Name;
            dtoAssetType.IsActive = vmEdit.IsActive;

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record updated.";
            return RedirectToAction("Index", "AssetType");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            // transfer dto to vm
            var vmDetails = UOW.AssetTypes.GetAll()
                .Select(r => new DetailsViewModel(r))
                .FirstOrDefault( r => r.Id == id);

            // display view
            return View("Details", vmDetails);
        }
    }
}