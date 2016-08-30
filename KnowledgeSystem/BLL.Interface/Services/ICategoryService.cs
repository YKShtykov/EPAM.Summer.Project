using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICategoryService
    {
        void Create(BllCategory category);
        void Update(BllCategory category);
        void Delete(int id);
        BllCategory Get(int id);
        IEnumerable<BllCategory> GetAll();       
    }
}
