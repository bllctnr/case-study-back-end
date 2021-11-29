using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Results;
using Core.Utilities.BusinessRules;
using DataAccess.Abstract;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [SecuredOperation("product.add, admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult businessRuleFails = BusinessRuleRunner.Run(CheckIfProductCodeExists(product));

            if (businessRuleFails != null)
            {
                return new ErrorResult(businessRuleFails.Message);
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.RecordsAdded);
        }

        [SecuredOperation("product.delete, admin")]
        public IResult Delete(int productId)
        {
            var productToDelete = _productDal.Get(p => p.Id == productId);
            _productDal.Delete(productToDelete);
            return new SuccessResult(Messages.RecordsDeleted);
        }

        [SecuredOperation("product.get, admin")]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.RecordsListed);
        }

        [SecuredOperation("product.get, admin")]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == productId), Messages.RecordsListed);
        }

        [SecuredOperation("product.update, admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(int productId, Product product)
        {
            product.Id = productId;
            _productDal.Update(product);
            return new SuccessResult(Messages.RecordsUpdated);
        }

        //Business rules
        private IResult CheckIfProductCodeExists(Product product)
        {
            var result = _productDal.Get(p => p.Code == product.Code);

            if (result != null)
            {
                return new ErrorResult(Messages.ProductCodeAlreadyExist); 
            }
            return new SuccessResult();
        }
    }
}
