using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlFormLocks : DapperSql<FormLocks>
    {
        public z_sqlFormLocks()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "FormLocks.LockNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "FormLocks.LockNo";
            DropDownTextColumn = "FormLocks.LockName";
            DropDownOrderColumn = "FormLocks.LockNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT FormLocks.Id, FormLocks.LockNo, vi_CodeFormLock.CodeName AS LockName, 
FormLocks.LockDate, FormLocks.Remark
FROM FormLocks 
LEFT OUTER JOIN vi_CodeFormLock ON FormLocks.LockNo = vi_CodeFormLock.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeFormLock.CodeName");
            return searchColumn;
        }
    }
}
