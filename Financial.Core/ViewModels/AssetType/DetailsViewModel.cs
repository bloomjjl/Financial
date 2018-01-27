using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetType
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
        }

        public DetailsViewModel(Models.AssetType dtoAssetType)
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

