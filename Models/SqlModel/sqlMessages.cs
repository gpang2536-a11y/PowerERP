using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Crypto.Securityhandler;

namespace powererp.Models
{
    public class z_sqlMessages : DapperSql<Messages>
    {
        public z_sqlMessages()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Messages.SendTime";
            DefaultOrderByDirection = "DESC";
            DropDownValueColumn = "Messages.Id";
            DropDownTextColumn = "Messages.Id";
            DropDownOrderColumn = "Messages.SendTime DESC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Messages.Id, Messages.IsRead, Messages.CodeNo, vi_CodeMessage.CodeName, 
Messages.SenderNo, Users_1.UserName AS SenderName, Messages.ReceiverNo, 
Users_2.UserName AS ReceiverName, Messages.SendTime, 
Messages.HeaderText, Messages.MessageText, 
Messages.Remark 
FROM Messages 
LEFT OUTER JOIN Users AS Users_2 ON Messages.ReceiverNo = Users_2.UserNo 
LEFT OUTER JOIN Users AS Users_1 ON Messages.SenderNo = Users_1.UserNo 
LEFT OUTER JOIN vi_CodeMessage ON Messages.CodeNo = vi_CodeMessage.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            SearchColumns = base.GetSearchColumns();
            SearchColumns.Add("vi_CodeMessage.CodeName");
            SearchColumns.Add("Users_1.UserName");
            SearchColumns.Add("Users_2.UserName");
            return SearchColumns;
        }

        /// <summary>
        /// 取得訊息列表
        /// </summary>
        /// <param name="codeNo">類別代號</param>
        /// <param name="receiverNo">接收者代號</param>
        /// <param name="rows">讀取筆數 , 0 表示全部</param>
        /// <param name="onlyUnRead">只讀取未讀資料</param>
        /// <returns></returns>
        public List<Messages> GetMessagesByReceiverNo(string codeNo, string receiverNo, int rows, bool onlyUnRead)
        {
            string sql_query = GetSQLSelect();
            string sql_rows = $"SELECT TOP {rows} ";
            if (rows > 0) sql_query = sql_query.Replace("SELECT ", sql_rows);
            sql_query += " WHERE Messages.CodeNo = @CodeNo AND Messages.ReceiverNo = @ReceiverNo ";
            if (onlyUnRead) sql_query += " AND Messages.IsRead = 0 ";
            sql_query += " ORDER BY Messages.SendTime DESC ";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("CodeNo", codeNo);
            parm.Add("ReceiverNo", receiverNo);
            var model = dpr.ReadAll<Messages>(sql_query, parm);
            return model;
        }
    }
}