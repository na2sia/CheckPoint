using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBModel;
using DAL.ModelsFromEntity;

namespace DAL
{
    public class ManagerRepository : IModelRepository<Manager>
    {
        private EFModel.SalesContext context = new EFModel.SalesContext();
        private EFModel.Manager ToEntity(Manager source)
        {
            return new EFModel.Manager() { Id = source.Id, FirstName = source.FirstName, LastName = source.LastName };
        }
        private Manager ToObject(EFModel.Manager source)
        {
            return new Manager() { Id = source.Id, FirstName = source.FirstName, };
        }

        public void Add(Manager item)
        {
            var itemToEntity = this.ToEntity(item);

            context.Managers.Add(itemToEntity);
        }

        public void Remove(int id)
        {
            EFModel.Manager manager = context.Managers.Find(id);
            if (manager != null)
                context.Managers.Remove(manager);
        }

        public void Update(Manager item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Manager> Items
        {
            get
            {
                foreach (var u in this.context.Managers)
                {
                    yield return this.ToObject(u);
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
