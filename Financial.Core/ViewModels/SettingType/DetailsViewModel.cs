﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.SettingType
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {

        }

        public DetailsViewModel(Models.SettingType dtoSettingType)
        {
            Id = dtoSettingType.Id;
            Name = dtoSettingType.Name;
            IsActive = dtoSettingType.IsActive;
        }


        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

    }
}
