using Financial.Business;
using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.Asset;
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
            var dbAssets = UOW.Assets.GetAll()
                .Where(r => r.IsActive);

            var vmIndex = new List<IndexViewModel>();
            foreach(var dtoAsset in dbAssets)
            {
                var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);
                var assetNameAdditionalInformaiton = GetAccountNameAdditionalInformation(dtoAsset);

                vmIndex.Add(new IndexViewModel(dtoAsset, assetNameAdditionalInformaiton, dtoAssetType));
            }
            /*
            var vmIndex = UOW.Assets.GetAll()
                .Where(r => r.IsActive)
                .Join(UOW.AssetTypes.GetAll(),
                    a => a.AssetTypeId, at => at.Id,
                    (a, at) => new { a, at })
                .Where(j => j.at.IsActive)
                .Join(UOW.AssetSettings.GetAll(),
                    j.a => j.a.Id, ast => ast.Id,
                    j.)
                .Select(j => new IndexViewModel(j.a, j.at))
                .OrderBy(vm => vm.AssetName)
                .ToList();
            */
            // display view
            return View("Index", vmIndex.OrderBy(r => r.AssetName));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // transfer vm to dto
            var dtoAsset = UOW.Assets.Get(vmEdit.Id);
            dtoAsset.Name = vmEdit.Name;
            dtoAsset.AssetTypeId = GetIntegerFromString(vmEdit.SelectedAssetTypeId);

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record updated.";
            return RedirectToAction("Details", "Asset", new { id = vmEdit.Id });
        } 

        [HttpGet]
        public ViewResult Details(int id)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            // transfer id to dto
            var dtoAsset = UOW.Assets.Get(id);
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // display view with message
            return View("Details", new DetailsViewModel(dtoAsset, dtoAssetType));
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            // transfer id to dto
            var dtoAsset = UOW.Assets.Get(id);
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // display view
            return View("Delete", new DeleteViewModel(dtoAsset, dtoAssetType));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel vmDelete)
        {
            // transfer vm to dto
            var dtoAsset = UOW.Assets.Get(vmDelete.Id);
            dtoAsset.IsActive = false;

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record Deleted";
            return RedirectToAction("Index", "Asset");
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