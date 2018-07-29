using Financial.Business.ServiceInterfaces;
using Financial.Core.Models;
using Financial.Data;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;
using System.Web.Mvc;

namespace Financial.WebApplication.Tests.Fakes.Services
{
    public class FakeAssetTransactionService : IAssetTransactionService
    {
        private IUnitOfWork _unitOfWork;

        public FakeAssetTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AddTransaction(Business.Models.AssetTransaction bmAssetTransaction)
        {
            if(bmAssetTransaction.AssetId == 0 ||
                bmAssetTransaction.TransactionTypeId == 0 || 
                bmAssetTransaction.TransactionCategoryId == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteTransaction(int assetTransactionId)
        {
            throw new NotImplementedException();
        }

        public List<Business.Models.AssetTransaction> GetListOfActiveTransactions()
        {
            var bmAssetTransactionList = new List<Business.Models.AssetTransaction>();

            for(int i = 0; i < 6; i++)
            {
                bmAssetTransactionList.Add( new Business.Models.AssetTransaction()
                {
                    AssetId = i,
                    AssetSelectList = new List<SelectListItem>(),
                    TransactionTypeSelectList = new List<SelectListItem>(),
                    TransactionCategorySelectList = new List<SelectListItem>()
                });
            }

            return bmAssetTransactionList;
        }

        public Business.Models.AssetTransaction GetTransactionOptions(int? assetId)
        {
            if(assetId == null)
            {
                return null;
            }
            var dtoAsset = _unitOfWork.Assets.Get((int)assetId);
            if(dtoAsset == null)
            {
                return null;
            }
            return new Business.Models.AssetTransaction()
            {
                AssetId = (int)assetId,
                AssetSelectList = new List<SelectListItem>(),
                TransactionTypeSelectList = new List<SelectListItem>(),
                TransactionCategorySelectList = new List<SelectListItem>()
            };
        }

        public Business.Models.AssetTransaction GetTransactionToDelete(int assetTransactionId)
        {
            throw new NotImplementedException();
        }

        public Business.Models.AssetTransaction GetTransactionToEdit(int assetTransactionId)
        {
            var dtoAssetTransaction = _unitOfWork.AssetTransactions.Get(assetTransactionId);
            if (dtoAssetTransaction == null)
            {
                return null;
            }

            return new Business.Models.AssetTransaction()
            {
                AssetTransactionId = dtoAssetTransaction.Id,
                AssetId = dtoAssetTransaction.AssetId,
                AssetSelectList = new List<SelectListItem>(),
                TransactionTypeSelectList = new List<SelectListItem>(),
                TransactionCategorySelectList = new List<SelectListItem>()
            };
        }

        public bool UpdateTransaction(Business.Models.AssetTransaction bmAssetTransaction)
        {
            throw new NotImplementedException();
        }
    }
}
