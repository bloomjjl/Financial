using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTransaction
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Models.AssetTransaction dtoAssetTransaction)
        {
            Id = dtoAssetTransaction.Id;
            Date = dtoAssetTransaction.TransactionDate;
            Amount = dtoAssetTransaction.Amount;
        }


        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
