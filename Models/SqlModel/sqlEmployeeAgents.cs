using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlEmployeeAgents : DapperSql<EmployeeAgents>
    {
        public z_sqlEmployeeAgents()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "EmployeeAgents.EmpNo, EmployeeAgents.AgentNo";
            DefaultOrderByDirection = "ASC,ASC";
            DropDownValueColumn = "EmployeeAgents.AgentNo";
            DropDownTextColumn = "Agents.EmpName";
            DropDownOrderColumn = "EmployeeAgents.AgentNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT EmployeeAgents.Id, EmployeeAgents.IsEnabled, EmployeeAgents.EmpNo, 
Employees.EmpName, EmployeeAgents.AgentNo, Agents.EmpName AS AgentName, 
Departments.DeptName, Titles.TitleName, EmployeeAgents.Remark
FROM Titles 
RIGHT OUTER JOIN Employees AS Agents ON Titles.TitleNo = Agents.TitleNo 
LEFT OUTER JOIN Departments ON Agents.DeptNo = Departments.DeptNo 
RIGHT OUTER JOIN EmployeeAgents ON Agents.EmpNo = EmployeeAgents.AgentNo 
LEFT OUTER JOIN Employees ON EmployeeAgents.EmpNo = Employees.EmpNo 
";
            return str_query;
        }

        public EmployeeAgents GetData(string empNo, string agentNo)
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE EmployeeAgents.EmpNo = @EmpNo AND EmployeeAgents.AgentNo = @AgentNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("EmpNo", empNo);
            parm.Add("AgentNo", agentNo);
            var model = dpr.ReadSingle<EmployeeAgents>(sql_query, parm);
            return model;
        }

        public List<EmployeeAgents> GetDataList(string empNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<EmployeeAgents>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE EmployeeAgents.EmpNo = @EmpNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("EmpNo", empNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<EmployeeAgents>(sql_query, parm);
            return model;
        }
    }
}