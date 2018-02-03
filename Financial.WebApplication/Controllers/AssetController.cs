using Financial.Core;
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
            var sliAssetTypes = UOW.AssetTypes.FindAll(r => r.IsActive)
                .Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                })
                .ToList();
            
            // display view
            return View("Create", new CreateViewModel(sliAssetTypes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // transfer vm to dto
            UOW.Assets.Add(new Asset()
            {
                AssetTypeId = GetIntegerFromString(vmCreate.SelectedAssetTypeId),
                Name = vmCreate.AssetName,
                IsActive = true
            });

            // update db
            UOW.CommitTrans();

            // display view
            TempData["SuccessMessage"] = "Asset Created";
            return RedirectToAction("Index", "Asset");
        }
    }
}