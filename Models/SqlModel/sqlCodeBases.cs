using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlCodeBases : DapperSql<CodeBases>
    {
        public z_sqlCodeBases()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "CodeBases.BaseNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "CodeBases.BaseNo";
            DropDownTextColumn = "CodeBases.BaseName";
            DropDownOrderColumn = "CodeBases.BaseNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public List<CodeBases> GetDataList(bool isAdmin, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<CodeBases>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE IsAdmin = @IsAdmin ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("IsAdmin", isAdmin);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<CodeBases>(sql_query, parm);
            return model;
        }

        public List<SelectListItem> GetDropDownList(bool isAdmin, bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"{DropDownValueColumn} + ' ' + ";
            str_query += $"{DropDownTextColumn} AS Text , {DropDownValueColumn} AS Value FROM {EntityName} ";
            str_query += "WHERE IsAdmin = @IsAdmin ";
            str_query += $"ORDER BY {DropDownOrderColumn}";
            var parm = new DynamicParameters();
            parm.Add("IsAdmin", isAdmin);
            var model = dpr.ReadAll<SelectListItem>(str_query, parm);
            ErrorMessage = dpr.ErrorMessage;
            return model;
        }
    }
}