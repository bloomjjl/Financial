using Financial.Core;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetSettingController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AssetSettingController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetSettingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
    }
}