using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Date = dtoAssetTransaction.TransactionDate.ToString("MM/dd/yyyy");
            Amount = dtoAssetTransaction.Amount;
        }


        public int Id { get; set; }
        public string Date { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Amount { get; set; }
    }
}
