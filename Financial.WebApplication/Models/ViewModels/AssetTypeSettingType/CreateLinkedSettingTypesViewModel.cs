﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class CreateLinkedSettingTypesViewModel
    {
        public CreateLinkedSettingTypesViewModel()
        {

        }
        public CreateLinkedSettingTypesViewModel(Business.Models.AccountType bmAssetType,
            List<Business.Models.AttributeType> bmSettingTypes)
        {
            AssetTypeId = bmAssetType.AssetTypeId;
            AssetTypeName = bmAssetType.AssetTypeName;
            SettingTypes = bmSettingTypes;
        }

        public int AssetTypeId { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }

        public List<Business.Models.AccountTypeSettingType> LinkedAssetTypeSettingTypes { get; set; }
        public List<Business.Models.AttributeType> SettingTypes { get; set; }
    }
}
