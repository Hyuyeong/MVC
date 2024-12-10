using System;
using MVC.Models;

namespace MVC.ViewModels;

public class OrderVM
{
    public OrderHeader OrderHeader { get; set; }
    public IEnumerable<OrderDetail> OrderDetail { get; set; }
}
