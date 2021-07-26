using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Processor.Models
{
  public class OrderResponse
  {
    public int Customer { get; set; }
    public string Date { get; set; }
    public decimal Total { get; set; }
  }
}