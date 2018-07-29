using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetType
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
        }
        public DetailsViewModel(Business.Models.AssetType bmAssetType)
        {
            Id = bmAssetType.AssetTypeId;
            Name = bmAssetType.AssetTypeName;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}

