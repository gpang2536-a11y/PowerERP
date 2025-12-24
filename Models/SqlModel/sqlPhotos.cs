using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlPhotos : DapperSql<Photos>
    {
        public z_sqlPhotos()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Photos.CodeNo,Photos.FolderName";
            DefaultOrderByDirection = "ASC,ASC";
            DropDownValueColumn = "Photos.FolderName";
            DropDownTextColumn = "Photos.PhotoName";
            DropDownOrderColumn = "Photos.CodeNo ASC,Photos.FolderName ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
