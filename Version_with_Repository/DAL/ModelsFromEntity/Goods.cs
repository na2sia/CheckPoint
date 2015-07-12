using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAL.ModelsFromEntity
{
    public class Goods
    {
         
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}