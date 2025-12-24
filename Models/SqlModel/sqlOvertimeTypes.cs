using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlOvertimeTypes : DapperSql<OvertimeTypes>
    {
        public z_sqlOvertimeTypes()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "OvertimeTypes.TypeNo, OvertimeTypes.StartHour";
            DefaultOrderByDirection = "ASC, ASC";
            DropDownValueColumn = "OvertimeTypes.BaseNo";
            DropDownTextColumn = "OvertimeTypes.TypeName, OvertimeTypes.StartHour";
            DropDownOrderColumn = "OvertimeTypes.TypeNo ASC, OvertimeTypes.StartHour ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public List<OvertimeTypes> GetDataList(string baseNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<OvertimeTypes>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE TypeNo = @TypeNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("TypeNo", baseNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<OvertimeTypes>(sql_query, parm);
            return model;
        }
    }
}
