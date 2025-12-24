using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class vmUINVP004_WarehouseStatus : BaseClass
    {
        /// <summary>
        /// UINVP002 - 進貨入庫單表頭檔
        /// </summary>
        public Warehouses MasterModel { get; set; } = new Warehouses();
        /// <summary>
        /// UINVP002 - 進貨入庫單明細檔
        /// </summary>
        public IPagedList<InvDetails> DetailModel { get; set; } = new StaticPagedList<InvDetails>(Enumerable.Empty<InvDetails>(), 1, 10, 0);

        public vmUINVP004_WarehouseStatus()
        {
            using var sqlMaster = new z_sqlWarehouses();
            using var sqlDetail = new z_sqlInvDetails();
            MasterModel = sqlMaster.GetMasterData();
            if (MasterModel == null) MasterModel = new Warehouses();
            string baseNo = (MasterModel == null) ? "" : MasterModel.WarehouseNo;
            var model = sqlDetail.GetWarehouseDataList(baseNo);
            if (model == null)
                DetailModel ??= new StaticPagedList<InvDetails>(Enumerable.Empty<InvDetails>(), 1, 10, 0);
            else
                DetailModel = model.ToPagedList(SessionService.PageDetail, SessionService.PageDetailSize);
        }
    }
}