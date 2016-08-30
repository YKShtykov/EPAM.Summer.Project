using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IProfileRepository
    {
        void Create(int id);
        void Update(DalProfile profile);
        void Delete(int id);
        DalProfile Get(int key);
        IEnumerable<DalProfile> GetAll();
    }
}
