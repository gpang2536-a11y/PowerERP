using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlModules : DapperSql<Modules>
    {
        public z_sqlModules()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Modules.RoleNo,Modules.SortNo,Modules.ModuleNo";
            DefaultOrderByDirection = "ASC,ASC,ASC";
            DropDownValueColumn = "ModuleNo";
            DropDownTextColumn = "ModuleName";
            DropDownOrderColumn = "RoleNo ASC,SortNo ASC,ModuleNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
