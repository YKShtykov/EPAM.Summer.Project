using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IProfileService
    {
        BllProfile Get(int id);
        void Update(BllProfile profile);
        IEnumerable<BllProfile> Search(BllSearchModel model);
    }
}
