using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ModelsFromEntity;
using DAL.DBModel;

namespace DAL
{
    public class RoleRepository:IModelRepository<DAL.ModelsFromEntity.Role>
    {
        private EFModel.SalesContext context;
        public RoleRepository()
        {
            this.context = new EFModel.SalesContext();
        }
        private EFModel.Role ToEntity(Role source)
        {
            return new EFModel.Role() { Id = source.Id, Name = source.Name};
        }
        private Role ToObject(EFModel.Role source)
        {
            return new Role() { Id = source.Id, Name = source.Name};
        }
        public void Add(ModelsFromEntity.Role item)
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

        public IEnumerable<ModelsFromEntity.Role> Items
        {
            get
            {
                foreach (var i in this.context.Roles)
                {
                    yield return this.ToObject(i);
                };
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
