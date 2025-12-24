using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlEmployeeExperiences : DapperSql<EmployeeExperiences>
    {
        public z_sqlEmployeeExperiences()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "EmployeeExperiences.EmpNo, EmployeeExperiences.StartDate";
            DefaultOrderByDirection = "ASC,ASC";
            DropDownValueColumn = "EmployeeExperiences.EmpNo";
            DropDownTextColumn = "Employees.EmpName";
            DropDownOrderColumn = "EmployeeExperiences.EmpNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT EmployeeExperiences.Id, EmployeeExperiences.EmpNo, Employees.EmpName, 
EmployeeExperiences.CompName, EmployeeExperiences.DeptName, EmployeeExperiences.TitleName, 
EmployeeExperiences.BossName, EmployeeExperiences.Salary, EmployeeExperiences.StartDate, 
EmployeeExperiences.EndDate, EmployeeExperiences.QuitReason, EmployeeExperiences.Remark
FROM  EmployeeExperiences 
LEFT OUTER JOIN Employees ON EmployeeExperiences.EmpNo = Employees.EmpNo 
";
            return str_query;
        }

        public List<EmployeeExperiences> GetDataList(string empNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<EmployeeExperiences>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE EmployeeExperiences.EmpNo = @EmpNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("EmpNo", empNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<EmployeeExperiences>(sql_query, parm);
            return model;
        }
    }
}