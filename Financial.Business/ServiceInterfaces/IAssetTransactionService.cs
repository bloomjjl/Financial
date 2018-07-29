using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAssetTransactionService
    {
        List<Business.Models.AssetTransaction> GetListOfActiveTransactions();
        Business.Models.AssetTransaction GetTransactionOptions(int? assetId);
        bool AddTransaction(Business.Models.AssetTransaction bmAssetTransaction);
        Business.Models.AssetTransaction GetTransactionToEdit(int assetTransactionId);
        bool UpdateTransaction(Business.Models.AssetTransaction bmAssetTransaction);
        Business.Models.AssetTransaction GetTransactionToDelete(int assetTransactionId);
        bool DeleteTransaction(int assetTransactionId);
    }
}
