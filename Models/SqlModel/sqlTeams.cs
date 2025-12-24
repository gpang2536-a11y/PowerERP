using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlTeams : DapperSql<Teams>
    {
        public z_sqlTeams()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Teams.SortNo, Teams.TeamNo";
            DefaultOrderByDirection = "ASC, ASC";
            DropDownValueColumn = "Teams.TeamNo";
            DropDownTextColumn = "Teams.TeamName";
            DropDownOrderColumn = "Teams.SortNo ASC,Teams.TeamNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Teams.Id, Teams.SortNo, Teams.TeamNo, Teams.TeamName, Teams.EngName, Teams.GenderCode, 
vi_CodeGender.CodeName AS GenderName, Teams.DeptName, Teams.TitleName, Teams.TwitterUrl, 
Teams.FacebookUrl, Teams.LinkedinUrl, Teams.InstagramUrl, Teams.SkypeUrl, Teams.ContactEmail, 
Teams.DetailText, Teams.Remark
FROM Teams 
LEFT OUTER JOIN vi_CodeGender ON Teams.GenderCode = vi_CodeGender.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeGender.CodeName");
            return searchColumn;
        }
    }
}
