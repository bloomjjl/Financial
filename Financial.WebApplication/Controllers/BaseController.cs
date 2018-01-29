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

        public BaseController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public int GetIntegerFromString(string stringValue)
        {
            int integerValue = 0;
            int.TryParse(stringValue, out integerValue);
            return integerValue;
        }

    }
}