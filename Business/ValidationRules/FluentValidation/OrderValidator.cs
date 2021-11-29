using DataAccess.Abstract;
using Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    internal class OrderValidator : AbstractValidator<Order>
    {
        IOrderDal _orderDal;
        public OrderValidator(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public OrderValidator()
        {
            RuleFor(o => o.CustomerId).NotEmpty();
        }
    }
}
