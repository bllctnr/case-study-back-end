using Business.Abstract;
using Business.Constants;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.RecordsAdded);
        }

        public IResult Delete(int customerId)
        {
            Customer customerToDelete = _customerDal.Get(c => c.Id == customerId);
            _customerDal.Delete(customerToDelete);
            return new SuccessResult(Messages.RecordsDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.RecordsListed);
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == customerId), Messages.RecordsListed);
        }

        public IResult Update(int customerId, Customer customer)
        {
            customer.Id = customerId;
            _customerDal.Update(customer);
            return new SuccessResult(Messages.RecordsUpdated);
        }
    }
}
