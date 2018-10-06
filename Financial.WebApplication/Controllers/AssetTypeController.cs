using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.AssetType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financial.Business;
using Financial.Business.ServiceInterfaces;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IAccountTypeService _assetTypeService;

        
        public AssetTypeController(IUnitOfWork unitOfWork, IAccountTypeService assetTypeService)
            : base()
        {
            _unitOfWork = unitOfWork;
            _assetTypeService = assetTypeService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            try
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

                // transfer bm to vm
                var vmIndex = _assetTypeService.IndexGetModelList()
                    .Select(r => new IndexViewModel(r))
                    .ToList();

                return View("Index", vmIndex);
            }
            catch(Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return View("Index", new List<IndexViewModel>());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View("Create");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "AssetType");
                }

                // transfer vm to bm
                var bmAssetType = new Business.Models.AccountType()
                {
                    AssetTypeName = vmCreate.Name,
                };

                // update db
                bmAssetType.AssetTypeId = _assetTypeService.CreatePostUpdateDatabase(bmAssetType);
                if(bmAssetType.AssetTypeId == 0)
                {
                    ViewData["ErrorMessage"] = "Name already exists";
                    return View("Create", vmCreate);
                }

                // display View with message
                TempData["SuccessMessage"] = "Asset Type Created";
                return RedirectToAction("CreateLinkedSettingTypes", "AssetTypeSettingType", new { assetTypeId = bmAssetType.AssetTypeId });
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
                // get bm
                var bmAssetType = _assetTypeService.EditGetModel(id);
                if(bmAssetType == null)
                {
                    TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                    return RedirectToAction("Index", "AssetType");
                }

                // transfer bm to vm
                return View("Edit", new EditViewModel(bmAssetType));
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
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "AssetType");
                }

                // transfer vm to bm
                var bmAssetType = new Business.Models.AccountType()
                {
                    AssetTypeId = vmEdit.Id,
                    AssetTypeName = vmEdit.Name,
                };

                // update db
                var message = _assetTypeService.EditPostUpdateDatabase(bmAssetType);
                if(message != "Success")
                {
                    TempData["ErrorMessage"] = message;
                    return RedirectToAction("Index", "AssetType");
                }

                TempData["SuccessMessage"] = "Record updated.";
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
            try
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

                // transfer bm to vm
                var bmAssetType = _assetTypeService.DetailsGetModel(id);
                if(bmAssetType == null)
                {
                    TempData["ErrorMessage"] = "Unable to display record. Try again.";
                    return RedirectToAction("Index", "AssetType");
                }

                return View("Details", new DetailsViewModel(bmAssetType));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }

        }
    }
}