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
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.WriteBooks = new HashSet<WriteBook>();
        }
    
        public int idBooks { get; set; }
        public string codeBooks { get; set; }
        public string nameBooks { get; set; }
        public string describe { get; set; }
        public string Thumb { get; set; }
        public System.DateTime updateDay { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public int idTopic { get; set; }
        public int idPublisher { get; set; }
    
        public virtual Topic Topic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Publisher Publisher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WriteBook> WriteBooks { get; set; }
    }
}