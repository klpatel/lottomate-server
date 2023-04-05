using System.Linq;

namespace LotoMate.Framework.Authorisation
{
    public static class AuthPolicy
    {
        //At present policies are defined as roles in the database
        //To Do : define domain policies and tie up with roles

        public const string ClientAccess = "Client";
        public const string SystemAdmin = "SysAdmin";
        public const string ClientAdmin = "ClientAdmin";
        public const string ClientUser = "ClientUser";
        public const string UserAccess = "UserAccess";

        /// <summary>
        /// Retrieves Roles from database
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <returns></returns>
        //public static string[] GetRoles(IRoleRepository roleRepository)
        //{
        //    return roleRepository.Queryable().Select(x => x.Name).ToArray();
        //}
    }
}
