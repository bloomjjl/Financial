using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.AssetTypeRelationshipType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeRelationshipTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AssetTypeRelationshipTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetTypeRelationshipTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [ChildActionOnly]
        public ActionResult IndexLinkedAssetTypes(int relationshipTypeId)
        {
            /*
            // get ParentAssetTypes linked to Id
            var dbParentChildRelationships = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ParentChildRelationshipTypeId == relationshipTypeId)
                .ToList();

            // get ChildAssetTypes linked to Id
            var dbChildParentRelationships = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ChildParentRelationshipTypeId == relationshipTypeId)
                .ToList();

            // transfer Parent to vm
            var vmIndexLinkedAssetTypes = new List<IndexLinkedAssetTypesViewModel>();
            foreach (var dtoParentRelationship in dbParentChildRelationships)
            {
                var dtoParentAssetType = _unitOfWork.AssetTypes.Get(dtoParentRelationship.ParentAssetTypeId);
                var dtoChildAssetType = _unitOfWork.AssetTypes.Get(dtoParentRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentRelationship.ParentChildRelationshipTypeId);
                var dtoChildParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentRelationship.ChildParentRelationshipTypeId);
                vmIndexLinkedAssetTypes.Add(new IndexLinkedAssetTypesViewModel(dtoParentRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentChildRelationshipType, dtoChildParentRelationshipType));
            }

            // transfer Child to vm
            foreach (var dtoChildRelationship in dbChildParentRelationships)
            {
                var dtoParentAssetType = _unitOfWork.AssetTypes.Get(dtoChildRelationship.ParentAssetTypeId);
                var dtoChildAssetType = _unitOfWork.AssetTypes.Get(dtoChildRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoChildRelationship.ParentChildRelationshipTypeId);
                var dtoChildParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoChildRelationship.ChildParentRelationshipTypeId);
                vmIndexLinkedAssetTypes.Add(new IndexLinkedAssetTypesViewModel(dtoChildRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentChildRelationshipType, dtoChildParentRelationshipType));
            }

            // display view
            return PartialView("_IndexLinkedAssetTypes", vmIndexLinkedAssetTypes);
            */
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedRelationshipTypes(int assetTypeId)
        {
            // transfer supplied Id to dto
            var dtoSuppliedAssetType = _unitOfWork.AssetTypes.Get(assetTypeId);

            // get linked ParentRelationshipTypes to Id
            var dbParentRelationships = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ParentAssetTypeId == assetTypeId)
                .ToList();

            // transfer db to vm
            var vmIndexLinkedRelationshipTypes = new List<IndexLinkedRelationshipTypesViewModel>();
            foreach (var dtoParentRelationship in dbParentRelationships)
            {
                var dtoParentAssetType = _unitOfWork.AssetTypes.Get(dtoParentRelationship.ParentAssetTypeId);
                var dtoChildAssetType = _unitOfWork.AssetTypes.Get(dtoParentRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = _unitOfWork.ParentChildRelationshipTypes.Get(dtoParentRelationship.ParentChildRelationshipTypeId);
                var dtoParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
                var dtoChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
                vmIndexLinkedRelationshipTypes.Add(new IndexLinkedRelationshipTypesViewModel(dtoParentRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentRelationshipType, dtoChildRelationshipType));
            }

            // get linked ChildRelationshipTypes to Id
            var dbChildRelationships = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ChildAssetTypeId == assetTypeId)
                .ToList();

            // transfer ChildParent to vm
            foreach (var dtoChildRelationship in dbChildRelationships)
            {
                var dtoParentAssetType = _unitOfWork.AssetTypes.Get(dtoChildRelationship.ParentAssetTypeId);
                var dtoChildAssetType = _unitOfWork.AssetTypes.Get(dtoChildRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = _unitOfWork.ParentChildRelationshipTypes.Get(dtoChildRelationship.ParentChildRelationshipTypeId);
                var dtoParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
                var dtoChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
                vmIndexLinkedRelationshipTypes.Add(new IndexLinkedRelationshipTypesViewModel(dtoChildRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentRelationshipType, dtoChildRelationshipType));
            }

            return PartialView("_IndexLinkedRelationshipTypes", vmIndexLinkedRelationshipTypes);
        }

        [HttpGet]
        public ViewResult Create(int assetTypeId)
        {
            // transfer id to dto
            var dtoSuppliedAssetType = _unitOfWork.AssetTypes.Get(assetTypeId);

            // get drop down lists
            List<SelectListItem> sliRelationshipLevels = GetRelationshipLevels(null);
            List<SelectListItem> sliParentRelationshipTypes = GetParentRelationshipTypes();
            List<SelectListItem> sliChildRelationshipTypes = GetChildRelationshipTypes();
            List<SelectListItem> sliLinkAssetTypes = GetLinkAssetTypes(assetTypeId);

            // display view
            return View("Create", new CreateViewModel(dtoSuppliedAssetType, sliRelationshipLevels, sliParentRelationshipTypes, sliChildRelationshipTypes, sliLinkAssetTypes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Encountered a problem. Try again.";
                return RedirectToAction("Index", "AssetType");
            }

            // check for existing link
            var countParent = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Count(r => r.ParentAssetTypeId == vmCreate.SuppliedAssetTypeId &&
                            r.ChildAssetTypeId == GetIntegerForString(vmCreate.SelectedLinkAssetType) &&
                            r.ParentChildRelationshipTypeId == GetIntegerForString(vmCreate.SelectedRelationshipType) &&
                            r.IsActive);
            var countChild = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Count(r => r.ParentAssetTypeId == GetIntegerForString(vmCreate.SelectedLinkAssetType) &&
                            r.ChildAssetTypeId == vmCreate.SuppliedAssetTypeId &&
                            r.ParentChildRelationshipTypeId == GetIntegerForString(vmCreate.SelectedRelationshipType) &&
                            r.IsActive);
            if(countParent > 0 || countChild > 0)
            {
                // get drop down lists
                vmCreate.RelationshipLevels = GetRelationshipLevels(null);
                vmCreate.ParentRelationshipTypes = GetParentRelationshipTypes();
                vmCreate.ChildRelationshipTypes = GetChildRelationshipTypes();
                vmCreate.LinkAssetTypes = GetLinkAssetTypes(vmCreate.SuppliedAssetTypeId);

                // redisplay view
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Create", vmCreate);
            }

            // transfer vm to dto
            if (vmCreate.SelectedRelationshipLevel == "Parent")
            {
                _unitOfWork.AssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType
                {
                    ParentAssetTypeId = vmCreate.SuppliedAssetTypeId,
                    ChildAssetTypeId = GetIntegerForString(vmCreate.SelectedLinkAssetType),
                    ParentChildRelationshipTypeId = GetIntegerForString(vmCreate.SelectedRelationshipType),
                    IsActive = true
                });
            }
            else // supplied AssetType == Child
            {
                _unitOfWork.AssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType
                {
                    ParentAssetTypeId = GetIntegerForString(vmCreate.SelectedLinkAssetType),
                    ChildAssetTypeId = vmCreate.SuppliedAssetTypeId,
                    ParentChildRelationshipTypeId = GetIntegerForString(vmCreate.SelectedRelationshipType),
                    IsActive = true
                });
            }

            // update db
            _unitOfWork.CommitTrans();

            // return view
            return RedirectToAction("Details", "AssetType", new { id = vmCreate.SuppliedAssetTypeId });
        }

        [ChildActionOnly]
        public ActionResult CreateLinkedRelationshipTypes(int assetTypeId, string relationshipLevel)
        {
            /*
            List<SelectListItem> sliRelationshipTypes = NewMethod(relationshipLevel);
            */
            // display view
            return PartialView("_CreateLinkedRelationshipTypes", new CreateLinkedRelationshipTypesViewModel());
        }

        [HttpGet]
        public ViewResult Edit(int id, int assetTypeId)
        {
            // get dto for supplied id
            var dtoSuppliedAssetType = _unitOfWork.AssetTypes.Get(assetTypeId);
            var dtoAssetTypeRelationshipType = _unitOfWork.AssetTypesRelationshipTypes.Get(id);

            // Selected values
            var selectedRelationshipLevel = dtoAssetTypeRelationshipType.ParentAssetTypeId == dtoSuppliedAssetType.Id ?
                "Parent" : "Child";

            // get drop down lists
            List<SelectListItem> sliRelationshipLevels = GetRelationshipLevels(selectedRelationshipLevel);

            // display view
            return View("Edit", new EditViewModel(dtoSuppliedAssetType, sliRelationshipLevels, selectedRelationshipLevel));
        }

        private List<SelectListItem> GetLinkAssetTypes(int assetTypeId)
        {
            List<SelectListItem> sliLinkAssetTypes = new List<SelectListItem>();
            var dbAssetTypes = _unitOfWork.AssetTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.Id != assetTypeId)
                .ToList();

            foreach (var dtoLinkAssetType in dbAssetTypes)
            {
                var countParentLinks = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                    .Where(r => r.ParentAssetTypeId == assetTypeId && r.ChildAssetTypeId == dtoLinkAssetType.Id)
                    .Count(r => r.IsActive);

                if (countParentLinks == 0)
                {
                    sliLinkAssetTypes.Add(new SelectListItem()
                    {
                        Value = dtoLinkAssetType.Id.ToString(),
                        Text = dtoLinkAssetType.Name
                    });
                }
            }

            return sliLinkAssetTypes;
        }

        private List<SelectListItem> GetParentRelationshipTypes()
        {
            return _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Join(_unitOfWork.RelationshipTypes.GetAll().Where(r => r.IsActive).ToList(),
                    atrt => atrt.ParentRelationshipTypeId, rt => rt.Id, (atrt, rt) => new SelectListItem()
                    {
                        Value = atrt.Id.ToString(),
                        Text = rt.Name
                    })
                .ToList();
        }

        private List<SelectListItem> GetChildRelationshipTypes()
        {
            return _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Join(_unitOfWork.RelationshipTypes.GetAll().Where(r => r.IsActive).ToList(),
                    atrt => atrt.ChildRelationshipTypeId, rt => rt.Id, (atrt, rt) => new SelectListItem()
                    {
                        Value = atrt.Id.ToString(),
                        Text = rt.Name
                    })
                .ToList();
        }

        private static List<SelectListItem> GetRelationshipLevels(string selectedValue)
        {
            List<SelectListItem> sliRelationshipLevel = new List<SelectListItem>();
            sliRelationshipLevel.Add(new SelectListItem()
            {
                Value = "Parent",
                Selected = "Parent" == selectedValue,
                Text = "Parent"
            });
            sliRelationshipLevel.Add(new SelectListItem()
            {
                Value = "Child",
                Selected = "Child" == selectedValue,
                Text = "Child"
            });
            return sliRelationshipLevel;
        }

        private int GetIntegerForString(string stringValue)
        {
            int integerValue = 0;
            int.TryParse(stringValue, out integerValue);
            return integerValue;
        }
    }
}