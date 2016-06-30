using System.Collections.Generic;
using System.Threading.Tasks;
using SampleApi.Entities;

namespace SampleApi.Business
{
    public interface IMyDemoEntityBL
    {
        void Delete(int id);
        MyDemoEntity Get(int id);
        List<MyDemoEntity> GetAll();
        int Save(MyDemoEntity demoentity);
        Task SaveAsync(MyDemoEntity demoentity);
    }
}