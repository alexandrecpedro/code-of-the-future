using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class OrderItem : BaseModel
    {   
        [Required]
        [DataMember]
        public Order Order { get; private set; }
        [Required]
        [DataMember]
        public Product Product { get; private set; }
        [Required]
        [DataMember]
        public int Quantity { get; private set; }
        [Required]
        [DataMember]
        public decimal UnitPrice { get; private set; }
        [DataMember]
        public decimal Subtotal => Quantity * UnitPrice;

        public OrderItem()
        {

        }

        public OrderItem(Order order, Product product, int quantity, decimal unitPrice)
        {
            Order = order;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
