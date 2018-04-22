using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAssetService : IDisposable
    {
        List<SelectListItem> GetSelectListOfAssets(string selectedId);
    }
}
