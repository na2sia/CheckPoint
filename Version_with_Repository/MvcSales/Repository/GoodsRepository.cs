using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSales.Models;
using MvcSales.Repository;

namespace MvcSales
{
    public class GoodsRepository:IModelRepository<Goods> 
    {
        private SalesContext context = new SalesContext();

        //private EFModel.Goods ToEntity(Goods source)
        //{
        //    return new EFModel.Goods() { Id = source.Id, Name = source.Name, Price = source.Price};
        //}
        //private Goods ToObject(EFModel.Goods source)
        //{
        //    return new Goods() { Id = source.Id, Name = source.Name, Price = source.Price };
        //}
        public void Add(Goods item)
        {
           // var itemToEntity = this.ToEntity(item);
            context.Goods.Add(item);
        }

        public void Remove(int id)
        {
            Goods goods = context.Goods.Find(id);
            if (goods != null)
                context.Goods.Remove(goods);
        }
        public void Update(Goods item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Goods> Items
        {
            get
            {
                return context.Goods;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
