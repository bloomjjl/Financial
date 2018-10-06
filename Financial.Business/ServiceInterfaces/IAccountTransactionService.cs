using Financial.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAccountTransactionService
    {
        List<AccountTransaction> GetListOfActiveTransactions();
        Account GetAccountForTransaction(int? assetId);
        bool AddTransaction(AccountTransaction bmAssetTransaction);
        AccountTransaction GetTransactionToEdit(int assetTransactionId);
        bool UpdateTransaction(AccountTransaction bmAssetTransaction);
        AccountTransaction GetTransactionToDelete(int assetTransactionId);
        bool DeleteTransaction(int assetTransactionId);

        List<SelectListItem> GetAccountSelectList(string selectedId);
        List<SelectListItem> GetTransactionTypeSelectList(string selectedId);
        List<SelectListItem> GetTransactionCategorySelectList(string selectedId);
    }
}
