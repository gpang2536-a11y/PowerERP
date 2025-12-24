using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerERP.Models;

namespace powererp.Models
{
    public class z_sqlOvertimes : DapperSql<Overtimes>
    {
        public z_sqlOvertimes()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Overtimes.SheetNo";
            DefaultOrderByDirection = "DESC";
            DropDownValueColumn = "Overtimes.SheetNo";
            DropDownTextColumn = "Overtimes.SheetNo";
            DropDownOrderColumn = "Overtimes.SheetNo DESC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT dbo.Overtimes.Id, dbo.Overtimes.BaseNo, dbo.Overtimes.SheetNo, dbo.Overtimes.SheetDate, 
dbo.Overtimes.EmpNo, dbo.Employees.EmpName, dbo.Overtimes.DeptNo, dbo.Overtimes.DeptName, dbo.Overtimes.ReasonText, 
dbo.Overtimes.TypeNo, dbo.vi_CodeOvertime.CodeName AS TypeName, dbo.Overtimes.StartTime, 
dbo.Overtimes.EndTime, dbo.Overtimes.Hours, dbo.Overtimes.Remark 
FROM dbo.Overtimes 
LEFT OUTER JOIN dbo.vi_CodeOvertime ON dbo.Overtimes.TypeNo = dbo.vi_CodeOvertime.CodeNo 
LEFT OUTER JOIN dbo.Employees ON dbo.Overtimes.EmpNo = dbo.Employees.EmpNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("dbo.vi_CodeOvertime.CodeName");
            return searchColumn;
        }

        public List<int> GetMonthhours(int year)
        {
            List<int> hoursList = new List<int>();
            string str_query = "SELECT SUM(Hours) AS Hours FROM Overtimes WHERE YEAR(SheetDate) = @year AND Month(SheetDate) = ";
            var parm = new DynamicParameters();
            parm.Add("@year", year);
            for (int i = 1; i <= 12; i++)
            {
                var month_query = str_query + i.ToString();
                var model = dpr.ReadSingle<vmUHRMP003_Hours>(month_query, parm);
                if (model != null)
                    hoursList.Add(model.Hours);
                else
                    hoursList.Add(0);
            }
            return hoursList;
        }
    }
}