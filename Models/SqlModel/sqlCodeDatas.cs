using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlCodeDatas : DapperSql<CodeDatas>
    {
        public z_sqlCodeDatas()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "CodeDatas.SortNo,CodeDatas.CodeNo";
            DefaultOrderByDirection = "ASC,ASC";
            DropDownValueColumn = "CodeDatas.CodeNo";
            DropDownTextColumn = "CodeDatas.CodeName";
            DropDownOrderColumn = "CodeDatas.SortNo ASC,CodeDatas.CodeNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public List<CodeDatas> GetDataList(string baseNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<CodeDatas>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE BaseNo = @BaseNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("BaseNo", baseNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<CodeDatas>(sql_query, parm);
            return model;
        }

        public List<SelectListItem> GetDropDownList(string baseNo, bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"{DropDownValueColumn} + ' ' + ";
            str_query += $"{DropDownTextColumn} AS Text , {DropDownValueColumn} AS Value FROM CodeDatas ";
            str_query += "WHERE BaseNo = @BaseNo AND IsEnabled = @IsEnabled ";
            str_query += $"ORDER BY {DropDownOrderColumn}";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsEnabled", true);
            parm.Add("BaseNo", baseNo);
            var model = dpr.ReadAll<SelectListItem>(str_query, parm);
            return model;
        }

        public override string GetSQLWhere()
        {
            string str_query = "";
            return str_query;
        }
    }
}