using Financial.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAccountTypeService
    {
        List<Business.Models.AccountType> IndexGetModelList();
        int CreatePostUpdateDatabase(AccountType bmAssetType);
        Business.Models.AccountType EditGetModel(int assetTypeId);
        string EditPostUpdateDatabase(Business.Models.AccountType bmAssetType);
        Business.Models.AccountType DetailsGetModel(int assetTypeId);



        List<SelectListItem> GetAssetTypesDropDownList(int? selectedId);

        

    }
}
