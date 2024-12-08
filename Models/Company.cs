using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Company
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    [Display(Name = "Company Name")]
    public string Name { get; set; }

    [Display(Name = "Address")]
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }

    [Display(Name = "Phone")]
    public string? PhoneNumber { get; set; }
}
