using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.AssetSetting
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {

        }

        public CreateViewModel(Core.Models.Asset dtoAsset, Core.Models.SettingType dtoSettingType)
        {
            AssetId = dtoAsset.Id;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
        }


        public int AssetId { get; set; }
        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }
        public string Value { get; set; }
    }
}
