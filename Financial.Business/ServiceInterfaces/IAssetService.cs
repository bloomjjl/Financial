using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Financial.Business.Models;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAssetService
    {
        List<SelectListItem> GetSelectListOfAssets(string selectedId);
    }
}
