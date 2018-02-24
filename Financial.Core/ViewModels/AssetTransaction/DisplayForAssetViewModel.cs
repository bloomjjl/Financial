using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTransaction
{
    public class DisplayForAssetViewModel
    {
        public DisplayForAssetViewModel() { }

        public DisplayForAssetViewModel(Models.AssetTransaction dtoAssetTransaction)
        {
            Id = dtoAssetTransaction.Id;
            DueDate = dtoAssetTransaction.DueDate.ToString("MM/dd/yyyy");
            ClearDate = dtoAssetTransaction.ClearDate.ToString("MM/dd/yyyy");
            Amount = dtoAssetTransaction.Amount;
            Note = dtoAssetTransaction.Note;
        }


        public int Id { get; set; }
        [Display(Name = "Due")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string DueDate { get; set; }
        [Display(Name = "Cleared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string ClearDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}
