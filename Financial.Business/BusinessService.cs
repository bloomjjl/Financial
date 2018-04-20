using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business
{
    public class BusinessService : IBusinessService
    {
        private IUnitOfWork _unitOfWork;

        public BusinessService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public BusinessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
