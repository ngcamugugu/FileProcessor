using System.Collections.Generic;

namespace Processor.Models
{
  public class GroupedOrder
  {
    public string Key { get; set; }
    public List<CustomerOrder> Orders { get; set; }
  }
}