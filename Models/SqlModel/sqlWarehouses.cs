using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlWarehouses : DapperSql<Warehouses>
    {
        public z_sqlWarehouses()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Warehouses.WarehouseNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Warehouses.WarehouseNo";
            DropDownTextColumn = "Warehouses.WarehouseName";
            DropDownOrderColumn = "Warehouses.WarehouseNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Id, WarehouseNo, WarehouseName,Remark ,
(SELECT SUM(InvDetails.Qty) FROM InvDetails WHERE WarehouseNo = Warehouses.WarehouseNo) AS Qty 
FROM Warehouses 
";
            return AddRowNoColumn(str_query);
        }

        /// <summary>
        /// 取得目前主檔資料
        /// </summary>
        /// <returns></returns>
        public Warehouses GetMasterData()
        {
            var model = GetAllData().Where(x => x.RowNo == SessionService.PageMaster).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 以 BaseNo 設定目前的記錄值
        /// </summary>
        /// <param name="baseno"></param>
        public override void SetBaseNo(string baseno)
        {
            var models = GetAllData();
            var model = models.Where(x => x.WarehouseNo == baseno).FirstOrDefault();
            SessionService.PageMaster = model.RowNo;
            SetMasterPage();
        }
    }
}