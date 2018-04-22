﻿using Financial.Business.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class CreateLinkedSettingTypesViewModel
    {
        public CreateLinkedSettingTypesViewModel()
        {

        }

        public CreateLinkedSettingTypesViewModel(Core.Models.AssetType dtoAssetType, 
            List<LinkedAssetTypeSettingType> atstLinks)
        {
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            LinkedAssetTypeSettingTypes = atstLinks; 
        }

        public int AssetTypeId { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }

        public List<LinkedAssetTypeSettingType> LinkedAssetTypeSettingTypes { get; set; }
    }
}
