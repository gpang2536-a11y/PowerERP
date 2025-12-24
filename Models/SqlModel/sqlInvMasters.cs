using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace powererp.Models
{
    public class z_sqlInvMasters : DapperSql<InvMasters>
    {
        public z_sqlInvMasters()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "InvMasters.ProductNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "InvMasters.ProductNo";
            DropDownTextColumn = "Products.ProdName";
            DropDownOrderColumn = "InvMasters.ProductNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT  InvMasters.Id, InvMasters.BaseNo, InvMasters.ProductNo, Products.ProdName AS ProductName, InvMasters.Qty, 
InvMasters.Remark
FROM InvMasters 
LEFT OUTER JOIN Products ON InvMasters.ProductNo = Products.ProdNo 
";
            return AddRowNoColumn(str_query);
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("Products.ProdName");
            return searchColumn;
        }

        /// <summary>
        /// 設定主檔目前記錄位置
        /// </summary>
        public override void SetMasterPage()
        {
            var models = GetAllData();
            var model = GetMasterData();
            SessionService.MasterId = (model != null) ? model.Id : 0;
            SessionService.MasterNo = (model != null) ? model.ProductNo : "";
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
        public InvMasters GetMasterData(int id)
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
        public InvMasters GetMasterData()
        {
            var model = GetAllData().Where(x => x.RowNo == SessionService.PageMaster).FirstOrDefault();
            return model;
        }
    }
}