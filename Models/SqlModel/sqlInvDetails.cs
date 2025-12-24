using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace powererp.Models
{
    public class z_sqlInvDetails : DapperSql<InvDetails>
    {
        public z_sqlInvDetails()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "InvDetails.WareHouseNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "InvDetails.WareHouseNo";
            DropDownTextColumn = "InvDetails.WareHouseNo";
            DropDownOrderColumn = "InvDetails.WareHouseNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT InvDetails.Id, InvDetails.WareHouseNo, Warehouses.WarehouseName, 
InvDetails.ProductNo, Products.ProdName AS ProductName, InvDetails.Qty, InvDetails.Remark
FROM InvDetails 
LEFT OUTER JOIN Products ON InvDetails.ProductNo = Products.ProdNo 
LEFT OUTER JOIN Warehouses ON InvDetails.WareHouseNo = Warehouses.WarehouseNo
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("Products.ProdName");
            searchColumn.Add("Warehouses.WarehouseName");
            return searchColumn;
        }

        public int DeleteMasterData(string prodNo)
        {
            string str_sql = "DELETE FROM InvDetails WHERE ProductNo = @prodNo ";
            var parm = new DynamicParameters();
            parm.Add("@prodNo", prodNo);
            return dpr.Execute(str_sql, parm);
        }

        public List<InvDetails> GetWarehouseDataList(string WarehouseNo)
        {
            string str_sql = GetSQLSelect();
            str_sql += "WHERE InvDetails.WareHouseNo = @WareHouseNo ";
            str_sql += GetSQLOrderBy();
            var parm = new DynamicParameters();
            parm.Add("@WareHouseNo", WarehouseNo);
            return dpr.ReadAll<InvDetails>(str_sql, parm);
        }

        /// <summary>
        /// 取得指定商品編號截止日期前 12 月的庫存異動數量統計
        /// </summary>
        /// <param name="codeNo">庫存類別</param>
        /// <param name="prodNo">商品編號</param>
        /// <param name="endDate">截止日期</param>
        /// <returns></returns>
        public List<int> GetProductYearQty(string codeNo, string prodNo, string endDate)
        {
            var DateEnd = DateTime.Parse(endDate);

            var model = new List<int>();
            int int_index = 0;
            int int_qty = 0;
            string str_select = "";
            string str_from = "FROM Inventorys INNER JOIN InventorysDetail ON Inventorys.BaseNo = InventorysDetail.ParentNo ";
            string str_where = "";
            string str_sql = "";
            for (int i = 0; i < 12; i++)
            {
                int_index = i * -1;
                var month = DateEnd.AddMonths(int_index).ToString("yyyyMM") + "%";
                str_select = $"SELECT SUM(CASE Inventorys.SheetCode WHEN '{codeNo}' THEN InventorysDetail.Qty ELSE 0 END) Qty ";
                str_where = $"WHERE InventorysDetail.ProductNo = '{prodNo}' AND Inventorys.SheetNo LIKE '{month}'";
                str_sql = str_select + str_from + str_where;
                int_qty = 0;
                var data = dpr.ReadSingle<InventorysDetail>(str_sql);
                if (data != null) int_qty = data.Qty;
                model.Add(int_qty);
            }
            return model;
        }
    }
}