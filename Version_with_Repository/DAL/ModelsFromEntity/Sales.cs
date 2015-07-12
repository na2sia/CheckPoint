using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAL.ModelsFromEntity
{
    public class Sales
    { 
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int GoodsId { get; set; }
        public Goods Goods { get; set; }

    }
}