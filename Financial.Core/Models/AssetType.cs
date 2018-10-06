using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.Models
{
    [Table("AccountTypes")]
    public class AssetType : BaseEntity
    {
        [Required]
        public string Name { get; set; }



        //public ICollection<Asset> Assets { get; set; }


        public static readonly int IdForCreditCard = 3;
    }
}
