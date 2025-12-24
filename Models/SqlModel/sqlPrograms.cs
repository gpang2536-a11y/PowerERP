using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlPrograms : DapperSql<Programs>
    {
        public z_sqlPrograms()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Programs.PrgNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Programs.PrgNo";
            DropDownTextColumn = "Programs.PrgName";
            DropDownOrderColumn = "Programs.PrgNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Programs.Id, Programs.IsEnabled, Programs.IsPageSize, Programs.IsSearch, Programs.RoleNo, 
Roles.RoleName, Programs.ModuleNo, Modules.ModuleName, Programs.SortNo, Programs.PrgNo, Programs.PrgName, 
Programs.CodeNo, vi_CodeProgram.CodeName, Programs.LockNo, vi_CodeFormLock.CodeName AS LockName , 
Programs.AreaName, Programs.ControllerName, Programs.ActionName, Programs.ParmValue, Programs.Remark 
FROM Programs 
LEFT OUTER JOIN vi_CodeProgram ON Programs.CodeNo = vi_CodeProgram.CodeNo 
LEFT OUTER JOIN vi_CodeFormLock ON Programs.LockNo = vi_CodeFormLock.CodeNo 
LEFT OUTER JOIN Modules ON Programs.ModuleNo = Modules.ModuleNo 
LEFT OUTER JOIN Roles ON Programs.RoleNo = Roles.RoleNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            SearchColumns = base.GetSearchColumns();
            SearchColumns.Add("Roles.RoleName");
            SearchColumns.Add("Modules.ModuleName");
            SearchColumns.Add("vi_CodeProgram.CodeName");
            SearchColumns.Add("vi_CodeFormLock.CodeName");
            return SearchColumns;
        }

        public List<Programs> GetDataList(string baseNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Programs>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE Programs.ModuleNo = @BaseNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("BaseNo", baseNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Programs>(sql_query, parm);
            return model;
        }
    }
}
