using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Processor
{
  public class BusinessRuleViolation: Exception
  {
    public List<string> Errors { get; }
    public BusinessRuleViolation(string message):base(message)
    {
      Errors = new List<string> { message };
    }
    public BusinessRuleViolation(List<string> errors)
    {
      Errors = Errors;
    }
  }
}