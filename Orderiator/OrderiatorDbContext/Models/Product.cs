namespace Orderiator.DataModel.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public bool Discontinued { get; set; }

        public short UnitsInStock { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
