using Financial.Core;
using Financial.Core.Models;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AssetTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ViewResult Index()
        {
            try
            {
                if(TempData["ErrorMessage"] != null)
                {
                    ModelState.AddModelError("", TempData["ErrorMessage"].ToString());
                }

                var vmAssetTypes = _unitOfWork.AssetTypes.GetAll().Where(r => r.IsActive).ToList();
                return View("Index", vmAssetTypes);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to view information at this time. Try again later.");
                List<AssetType> vmAssetTypes = new List<AssetType>();
                return View("Index", vmAssetTypes);
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetType vmCreate)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Create", "AssetType");
            }
            if(string.IsNullOrEmpty(vmCreate.Name))
            {
                ModelState.AddModelError("", "Name is required.");
                return View("Create", vmCreate);
            }

            try
            {
                // look for existing entity with matching name
                AssetType dbAssetType = _unitOfWork.AssetTypes.Find(r => r.Name == vmCreate.Name); 

                // Active entity found
                if(dbAssetType != null && dbAssetType.IsActive)
                {
                    // YES. STOP. do not duplicate entry
                    ModelState.AddModelError("", "Name already exists.");
                    return View("Create", vmCreate);
                }

                // Is this a new entity
                if (dbAssetType == null)
                {
                    // YES. create new entity
                    AssetType dtoAssetType = new AssetType()
                    {
                        Name = vmCreate.Name
                    };

                    _unitOfWork.AssetTypes.Add(dtoAssetType);
                    _unitOfWork.CommitTrans();
                }
                else
                {
                    // update existing entity to active
                    AssetType dtoAssetType = new AssetType()
                    {
                        Id = dbAssetType.Id,
                        Name = dbAssetType.Name,
                        IsActive = true,
                    };

                    _unitOfWork.AssetTypes.Update(dtoAssetType);
                    _unitOfWork.CommitTrans();
                }

                return RedirectToAction("Index", "AssetType");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to add record at this time. Try again later.");
                return View("Create", vmCreate);
            }
        }

        [HttpGet]
        public ActionResult Edit(int assetTypeId)
        {
            try
            {
                var vmEdit = _unitOfWork.AssetTypes.Get(assetTypeId);

                if (vmEdit == null)
                {
                    TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                    return RedirectToAction("Index", "AssetType");
                }

                return View("Edit", vmEdit);
            }
            catch(Exception)
            {
                TempData["ErrorMessage"] = "Unable to edit record at this time. Try again later.";
                return RedirectToAction("Index", "AssetType");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssetType vmEdit)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }
            if (string.IsNullOrEmpty(vmEdit.Name))
            {
                ModelState.AddModelError("", "Asset Type Name is required.");
                return View("Edit", vmEdit);
            }

            try
            {
                // stop if entity to update not found
                if(!_unitOfWork.AssetTypes.Exists(vmEdit.Id))
                {
                    TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                    return RedirectToAction("Index", "AssetType");
                }

                // look for active entity with matching name
                AssetType dbAssetTypeDuplicate = _unitOfWork.AssetTypes.Find(r => r.Name == vmEdit.Name && r.Id != vmEdit.Id);
                if (dbAssetTypeDuplicate != null && dbAssetTypeDuplicate.IsActive)
                {
                    // STOP. Duplicate entity found
                    ModelState.AddModelError("", "Name already exists.");
                    return View("Edit", vmEdit);
                }

                // save changes
                _unitOfWork.AssetTypes.Update(vmEdit);
                _unitOfWork.CommitTrans();

                return RedirectToAction("Index", "AssetType");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to edit record at this time. Try again later.");
                return View("Edit", vmEdit);
            }
        }
    }
}