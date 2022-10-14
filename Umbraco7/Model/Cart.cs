using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco7.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public string Images { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime toDate { get; set; }
        public double price { get; set; }
        public int Numbers { get; set; }
    }
}