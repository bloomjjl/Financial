using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.SettingType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class SettingTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public SettingTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public SettingTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ViewResult Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"].ToString();
            }

            try
            {
                var vmIndex = _unitOfWork.SettingTypes.GetAll()
                    .Select(r => new IndexViewModel(r))
                    .OrderBy(r => r.Name)
                    .ToList();
                /*
                // get records from db
                List<SettingType> dbSettingTypes = _unitOfWork.SettingTypes.GetAll().OrderBy(r => r.Name).ToList();

                // transfer dto to vm
                foreach (var dtoSettingType in dbSettingTypes)
                {
                    vmIndex.Add(new IndexViewModel(dtoSettingType));
                }
                */
                return View("Index", vmIndex);
            }
            catch (Exception)
            {
                //ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
                return View("Index");
            }
        }
    }
}