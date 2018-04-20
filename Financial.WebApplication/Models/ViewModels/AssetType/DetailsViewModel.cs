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

        public DetailsViewModel(Core.Models.AssetType dtoAssetType)
        {
            Id = dtoAssetType.Id;
            Name = dtoAssetType.Name;
            IsActive = dtoAssetType.IsActive;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}

