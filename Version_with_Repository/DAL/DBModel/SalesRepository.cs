using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBModel;
using DAL.ModelsFromEntity;

namespace DAL
{
    public class SalesRepository : IModelRepository<Sales>
    {
        private EFModel.SalesContext context = new EFModel.SalesContext();

        private EFModel.Sales ToEntity(Sales source)
        {
            return new EFModel.Sales()
               {
                   Id = source.Id,
                   ManagerId = source.ManagerId,
                   GoodsId = source.GoodsId,
                   ClientId = source.ClientId,
                   Date = source.Date,
                   Cost = source.Cost
               };
        }
        private Sales ToObject(EFModel.Sales source)
        {
            return new Sales()
                {
                    Id = source.Id,
                    ManagerId = source.ManagerId,
                    GoodsId = source.GoodsId,
                    ClientId = source.ClientId,
                    Date = source.Date,
                    Cost = source.Cost
                };
        }

        public void Add(Sales item)
        {
            var itemToEntity = this.ToEntity(item);
            context.Sales.Add(itemToEntity);
            SaveChanges();
        }

        public void Remove(int id)
        {
            EFModel.Sales sales = context.Sales.Find(id);
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
                foreach (var i in this.context.Sales)
                {
                    yield return this.ToObject(i);
                }
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
