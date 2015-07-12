using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcSales.Models
{
    public class Sales
    { 
        public int Id { get; set; }
     
        [Required]
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Required]
        [Display(Name = "Стоимость")]
        [DataType(DataType.MultilineText)]
        public double Cost { get; set; }
        
        [Display(Name = "Менеджер")]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        
        [Display(Name = "Клиент")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        
        [Display(Name = "Товар")]
        public int GoodsId { get; set; }
        public Goods Goods { get; set; }

    }
}