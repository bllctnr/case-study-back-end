using Entities;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        public List<OrderProductDto> GetProductsOfOrder(int orderId);
        public List<OrderForListDto> GetAllOrderForListDto();

    }
}
