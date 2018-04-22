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

            try
            {
                // transfer dto to vm
                var vmIndex = UOW.AssetTypes.GetAllOrderedByName()
                    .Select(r => new IndexViewModel(r))
                    .ToList();

                // display view
                return View("Index", vmIndex);
            }
            catch(Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return View("Index", new List<IndexViewModel>());
            }
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
            try
            {
                // validation
                if (ModelState.IsValid)
                {
                    // check for duplicate
                    var count = UOW.AssetTypes.CountMatching(vmCreate.Name);
                    if (count == 0)
                    {
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
                    // display view with message
                    ViewData["ErrorMessage"] = "Record already exists";
                    return View("Create", vmCreate);
                }
                TempData["ErrorMessage"] = "Unable to create record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            { 
                // transfer dto to vm
                var dtoAssetType = UOW.AssetTypes.Get(id);
                if (dtoAssetType != null)
                {
                    // display view
                    return View("Edit", new EditViewModel(dtoAssetType));
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            try
            {
                // validation
                if (ModelState.IsValid)
                {
                    // check for duplicate
                    var count = UOW.AssetTypes.CountMatching(vmEdit.Id, vmEdit.Name);
                    if (count == 0)
                    {
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
                    // display view with message
                    ViewData["ErrorMessage"] = "Record already exists";
                    return View("Edit", vmEdit);
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            try
            { 
                // transfer dto to vm
                var dtoAssetType = UOW.AssetTypes.Get(id);
                if(dtoAssetType != null)
                {
                    // display view
                    return View("Details", new DetailsViewModel(dtoAssetType));
                }
                TempData["ErrorMessage"] = "Unable to display record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }

        }
    }
}