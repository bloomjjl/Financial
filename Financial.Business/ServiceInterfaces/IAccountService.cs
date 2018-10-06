using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Financial.Business.Models;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAccountService
    {
        List<Account> GetListOfAccounts();
        List<SelectListItem> GetSelectListOfAccounts(int? selectedId);

    }
}
