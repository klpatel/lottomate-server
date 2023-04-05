using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Framework.EnumModels
{
    public class UserRole : Enumeration
    {
        public static readonly UserRole Agency = new UserRole(3, "Agency");
        public static readonly UserRole Client = new UserRole(4, "Client");
        public static readonly UserRole Planner = new UserRole(5, "Planner");
        public static readonly UserRole Researcher = new UserRole(6, "Researcher");
        public static readonly UserRole Contact = new UserRole(7, "Contact");
        public static readonly UserRole Coordinator = new UserRole(8, "Coordinator");
        public static readonly UserRole Visitor = new UserRole(11, "Visitor");
        public static readonly UserRole SystemAdmin = new UserRole(12, "SystemAdmin");
        public static readonly UserRole SupportAdmin = new UserRole(13, "SupportAdmin");
        public static readonly UserRole SystemUser = new UserRole(14, "SystemUser");
        public static readonly UserRole SupportUser = new UserRole(15, "SupportUser");
        public static readonly UserRole AccountManager = new UserRole(17, "AccountManager");
        public static readonly UserRole ResearcherUser = new UserRole(18, "ResearcherUser");


        private UserRole() { }
        private UserRole(int id, string value) : base(id, value) { }
    }
}
