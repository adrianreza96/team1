//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FeastFreedom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Order
    {
        public int OrderId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> MenuId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual User User { get; set; }
    }
}
