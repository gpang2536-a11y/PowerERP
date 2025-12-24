using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlInventorys : DapperSql<Inventorys>
    {
        public z_sqlInventorys()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Inventorys.SheetNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Inventorys.SheetNo";
            DropDownTextColumn = "Inventorys.SheetNo";
            DropDownOrderColumn = "Inventorys.SheetNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Inventorys.Id, Inventorys.BaseNo, Inventorys.IsConfirm , Inventorys.IsCancel , 
Inventorys.TypeNo, InventorysType.TypeName , Inventorys.SheetCode, 
CASE Inventorys.SheetCode WHEN 'I' THEN '入庫' ELSE '出庫' END AS CodeName, 
Inventorys.SheetNo, Inventorys.SheetDate, Inventorys.WarehouseNo, Warehouses.WarehouseName, 
Inventorys.TargetNo, Inventorys.TargetName, Inventorys.HandleNo, Users.UserName AS HandleName, 
Inventorys.Remark 
FROM Inventorys 
LEFT OUTER JOIN Users ON Inventorys.HandleNo = Users.UserNo 
LEFT OUTER JOIN Warehouses ON Inventorys.WarehouseNo = Warehouses.WarehouseNo 
LEFT OUTER JOIN InventorysType ON Inventorys.TypeNo = InventorysType.TypeNo 
";
            return AddRowNoColumn(str_query);
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("InventorysType.TypeName");
            searchColumn.Add("Warehouses.WarehouseName");
            searchColumn.Add("Users.UserName");
            return searchColumn;
        }

        /// <summary>
        /// 取得 SQL 的 Where 條件字串
        /// </summary>
        /// <returns></returns>
        public override string GetSQLWhere()
        {
            string str_where = $" WHERE Inventorys.TypeNo = '{SessionService.BaseNo}' ";
            return str_where;
        }

        /// <summary>
        /// 設定主檔目前記錄位置
        /// </summary>
        public override void SetMasterPage()
        {
            var models = GetAllData();
            var model = GetMasterData();
            SessionService.MasterId = (model != null) ? model.Id : 0;
            SessionService.MasterNo = (model != null) ? model.SheetNo : "";
            SessionService.MasterBaseNo = (model != null) ? model.BaseNo : "";
            SessionService.PageMaster = (model != null) ? model.RowNo : 0;
            SessionService.PageCountMaster = (model != null) ? models.Count() : 0;
        }

        /// <summary>
        /// 以 BaseNo 設定目前的記錄值
        /// </summary>
        /// <param name="baseno"></param>
        public override void SetBaseNo(string baseno)
        {
            var models = GetAllData();
            var model = models.Where(x => x.BaseNo == baseno).FirstOrDefault();
            SessionService.PageMaster = model.RowNo;
            SetMasterPage();
        }

        /// <summary>
        /// 取得指定記錄 Id的單筆資料
        /// </summary>
        /// <param name="id">記錄 Id</param>
        /// <returns></returns>
        public Inventorys GetMasterData(int id)
        {
            SessionService.PageMaster = id;
            var model = GetMasterData();
            SetMasterPage();
            return model;
        }

        /// <summary>
        /// 取得目前主檔資料
        /// </summary>
        /// <returns></returns>
        public Inventorys GetMasterData()
        {
            var model = GetAllData().Where(x => x.RowNo == SessionService.PageMaster).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 確認單據
        /// </summary>
        /// <param name="id">單據 ID</param>
        public bool Confirm(int id)
        {
            ErrorMessage = "";
            var model = GetData(id);
            if (model == null) { ErrorMessage = "找不到指定的單據資料!!"; return false; }
            if (model.IsConfirm) { ErrorMessage = "單據已經過確認，無法重覆確認!!"; return false; }
            if (model.IsCancel) { ErrorMessage = "單據已經過作廢，無法確認!!"; return false; }

            using var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            string sql_query = $"UPDATE Inventorys SET IsConfirm = @IsConfirm WHERE Inventorys.Id = @Id";
            parm.Add("Id", id);
            parm.Add("IsConfirm", true);
            AffectedRows = dpr.Execute(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
            return (AffectedRows > 0);
        }

        /// <summary>
        /// 取消確認單據
        /// </summary>
        /// <param name="id">單據 ID</param>
        public bool Undo(int id)
        {
            ErrorMessage = "";
            var model = GetData(id);
            if (model == null) { ErrorMessage = "找不到指定的單據資料!!"; return false; }
            if (!model.IsConfirm) { ErrorMessage = "單據未確認，無法取消確認!!"; return false; }
            if (model.IsCancel) { ErrorMessage = "單據已經過作廢，無法確認!!"; return false; }

            using var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            string sql_query = $"UPDATE Inventorys SET IsConfirm = @IsConfirm WHERE Inventorys.Id = @Id";
            parm.Add("Id", id);
            parm.Add("IsConfirm", false);
            AffectedRows = dpr.Execute(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
            return (AffectedRows > 0);
        }

        /// <summary>
        /// 作廢單據
        /// </summary>
        /// <param name="id">單據 ID</param>
        public bool Cancel(int id)
        {
            ErrorMessage = "";
            var model = GetData(id);
            if (model == null) { ErrorMessage = "找不到指定的單據資料!!"; return false; }
            if (model.IsCancel) { ErrorMessage = "單據已經過作廢，無法再次作廢!!"; return false; }

            using var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            string sql_query = $"UPDATE Inventorys SET IsCancel = @IsCancel WHERE Inventorys.Id = @Id";
            parm.Add("Id", id);
            parm.Add("IsCancel", true);
            AffectedRows = dpr.Execute(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
            return (AffectedRows > 0);
        }
    }
}