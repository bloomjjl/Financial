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
    public class AssetController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AssetController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            // transfer db 
            var dbAssetTypes = _unitOfWork.AssetTypes.GetAll()
                .Where(r => r.IsActive)
                .ToList();
            
            // transfer db to sli
            var sliAssetTypes = dbAssetTypes
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
            int assetTypeId = 0;
            int.TryParse(vmCreate.SelectedAssetTypeId, out assetTypeId);
            _unitOfWork.Assets.Add(new Asset()
            {
                AssetTypeId = assetTypeId,
                Name = vmCreate.AssetName,
                IsActive = true
            });

            // update db
            _unitOfWork.CommitTrans();

            // display view
            return View();
        }
    }
}