using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Processor.Models
{
  public partial class CustomerOrder
  {
    protected override IValidator GetValidator()
    {
      return new CustomerOrderValidator();
    }
    class CustomerOrderValidator: AbstractValidator<CustomerOrder>
    {
      public CustomerOrderValidator()
      {
        RuleFor(x => x.OrderAmount).GreaterThan(0M).WithMessage("The order amount should not be less than or equal R0.");
        RuleFor(x => x.OrderDate).LessThanOrEqualTo(DateTime.Now).WithMessage("The order date cannot be a date in the future.");
        RuleFor(x => x.OrderDate.DayOfWeek).NotEqual(DayOfWeek.Sunday).WithMessage("We do not accept orders on a Sunday.");
        RuleFor(x => x.CustomerId).GreaterThan(-1).WithMessage("Invalid customer ID. The customer ID should not be less than zero.");
      }
    }
  }
  
}