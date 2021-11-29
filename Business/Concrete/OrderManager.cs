using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Results;
using Core.Utilities.BusinessRules;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        IProductDal _productDal;
        IOrderProductDal _orderProductDal;

        public OrderManager(IOrderDal orderDal, IProductDal productDal, IOrderProductDal orderProductDal)
        {
            _orderDal = orderDal;
            _productDal = productDal;
            _orderProductDal = orderProductDal;
        }

        [SecuredOperation("order.add,admin")]
        public IResult Add(OrderWithProductsForAddNewOrderDto orderWithProducts)
        {
            //Check business rule before process.
            IResult businessRuleFails = BusinessRuleRunner.Run(CheckStockAmounts(orderWithProducts.OrderProducts));

            if (businessRuleFails != null)
            {
                return new ErrorResult(businessRuleFails.Message);
            }

            decimal totalPrize = 0;
            orderWithProducts.OrderProducts.ForEach(product => {
                totalPrize += product.Price;
            });
            Order orderToAdd = new Order
            {
                CustomerId = orderWithProducts.CustomerId,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrize
            };
            
            _orderDal.Add(orderToAdd);

            UpdateStocks(orderWithProducts.OrderProducts);
            AddOrderProducts(orderWithProducts.OrderProducts, orderToAdd.Id);

            return new SuccessResult(Messages.RecordsAdded);
        }

        [SecuredOperation("order.delete, admin")]
        public IResult Delete(int orderId)
        {
            Order orderToDelete = _orderDal.Get(o => o.Id == orderId);
            _orderDal.Delete(orderToDelete);
            return new SuccessResult(Messages.RecordsDeleted);
        }

        [SecuredOperation("order.get, admin")]
        public IDataResult<List<OrderForListDto>> GetAll()
        {
            return new SuccessDataResult<List<OrderForListDto>>(_orderDal.GetAllOrderForListDto(), Messages.RecordsListed);
        }


        [SecuredOperation("order.get, admin")]
        public IDataResult<Order> GetById(int orderId)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(o => o.Id== orderId), Messages.RecordsListed);
        }

        public IDataResult<List<OrderProductDto>> GetProductsOfOrder(int orderId)
        {
            var result = _orderDal.GetProductsOfOrder(orderId);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<OrderProductDto>>(result, Messages.RecordsListed);
            }
            
            return new ErrorDataResult<List<OrderProductDto>>(null, Messages.RecordNotFount);
        }

        [SecuredOperation("order.update, admin")]
        public IResult Update(int orderId, Order order)
        {
            order.Id = orderId;
            _orderDal.Update(order);
            return new SuccessResult(Messages.RecordsUpdated);
        }

        private IResult UpdateStocks(List<OrderProductDto> productsWithOrderInfo)
        {
            productsWithOrderInfo.ForEach(productWithOrderinfo =>
            {
                Product productToUpdate = _productDal.Get(p => p.Id == productWithOrderinfo.Product.Id);
                productToUpdate.StockAmount -= productWithOrderinfo.OrderAmount;
                _productDal.Update(productToUpdate);
            });

            return new SuccessResult();
        }


        private IResult AddOrderProducts(List<OrderProductDto> productsWithOrderInfo, int orderId)
        {
            productsWithOrderInfo.ForEach(p =>
            {
                OrderProduct orderProductToAdd = new OrderProduct
                {
                    OrderId = orderId,
                    ProductId = p.Product.Id,
                    OrderAmount = p.OrderAmount,
                    Price = p.Price
                };

                _orderProductDal.Add(orderProductToAdd);
            });

            return new SuccessResult();
        }

        // Business Rules
        private IResult CheckStockAmounts(List<OrderProductDto> productsWithOrderInfo)
        {
            List<OrderProductDto> result = new List<OrderProductDto>();

            productsWithOrderInfo.ForEach(productWithOrderInfo =>
            {
                int stockAmount = _productDal.Get(p => p.Id == productWithOrderInfo.Product.Id).StockAmount;
                if (productWithOrderInfo.OrderAmount > stockAmount)
                {
                    result.Add(productWithOrderInfo);
                }
            });


            if (result.Count > 0)
            {
                return new ErrorDataResult<List<OrderProductDto>>(result, Messages.OutOfStockItemsInOrder);
            }
            return new SuccessResult();
        }

    }
}

