using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class Goods
    {
        // ID 
        public int Id { get; set; }
        // Имя
        [Required]
        [Display(Name = "Название")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        // Фамилия
        [Required]
        [Display(Name = "Цена")]
       // [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        [DataType(DataType.MultilineText)]
        public double Price { get; set; }

    }
}