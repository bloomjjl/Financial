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
    public class RelationshipTypeController : BaseController
    {
        public RelationshipTypeController()
            : base()
        {
        }

        public RelationshipTypeController(IUnitOfWork unitOfWork)
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
            var vmIndex = UOW.RelationshipTypes.GetAll()
                .Select(r => new IndexViewModel(r))
                .ToList();

            // display view
            return View("Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create()
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            // display view
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // validation
            if(!ModelState.IsValid)
            {
                return View("Create", vmCreate);
            }

            // check for duplicate
            var count = UOW.RelationshipTypes.GetAll()
                .Count(r => r.Name == vmCreate.Name);
            if (count > 0)
            {
                // display view with message
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Create", vmCreate);
            }

            // transfer vm to dto
            UOW.RelationshipTypes.Add(new RelationshipType()
            {
                Name = vmCreate.Name,
                IsActive = true
            });

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record created";
            return RedirectToAction("Index", "RelationshipType");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            // transfer dto to vm
            var vmEdit = UOW.RelationshipTypes.GetAll()
                .Select(r => new EditViewModel(r))
                .FirstOrDefault(r => r.Id == id);

            // display view
            return View("Edit", vmEdit);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // validation
            if(!ModelState.IsValid)
            {
                return View("Edit", vmEdit);
            }

            // check for duplicate
            var count = UOW.RelationshipTypes.GetAll()
                .Where(r => r.Name == vmEdit.Name)
                .Count(r => r.Id != vmEdit.Id);
            if(count > 0)
            {
                // display view with message
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Edit", vmEdit);
            }

            // transfer vm to dto
            var dtoRelationshipType = UOW.RelationshipTypes.Get(vmEdit.Id);
            dtoRelationshipType.Name = vmEdit.Name;
            dtoRelationshipType.IsActive = vmEdit.IsActive;

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record updated";
            return RedirectToAction("Index", "RelationshipType");
        }

        [HttpGet]
        public ViewResult Details(int id)
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
            var vmDetails = UOW.RelationshipTypes.GetAll()
                .Select(r => new DetailsViewModel(r))
                .FirstOrDefault(r => r.Id == id);

            // display view
            return View("Details", vmDetails);
        }
    }
}