using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Results;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    internal class OrderProductManager : IOrderProductService
    {
        IOrderProductDal _orderProductDal;
        public OrderProductManager(IOrderProductDal orderProductDal)
        {
            _orderProductDal = orderProductDal;
        }

        [SecuredOperation("order.add, admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(OrderProduct orderProduct)
        {
            _orderProductDal.Add(orderProduct);
            return new SuccessResult(Messages.RecordsAdded);
        }

        [SecuredOperation("order.delete, admin")]
        public IResult Delete(int orderProductId)
        {
            var orderProductToDelete = _orderProductDal.Get(o => o.Id == orderProductId);
            _orderProductDal.Delete(orderProductToDelete);
            return new SuccessResult(Messages.RecordsDeleted);
        }

        [SecuredOperation("order.get, admin")]
        public IDataResult<List<OrderProduct>> GetAll()
        {
            return new SuccessDataResult<List<OrderProduct>>(_orderProductDal.GetAll(), Messages.RecordsListed);
        }

        [SecuredOperation("order.get, admin")]
        public IDataResult<OrderProduct> GetById(int orderProductId)
        {
            return new SuccessDataResult<OrderProduct>(_orderProductDal.Get(p => p.Id == orderProductId), Messages.RecordsListed);
        }

        [SecuredOperation("order.update, admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(int orderProductId, OrderProduct orderProduct)
        {
            orderProduct.Id = orderProductId;
            _orderProductDal.Update(orderProduct);
            return new SuccessResult(Messages.RecordsUpdated);
        }

    }
}
