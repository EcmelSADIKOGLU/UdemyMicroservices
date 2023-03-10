using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserID { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> basketItems { get; set; }
        public decimal TotalPrice 
        { 
            get => basketItems.Sum(b => b.Price * b.Quantity);
        }
    }
}
