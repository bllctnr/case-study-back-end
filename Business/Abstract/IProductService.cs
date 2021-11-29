using Core.Results;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int productId);
        IDataResult<List<Product>> GetAll();
        IResult Add(Product product);
        IResult Delete(int productId);
        IResult Update(int productId, Product product);
    }
}
