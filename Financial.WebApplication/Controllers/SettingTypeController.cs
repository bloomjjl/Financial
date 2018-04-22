using Financial.Business.Utilities;
using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.SettingType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class SettingTypeController : BaseController
    {
        public SettingTypeController()
            : base()
        {
        }

        public SettingTypeController(IUnitOfWork unitOfWork)
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
                var vmIndex = UOW.SettingTypes.GetAllOrderedByName()
                    .Select(r => new IndexViewModel(r))
                    .ToList();

                // display view
                return View("Index", vmIndex);
            }
            catch(Exception)
            {
                ViewData["ErrorMessage"] = "Encountered problem";
                return View("Index", new List<IndexViewModel>());
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

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
                    var count = UOW.SettingTypes.CountMatching(vmCreate.Name);
                    if (count == 0)
                    {
                        // transfer vm to dto
                        var dtoSettingType = new SettingType()
                        {
                            Name = vmCreate.Name,
                            IsActive = true
                        };

                        // update db
                        UOW.SettingTypes.Add(dtoSettingType);
                        UOW.CommitTrans();

                        // display view with message
                        TempData["SuccessMessage"] = "Record created";
                        return RedirectToAction("CreateLinkedAssetTypes", "AssetTypeSettingType", new { settingTypeId = dtoSettingType.Id });
                    }
                    // display view with message
                    ViewData["ErrorMessage"] = "Record already exists";
                    return View("Create", vmCreate);
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
        public ActionResult Edit(int id)
        {
            try
            { 
                // transfer dto to vm
                var dtoSettingType = UOW.SettingTypes.Get(id);
                if (dtoSettingType != null)
                {
                    // display view
                    return View("Edit", new EditViewModel(dtoSettingType));
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
        public ActionResult Edit(EditViewModel vmEdit)
        {
            try
            {
                // validation
                if (ModelState.IsValid)
                {
                    // check for duplicate
                    var count = UOW.SettingTypes.CountMatching(vmEdit.Id, vmEdit.Name);
                    if (count == 0)
                    {
                        // transfer vm to dto
                        var dtoSettingType = UOW.SettingTypes.Get(vmEdit.Id);
                        dtoSettingType.Name = vmEdit.Name;
                        dtoSettingType.IsActive = vmEdit.IsActive;

                        // update db
                        UOW.CommitTrans();

                        // display view with message
                        TempData["SuccessMessage"] = "Record updated";
                        return RedirectToAction("Index", "SettingType");
                    }
                    // display view with message
                    ViewData["ErrorMessage"] = "Record already exists";
                    return View("Edit", vmEdit);
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

        [HttpGet]
        public ActionResult Details(int? id)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            try
            {
                // transfer dto to vm
                var dtoSettingType = UOW.SettingTypes.Get(DataTypeUtility.GetIntegerFromString(id.ToString()));
                if(dtoSettingType != null)
                {
                    // display view
                    return View("Details", new DetailsViewModel(dtoSettingType));
                }
                TempData["ErrorMessage"] = "Unable to display record. Try again.";
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