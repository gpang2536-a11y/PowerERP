using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlRoles : DapperSql<Roles>
    {
        public z_sqlRoles()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Roles.RoleNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Roles.RoleNo";
            DropDownTextColumn = "Roles.RoleName";
            DropDownOrderColumn = "Roles.RoleNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}