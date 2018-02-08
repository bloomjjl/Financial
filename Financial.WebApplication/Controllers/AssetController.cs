﻿using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.Asset;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetController : BaseController
    {
        public AssetController()
            : base()
        {
        }

        public AssetController(IUnitOfWork unitOfWork)
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
            var vmIndex = UOW.Assets.FindAll(r => r.IsActive)
                .Join(UOW.AssetTypes.FindAll(r => r.IsActive),
                    a => a.AssetTypeId, at => at.Id,
                    (a, at) => new IndexViewModel(a, at))
                .ToList();

            // display view
            return View("Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create()
        {
            // transfer dto to sli
            var sliAssetTypes = GetAssetTypesDropDownList(null);

            // display view
            return View("Create", new CreateViewModel(sliAssetTypes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // transfer vm to dto
            var dtoAsset = new Asset()
            {
                AssetTypeId = GetIntegerFromString(vmCreate.SelectedAssetTypeId),
                Name = vmCreate.AssetName,
                IsActive = true
            };
            UOW.Assets.Add(dtoAsset);

            // update db
            UOW.CommitTrans();

            // display view
            TempData["SuccessMessage"] = "Asset Created";
            return RedirectToAction("Create", "AssetSetting", new { assetId = dtoAsset.Id });
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            // transfer id to dto
            var dtoAsset = UOW.Assets.Get(id);
            var sliAssetTypes = GetAssetTypesDropDownList(dtoAsset.AssetTypeId);

            // display view
            return View("Edit", new EditViewModel(dtoAsset, sliAssetTypes));
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            // transfer id to vm
            var dtoAsset = UOW.Assets.Get(id);
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // display view with message
            return View("Details", new DetailsViewModel(dtoAsset, dtoAssetType));
        }

        private List<SelectListItem> GetAssetTypesDropDownList(int? selectedId)
        {
            return UOW.AssetTypes.FindAll(r => r.IsActive)
                .Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Selected = r.Id == selectedId,
                    Text = r.Name
                })
                .ToList();
        }
    }
}