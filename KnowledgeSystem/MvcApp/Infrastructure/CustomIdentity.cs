using System.Security.Principal;

namespace MvcApp.Infrastructure
{
    /// <summary>
    /// Class for user identity
    /// </summary>
    public class CustomIdentity : IIdentity
    {
        /// <summary>
        /// Type of autentification
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "CustomIdentity";
            }
        }

        /// <summary>
        /// Is user autentificated?
        /// </summary>
        public bool IsAuthenticated { get; set; }        

        /// <summary>
        /// User id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User mail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User roles
        /// </summary>
        public string[] Roles { get; set; }
    }
}