using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlCalendars : DapperSql<Calendars>
    {
        public z_sqlCalendars()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Calendars.StartDate ASC,Calendars.StartTime ASC";
            DefaultOrderByDirection = "";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Calendars.Id, Calendars.TargetCode, Calendars.TargetNo, Calendars.CodeNo, vi_CodeCalendar.CodeName, 
Calendars.SubjectName, Calendars.StartDate, Calendars.StartTime, Calendars.StartDateTime, Calendars.EndDate, 
Calendars.EndTime, Calendars.EndDateTime, Calendars.ColorName, Calendars.IsFullday, Calendars.PlaceName, 
Calendars.ContactName, Calendars.ContactTel, Calendars.PlaceAddress, Calendars.RoomNo, Calendars.ResourceText, 
Calendars.Description, Calendars.Remark
FROM  Calendars 
LEFT OUTER JOIN vi_CodeCalendar ON Calendars.CodeNo = vi_CodeCalendar.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "vi_CodeCalendar.CodeName",
                    "Calendars.SubjectName",
                    "Calendars.PlaceName",
                    "Calendars.ContactName",
                    "Calendars.ContactTel",
                    "Calendars.PlaceAddress",
                    "Calendars.RoomNo",
                    "Calendars.ResourceText",
                    "Calendars.Description",
                    "Calendars.Remark"
                     };
            return searchColumn;
        }

        public override Calendars GetData(int id)
        {
            var model = new Calendars();
            if (id == 0)
            {
                //新增預設值
                model.CodeNo = "Public";
                model.StartDate = DateTime.Today;
                model.EndDate = DateTime.Today;
                model.StartTime = "08:00";
                model.EndTime = "09:00";
            }
            else
            {
                string sql_query = GetSQLSelect();
                sql_query += " WHERE Calendars.Id = @Id";
                DynamicParameters parm = new DynamicParameters();
                parm.Add("Id", id);
                model = dpr.ReadSingle<Calendars>(sql_query, parm);
            }
            return model;
        }

        public List<Calendars> GetDataList(string targetCodeNo, string targetNo)
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("TargetCode", targetCodeNo);
            parm.Add("TargetNo", targetNo);
            var model = dpr.ReadAll<Calendars>(str_query, parm);
            return model;
        }

        public List<SelectListItem> GetDropDownList(string targetCodeNo, string targetNo)
        {
            string str_query = "SELECT ";
            str_query += "SubjectName AS Text , Id AS Value FROM Calendars ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY Id";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("TargetCode", targetCodeNo);
            parm.Add("TargetNo", targetNo);
            var model = dpr.ReadAll<SelectListItem>(str_query, parm);
            return model;
        }

        public override string GetSQLWhere()
        {
            string str_query = " WHERE Calendars.TargetCode = @TargetCode AND Calendars.TargetNo = @TargetNo ";
            return str_query;
        }

        /// <summary>
        /// 取得使用者近期活動
        /// </summary>
        /// <param name="targetCode">對象類別</param>
        /// <param name="targetNo">對象代號</param>
        /// <param name="rows">取得筆數 , 0 = 全部</param>
        public List<Calendars> GetNearCalendar(string targetCode, string targetNo, int rows)
        {
            string str_query = "SELECT ";
            if (rows > 0) str_query += $"top {rows}";
            str_query += "dbo.fn_get_time_from_now(StartDateTime) AS StartTime , SubjectName FROM Calendars ";
            str_query += "WHERE TargetCode=@TargetCode AND TargetNo=@TargetNo AND StartDateTime>GETDATE() ";
            str_query += "ORDER BY StartDateTime ASC";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("TargetCode", targetCode);
            parm.Add("TargetNo", targetNo);
            var model = dpr.ReadAll<Calendars>(str_query, parm);
            return model;
        }
    }
}
