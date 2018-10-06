using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("TransactionTypes")]
    public class TransactionType : BaseEntity
    {
        [Required]
        public string Name { get; set; }




        public ICollection<AssetTransaction> AssetTransactions { get; set; }



        public static readonly int IdForExpense = 1;
        public static readonly int IdForIncome = 2;
    }
}
