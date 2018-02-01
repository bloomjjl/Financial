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

            // transfer dto to vm for supplied asset type id == child
            var vmIndex = UOW.AssetTypes.FindAll(r => r.IsActive)
                .Join(UOW.AssetTypesRelationshipTypes.FindAll(r => r.IsActive),
                    at => at.Id, atrt => atrt.ParentAssetTypeId,
                    (at, atrt) => new { at, atrt })
                .Where(j => j.atrt.ChildAssetTypeId == dtoSuppliedAssetType.Id)
                .ToList()
                .Join(UOW.ParentChildRelationshipTypes.FindAll(r => r.IsActive),
                    j => j.atrt.ParentChildRelationshipTypeId, pcrt => pcrt.Id,
                    (j, pcrt) => new { j, pcrt })
                .ToList()
                .Join(UOW.RelationshipTypes.FindAll(r => r.IsActive),
                    j2 => j2.pcrt.ChildRelationshipTypeId, rt => rt.Id,
                    (j2, rt) => new IndexViewModel(j2.j.atrt, dtoSuppliedAssetType, j2.j.at, rt))
                .OrderBy(r => r.LinkedAssetTypeName)
                .ToList();

            // transfer dto to vm for supplied asset type id == parent
            var vmIndexParent = UOW.AssetTypes.FindAll(r => r.IsActive)
                .Join(UOW.AssetTypesRelationshipTypes.FindAll(r => r.IsActive),
                    at => at.Id, atrt => atrt.ChildAssetTypeId,
                    (at, atrt) => new { at, atrt })
                .Where(j => j.atrt.ParentAssetTypeId == dtoSuppliedAssetType.Id)
                .ToList()
                .Join(UOW.ParentChildRelationshipTypes.FindAll(r => r.IsActive),
                    j => j.atrt.ParentChildRelationshipTypeId, pcrt => pcrt.Id,
                    (j, pcrt) => new { j, pcrt })
                .ToList()
                .Join(UOW.RelationshipTypes.FindAll(r => r.IsActive),
                    j2 => j2.pcrt.ParentRelationshipTypeId, rt => rt.Id,
                    (j2, rt) => new IndexViewModel(j2.j.atrt, dtoSuppliedAssetType, j2.j.at, rt))
                .OrderBy(r => r.LinkedAssetTypeName)
                .ToList();
            foreach(var vmParent in vmIndexParent)
            {
                vmIndex.Add(vmParent);
            }

            return PartialView("_Index", vmIndex);
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
            List<SelectListItem> sliLinkAssetTypes = GetAssetTypes(assetTypeId, null, null, null);

            // display view
            return View("Create", new CreateViewModel(dtoSuppliedAssetType, sliRelationshipLevels, sliLinkAssetTypes, null, null));
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
            int count = CountExistingLinks(id, vmCreate.SuppliedAssetTypeId, vmCreate.SelectedLinkedAssetTypeId, vmCreate.SelectedParentChildRelationshipTypeId);

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
                    ChildAssetTypeId = GetIntegerFromString(vmCreate.SelectedLinkedAssetTypeId.ToString()),
                    ParentChildRelationshipTypeId = GetIntegerFromString(vmCreate.SelectedParentChildRelationshipTypeId),
                    IsActive = true
                });
            }
            else // supplied AssetType == Child
            {
                UOW.AssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType
                {
                    ParentAssetTypeId = GetIntegerFromString(vmCreate.SelectedLinkedAssetTypeId),
                    ChildAssetTypeId = vmCreate.SuppliedAssetTypeId,
                    ParentChildRelationshipTypeId = GetIntegerFromString(vmCreate.SelectedParentChildRelationshipTypeId),
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
        
        /*
        [HttpGet]
        public ActionResult DisplayLinkAssetTypes(int suppliedAssetTypeId, string selectedRelationshipLevelId, string selectedParentChildRelationshipTypeId, int? selectedAssetTypeId)
        {
            // get filtered list to display
            List<SelectListItem> sliAssetTypes = GetAssetTypes(suppliedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedAssetTypeId);

            // display view
            return PartialView("_DisplayLinkAssetTypes", new DisplayLinkAssetTypesViewModel(sliAssetTypes, selectedAssetTypeId.ToString()));
        }
        */

        [HttpGet]
        public ViewResult Edit(int id, int suppliedAssetTypeId)
        {
            // get dto for supplied id
            var dtoSuppliedAssetType = UOW.AssetTypes.Get(suppliedAssetTypeId);
            var dtoAssetTypeRelationshipType = UOW.AssetTypesRelationshipTypes.Get(id);

            // Selected values
            var selectedRelationshipLevelId = dtoAssetTypeRelationshipType.ParentAssetTypeId == dtoSuppliedAssetType.Id ?
                "Parent" : "Child";
            var selectedParentChildRelationshipTypeId = dtoAssetTypeRelationshipType.ParentChildRelationshipTypeId;
            var selectedLinkedAssetTypeId = dtoAssetTypeRelationshipType.ParentAssetTypeId == dtoSuppliedAssetType.Id ?
                dtoAssetTypeRelationshipType.ChildAssetTypeId : dtoAssetTypeRelationshipType.ParentAssetTypeId;

            // get drop down lists
            List<SelectListItem> sliRelationshipLevels = GetRelationshipLevels(selectedRelationshipLevelId);
            List<SelectListItem> sliLinkAssetTypes = GetAssetTypes(suppliedAssetTypeId, selectedRelationshipLevelId, 
                dtoAssetTypeRelationshipType.ParentChildRelationshipTypeId.ToString(), selectedLinkedAssetTypeId);

            // display view
            return View("Edit", new EditViewModel(dtoAssetTypeRelationshipType, dtoSuppliedAssetType, sliRelationshipLevels, selectedRelationshipLevelId,
                selectedParentChildRelationshipTypeId.ToString(), sliLinkAssetTypes, selectedLinkedAssetTypeId.ToString()));
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
            int count = CountExistingLinks(vmEdit.Id, vmEdit.SuppliedAssetTypeId, vmEdit.SelectedLinkedAssetTypeId, vmEdit.SelectedParentChildRelationshipTypeId);

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
                .Where(r => r.ChildAssetTypeId == GetIntegerFromString(vmEdit.SelectedLinkedAssetTypeId))
                .Where(r => r.ParentChildRelationshipTypeId == GetIntegerFromString(vmEdit.SelectedParentChildRelationshipTypeId))
                .Count(r => r.IsActive);
            var countChildRelationship = UOW.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.Id == vmEdit.Id)
                .Where(r => r.ParentAssetTypeId == GetIntegerFromString(vmEdit.SelectedLinkedAssetTypeId))
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
                    dtoAssetTypeRelationshipType.ChildAssetTypeId = GetIntegerFromString(vmEdit.SelectedLinkedAssetTypeId);
                }
                else
                {
                    dtoAssetTypeRelationshipType.ParentAssetTypeId = GetIntegerFromString(vmEdit.SelectedLinkedAssetTypeId);
                    dtoAssetTypeRelationshipType.ChildAssetTypeId = vmEdit.SuppliedAssetTypeId;
                }

                // update db
                UOW.CommitTrans();
            }

            // display view with message
            TempData["SuccessMessage"] = "Parent-Child link updated.";
            return RedirectToAction("Details", "AssetType", new { id = vmEdit.SuppliedAssetTypeId });
        }

        [HttpGet]
        public ViewResult Delete(int id, int suppliedAssetTypeId)
        {
            // transfer values to dto
            var dtoAssetTypeRelationshipType = UOW.AssetTypesRelationshipTypes.Get(id);
            var dtoSuppliedAssetType = UOW.AssetTypes.Get(suppliedAssetTypeId);
            var dtoParentChildRelationshipType = UOW.ParentChildRelationshipTypes.Get(dtoAssetTypeRelationshipType.ParentChildRelationshipTypeId);
            var dtoLinkedAssetType = new AssetType();
            var dtoRelationshipType = new RelationshipType();

            // transfer Parent or Child info to dto
            if(dtoAssetTypeRelationshipType.ParentAssetTypeId == dtoSuppliedAssetType.Id)
            {
                dtoLinkedAssetType = UOW.AssetTypes.Get(dtoAssetTypeRelationshipType.ChildAssetTypeId);
                dtoRelationshipType = UOW.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
            }
            else if (dtoAssetTypeRelationshipType.ChildAssetTypeId == dtoSuppliedAssetType.Id)
            {
                dtoLinkedAssetType = UOW.AssetTypes.Get(dtoAssetTypeRelationshipType.ParentAssetTypeId);
                dtoRelationshipType = UOW.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
            }

            // display view
            return View("Delete", new DeleteViewModel(dtoAssetTypeRelationshipType, dtoSuppliedAssetType, dtoLinkedAssetType, dtoRelationshipType));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel vmDelete)
        {
            // transfer vm to dto
            var dtoAssetTypeRelationshipType = UOW.AssetTypesRelationshipTypes.Get(vmDelete.Id);
            dtoAssetTypeRelationshipType.IsActive = false;

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Relationship deleted.";
            return RedirectToAction("Details", "AssetType", new { id = vmDelete.SuppliedAssetTypeId });
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
                        rt => rt.Id, pcrt => pcrt.ParentRelationshipTypeId,
                        (rt, pcrt) => new SelectListItem()
                        {
                            Value = pcrt.Id.ToString(),
                            Selected = pcrt.Id == selectedParentChildRelationshipTypeId,
                            Text = rt.Name
                        })
                    .ToList();
            }
            else if (selectedRelationshipLevelId == "Child")
            {
                // display list of all parent relationship types
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