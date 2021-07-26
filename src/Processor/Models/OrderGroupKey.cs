using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Processor.Models
{
  public class OrderGroupKey
  {
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
  }
}