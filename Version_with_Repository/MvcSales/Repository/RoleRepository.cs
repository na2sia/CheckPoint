using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSales.Repository;
using MvcSales.Models;

namespace MvcSales
{
    public class RoleRepository:IModelRepository<Role>
    {
        private SalesContext context;
        public RoleRepository()
        {
            this.context = new SalesContext();
        }
        //private EFModel.Role ToEntity(Role source)
        //{
        //    return new EFModel.Role() { Id = source.Id, Name = source.Name};
        //}
        //private Role ToObject(EFModel.Role source)
        //{
        //    return new Role() { Id = source.Id, Name = source.Name};
        //}
        public void Add(Role item)
        {
            throw new NotImplementedException();
        }
        public void Update(Role item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Items
        {
            get
            {
                return context.Roles;
            }
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
