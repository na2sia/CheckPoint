using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Internal.Web.Utils;
using System.ComponentModel.DataAnnotations;

namespace MvcSales.Models
{
    public class LogViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}