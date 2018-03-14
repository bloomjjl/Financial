using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.TransactionCategory
{
    public class IndexViewModel
    {
        public IndexViewModel() { }
        public IndexViewModel(Models.TransactionCategory dtoTransactionCategory)
        {
            Id = dtoTransactionCategory.Id;
            Name = dtoTransactionCategory.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
