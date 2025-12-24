using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlServices : DapperSql<Services>
    {
        public z_sqlServices()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Services.SortNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Services.SortNo";
            DropDownTextColumn = "Services.SortNo";
            DropDownOrderColumn = "Services.SortNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
