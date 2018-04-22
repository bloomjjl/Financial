using Financial.Business;
using Financial.Core;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class BaseController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IBusinessService _businessService;

        public BaseController()
        {
            _unitOfWork = new UnitOfWork();
            _businessService = new BusinessService(_unitOfWork);
        }

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _businessService = new BusinessService(_unitOfWork);
        }

        public IUnitOfWork UOW
        {
            get
            {
                return _unitOfWork;
            }
            set
            {
                _unitOfWork = value;
            }
        }

        public IBusinessService BS
        {
            get
            {
                return _businessService;
            }
            set
            {
                _businessService = value;
            }
        }

    }
}