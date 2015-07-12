﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBModel;
using DAL.ModelsFromEntity;
using System.Data.Entity;

namespace DAL
{
    public class UserRepository : IModelRepository<User>
    {
        private EFModel.SalesContext context;
        public UserRepository()
        {
            this.context = new EFModel.SalesContext();
        }
        private EFModel.User ToEntity(User source)
        {
            return new EFModel.User() { Id = source.Id, Name = source.Name, Login = source.Login, Password=source.Password, RoleId=source.RoleId };
        }
        private User ToObject(EFModel.User source)
        {
            return new User() { Id = source.Id, Name = source.Name, Login = source.Login, Password = source.Password, RoleId = source.RoleId };
        }

        public void Add(User item)
        {
            var itemToEntity = this.ToEntity(item);

            context.Users.Add(itemToEntity);
        }

        public void Remove(int id)
        {
            EFModel.User user = context.Users.Find(id);
            if (user!=null)
            context.Users.Remove(user);
        }

        public void Update(User item)
        {
            if (context != null)
            {
                var itemToEntity = this.ToEntity(item);
                context.Entry(itemToEntity).State = EntityState.Modified;
                //context.Users.Attach(itemToEntity);
            }
        }

        public IEnumerable<User> Items
        {
            get
            {
                foreach (var u in this.context.Users)
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
