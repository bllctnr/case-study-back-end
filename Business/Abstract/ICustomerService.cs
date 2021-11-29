using Core.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<Customer> GetById(int customerId);
        IDataResult<List<Customer>> GetAll();
        IResult Add(Customer customer);
        IResult Delete(int customerId);
        IResult Update(int customerId, Customer customer);
    }
}
