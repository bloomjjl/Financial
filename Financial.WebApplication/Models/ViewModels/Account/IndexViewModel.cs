using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;

namespace Financial.WebApplication.Models.ViewModels.Account
{
    public class IndexViewModel
    {
        public IndexViewModel() {}

        public IndexViewModel(Business.Models.Account bmAccount)
        {
            Id = bmAccount.AssetId;
            AssetName = bmAccount.AssetName;
            AssetTypeName = bmAccount.AssetTypeName;
        }


        public int Id { get; set; }
        [Display(Name = "Name")]
        public string AssetName { get; set; }
        [Display(Name = "Type")]
        public string AssetTypeName { get; set; }

    }
}
