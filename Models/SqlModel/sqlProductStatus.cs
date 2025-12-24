using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlProductStatus : DapperSql<ProductStatus>
    {
        public z_sqlProductStatus()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "ProductStatus.StatusNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "ProductStatus.StatusNo";
            DropDownTextColumn = "ProductStatus.StatusName";
            DropDownOrderColumn = "ProductStatus.StatusNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
