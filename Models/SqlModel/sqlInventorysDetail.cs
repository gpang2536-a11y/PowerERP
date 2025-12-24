using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlInventorysDetail : DapperSql<InventorysDetail>
    {
        public z_sqlInventorysDetail()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "InventorysDetail.ProductNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "InventorysDetail.ProductNo";
            DropDownTextColumn = "InventorysDetail.ProductNo";
            DropDownOrderColumn = "InventorysDetail.ProductNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT InventorysDetail.Id, InventorysDetail.ParentNo, InventorysDetail.ProductNo, 
Products.ProdName,  Inventorys.WarehouseNo, InventorysDetail.Qty
FROM InventorysDetail 
INNER JOIN Inventorys ON InventorysDetail.ParentNo = Inventorys.BaseNo 
LEFT OUTER JOIN Products ON InventorysDetail.ProductNo = Products.ProdNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("Products.ProdName");
            return searchColumn;
        }

        public override List<InventorysDetail> GetDataList(string ParentNo)
        {
            string str_sql = GetSQLSelect();
            str_sql += "WHERE InventorysDetail.ParentNo = @ParentNo ";
            str_sql += GetSQLOrderBy();
            var parm = new DynamicParameters();
            parm.Add("@ParentNo", ParentNo);
            return dpr.ReadAll<InventorysDetail>(str_sql, parm);
        }

        public int DeleteMasterData(int id)
        {
            string str_sql = "DELETE FROM InventorysDetail WHERE ParentId = @Id ";
            var parm = new DynamicParameters();
            parm.Add("@Id", id);
            return dpr.Execute(str_sql, parm);
        }

        /// <summary>
        /// 取得指定月份的每日庫存異動數量統計
        /// </summary>
        /// <param name="typeNo">庫存類別</param>
        /// <param name="sheetDate">庫存日期</param>
        /// <returns></returns>
        public List<int> GetInventoryDailyQty(string typeNo, string sheetDate)
        {
            DateTime sheetDateTime = DateTime.Parse(sheetDate);
            string sheetMonth = sheetDateTime.ToString("yyyyMM") + "%";
            var model = new List<int>();
            string str_sql = @"
SELECT  RIGHT(LEFT(SheetNo,8),2)  AS ProductNo, SUM(InventorysDetail.Qty) AS Qty
FROM  Inventorys 
INNER JOIN InventorysDetail ON Inventorys.BaseNo = InventorysDetail.ParentNo
WHERE   (Inventorys.TypeNo = @TypeNo) AND SheetNo LIKE @SheetDate
GROUP BY RIGHT(LEFT(SheetNo,8),2) 
ORDER BY RIGHT(LEFT(SheetNo,8),2) ASC
";
            var parm = new DynamicParameters();
            parm.Add("@TypeNo", typeNo);
            parm.Add("@SheetDate", sheetMonth);
            var list = dpr.ReadAll<InventorysDetail>(str_sql, parm);
            int daysInMonth = DateTime.DaysInMonth(sheetDateTime.Year, sheetDateTime.Month);

            if (list != null && list.Count > 0)
            {
                for (int i = 1; i <= daysInMonth; i++)
                {
                    var item = list.Where(x => x.ProductNo == i.ToString("00")).FirstOrDefault();
                    if (item != null)
                        model.Add(item.Qty);
                    else
                        model.Add(0);
                }
            }
            else
            {
                for (int i = 1; i <= daysInMonth; i++)
                {
                    model.Add(0);
                }
            }
            return model;
        }
    }
}