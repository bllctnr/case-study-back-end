using Core.Results;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<Order> GetById(int orderId);
        IDataResult<List<OrderForListDto>> GetAll();
        IDataResult<List<OrderProductDto>> GetProductsOfOrder(int orderId);
        IResult Add(OrderWithProductsForAddNewOrderDto orderWithProducts);
        IResult Delete(int orderId);
        IResult Update(int orderId, Order order);
    }
}
