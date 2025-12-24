using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class vmUINVP001_Inventory : BaseClass
    {
        /// <summary>
        /// UINVP002 - 進貨入庫單表頭檔
        /// </summary>
        public Inventorys MasterModel { get; set; } = new Inventorys();
        /// <summary>
        /// UINVP002 - 進貨入庫單明細檔
        /// </summary>
        public List<InventorysDetail> DetailModel { get; set; } = new List<InventorysDetail>();

        public vmUINVP001_Inventory()
        {
            using var sqlInventorys = new z_sqlInventorys();
            using var sqlInventorysDetail = new z_sqlInventorysDetail();
            MasterModel = sqlInventorys.GetMasterData();
            if (MasterModel == null) MasterModel = new Inventorys();
            string baseNo = (MasterModel == null) ? "" : MasterModel.BaseNo;
            DetailModel = sqlInventorysDetail.GetDataList(baseNo);
            if (DetailModel == null) DetailModel = new List<InventorysDetail>();
        }
    }
}