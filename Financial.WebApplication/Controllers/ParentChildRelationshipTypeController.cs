﻿using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.ParentChildRelationshipType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financial.Business;

namespace Financial.WebApplication.Controllers
{
    public class ParentChildRelationshipTypeController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IBusinessService _businessService;


        public ParentChildRelationshipTypeController(IUnitOfWork unitOfWork, IBusinessService businessService)
            : base()
        {
            _unitOfWork = unitOfWork;
            _businessService = businessService;
        }

        [ChildActionOnly]
        public ActionResult Index(int relationshipTypeId)
        {
            //transfer dto for id
            var dtoSuppliedRelationshipType = _unitOfWork.RelationshipTypes.Get(relationshipTypeId);

            // transfer db for supplied ParentId
            var dbParentRelationshipTypes = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ParentRelationshipTypeId == relationshipTypeId)
                .ToList();
            
            // transfer db for supplied ChildId
            var dbChildRelationshipTypes = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ChildRelationshipTypeId == relationshipTypeId)
                .ToList();

            // tranfer dbParent to vm
            var vmIndex = new List<IndexViewModel>();
            foreach(var dtoParentChildRelationshipType in dbParentRelationshipTypes)
            {
                var dtoParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
                var dtoChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
                vmIndex.Add(new IndexViewModel(dtoParentChildRelationshipType, dtoSuppliedRelationshipType, dtoParentRelationshipType, dtoChildRelationshipType));
            }

            // tranfer dbChild to vm
            foreach (var dtoParentChildRelationshipType in dbChildRelationshipTypes)
            {
                var dtoParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
                var dtoChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
                vmIndex.Add(new IndexViewModel(dtoParentChildRelationshipType, dtoSuppliedRelationshipType, dtoParentRelationshipType, dtoChildRelationshipType));
            }

            // display view
            return PartialView("_Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create(int relationshipTypeId)
        {
            // transfer dto for id
            var dtoSuppliedRelationshipType = _unitOfWork.RelationshipTypes.Get(relationshipTypeId);

            // transfer db to sli
            List<SelectListItem> sliRelationshipTypes = GetDropDownListForRelationshipTypes(relationshipTypeId, null);

            // transfer levels to sli
            List<SelectListItem> sliRelationshipLevels = GetDropDownListForRelationshipLevels(null);

            // display view
            return View("Create", new CreateViewModel(dtoSuppliedRelationshipType, sliRelationshipLevels, sliRelationshipTypes));
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // validation
            if(!ModelState.IsValid)
            {
                return View("Create", vmCreate);
            }

            // link duplicated?
            var countExistingParentLinks = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.ParentRelationshipTypeId == vmCreate.SuppliedRelationshipTypeId)
                .Where(r => r.ChildRelationshipTypeId == Business.Utilities.DataTypeUtility.GetIntegerFromString(vmCreate.SelectedLinkedRelationshipType))
                .Count(r => r.IsActive);
            var countExistingChildLinks = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.ChildRelationshipTypeId == vmCreate.SuppliedRelationshipTypeId)
                .Where(r => r.ParentRelationshipTypeId == Business.Utilities.DataTypeUtility.GetIntegerFromString(vmCreate.SelectedLinkedRelationshipType))
                .Count(r => r.IsActive);
            if (countExistingParentLinks > 0 || countExistingChildLinks > 0)
            {
                // update Drop Down Lists for vm
                vmCreate.LinkedRelationshipTypes = GetDropDownListForRelationshipTypes(vmCreate.SuppliedRelationshipTypeId, null);
                vmCreate.RelationshipLevels = GetDropDownListForRelationshipLevels(null);

                // redisplay view
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Create", vmCreate);
            }

            // determine relationship level
            int parentRelationshipType = 0;
            int childRelationshipType = 0;
            if(vmCreate.SelectedRelationshipLevel == "Parent-Child")
            {
                parentRelationshipType = vmCreate.SuppliedRelationshipTypeId;
                childRelationshipType = Business.Utilities.DataTypeUtility.GetIntegerFromString(vmCreate.SelectedLinkedRelationshipType);
            }
            else // Child-Parent
            { 
                parentRelationshipType = Business.Utilities.DataTypeUtility.GetIntegerFromString(vmCreate.SelectedLinkedRelationshipType);
                childRelationshipType = vmCreate.SuppliedRelationshipTypeId;
            }

            // transfer vm to dto
            _unitOfWork.ParentChildRelationshipTypes.Add(new ParentChildRelationshipType()
            {
                ParentRelationshipTypeId = parentRelationshipType,
                ChildRelationshipTypeId = childRelationshipType,
                IsActive = true
            });

            // update db
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Details", "RelationshipType", new { id = vmCreate.SuppliedRelationshipTypeId });
        }

        [HttpGet]
        public ViewResult Edit(int id, int relationshipTypeId)
        {
            // transfer dto for id
            var dtoSuppliedParentChildRelationshipType = _unitOfWork.ParentChildRelationshipTypes.Get(id);
            var dtoSuppliedRelationshipType = _unitOfWork.RelationshipTypes.Get(relationshipTypeId);

            // transfer levels to sli
            var selectedRelationshipLevelId = dtoSuppliedParentChildRelationshipType.ParentRelationshipTypeId == relationshipTypeId ?
                "Parent-Child" : "Child-Parent";
            List<SelectListItem> sliRelationshipLevels = GetDropDownListForRelationshipLevels(selectedRelationshipLevelId);

            // transfer db to sli
            var selectedRelationshipTypeId = dtoSuppliedParentChildRelationshipType.ParentRelationshipTypeId == relationshipTypeId ? 
                dtoSuppliedParentChildRelationshipType.ChildRelationshipTypeId : 
                dtoSuppliedParentChildRelationshipType.ParentRelationshipTypeId;
            List<SelectListItem> sliRelationshipTypes = GetDropDownListForRelationshipTypes(relationshipTypeId, selectedRelationshipTypeId);

            // display view
            return View("Edit", new EditViewModel(dtoSuppliedParentChildRelationshipType, dtoSuppliedRelationshipType, sliRelationshipLevels, selectedRelationshipLevelId, sliRelationshipTypes, selectedRelationshipTypeId));
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            if(!ModelState.IsValid)
            {
                return View("Edit", vmEdit);
            }

            // duplicated relationship?
            var countExistingParentChildRelationship = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.Id != vmEdit.Id)
                .Where(r => r.ParentRelationshipTypeId == vmEdit.RelationshipTypeId)
                .Where(r => r.ChildRelationshipTypeId == Business.Utilities.DataTypeUtility.GetIntegerFromString(vmEdit.SelectedRelationshipType))
                .Count(r => r.IsActive);
            var countExistingChildParentRelationship = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                .Where(r => r.Id != vmEdit.Id)
                .Where(r => r.ChildRelationshipTypeId == vmEdit.RelationshipTypeId)
                .Where(r => r.ParentRelationshipTypeId == Business.Utilities.DataTypeUtility.GetIntegerFromString(vmEdit.SelectedRelationshipType))
                .Count(r => r.IsActive);

            if (countExistingParentChildRelationship > 0 || countExistingChildParentRelationship > 0)
            {
                // update Drop Down Lists for vm
                vmEdit.RelationshipTypes = GetDropDownListForRelationshipTypes(vmEdit.RelationshipTypeId, Business.Utilities.DataTypeUtility.GetIntegerFromString(vmEdit.SelectedRelationshipType));
                vmEdit.RelationshipLevels = GetDropDownListForRelationshipLevels(vmEdit.SelectedRelationshipLevel);

                // redisplay view
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Edit", vmEdit);
            }

            // transfer vm to dto
            var dtoParentChildRelationshipType = _unitOfWork.ParentChildRelationshipTypes.Get(vmEdit.Id);
            if(vmEdit.SelectedRelationshipLevel == "Parent-Child")
            {
                dtoParentChildRelationshipType.ParentRelationshipTypeId = vmEdit.RelationshipTypeId;
                dtoParentChildRelationshipType.ChildRelationshipTypeId = Business.Utilities.DataTypeUtility.GetIntegerFromString(vmEdit.SelectedRelationshipType);
            }
            else // Child-Parent
            {
                dtoParentChildRelationshipType.ParentRelationshipTypeId = Business.Utilities.DataTypeUtility.GetIntegerFromString(vmEdit.SelectedRelationshipType);
                dtoParentChildRelationshipType.ChildRelationshipTypeId = vmEdit.RelationshipTypeId; 
            }

            // update db
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Details", "RelationshipType", new { id = vmEdit.RelationshipTypeId });
        }

        [HttpGet]
        public ViewResult Delete(int id, int relationshipTypeId)
        {
            // transfer values to dto
            var dtoParentChildRelationshipType = _unitOfWork.ParentChildRelationshipTypes.Get(id);
            var dtoRelationshipType = _unitOfWork.RelationshipTypes.Get(relationshipTypeId);
            var dtoParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ParentRelationshipTypeId);
            var dtoChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentChildRelationshipType.ChildRelationshipTypeId);
            
            // display view
            return View("Delete", new DeleteViewModel(dtoParentChildRelationshipType, dtoRelationshipType, dtoParentRelationshipType, dtoChildRelationshipType));
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel vmDelete)
        {
            if(!ModelState.IsValid)
            {
                return View("Delete", vmDelete);
            }

            // update dto
            var dtoParentChildRelationshipType = _unitOfWork.ParentChildRelationshipTypes.Get(vmDelete.Id);
            dtoParentChildRelationshipType.IsActive = false;

            // update db
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Details", "RelationshipType", new { id = vmDelete.RelationshipTypeId });
        }

        private List<SelectListItem> GetDropDownListForRelationshipTypes(int relationshipTypeId, int? selectedId)
        {
            // validate selectedId
            var intSelectedId = 0;
            int.TryParse(selectedId.ToString(), out intSelectedId);

            // transfer db
            var dbRelationshipTypes = _unitOfWork.RelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.Id != relationshipTypeId)
                .ToList();

            // transfer db to sli
            var sliRelationshipTypes = new List<SelectListItem>();
            foreach (var dtoRelationshipType in dbRelationshipTypes)
            {
                var countLinkedParentRelationships = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                    .Where(r => r.ParentRelationshipTypeId == relationshipTypeId)
                    .Where(r => r.ChildRelationshipTypeId == dtoRelationshipType.Id)
                    .Where(r => r.ParentRelationshipTypeId != selectedId)
                    .Where(r => r.ChildRelationshipTypeId != selectedId)
                    .Count(r => r.IsActive);
                var countLinkedChildRelationships = _unitOfWork.ParentChildRelationshipTypes.GetAll()
                    .Where(r => r.ChildRelationshipTypeId == relationshipTypeId)
                    .Where(r => r.ParentRelationshipTypeId == dtoRelationshipType.Id)
                    .Where(r => r.ChildRelationshipTypeId != selectedId)
                    .Where(r => r.ParentRelationshipTypeId != selectedId)
                    .Count(r => r.IsActive);

                // add if existing link not found
                if (countLinkedParentRelationships == 0 && countLinkedChildRelationships == 0)
                {
                    sliRelationshipTypes.Add(new SelectListItem()
                    {
                        Value = dtoRelationshipType.Id.ToString(),
                        Selected = dtoRelationshipType.Id == intSelectedId,
                        Text = dtoRelationshipType.Name
                    });
                }
            }

            return sliRelationshipTypes;
        }

        private static List<SelectListItem> GetDropDownListForRelationshipLevels(string selectedValue)
        {
            var sliRelationshipLevels = new List<SelectListItem>();
            sliRelationshipLevels.Add(new SelectListItem()
            {
                Value = "Parent-Child",
                Selected = "Parent-Child" == selectedValue,
                Text = "Parent-Child"
            });
            sliRelationshipLevels.Add(new SelectListItem()
            {
                Value = "Child-Parent",
                Selected = "Child-Parent" == selectedValue,
                Text = "Child-Parent"
            });
            return sliRelationshipLevels;
        }
    }
}