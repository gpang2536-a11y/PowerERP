using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlDepartments : DapperSql<Departments>
    {
        public z_sqlDepartments()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Departments.DeptNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Departments.DeptNo";
            DropDownTextColumn = "Departments.DeptName";
            DropDownOrderColumn = "Departments.DeptNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT  Departments.Id, Departments.DeptNo, Departments.DeptName, 
Departments.BossNo, Employees.EmpName AS BossName, Departments.Remark
FROM Departments 
LEFT OUTER JOIN Employees ON Departments.BossNo = Employees.EmpNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("Employees.EmpName");
            return searchColumn;
        }
    }
}