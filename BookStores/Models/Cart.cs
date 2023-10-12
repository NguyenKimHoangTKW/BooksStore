using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStores.Models
{
    public class Cart
    {
        private dbBookStoreEntities db = new dbBookStoreEntities();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        public Cart(int ms)
        {
            iMaSach = ms;
            Book b = db.Books.Single(n => n.idBooks == iMaSach);
            sTenSach = b.nameBooks;
            sAnhBia = b.Thumb;
            dDonGia = double.Parse(b.price.ToString());
            iSoLuong = 1;
        }
    }
}