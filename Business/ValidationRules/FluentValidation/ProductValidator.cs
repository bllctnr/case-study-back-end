using DataAccess.Abstract;
using Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    internal class ProductValidator : AbstractValidator<Product>
    {
        IProductDal _productDal;
        public ProductValidator(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.StockAmount).GreaterThan(0);
        }
    }
}
