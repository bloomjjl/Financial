using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetSetting
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {

        }

        public int Id { get; set; }
        public int AssetId { get; set; }
        public int SettingTypeId { get; set; }
        public string Value { get; set; }
    }
}
