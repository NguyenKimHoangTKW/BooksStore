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
    
    public partial class WriteBook
    {
        public int idAuthor { get; set; }
        public int idBooks { get; set; }
        public string access { get; set; }
        public string location { get; set; }
        public int idWriteBook { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}