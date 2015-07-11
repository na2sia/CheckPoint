using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class Sales
    {
        // ID 
        public int Id { get; set; }
        // Дата
        [Required]
        [Display(Name = "Дата")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        // Стоимость
        [Required]
        [Display(Name = "Стоимость")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public double Cost { get; set; }
        // Менеджер// Внешний ключ Категория
        //[Required]
        [Display(Name = "Менеджер")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        // Клиент// Внешний ключ Категория
        //[Required]
        [Display(Name = "Клиент")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        // Товар// Внешний ключ Категория
        //[Required]
        [Display(Name = "Товар")]
        public int GoodsId { get; set; }
        public Goods Goods { get; set; }

    }
}