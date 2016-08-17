using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICategoryService
    {
        BllCategory GetById(int id);
        IEnumerable<BllCategory> GetAll();
        void Create(BllCategory category);
        void Delete(int id);
        void Update(BllCategory entity);
    }
}
