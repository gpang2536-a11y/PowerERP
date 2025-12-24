using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGet.Common;

namespace powererp.Models
{
    public class z_sqlLogs : DapperSql<Logs>
    {
        public z_sqlLogs()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Logs.KeyNo";
            DefaultOrderByDirection = "DESC";
            DropDownValueColumn = "Logs.KeyNo";
            DropDownTextColumn = "Logs.KeyNo";
            DropDownOrderColumn = "Logs.KeyNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Logs.Id, Logs.KeyNo,Logs.LogDate, Logs.LogTime, Logs.LastDate, Logs.LastTime, Logs.LogCode , Logs.LogLevel, 
Logs.LogInterval, Logs.LogMessage,vi_CodeLogCode.CodeName AS CodeName, vi_CodeLogLevel.CodeName AS LevelName, 
vi_CodeLogInterval.CodeName AS IntervalName, Logs.UserNo, Logs.UserName, Logs.TargetNo, Logs.IpAddress, 
Logs.LogNo, Logs.LogQty, Logs.Remark 
FROM Logs 
LEFT OUTER JOIN vi_CodeLogCode ON Logs.LogCode = vi_CodeLogCode.CodeNo 
LEFT OUTER JOIN vi_CodeLogLevel ON Logs.LogLevel = vi_CodeLogLevel.CodeNo 
LEFT OUTER JOIN vi_CodeLogInterval ON Logs.LogInterval = vi_CodeLogInterval.CodeNo
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeLogCode.CodeName");
            searchColumn.Add("vi_CodeLogLevel.CodeName");
            searchColumn.Add("vi_CodeLogInterval.CodeName");
            return searchColumn;
        }

        /// <summary>
        /// 設定 LogLevel 及 LogInterval
        /// </summary>
        /// <param name="model">日誌資料</param>
        public Logs SetLogLevelAndInterval(Logs model)
        {
            if (model != null)
            {
                using var logCode = new z_sqlLogCodes();
                var data = logCode.GetData(model.LogCode);
                if (data != null)
                {
                    model.LogLevel = data.LogLevel;
                    model.LogInterval = data.LogInterval;
                }
            }
            return model;
        }

        /// <summary>
        /// 加入日誌
        /// </summary>
        /// <param name="model">日誌資料</param>
        public void AddLog(Logs model)
        {
            string startTime = "";
            string endTime = "";

            switch (model.LogInterval)
            {
                case "Hour":
                    startTime = model.LogTime.GetValueOrDefault().ToString("yyyy-MM-dd HH") + ":00:00";
                    endTime = model.LogTime.GetValueOrDefault().ToString("yyyy-MM-dd HH") + ":59:59";
                    break;
                case "Day":
                    startTime = model.LogTime.GetValueOrDefault().ToString("yyyy-MM-dd") + " 00:00:00";
                    endTime = model.LogTime.GetValueOrDefault().ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "Month":
                    startTime = model.LogTime.GetValueOrDefault().ToString("yyyy-MM") + "-01 00:00:00";
                    endTime = model.LogTime.GetValueOrDefault().ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(model.LogTime.GetValueOrDefault().Year, model.LogTime.GetValueOrDefault().Month).ToString("00") + " 23:59:59";
                    break;
                case "Year":
                    startTime = model.LogTime.GetValueOrDefault().ToString("yyyy") + "-01-01 00:00:00";
                    endTime = model.LogTime.GetValueOrDefault().ToString("yyyy") + "-12-31 23:59:59";
                    break;
            }

            string sql_query = GetSQLSelect();
            sql_query += " WHERE Logs.LogTime between @LogTime1 AND @LogTime2 AND Logs.LogCode = @LogCode AND Logs.UserNo = @UserNo AND ";
            sql_query += " Logs.LogNo = @LogNo AND TargetNo = @TargetNo AND Logs.IpAddress = @IpAddress AND Logs.LogMessage = @LogMessage";

            DynamicParameters parm1 = new DynamicParameters();

            parm1.Add("LogTime1", startTime);
            parm1.Add("LogTime2", endTime);
            parm1.Add("LogCode", model.LogCode);
            parm1.Add("UserNo", model.UserNo);
            parm1.Add("LogNo", model.LogNo);
            parm1.Add("TargetNo", model.TargetNo);
            parm1.Add("IpAddress", model.IpAddress);
            parm1.Add("LogMessage", model.LogMessage);

            var logModel = dpr.ReadSingle<Logs>(sql_query, parm1);
            var parm2 = new DynamicParameters();
            if (logModel != null && logModel.Id > 0)
            {
                logModel.LogQty += model.LogQty;
                sql_query = "UPDATE Logs SET LogQty = @LogQty , LastDate = @LastDate, LastTime = @LastTime WHERE Id = @Id";

                parm2.Add("LastDate", logModel.LastDate);
                parm2.Add("LastTime", logModel.LastTime);
                parm2.Add("LogQty", logModel.LogQty);
                parm2.Add("Id", logModel.Id);
                dpr.Execute(sql_query, parm2);
            }
            else
            {
                sql_query = @"
INSERT INTO Logs (KeyNo, LogDate, LogTime, LastDate, LastTime, LogCode, LogLevel, LogInterval, LogMessage, UserNo, UserName, TargetNo, IpAddress, LogNo, LogQty, Remark)
VALUES 
(@KeyNo, @LogDate, @LogTime, @LastDate, @LastTime, @LogCode, @LogLevel, @LogInterval, @LogMessage, @UserNo, @UserName, @TargetNo, @IpAddress, @LogNo, @LogQty, @Remark)";
                string keyno = DateTime.Now.ToString("yyyyMMddHHmmssff") + Guid.NewGuid().ToString().Substring(0, 10);
                parm2.Add("KeyNo", keyno);
                parm2.Add("LogDate", model.LogDate);
                parm2.Add("LogTime", model.LogTime);
                parm2.Add("LastDate", model.LastDate);
                parm2.Add("LastTime", model.LastTime);
                parm2.Add("LogCode", model.LogCode);
                parm2.Add("LogLevel", model.LogLevel);
                parm2.Add("LogInterval", model.LogInterval);
                parm2.Add("LogMessage", model.LogMessage);
                parm2.Add("UserNo", model.UserNo);
                parm2.Add("UserName", model.UserName);
                parm2.Add("TargetNo", model.TargetNo);
                parm2.Add("IpAddress", model.IpAddress);
                parm2.Add("LogNo", model.LogNo);
                parm2.Add("LogQty", model.LogQty);
                parm2.Add("Remark", model.Remark);
                dpr.Execute(sql_query, parm2);
            }
        }
    }
}