using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order:Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }

        //owned entity type
        //eğer müdahalede bulunmazska ef core adress içerisindeki her prop için order tablosunda de bir sutun oluşturur. 
        //ama biz address adında yeni bir tablo oluşturup ikisini maplemek istiyoruz. O yüzden owned kullanıyoruz.
        public Address Address { get; private set; }

        public string BuyerId { get; private set; }

        //backing fields

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {

        }
        public Order(Address address, string buyerId)
        {
            Address = address;
            BuyerId = buyerId;
            _orderItems= new List<OrderItem>();
            CreatedDate = DateTime.Now;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x=> x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);  
    }
}
