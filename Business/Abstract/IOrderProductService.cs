using Core.Results;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;


namespace Business.Abstract
{
    public interface IOrderProductService
    {
        IDataResult<OrderProduct> GetById(int orderProductId);
        IDataResult<List<OrderProduct>> GetAll();
        IResult Add(OrderProduct orderProduct);
        IResult Delete(int orderProductId);
        IResult Update(int orderProductId, OrderProduct orderProduct);
    }
}
