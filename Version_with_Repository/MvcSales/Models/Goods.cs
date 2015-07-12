using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcSales.Models
{
    public class Goods
    {
         
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Название")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Цена")]
        [DataType(DataType.MultilineText)]
        public double Price { get; set; }

    }
}