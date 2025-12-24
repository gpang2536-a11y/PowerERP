using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class vmUINVP003_InvStatus : BaseClass
    {
        /// <summary>
        /// UINVP002 - 進貨入庫單表頭檔
        /// </summary>
        public InvMasters MasterModel { get; set; } = new InvMasters();
        /// <summary>
        /// UINVP002 - 進貨入庫單明細檔
        /// </summary>
        public List<InvDetails> DetailModel { get; set; } = new List<InvDetails>();

        public vmUINVP003_InvStatus()
        {
            using var sqlMaster = new z_sqlInvMasters();
            using var sqlDetail = new z_sqlInvDetails();
            MasterModel = sqlMaster.GetMasterData();
            if (MasterModel == null) MasterModel = new InvMasters();
            string baseNo = (MasterModel == null) ? "" : MasterModel.ProductNo;
            DetailModel = sqlDetail.GetDataList(baseNo);
            if (DetailModel == null) DetailModel = new List<InvDetails>();
        }
    }
}