using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.ServiceInterfaces
{
    public interface ISelectListService
    {
        List<SelectListItem> TransactionCategories(string selectedId);
        List<SelectListItem> TransactionDescriptions(string selectedId);
        List<SelectListItem> TransactionTypes(string selectedId);
    }
}
