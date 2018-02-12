using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTransaction
{
    public class CreateViewModel
    {
        public CreateViewModel() { }

        public CreateViewModel(Models.Asset dtoAsset, Models.AssetType dtoAssetType, 
            List<SelectListItem> sliTransactionTypes, List<SelectListItem> sliTransactionCategories, List<SelectListItem> sliTransactionDescriptions)
        {
            Id = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
            Date = DateTime.Now;
            TransactionTypes = sliTransactionTypes;
            TransactionCategories = sliTransactionCategories;
            TransactionDescriptions = sliTransactionDescriptions;
        }

        public int Id { get; set; }
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }
        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string SelectedTransactionTypeId { get; set; }
        public IEnumerable<SelectListItem> TransactionTypes { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string SelectedTransactionCategoryId { get; set; }
        public IEnumerable<SelectListItem> TransactionCategories { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string SelectedTransactionDescriptionId { get; set; }
        public IEnumerable<SelectListItem> TransactionDescriptions { get; set; }
    }
}
