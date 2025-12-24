using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlNews : DapperSql<News>
    {
        public z_sqlNews()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "News.PublishDate";
            DefaultOrderByDirection = "DESC";
            DropDownValueColumn = "HeaderName";
            DropDownTextColumn = "HeaderName";
            DropDownOrderColumn = "HeaderName ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT  News.Id, News.CodeNo, vi_CodeNews.CodeName, News.PublishDate, News.HeaderName, 
News.DetailText, News.Remark
FROM News 
LEFT OUTER JOIN vi_CodeNews ON News.CodeNo = vi_CodeNews.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeNews.CodeName");
            return searchColumn;
        }

        /// <summary>
        /// 取得近期最新消息
        /// </summary>
        /// <param name="rows">取得筆數 , 0 = 全部</param>
        public List<News> GetNearNews(int rows)
        {
            string str_query = "SELECT ";
            if (rows > 0) str_query += $"top {rows} ";
            str_query += "dbo.fn_get_time_from_now(PublishDate) AS Remark, ";
            str_query += "Id, PublishDate , DetailText , HeaderName ";
            str_query += "FROM News WHERE PublishDate > GetDate() ";
            str_query += "ORDER BY PublishDate ASC";
            var model = dpr.ReadAll<News>(str_query);
            return model;
        }
    }
}
