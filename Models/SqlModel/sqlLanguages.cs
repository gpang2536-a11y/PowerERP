using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlLanguages : DapperSql<Languages>
    {
        public z_sqlLanguages()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Languages.LangNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Languages.LangNo";
            DropDownTextColumn = "Languages.LangName";
            DropDownOrderColumn = "Languages.LangNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}