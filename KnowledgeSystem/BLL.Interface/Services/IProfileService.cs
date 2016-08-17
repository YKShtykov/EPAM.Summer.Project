﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IProfileService
    {
        BllProfile GetProfile(int id);
        void EditProfile(BllProfile profile);
    }
}
