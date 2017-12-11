using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.AssetType;
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
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"].ToString();
            }

            var vmIndex = new List<IndexViewModel>();

            try
            {
                // get values from db
                var dbAssetTypes  = _unitOfWork.AssetTypes.GetAll().OrderBy(r => r.Name).ToList();

                if(dbAssetTypes == null || dbAssetTypes.Count <= 0)
                {
                    ViewData["ErrorMessage"] = "Unable to view information. Try again later.";
                }

                // transfer dto to vm
                foreach(var dtoAssetType in dbAssetTypes)
                {
                    vmIndex.Add(new IndexViewModel(dtoAssetType));
                }

                return View("Index", vmIndex);
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = "Unable to view information at this time. Try again later.";
                return View("Index", vmIndex);
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }

            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Create", "AssetType", new { id = 1 });
            }
            if(string.IsNullOrEmpty(vmCreate.Name))
            {
                ViewData["ErrorMessage"] = "Name is required.";
                return View("Create", vmCreate);
            }

            try
            {
                // look for existing entity with matching name
                AssetType dtoAssetType = _unitOfWork.AssetTypes.Find(r => r.Name == vmCreate.Name);

                // Is this a new entity
                if (dtoAssetType == null)
                {
                    // YES. create new entity
                    AssetType assetType = new AssetType()
                    {
                        Name = vmCreate.Name,
                        IsActive = true
                    };

                    _unitOfWork.AssetTypes.Add(assetType);
                    _unitOfWork.CommitTrans();
                    TempData["SuccessMessage"] = "Record created.";
                    return RedirectToAction("Index", "AssetType");
                }

                // Active entity found
                if (dtoAssetType.IsActive)
                {
                    // YES. STOP. do not duplicate entry
                    ViewData["ErrorMessage"] = "Name already exists.";
                    return View("Create", vmCreate);
                }
                else
                {
                    // update existing entity to active
                    dtoAssetType.IsActive = true;

                    _unitOfWork.AssetTypes.Update(dtoAssetType);
                    _unitOfWork.CommitTrans();
                    TempData["SuccessMessage"] = "Record is visible.";
                    return RedirectToAction("Index", "AssetType");
                }
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Unable to add record at this time. Try again later.";
                return View("Create", vmCreate);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }
            if(id == null)
            {
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }

            try
            {
                // get db values
                var dtoAssetType = _unitOfWork.AssetTypes.Get((int)id);
                if (dtoAssetType == null)
                {
                    TempData["ErrorMessage"] = "Unable to edit record. Try again later.";
                    return RedirectToAction("Index", "AssetType");
                }

                // transfer dto to vm
                var vmEdit = new EditViewModel(dtoAssetType);

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
        public ActionResult Edit(EditViewModel vmEdit)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }
            if (string.IsNullOrEmpty(vmEdit.Name))
            {
                TempData["ErrorMessage"] = "Asset Type Name is required.";
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

                // get record from db
                var dtoAssetType = _unitOfWork.AssetTypes.Get(vmEdit.Id);

                // look for active entity with matching name
                int? count = _unitOfWork.AssetTypes.GetAll()
                    .Where(r => r.Name == vmEdit.Name)
                    .Where(r => r.Id != dtoAssetType.Id)
                    .Count();
                if (count != null && count > 0)
                {
                    // STOP. Duplicate entity found
                    ViewData["ErrorMessage"] = "Name already exists.";
                    return View("Edit", vmEdit);
                }

                // get message to display
                if (dtoAssetType.Name == vmEdit.Name &&
                    dtoAssetType.IsActive == vmEdit.IsActive)
                {
                    // nothing changed
                    TempData["SuccessMessage"] = null;
                }
                else
                {
                    TempData["SuccessMessage"] = "Record updated.";
                }

                // save changes
                dtoAssetType.Name = vmEdit.Name;
                dtoAssetType.IsActive = vmEdit.IsActive;

                _unitOfWork.AssetTypes.Update(dtoAssetType);
                _unitOfWork.CommitTrans();

                return RedirectToAction("Index", "AssetType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unable to edit record at this time. Try again later.";
                return View("Edit", vmEdit);
            }
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }
            if (id == null)
            {
                TempData["ErrorMessage"] = "Unable to display record. Try again.";
                return RedirectToAction("Index", "AssetType");
            }

            try
            { 
                // get db values
                var dtoAssetType = _unitOfWork.AssetTypes.Get((int)id);
                if (dtoAssetType == null)
                {
                    TempData["ErrorMessage"] = "Unable to display record. Try again.";
                    return RedirectToAction("Index", "AssetType");
                }

                // transfer dto to vm
                var vmDetails = new DetailsViewModel(dtoAssetType);

                return View("Details", vmDetails);
            }
            catch(Exception)
            {
                TempData["ErrorMessage"] = "Unable to display record. Try again later.";
                return RedirectToAction("Index", "AssetType");
            }

        }
    }
}