using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.Account
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
        }

        public CreateViewModel(List<SelectListItem> sliAssetTypes)
        {
            AssetTypes = sliAssetTypes;
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string AssetName { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string SelectedAssetTypeId { get; set; }
        public IEnumerable<SelectListItem> AssetTypes { get; set; }
    }
}
