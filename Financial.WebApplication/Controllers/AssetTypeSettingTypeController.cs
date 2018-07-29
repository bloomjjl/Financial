using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financial.Business;
//using Financial.Business.Models.BusinessModels;
using Financial.Business.Utilities;
using Financial.WebApplication.Models.ViewModels.AssetTypeSettingType;
using Financial.Data;
using Financial.Business.Models;
using Financial.Business.ServiceInterfaces;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeSettingTypeController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IAssetTypeSettingTypeService _assetTypeSettingTypeService;
        
        public AssetTypeSettingTypeController(IUnitOfWork unitOfWork, IAssetTypeSettingTypeService assetTypeSettingTypeService)
            : base()
        {
            _unitOfWork = unitOfWork;
            _assetTypeSettingTypeService = assetTypeSettingTypeService;
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedSettingTypes(int assetTypeId)
        {
            try
            {
                // transfer bm to vm
                var vmIndexLinkedSettingTypes = _assetTypeSettingTypeService.GetListOfLinkedSettingTypes(assetTypeId)
                    .OrderBy(r => r.SettingTypeName)
                    .Select(r => new IndexLinkedSettingTypesViewModel(r))
                    .ToList();

                return PartialView("_IndexLinkedSettingTypes", vmIndexLinkedSettingTypes);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Encountered problem";
                return PartialView("_IndexLinkedSettingTypes", new List<IndexLinkedSettingTypesViewModel>());
            }
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedAssetTypes(int settingTypeId)
        {
            try
            {
                // transfer bm to vm
                var vmIndexLinkedAssetTypes = _assetTypeSettingTypeService.GetListOfLinkedAssetTypes(settingTypeId)
                    .OrderBy(r => r.AssetTypeName)
                    .Select(r => new IndexLinkedSettingTypesViewModel(r))
                    .ToList();

                return PartialView("_IndexLinkedAssetTypes", vmIndexLinkedAssetTypes);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Encountered problem";
                return PartialView("_IndexLinkedAssetTypes", new List<IndexLinkedAssetTypesViewModel>());
            }
        }

        [HttpGet]
        public ActionResult CreateLinkedSettingTypes(int assetTypeId)
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

                // get bm for supplied id
                var bmAssetType = _assetTypeSettingTypeService.CreateLinkedSettingTypesGetModel(assetTypeId);
                if(bmAssetType == null)
                {
                    TempData["ErrorMessage"] = "Unable to create record. Try again.";
                    return RedirectToAction("Index", "AssetType");
                }

                // get bm for linked setting types
                var bmSettingTypes = _assetTypeSettingTypeService.GetListOfSettingTypesWithLinkedAssetType(bmAssetType.AssetTypeId);

                // transfer bm to vm
                return View("CreateLinkedSettingTypes", new CreateLinkedSettingTypesViewModel(bmAssetType, bmSettingTypes));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkedSettingTypes(CreateLinkedSettingTypesViewModel vmCreateLinkedSettingTypes)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "AssetType");
                }

                // transfer vm to bm
                foreach(var vmSettingType in vmCreateLinkedSettingTypes.SettingTypes)
                {

                }




                if (ModelState.IsValid)
                {
                    // transfer vm to db
                    foreach (var atstLink in vmCreateLinkedSettingTypes.LinkedAssetTypeSettingTypes)
                    {
                        _unitOfWork.AssetTypeSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                        {
                            AssetTypeId = atstLink.AssetTypeId,
                            SettingTypeId = atstLink.SettingTypeId,
                            //IsActive = atstLink.IsActive
                        });
                    }

                    // complete db update
                    _unitOfWork.CommitTrans();

                    // display view with message
                    TempData["SuccessMessage"] = "Linked setting types created.";
                    return RedirectToAction("Index", "AssetType");
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
        public ActionResult CreateLinkedAssetTypes(int? settingTypeId)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            try
            { 
                // transfer dto to vm
                var dtoSettingType = _unitOfWork.SettingTypes.Get(DataTypeUtility.GetIntegerFromString(settingTypeId.ToString()));
                if (dtoSettingType != null)
                {
                    /*
                    var atstLinks = _unitOfWork.AssetTypes.GetAllActive()
                        .Select(r => new AssetTypeSettingType(r, dtoSettingType))
                        .ToList();
                    */
                    // display view
                    //return View("CreateLinkedAssetTypes", new CreateLinkedAssetTypesViewModel(dtoSettingType, atstLinks));
                    return View("CreateLinkedAssetTypes", new CreateLinkedAssetTypesViewModel());
                }
                TempData["ErrorMessage"] = "Unable to create record. Try again.";
                return RedirectToAction("Index", "SettingType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "SettingType");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkedAssetTypes(CreateLinkedAssetTypesViewModel vmCreateLinkedAssetTypes)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    // transfer vm to db
                    foreach (var atstLink in vmCreateLinkedAssetTypes.LinkedAssetTypeSettingTypes)
                    {
                        _unitOfWork.AssetTypeSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                        {
                            AssetTypeId = atstLink.AssetTypeId,
                            SettingTypeId = atstLink.SettingTypeId,
                            //IsActive = atstLink.IsActive
                        });
                    }

                    // complete db update
                    _unitOfWork.CommitTrans();

                    // display view with message
                    TempData["SuccessMessage"] = "Linked asset types created";
                    return RedirectToAction("Index", "SettingType", new { id = vmCreateLinkedAssetTypes.SettingTypeId });
                }
                TempData["ErrorMessage"] = "Unable to create record. Try again.";
                return RedirectToAction("Index", "SettingType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "SettingType");
            }
        }

        [HttpGet]
        public ActionResult EditLinkedSettingTypes(int assetTypeId)
        {
            try
            {
                // transfer dto to bm
                var bmAssetType = _assetTypeSettingTypeService.EditLinkedSettingTypesGetModel(assetTypeId);
                if(bmAssetType == null)
                {
                    TempData["ErrorMessage"] = "Problem displaying asset type";
                    return RedirectToAction("Index", "AssetType");
                }

                //var bmSettingTypes = _businessService.AssetTypeSettingTypeService.GetListOfSettingTypesWithLinkedAssetType(assetTypeId);

                return View("EditLinkedSettingTypes", new EditLinkedSettingTypesViewModel(bmAssetType));


                /*
                // transfer dto for Id
                var dtoAssetType = _unitOfWork.AssetTypes.Get(assetTypeId);
                if (dtoAssetType != null)
                {
                    
                    // get list of all active setting types
                    var atstLinks = new List<AssetTypeSettingType>();
                    var dbSettingTypes = _unitOfWork.SettingTypes.GetAllActive();
                    foreach (var dtoSettingType in dbSettingTypes)
                    {
                        // transfer dto to vm
                        var dtoAssetTypeSettingType = _unitOfWork.AssetTypesSettingTypes.Get(dtoAssetType.Id, dtoSettingType.Id);
                        var link = dtoAssetTypeSettingType != null
                            ? new AssetTypeSettingType(dtoAssetTypeSettingType, dtoAssetType, dtoSettingType)
                            : new AssetTypeSettingType(new Core.Models.AssetTypeSettingType(), dtoAssetType, dtoSettingType);
                        atstLinks.Add(link);
                    }
                    
                    // display view
                    //return View("EditLinkedSettingTypes", new EditLinkedSettingTypesViewModel(dtoAssetType, atstLinks));
                    return View("EditLinkedSettingTypes", new EditLinkedSettingTypesViewModel());
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetType");
                */
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetType");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLinkedSettingTypes(EditLinkedSettingTypesViewModel vmEditLinks)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    /*
                    foreach (var atstLink in vmEditLinks.LinkedAssetTypeSettingTypes)
                    {
                        // transfer vm to dto
                        var dtoAssetTypeSettingType = _unitOfWork.AssetTypesSettingTypes.Get(atstLink.Id);
                        if (dtoAssetTypeSettingType != null)
                        {
                            // update dto
                            dtoAssetTypeSettingType.IsActive = atstLink.IsActive;
                        }
                        else if(atstLink.Id == 0)
                        {
                            // create new dto
                            _unitOfWork.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                            {
                                AssetTypeId = atstLink.AssetTypeId,
                                SettingTypeId = atstLink.SettingTypeId,
                                IsActive = true
                            });
                        }
                    }

                    // complete db update
                    _unitOfWork.CommitTrans();
                    */
                    // display view with message
                    TempData["SuccessMessage"] = "Linked setting types updated.";
                    return RedirectToAction("Details", "AssetType", new { id = vmEditLinks.AssetTypeId });
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

        [ChildActionOnly]
        public ActionResult IndexSettingTypesForAssetType(int assetTypeId)
        {
            try
            {
                var vmIndex = _assetTypeSettingTypeService.GetListOfSettingTypesWithLinkedAssetType(assetTypeId)
                    .Select(st => new IndexSettingTypesForAssetTypeViewModel(assetTypeId, st))
                    .ToList();

                return PartialView("_IndexSettingTypesForAssetType", vmIndex);
            }
            catch (Exception)
            {
                return PartialView("_IndexSettingTypesForAssetType", new List<IndexSettingTypesForAssetTypeViewModel>());
            }
        }

        [HttpGet]
        public ActionResult EditLinkedAssetTypes(int settingTypeId)
        {
            try
            { 
                // transfer dto for id
                var dtoSettingType = _unitOfWork.SettingTypes.Get(settingTypeId);
                if (dtoSettingType != null)
                {
                    /*
                    // get list of all active asset types
                    var atstLinks = new List<AssetTypeSettingType>();
                    var dbAssetTypes = _unitOfWork.AssetTypes.GetAllActive();
                    foreach (var dtoAssetType in dbAssetTypes)
                    {
                        // transfer dto to vm
                        var dtoAssetTypeSettingType = _unitOfWork.AssetTypesSettingTypes.Get(dtoAssetType.Id, dtoSettingType.Id);
                        var link = dtoAssetTypeSettingType != null
                            ? new AssetTypeSettingType(dtoAssetTypeSettingType, dtoAssetType, dtoSettingType)
                            : new AssetTypeSettingType(new Core.Models.AssetTypeSettingType(), dtoAssetType, dtoSettingType);
                        atstLinks.Add(link);
                    }
                    */

                    // display view
                    //return View("EditLinkedAssetTypes", new EditLinkedAssetTypesViewModel(dtoSettingType, atstLinks));
                    return View("EditLinkedAssetTypes", new EditLinkedAssetTypesViewModel());
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "SettingType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "SettingType");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLinkedAssetTypes(EditLinkedAssetTypesViewModel vmEditLinkedAssetTypes)
        {
            try
            { 
                if(ModelState.IsValid)
                { 
                    /*
                    // transfer vm to dto
                    foreach(var atstLink in vmEditLinkedAssetTypes.LinkedAssetTypeSettingTypes)
                    {
                        // transfer vm to dto
                        var dtoAssetTypeSettingType = _unitOfWork.AssetTypesSettingTypes.Get(atstLink.Id);
                        if (dtoAssetTypeSettingType != null)
                        {
                            // update dto
                            dtoAssetTypeSettingType.IsActive = atstLink.IsActive;
                        }
                        else if (atstLink.Id == 0)
                        {
                            // create new dto
                            _unitOfWork.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                            {
                                AssetTypeId = atstLink.AssetTypeId,
                                SettingTypeId = atstLink.SettingTypeId,
                                IsActive = true
                            });
                        }
                    }

                    // update db
                    _unitOfWork.CommitTrans();
                    */
                    // display view with message
                    TempData["SuccessMessage"] = "Linked asset types updated.";
                    return RedirectToAction("Details", "SettingType", new { id = vmEditLinkedAssetTypes.SettingTypeId });
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "SettingType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "SettingType");
            }
        }


    }
}