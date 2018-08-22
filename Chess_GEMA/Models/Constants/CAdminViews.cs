using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess_GEMA.Models.Constants
{
    public class CAdminViews
    {
        // Admin
        private const string RootPath = "~/Views/Admin/";

        // View names and postfixes
        private const string IndexView = "Index";
        private const string Postfix = ".cshtml";

        // Admin
        private const string Admin = "Admin/";
        public const string AdminIndex = RootPath + Admin + IndexView + Postfix;
        public const string AdminForm = RootPath + Admin + "AdminForm" + Postfix;

        // Roles
        private const string Roles = "Roles/";
        public const string RolesIndex = RootPath + Roles + IndexView + Postfix;
        public const string RolesForm = RootPath + Roles + "RolesForm" + Postfix;

    }
}