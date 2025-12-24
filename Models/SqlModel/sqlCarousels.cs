using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlCarousels : DapperSql<Carousels>
    {
        public z_sqlCarousels()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Carousels.SortNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Carousels.SortNo";
            DropDownTextColumn = "Carousels.SortNo";
            DropDownOrderColumn = "Carousels.SortNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
