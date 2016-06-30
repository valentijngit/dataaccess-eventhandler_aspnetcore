using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleApi.Entities;
using Toolbox.DataAccess;

namespace SampleApi.Business
{
    public class MyDemoEntityBL: IMyDemoEntityBL
    {

        private readonly IUowProvider _uowProvider;
        //private readonly IKeyReader _keyReader;
        //private readonly IAuthContext _authContext;

        public MyDemoEntityBL(IUowProvider uowProvider)
        {            
            _uowProvider = uowProvider;
            //_keyReader = keyReader;
            //_authContext = authContext;

        }



        public List<MyDemoEntity> GetAll()
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<MyDemoEntity>();
                var demoentities = repository.GetAll();
                

                return demoentities.ToList();
            }
        }



        public MyDemoEntity Get(int id)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<MyDemoEntity>();
                var demoentity = repository.Get(id);               

                return demoentity;
            }
        }

        public int Save(MyDemoEntity demoentity)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<MyDemoEntity>();
                if (demoentity.Id == 0)
                {
                    repository.Add(demoentity);
                }
                else
                {
                    repository.Update(demoentity);
                }

                uow.SaveChanges();
                return demoentity.Id;
            }
        }


        public async Task SaveAsync(MyDemoEntity demoentity)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<MyDemoEntity>();
                if (demoentity.Id == 0)
                {
                    repository.Add(demoentity);
                }
                else
                {
                    repository.Update(demoentity);
                }

              
                await uow.SaveChangesAsync();              
               

            }
        }



        public void Delete(int id)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<MyDemoEntity>();
                repository.Remove(id);

                uow.SaveChanges();
            }
        }

    }
}
