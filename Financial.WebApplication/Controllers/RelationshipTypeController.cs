using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.RelationshipType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class RelationshipTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public RelationshipTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public RelationshipTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ViewResult Index()
        {
            // Transfer TempData messages to ViewData
            if(TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            // transfer db to vm
            var vmIndex = _unitOfWork.RelationshipTypes.GetAll()
                .Select(r => new IndexViewModel(r))
                .ToList();

            // display view
            return View("Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create()
        {
            // display view
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Encountered a problem. Try again.";
                return RedirectToAction("Index", "RelationshipType");
            }

            // check for duplicate
            var existingCount = _unitOfWork.RelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Count(r => r.Name == vmCreate.Name);
            if (existingCount > 0)
            {
                // display view
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Create", vmCreate);
            }

            // transfer vm to dto
            _unitOfWork.RelationshipTypes.Add(new RelationshipType()
            {
                Name = vmCreate.Name,
                IsActive = true
            });

            // update db
            _unitOfWork.CommitTrans();

            // display view
            TempData["SuccessMessage"] = "Record created";
            return RedirectToAction("Index", "RelationshipType");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            // transfer dto to vm
            var dtoRelationshipType = _unitOfWork.RelationshipTypes.Get(id);

            // display view
            return View("Edit", new EditViewModel(dtoRelationshipType));
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Encountered a problem. Try again.";
                return RedirectToAction("Index", "RelationshipType");
            }

            // check for duplicate
            var existingCount = _unitOfWork.RelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.Name == vmEdit.Name)
                .Count(r => r.Id != vmEdit.Id);
            if(existingCount > 0)
            {
                // display view
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Edit", vmEdit);
            }

            // transfer vm to dto
            var dtoRelationshipType = _unitOfWork.RelationshipTypes.Get(vmEdit.Id);
            dtoRelationshipType.Name = vmEdit.Name;
            dtoRelationshipType.IsActive = vmEdit.IsActive;

            // update db
            _unitOfWork.CommitTrans();

            // display view
            TempData["SuccessMessage"] = "Record updated";
            return RedirectToAction("Index", "RelationshipType");
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            // Transfer TempData messages to ViewData
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            // get dto for id
            var dtoRelationshipType = _unitOfWork.RelationshipTypes.Get(id);

            // display view
            return View("Details", new DetailsViewModel(dtoRelationshipType));
        }
    }
}