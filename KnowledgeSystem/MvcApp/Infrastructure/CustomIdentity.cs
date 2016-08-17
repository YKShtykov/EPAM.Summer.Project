using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MvcApp.Infrastructure
{
    public class CustomIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                return "CustomIdentity";
            }
        }

        public bool IsAuthenticated { get; set; }        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}