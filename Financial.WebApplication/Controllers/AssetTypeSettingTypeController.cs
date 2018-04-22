using Financial.Business.Models.BusinessModels;
using Financial.Business.Utilities;
using Financial.Core;
using Financial.WebApplication.Models.ViewModels.AssetTypeSettingType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeSettingTypeController : BaseController
    {
        public AssetTypeSettingTypeController()
            : base()
        {
        }

        public AssetTypeSettingTypeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedSettingTypes(int assetTypeId)
        {
            try
            { 
                // transfer dto to vm
                var vmIndexLinkedSettingTypes = new List<IndexLinkedSettingTypesViewModel>();
                var dbAssetTypeSettingTypes = UOW.AssetTypesSettingTypes.GetAllActiveForAssetType(assetTypeId);
                foreach (var dtoATST in dbAssetTypeSettingTypes)
                {
                    var dtoSettingType = UOW.SettingTypes.GetActive(dtoATST.SettingTypeId);
                    if (dtoSettingType != null)
                    {
                        vmIndexLinkedSettingTypes.Add(new IndexLinkedSettingTypesViewModel(dtoSettingType, dtoATST));
                    }
                }
                // display view
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
                // transfer dto to vm
                var vmIndexLinkedAssetTypes = new List<IndexLinkedAssetTypesViewModel>();
                var dbAssetTypeSettingTypes = UOW.AssetTypesSettingTypes.GetAllActiveForSettingType(settingTypeId);
                foreach (var dtoATST in dbAssetTypeSettingTypes)
                {
                    var dtoAssetType = UOW.AssetTypes.GetActive(dtoATST.AssetTypeId);
                    if (dtoAssetType != null)
                    {
                        vmIndexLinkedAssetTypes.Add(new IndexLinkedAssetTypesViewModel(dtoAssetType, dtoATST));
                    }
                }

                // display view
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
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            try
            { 
                // transfer dto to vm
                var dtoAssetType = UOW.AssetTypes.Get(assetTypeId);
                if (dtoAssetType != null)
                {
                    var atstLinks = UOW.SettingTypes.GetAllActive()
                        .Select(r => new LinkedAssetTypeSettingType(dtoAssetType, r))
                        .ToList();

                    // display view
                    return View("CreateLinkedSettingTypes", new CreateLinkedSettingTypesViewModel(dtoAssetType, atstLinks));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkedSettingTypes(CreateLinkedSettingTypesViewModel vmCreateLinkedSettingTypes)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    // transfer vm to db
                    foreach (var atstLink in vmCreateLinkedSettingTypes.LinkedAssetTypeSettingTypes)
                    {
                        UOW.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                        {
                            AssetTypeId = atstLink.AssetTypeId,
                            SettingTypeId = atstLink.SettingTypeId,
                            IsActive = atstLink.IsActive
                        });
                    }

                    // complete db update
                    UOW.CommitTrans();

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
                var dtoSettingType = UOW.SettingTypes.Get(DataTypeUtility.GetIntegerFromString(settingTypeId.ToString()));
                if (dtoSettingType != null)
                {
                    var atstLinks = UOW.AssetTypes.GetAllActive()
                        .Select(r => new LinkedAssetTypeSettingType(r, dtoSettingType))
                        .ToList();

                    // display view
                    return View("CreateLinkedAssetTypes", new CreateLinkedAssetTypesViewModel(dtoSettingType, atstLinks));
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
                        UOW.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                        {
                            AssetTypeId = atstLink.AssetTypeId,
                            SettingTypeId = atstLink.SettingTypeId,
                            IsActive = atstLink.IsActive
                        });
                    }

                    // complete db update
                    UOW.CommitTrans();

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
                // transfer dto for Id
                var dtoAssetType = UOW.AssetTypes.Get(assetTypeId);
                if (dtoAssetType != null)
                {
                    // get list of all active setting types
                    var atstLinks = new List<LinkedAssetTypeSettingType>();
                    var dbSettingTypes = UOW.SettingTypes.GetAllActive();
                    foreach (var dtoSettingType in dbSettingTypes)
                    {
                        // transfer dto to vm
                        var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.Get(dtoAssetType.Id, dtoSettingType.Id);
                        var link = dtoAssetTypeSettingType != null
                            ? new LinkedAssetTypeSettingType(dtoAssetTypeSettingType, dtoAssetType, dtoSettingType)
                            : new LinkedAssetTypeSettingType(new Core.Models.AssetTypeSettingType(), dtoAssetType, dtoSettingType);
                        atstLinks.Add(link);
                    }
                    // display view
                    return View("EditLinkedSettingTypes", new EditLinkedSettingTypesViewModel(dtoAssetType, atstLinks));
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
        public ActionResult EditLinkedSettingTypes(EditLinkedSettingTypesViewModel vmEditLinks)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    foreach (var atstLink in vmEditLinks.LinkedAssetTypeSettingTypes)
                    {
                        // transfer vm to dto
                        var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.Get(atstLink.Id);
                        if (dtoAssetTypeSettingType != null)
                        {
                            // update dto
                            dtoAssetTypeSettingType.IsActive = atstLink.IsActive;
                        }
                        else if(atstLink.Id == 0)
                        {
                            // create new dto
                            UOW.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                            {
                                AssetTypeId = atstLink.AssetTypeId,
                                SettingTypeId = atstLink.SettingTypeId,
                                IsActive = true
                            });
                        }
                    }

                    // complete db update
                    UOW.CommitTrans();

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

        [HttpGet]
        public ActionResult EditLinkedAssetTypes(int settingTypeId)
        {
            try
            { 
                // transfer dto for id
                var dtoSettingType = UOW.SettingTypes.Get(settingTypeId);
                if (dtoSettingType != null)
                {
                    // get list of all active asset types
                    var atstLinks = new List<LinkedAssetTypeSettingType>();
                    var dbAssetTypes = UOW.AssetTypes.GetAllActive();
                    foreach (var dtoAssetType in dbAssetTypes)
                    {
                        // transfer dto to vm
                        var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.Get(dtoAssetType.Id, dtoSettingType.Id);
                        var link = dtoAssetTypeSettingType != null
                            ? new LinkedAssetTypeSettingType(dtoAssetTypeSettingType, dtoAssetType, dtoSettingType)
                            : new LinkedAssetTypeSettingType(new Core.Models.AssetTypeSettingType(), dtoAssetType, dtoSettingType);
                        atstLinks.Add(link);
                    }

                    // display view
                    return View("EditLinkedAssetTypes", new EditLinkedAssetTypesViewModel(dtoSettingType, atstLinks));
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
                    // transfer vm to dto
                    foreach(var atstLink in vmEditLinkedAssetTypes.LinkedAssetTypeSettingTypes)
                    {
                        // transfer vm to dto
                        var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.Get(atstLink.Id);
                        if (dtoAssetTypeSettingType != null)
                        {
                            // update dto
                            dtoAssetTypeSettingType.IsActive = atstLink.IsActive;
                        }
                        else if (atstLink.Id == 0)
                        {
                            // create new dto
                            UOW.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                            {
                                AssetTypeId = atstLink.AssetTypeId,
                                SettingTypeId = atstLink.SettingTypeId,
                                IsActive = true
                            });
                        }
                    }

                    // update db
                    UOW.CommitTrans();

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