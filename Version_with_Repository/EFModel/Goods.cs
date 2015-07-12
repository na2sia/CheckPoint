using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFModel
{
    public class Goods
    {
         
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}