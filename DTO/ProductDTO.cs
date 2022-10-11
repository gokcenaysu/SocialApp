using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.DTO
{
    public class ProductDTO
    {
         public int Id { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}