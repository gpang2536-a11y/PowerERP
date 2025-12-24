using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlInventorysType : DapperSql<InventorysType>
    {
        public z_sqlInventorysType()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "InventorysType.TypeNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "InventorysType.TypeNo";
            DropDownTextColumn = "InventorysType.TypeName";
            DropDownOrderColumn = "InventorysType.TypeNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
