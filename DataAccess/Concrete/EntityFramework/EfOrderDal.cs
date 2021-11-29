using DataAccess.Abstract;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, CaseStudyContext>, IOrderDal
    {
        public List<OrderProductDto> GetProductsOfOrder(int orderId)
        {
            using (var context = new CaseStudyContext())
            {
                var result = from orderProduct in context.OrdersProducts
                             join product in context.Products
                             on orderProduct.ProductId equals product.Id
                             where orderProduct.OrderId == orderId
                             select new OrderProductDto { Product = product, OrderAmount = orderProduct.OrderAmount, Price = orderProduct.Price };
                return result.ToList();
            }
        }

        public List<OrderForListDto> GetAllOrderForListDto()
        {
            using (var context = new CaseStudyContext())
            {
                var result = from order in context.Orders
                             join customer in context.Customers
                             on order.CustomerId equals customer.Id
                             select new OrderForListDto
                             {
                                 Id = order.Id,
                                 CustomerId = order.CustomerId,
                                 CustomerName = customer.Name,
                                 OrderDate = order.OrderDate,
                                 TotalPrice = order.TotalPrice
                             };

                return result.ToList();
            }
        }
    }
}
