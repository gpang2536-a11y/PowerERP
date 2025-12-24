using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlCategorys : DapperSql<Categorys>
    {
        public z_sqlCategorys()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Categorys.ParentNo,Categorys.SortNo,Categorys.CategoryNo";
            DefaultOrderByDirection = "ASC,ASC,ASC";
            DropDownValueColumn = "Categorys.CategoryNo";
            DropDownTextColumn = "Categorys.CategoryName";
            DropDownOrderColumn = "Categorys.ParentNo ASC,Categorys.SortNo ASC,Categorys.CategoryNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
