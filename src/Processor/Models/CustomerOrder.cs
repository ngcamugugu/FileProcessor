using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Processor.Models
{
  public partial class CustomerOrder : BEBase
  {
    private int _messageId;
    public int MessageId
    {
      get { return _messageId; }
      set
      {
        if (_messageId != value)
        {
          _messageId = value;
          OnPropertyChanged(() => _messageId);
        }
      }
    }
    private int _customerId;
    public int CustomerId
    {
      get { return _customerId; }
      set
      {
        if (_customerId != value)
        {
          _customerId = value;
          OnPropertyChanged(() => _customerId);
        }
      }
    }
    private int _orderId;
    public int OrderId
    {
      get { return _orderId; }
      set
      {
        if (_orderId != value)
        {
          _orderId = value;
          OnPropertyChanged(() => _orderId);
        }
      }
    }
    private decimal _orderAmount;
    public decimal OrderAmount
    {
      get { return _orderAmount; }
      set
      {
        if (_orderAmount != value)
        {
          _orderAmount = value;
          OnPropertyChanged(() => _orderAmount);
        }
      }
    }
    private DateTime _orderDate;
    public DateTime OrderDate
    {
      get { return _orderDate; }
      set
      {
        if (_orderDate != value)
        {
          _orderDate = value;
          OnPropertyChanged(() => _orderDate);
        }
      }
    }
  }
}