namespace Models.ViewModels
{
    public class OrderItemDTO
    {
        public OrderItemDTO()
        {

        }

        public OrderItemDTO(string productCode, string productName, int productQuantity, decimal productUnitPrice)
        {
            ProductCode = productCode;
            ProductName = productName;
            ProductQuantity = productQuantity;
            ProductUnitPrice = productUnitPrice;
        }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public decimal Subtotal => ProductQuantity * ProductUnitPrice;
    }
}
