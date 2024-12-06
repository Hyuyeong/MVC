using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }
    public string Description { get; set; }

    public string ISBN { get; set; }
    public string Author { get; set; }

    [Required]
    [DisplayName("Price")]
    [Range(1, 1000)]
    public double ListPrice { get; set; }

    [Required]
    [DisplayName("Price for 1-50")]
    [Range(1, 1000)]
    public double Price { get; set; }

    [Required]
    [DisplayName("Price for 50+")]
    [Range(1, 1000)]
    public double Price50 { get; set; }

    [Required]
    [DisplayName("Price for 100+")]
    [Range(1, 1000)]
    public double Price100 { get; set; }

    [DisplayName("Category")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }

    [ValidateNever]
    public string ImageUrl { get; set; } = "https://placehold.co/500x600/png";
}
