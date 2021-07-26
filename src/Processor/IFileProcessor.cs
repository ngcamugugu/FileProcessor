using Processor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
  public interface IFileProcessor
  {
    Task<List<OrderResponse>> ProcessOrder(byte[] csvFile);
  }
}
