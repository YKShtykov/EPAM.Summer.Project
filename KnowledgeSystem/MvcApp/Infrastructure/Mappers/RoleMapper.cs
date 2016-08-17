//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using BLL.Interface;
//using MvcApp.ViewModels;

//namespace MvcApp.Infrastructure.Mappers
//{
//    public static class RoleMapper
//    {
//        public static BllRole MapRole(MvcRole role)
//        {
//            return new BllRole()
//            {
//                Id = role.Id,
//                Name = role.Name
//            };
//        }

//        public static MvcRole MapRole(BllRole role)
//        {
//            return new MvcRole()
//            {
//                Id = role.Id,
//                Name = role.Name
//            };
//        }
//    }
//}