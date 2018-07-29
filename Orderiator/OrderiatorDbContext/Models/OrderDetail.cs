
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orderiator.DataModel.Models
{
    [Table("Order Details")]
    public class OrderDetail
    {
       [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }
    }
}
