using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.TransactionCategory
{
    public class EditViewModel
    {
        public EditViewModel() { }
        public EditViewModel(Core.Models.TransactionCategory dtoTransactionCategory)
        {
            Id = dtoTransactionCategory.Id;
            Name = dtoTransactionCategory.Name;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
