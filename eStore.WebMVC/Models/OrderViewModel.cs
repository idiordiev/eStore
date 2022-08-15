using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eStore.WebMVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        [Display(Name = "City")]
        public string ShippingCity { get; set; }
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }
        [Display(Name = "Postal code")]
        public string ShippingPostalCode { get; set; }
        public IList<OrderItemViewModel> OrderItems { get; set; }
    }
}