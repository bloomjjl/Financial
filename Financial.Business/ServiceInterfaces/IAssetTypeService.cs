using Financial.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAssetTypeService
    {
        List<Business.Models.AssetType> IndexGetModelList();
        int CreatePostUpdateDatabase(AssetType bmAssetType);
        Business.Models.AssetType EditGetModel(int assetTypeId);
        string EditPostUpdateDatabase(Business.Models.AssetType bmAssetType);
        Business.Models.AssetType DetailsGetModel(int assetTypeId);



        List<SelectListItem> GetAssetTypesDropDownList(int? selectedId);

        

    }
}
