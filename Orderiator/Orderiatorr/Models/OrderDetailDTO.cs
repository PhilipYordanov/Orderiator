namespace Orderiator.Web.Models
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }
    }
}
