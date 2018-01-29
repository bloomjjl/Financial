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
    public class AssetTypeRelationshipTypeController : BaseController
    {
        public AssetTypeRelationshipTypeController()
            : base()
        {
        }

        public AssetTypeRelationshipTypeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        
        [ChildActionOnly]
        public ActionResult Index(int assetTypeId)
        {
            // transfer supplied Id to dto
            var dtoSuppliedAssetType = UOW.AssetTypes.Get(assetTypeId);

            // get linked ParentRelationshipTypes to Id
            var dbParentRelationships = UOW.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ParentAssetTypeId == assetTypeId)
                .ToList();

            // transfer db to vm
            var vmIndexLinkedRelationshipTypes = new List<IndexViewModel>();
            foreach (var dtoParentRelationship in dbParentRelationships)
            {
                var dtoParentAssetType = UOW.AssetTypes.Get(dtoParentRelationship.ParentAssetTypeId);
                var dtoChildAssetType = UOW.AssetTypes.Get(dtoParentRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = UOW.ParentChildRelationshipTypes.Get(dtoParentRelationship.ParentChildRelationshipTypeId);
                var dtoParentRelationshipType = UOW.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
                var dtoChildRelationshipType = UOW.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
                vmIndexLinkedRelationshipTypes.Add(new IndexViewModel(dtoParentRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentRelationshipType, dtoChildRelationshipType));
            }

            // get linked ChildRelationshipTypes to Id
            var dbChildRelationships = UOW.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ChildAssetTypeId == assetTypeId)
                .ToList();

            // transfer ChildParent to vm
            foreach (var dtoChildRelationship in dbChildRelationships)
            {
                var dtoParentAssetType = UOW.AssetTypes.Get(dtoChildRelationship.ParentAssetTypeId);
                var dtoChildAssetType = UOW.AssetTypes.Get(dtoChildRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = UOW.ParentChildRelationshipTypes.Get(dtoChildRelationship.ParentChildRelationshipTypeId);
                var dtoParentRelationshipType = UOW.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
                var dtoChildRelationshipType = UOW.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
                vmIndexLinkedRelationshipTypes.Add(new IndexViewModel(dtoChildRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentRelationshipType, dtoChildRelationshipType));
            }

            return PartialView("_Index", vmIndexLinkedRelationshipTypes);
        }

        [HttpGet]
        public ViewResult Create(int assetTypeId)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            // transfer id to dto
            var dtoSuppliedAssetType = UOW.AssetTypes.Get(assetTypeId);

            // get drop down lists
            List<SelectListItem> sliRelationshipLevels = GetRelationshipLevels(null);

            // display view
            return View("Create", new CreateViewModel(dtoSuppliedAssetType, sliRelationshipLevels));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // validation
            if (!ModelState.IsValid)
            {
                return View("Create", vmCreate);
            }

            // count existing link
            int id = 0;
            int count = CountExistingLinks(id, vmCreate.SuppliedAssetTypeId, vmCreate.SelectedLinkAssetType, vmCreate.SelectedParentChildRelationshipType);

            // links found
            if (count > 0)
            {
                // YES. get drop down lists
                vmCreate.RelationshipLevels = GetRelationshipLevels(null);

                // redisplay view with message
                ViewData["ErrorMessage"] = "Link already exists";
                return View("Create", vmCreate);
            }

            // transfer vm to dto
            if (vmCreate.SelectedRelationshipLevel == "Parent")
            {
                UOW.AssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType
                {
                    ParentAssetTypeId = vmCreate.SuppliedAssetTypeId,
                    ChildAssetTypeId = GetIntegerFromString(vmCreate.SelectedLinkAssetType.ToString()),
                    ParentChildRelationshipTypeId = GetIntegerFromString(vmCreate.SelectedParentChildRelationshipType),
                    IsActive = true
                });
            }
            else // supplied AssetType == Child
            {
                UOW.AssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType
                {
                    ParentAssetTypeId = GetIntegerFromString(vmCreate.SelectedLinkAssetType),
                    ChildAssetTypeId = vmCreate.SuppliedAssetTypeId,
                    ParentChildRelationshipTypeId = GetIntegerFromString(vmCreate.SelectedParentChildRelationshipType),
                    IsActive = true
                });
            }

            // update db
            UOW.CommitTrans();

            // return view with message
            TempData["SuccessMessage"] = "Parent-Child link created.";
            return RedirectToAction("Details", "AssetType", new { id = vmCreate.SuppliedAssetTypeId });
        }

        private int CountExistingLinks(int id, int suppliedAssetTypeId, string selectedAssetTypeId, string selectedParentChildRelationshipType)
        {
            var countParent = UOW.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.Id != id)
                .Where(r => r.ParentAssetTypeId == suppliedAssetTypeId)
                .Where(r => r.ChildAssetTypeId == GetIntegerFromString(selectedAssetTypeId))
                .Where(r => r.ParentChildRelationshipTypeId == GetIntegerFromString(selectedParentChildRelationshipType))
                .Count(r => r.IsActive);
            var countChild = UOW.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.Id != id)
                .Where(r => r.ParentAssetTypeId == GetIntegerFromString(selectedAssetTypeId))
                .Where(r => r.ChildAssetTypeId == suppliedAssetTypeId)
                .Where(r => r.ParentChildRelationshipTypeId == GetIntegerFromString(selectedParentChildRelationshipType))
                .Count(r => r.IsActive);

            return countParent + countChild;
        }

        [HttpGet]
        public ActionResult DisplayParentChildRelationshipTypes(int suppliedAssetTypeId, string selectedRelationshipLevelId, int? selectedParentChildRelationshipTypeId)
        {
            // get filtered list to display
            List<SelectListItem> sliParentChildRelationshipTypes = GetParentChildRelationshipTypes(suppliedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId);
            
            // display view
            return PartialView("_DisplayParentChildRelationshipTypes", new DisplayParentChildRelationshipTypesViewModel(sliParentChildRelationshipTypes, selectedParentChildRelationshipTypeId.ToString()));
        }
        
        [HttpGet]
        public ActionResult DisplayLinkAssetTypes(int suppliedAssetTypeId, string selectedRelationshipLevelId, string selectedParentChildRelationshipTypeId, int? selectedAssetTypeId)
        {
            // get filtered list to display
            List<SelectListItem> sliAssetTypes = GetAssetTypes(suppliedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedAssetTypeId);

            // display view
            return PartialView("_DisplayLinkAssetTypes", new DisplayLinkAssetTypesViewModel(sliAssetTypes, selectedAssetTypeId.ToString()));
        }

        [HttpGet]
        public ViewResult Edit(int id, int suppliedAssetTypeId)
        {
            // get dto for supplied id
            var dtoSuppliedAssetType = UOW.AssetTypes.Get(suppliedAssetTypeId);
            var dtoAssetTypeRelationshipType = UOW.AssetTypesRelationshipTypes.Get(id);

            // Selected values
            var selectedRelationshipLevel = dtoAssetTypeRelationshipType.ParentAssetTypeId == dtoSuppliedAssetType.Id ?
                "Parent" : "Child";

            // get drop down lists
            List<SelectListItem> sliRelationshipLevels = GetRelationshipLevels(selectedRelationshipLevel);

            // display view
            return View("Edit", new EditViewModel(dtoAssetTypeRelationshipType, dtoSuppliedAssetType, sliRelationshipLevels, selectedRelationshipLevel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // validation
            if (!ModelState.IsValid)
            {
                return View("Edit", vmEdit);
            }

            // count existing link
            int count = CountExistingLinks(vmEdit.Id, vmEdit.SuppliedAssetTypeId, vmEdit.SelectedAssetTypeId, vmEdit.SelectedParentChildRelationshipTypeId);

            // links found
            if (count > 0)
            {
                // YES. get drop down lists
                vmEdit.RelationshipLevels = GetRelationshipLevels(vmEdit.SelectedRelationshipLevel);

                // redisplay view with message
                ViewData["ErrorMessage"] = "Link already exists";
                return View("Edit", vmEdit);
            }

            // check for identical record
            var countParentRelationship = UOW.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.Id == vmEdit.Id)
                .Where(r => r.ParentAssetTypeId == vmEdit.SuppliedAssetTypeId)
                .Where(r => r.ChildAssetTypeId == GetIntegerFromString(vmEdit.SelectedAssetTypeId))
                .Where(r => r.ParentChildRelationshipTypeId == GetIntegerFromString(vmEdit.SelectedParentChildRelationshipTypeId))
                .Count(r => r.IsActive);
            var countChildRelationship = UOW.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.Id == vmEdit.Id)
                .Where(r => r.ParentAssetTypeId == GetIntegerFromString(vmEdit.SelectedAssetTypeId))
                .Where(r => r.ChildAssetTypeId == vmEdit.SuppliedAssetTypeId)
                .Where(r => r.ParentChildRelationshipTypeId == GetIntegerFromString(vmEdit.SelectedParentChildRelationshipTypeId))
                .Count(r => r.IsActive);

            // record changed?
            if (countParentRelationship + countChildRelationship == 0)
            {
                // transfer vm to dto
                var dtoAssetTypeRelationshipType = UOW.AssetTypesRelationshipTypes.Get(vmEdit.Id);


                // transfer vm to dto
                dtoAssetTypeRelationshipType.ParentChildRelationshipTypeId = GetIntegerFromString(vmEdit.SelectedParentChildRelationshipTypeId);
                if (vmEdit.SelectedRelationshipLevel == "Parent")
                {
                    dtoAssetTypeRelationshipType.ParentAssetTypeId = vmEdit.SuppliedAssetTypeId;
                    dtoAssetTypeRelationshipType.ChildAssetTypeId = GetIntegerFromString(vmEdit.SelectedAssetTypeId);
                }
                else
                {
                    dtoAssetTypeRelationshipType.ParentAssetTypeId = GetIntegerFromString(vmEdit.SelectedAssetTypeId);
                    dtoAssetTypeRelationshipType.ChildAssetTypeId = vmEdit.SuppliedAssetTypeId;
                }

                // update db
                UOW.CommitTrans();
            }

            // display view with message
            TempData["SuccessMessage"] = "Parent-Child link updated.";
            return RedirectToAction("Details", "AssetType", new { id = vmEdit.SuppliedAssetTypeId });
        }

        private List<SelectListItem> GetAssetTypes(int suppliedAssetTypeId, string selectedRelationshipLevelId, string selectedParentChildRelationshipTypeId, int? selectedAssetTypeId)
        {
            // create sli
            List<SelectListItem> sliAssetTypes = new List<SelectListItem>();

            // store all available asset types
            var dbAssetTypes = UOW.AssetTypes.FindAll(r => r.IsActive);

            // add to sli if link does NOT exist
            foreach(var dtoAssetType in dbAssetTypes)
            {
                // check for matching Parent-Child link
                var countParentLinks = UOW.AssetTypesRelationshipTypes.GetAll()
                    .Where(r => r.ParentAssetTypeId == suppliedAssetTypeId)
                    .Where(r => r.ChildAssetTypeId == dtoAssetType.Id)
                    .Where(r => r.ChildAssetTypeId != selectedAssetTypeId)
                    .Count(r => r.IsActive);

                // check for matching Child-Parent link
                var countChildLinks = UOW.AssetTypesRelationshipTypes.GetAll()
                    .Where(r => r.ParentAssetTypeId == dtoAssetType.Id)
                    .Where(r => r.ParentAssetTypeId != selectedAssetTypeId)
                    .Where(r => r.ChildAssetTypeId == suppliedAssetTypeId)
                    .Count(r => r.IsActive);

                // add if link not found
                if(countParentLinks + countChildLinks == 0)
                {
                    sliAssetTypes.Add(new SelectListItem()
                    {
                        Value = dtoAssetType.Id.ToString(),
                        Selected = dtoAssetType.Id == selectedAssetTypeId,
                        Text = dtoAssetType.Name
                    });
                }
            }

            return sliAssetTypes;
        }

        private List<SelectListItem> GetParentChildRelationshipTypes(int suppliedAssetTypeId, string selectedRelationshipLevelId, int? selectedParentChildRelationshipTypeId)
        {
            // get list based on relationship level
            if (selectedRelationshipLevelId == "Parent")
            {
                // display list of all child relationship types
                return UOW.RelationshipTypes.GetAll()
                    .Where(r => r.IsActive)
                    .Join(UOW.ParentChildRelationshipTypes.FindAll(r => r.IsActive),
                        rt => rt.Id, pcrt => pcrt.ChildRelationshipTypeId,
                        (rt, pcrt) => new SelectListItem()
                        {
                            Value = pcrt.Id.ToString(),
                            Selected = pcrt.Id == selectedParentChildRelationshipTypeId,
                            Text = rt.Name
                        })
                    .ToList();
                /*
                // only display linked child relationship types
                return UOW.AssetTypesRelationshipTypes.GetAll()
                    .Where(r => r.IsActive)
                    .Where(r => r.ParentAssetTypeId == suppliedAssetTypeId)
                    .Join(UOW.ParentChildRelationshipTypes.FindAll(r => r.IsActive),
                        atrt => atrt.ParentChildRelationshipTypeId, pcrt => pcrt.Id,
                        (atrt, pcrt) => new ParentChildRelationshipType()
                        {
                            Id = pcrt.Id,
                            ParentRelationshipTypeId = pcrt.ParentRelationshipTypeId,
                            ChildRelationshipTypeId = pcrt.ChildRelationshipTypeId,
                            IsActive = pcrt.IsActive
                        })
                    .ToList()
                    .Join(UOW.RelationshipTypes.FindAll(r => r.IsActive),
                        pcrt => pcrt.ChildRelationshipTypeId, rt => rt.Id,
                        (pcrt, rt) => new SelectListItem()
                        {
                            Value = pcrt.Id.ToString(),
                            Selected = pcrt.Id == selectedParentChildRelationshipTypeId,
                            Text = rt.Name
                        })
                    .ToList();
                */
            }
            else if (selectedRelationshipLevelId == "Child")
            {
                // display list of all parent relationship types
                return UOW.RelationshipTypes.GetAll()
                    .Where(r => r.IsActive)
                    .Join(UOW.ParentChildRelationshipTypes.FindAll(r => r.IsActive),
                        rt => rt.Id, pcrt => pcrt.ParentRelationshipTypeId,
                        (rt, pcrt) => new SelectListItem()
                        {
                            Value = pcrt.Id.ToString(),
                            Selected = pcrt.Id == selectedParentChildRelationshipTypeId,
                            Text = rt.Name
                        })
                    .ToList();
                /*
                // only display linked parent relationship types
                return UOW.AssetTypesRelationshipTypes.GetAll()
                    .Where(r => r.IsActive)
                    .Where(r => r.ChildAssetTypeId == suppliedAssetTypeId)
                    .Join(UOW.ParentChildRelationshipTypes.FindAll(r => r.IsActive),
                        atrt => atrt.ParentChildRelationshipTypeId, pcrt => pcrt.Id,
                        (atrt, pcrt) => new ParentChildRelationshipType()
                        {
                            Id = pcrt.Id,
                            ParentRelationshipTypeId = pcrt.ParentRelationshipTypeId,
                            ChildRelationshipTypeId = pcrt.ChildRelationshipTypeId,
                            IsActive = pcrt.IsActive
                        })
                    .ToList()
                    .Join(UOW.RelationshipTypes.FindAll(r => r.IsActive),
                        pcrt => pcrt.ParentRelationshipTypeId, rt => rt.Id,
                        (pcrt, rt) => new SelectListItem()
                        {
                            Value = pcrt.Id.ToString(),
                            Selected = pcrt.Id == selectedParentChildRelationshipTypeId,
                            Text = rt.Name
                        })
                    .ToList();
                */
            }

            // default empty list
            return new List<SelectListItem>();
        }

        private static List<SelectListItem> GetRelationshipLevels(string selectedValue)
        {
            // create sli
            List<SelectListItem> sliRelationshipLevel = new List<SelectListItem>();

            // transfer values to sli
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

    }
}