using System;
using MVC.Models;

namespace MVC.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

    public OrderHeader OrderHeader { get; set; }
}
