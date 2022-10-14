using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco7.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Price { get; set; }
    }
}