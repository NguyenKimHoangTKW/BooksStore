﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;

    public partial class UserAdmin
    {
        public int idAdmin { get; set; }
        public string codeAdmin { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên admin")]
        public string nameAdmin { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string passWord { get; set; }
        public string access { get; set; }
    }
}
