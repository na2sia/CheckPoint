using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSales.Repository;
using MvcSales.Models;
using System.Data.Entity;

namespace MvcSales
{
    public class SalesRepository : IModelRepository<Sales>
    {
        private SalesContext context = new SalesContext();

        //private EFModel.Sales ToEntity(Sales source)
        //{
        //    return new EFModel.Sales()
        //       {
        //           Id = source.Id,
        //           ManagerId = source.ManagerId,
        //           GoodsId = source.GoodsId,
        //           ClientId = source.ClientId,
        //           Date = source.Date,
        //           Cost = source.Cost
        //       };
        //}
        //private Sales ToObject(EFModel.Sales source)
        //{
        //    return new Sales()
        //        {
        //            Id = source.Id,
        //            ManagerId = source.ManagerId,
        //            GoodsId = source.GoodsId,
        //            ClientId = source.ClientId,
        //            Date = source.Date,
        //            Cost = source.Cost
        //        };
        //}

        public void Add(Sales item)
        {
            //var itemToEntity = this.ToEntity(item);
            context.Sales.Add(item);
           // SaveChanges();
        }

        public void Remove(int id)
        {
            Sales sales = context.Sales.Find(id);
            if (sales != null)
                context.Sales.Remove(sales);
        }

        public void Update(Sales item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sales> Items
        {
            get
            {
                return context.Sales.Include(x=>x.Client).Include(x=>x.Goods).Include(x=>x.Manager);
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
