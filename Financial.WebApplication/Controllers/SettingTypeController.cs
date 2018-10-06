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
using Financial.Business;

namespace Financial.WebApplication.Controllers
{
    public class SettingTypeController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IBusinessService _businessService;


        public SettingTypeController(IUnitOfWork unitOfWork, IBusinessService businessService)
            : base()
        {
            _unitOfWork = unitOfWork;
            _businessService = businessService;
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
                var vmIndex = _businessService.SettingTypeService.GetListOfSettingTypes()
                    .OrderBy(r => r.SettingTypeName)
                    .Select(r => new IndexViewModel(r))
                    .ToList();

                return View("Index", vmIndex);
            }
            catch(Exception)
            {
                ViewData["ErrorMessage"] = "Encountered problem";
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
                return RedirectToAction("Index", "SettingType");
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
                    return RedirectToAction("Index", "SettingType");
                }

                // transfer vm to bm
                var bmSettingType = new Business.Models.AttributeType()
                {
                    SettingTypeName = vmCreate.Name,
                };

                // update db
                bmSettingType.SettingTypeId = _businessService.SettingTypeService.AddSettingType(bmSettingType);
                if (bmSettingType.SettingTypeId == 0)
                {
                    ViewData["ErrorMessage"] = "Name already exists";
                    return View("Create", vmCreate);
                }

                // display View with message
                TempData["SuccessMessage"] = "Setting Type Created";
                return RedirectToAction("CreateLinkedAssetTypes", "AssetTypeSettingType", new { settingTypeId = bmSettingType.SettingTypeId });
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
                // get bm
                var bmSettingType = _businessService.SettingTypeService.GetSettingType(id);
                if (bmSettingType == null)
                {
                    TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                    return RedirectToAction("Index", "SettingType");
                }

                // transfer bm to vm
                return View("Edit", new EditViewModel(bmSettingType));
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
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "SettingType");
                }

                // transfer vm to bm
                var bmSettingType = new Business.Models.AttributeType()
                {
                    SettingTypeId = vmEdit.Id,
                    SettingTypeName = vmEdit.Name,
                };

                // update db
                var updated = _businessService.SettingTypeService.EditSettingType(bmSettingType);
                if (!updated)
                {
                    TempData["ErrorMessage"] = "Problem updating record. Try again.";
                    return RedirectToAction("Index", "SettingType");
                }

                TempData["SuccessMessage"] = "Record updated.";
                return RedirectToAction("Index", "SettingType");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "SettingType");
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
                var bmSettingType = _businessService.SettingTypeService.GetSettingType(id);
                if (bmSettingType == null)
                {
                    TempData["ErrorMessage"] = "Unable to display record. Try again.";
                    return RedirectToAction("Index", "SettingType");
                }

                return View("Details", new DetailsViewModel(bmSettingType));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "SettingType");
            }
        }


    }
}