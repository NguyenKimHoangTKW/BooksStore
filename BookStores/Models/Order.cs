//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStores.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int idOrder { get; set; }
        public string codeOrder { get; set; }
        public bool checkPay { get; set; }
        public string deliveryStatus { get; set; }
        public System.DateTime orderDate { get; set; }
        public System.DateTime deliveryDate { get; set; }
        public int idCustomer { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
