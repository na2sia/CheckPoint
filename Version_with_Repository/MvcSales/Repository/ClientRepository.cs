using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL.DBModel;
using MvcSales.Models;
using MvcSales.Repository;

namespace MvcSales
{
    public class ClientRepository : IModelRepository<Client> 
    {
        private SalesContext context;
        public ClientRepository()
        {
            this.context = new SalesContext();
        }
        //private EFModel.Client ToEntity(Client source)
        //{
        //    return new EFModel.Client() { Id = source.Id, FirstName = source.FirstName, LastName = source.LastName};
        //}
        //private Client ToObject(EFModel.Client source)
        //{
        //    return new Client() { Id = source.Id, FirstName = source.FirstName, LastName=source.LastName};
        //}

        public void Add(Client item)
        {
           // var itemToEntity = this.ToEntity(item);
            context.Clients.Add(item);
        }

        public void Remove(int id)
        {
            Client client = context.Clients.Find(id);
            if (client != null)
                context.Clients.Remove(client);
        }

        public void Update(Client item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> Items

        {
            get
            {
                return context.Clients;
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
