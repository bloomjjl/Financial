using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.TransactionCategory
{
    public class IndexViewModel
    {
        public IndexViewModel() { }
        public IndexViewModel(Core.Models.TransactionCategory dtoTransactionCategory)
        {
            Id = dtoTransactionCategory.Id;
            Name = dtoTransactionCategory.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
