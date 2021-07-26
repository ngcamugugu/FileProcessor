using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Processor.Models;
namespace Processor
{
  public class FileProcessor : IFileProcessor
  {
    public Task<List<OrderResponse>> ProcessOrder(byte[] csvFile)
    {
      List<CustomerOrder> orders = GetOrders(csvFile);
      var groupedOrders = orders.GroupBy(o => o.CustomerId + o.OrderDate.Date.ToString() )
        .Select(o => new GroupedOrder
        {
          Key = o.Key,
          Orders = o.ToList()
        });
      var orderKeys = orders
        .GroupBy( o => o.CustomerId + o.OrderDate.Date.ToString())
        .Select(o =>  o.FirstOrDefault().CustomerId + o.FirstOrDefault().OrderDate.Date.ToString())
        .ToList();
      var response = new List<OrderResponse>();
      foreach(var key in orderKeys)
      {
        GroupedOrder individualOrder = groupedOrders.Where(o => o.Key == key).First();
        decimal orderTotal = individualOrder.Orders.Sum(o => o.OrderAmount);
        if (orderTotal > 4000) throw new BusinessRuleViolation("The order amount should not be more than R4000");
        if(individualOrder.Orders.Any(o => !o.IsValid))
        {
          var errors = new List<string>();
          errors.AddRange(individualOrder.Orders.First(o => !o.IsValid).Errors);
          throw new BusinessRuleViolation(errors);
        }
        response.Add(new OrderResponse
        {
          Customer = individualOrder.Orders.First().CustomerId,
          Date = individualOrder.Orders.First().OrderDate.Date.ToString().Substring(0,10),
          Total = orderTotal
        });
      }
      return Task.FromResult(response);
    }


    private List<CustomerOrder> GetOrders(byte[] csvFile)
    {
      var orders = new List<CustomerOrder>();
      using(var fileStream = new MemoryStream(csvFile))
      {
        using (var reader = new StreamReader(fileStream))
        {
          while (!reader.EndOfStream)
          {
            string orderLine = reader.ReadLine();
            string[] orderLineColumnData = orderLine.Split(';');
            
            try
            {
              var order = new CustomerOrder
              {
                MessageId = int.Parse(orderLineColumnData[0].Trim()),
                CustomerId = int.Parse(orderLineColumnData[1].Trim()),
                OrderId = int.Parse(orderLineColumnData[2].Trim()),
                OrderAmount = Convert.ToDecimal(orderLineColumnData[3].Trim().Replace('.', ',')),
                OrderDate = DateTime.Parse(orderLineColumnData[4].Trim())
              };
              orders.Add(order);
            }
            catch (Exception ex)
            {

              throw;
            }
          }
        }
      }      
      return orders;
    }   
  }
}