using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlLogCodes : DapperSql<LogCodes>
    {
        public z_sqlLogCodes()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "LogCodes.CodeNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "LogCodes.CodeNo";
            DropDownTextColumn = "LogCodes.CodeName";
            DropDownOrderColumn = "LogCodes.CodeNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT LogCodes.Id, LogCodes.CodeNo, LogCodes.CodeName, LogCodes.LogLevel, 
vi_CodeLogLevel.CodeName AS LogLevelName, LogCodes.LogInterval, 
vi_CodeLogInterval.CodeName AS LogIntervalName, LogCodes.Remark
FROM LogCodes 
LEFT OUTER JOIN vi_CodeLogLevel ON LogCodes.LogLevel = vi_CodeLogLevel.CodeNo 
LEFT OUTER JOIN vi_CodeLogInterval ON LogCodes.LogInterval = vi_CodeLogInterval.CodeNo
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            SearchColumns = base.GetSearchColumns();
            SearchColumns.Add("vi_CodeLogLevel.CodeName");
            SearchColumns.Add("vi_CodeLogInterval.CodeName");
            return SearchColumns;
        }
    }
}