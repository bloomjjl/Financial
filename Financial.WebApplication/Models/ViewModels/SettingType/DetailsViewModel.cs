using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.SettingType
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {

        }

        public DetailsViewModel(Business.Models.SettingType bmSettingType)
        {
            Id = bmSettingType.SettingTypeId;
            Name = bmSettingType.SettingTypeName;
            //IsActive = bmSettingType.IsActive;
        }


        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

    }
}
