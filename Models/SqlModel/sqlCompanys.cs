using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlCompanys : DapperSql<Companys>
    {
        public z_sqlCompanys()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Companys.CompNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Companys.CompNo";
            DropDownTextColumn = "Companys.ShortName";
            DropDownOrderColumn = "Companys.CompNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Companys.Id, Companys.IsDefault, Companys.IsEnabled, Companys.CodeNo, vi_CodeCompany.CodeName, 
Companys.CompNo, Companys.CompName, Companys.ShortName, Companys.EngName, Companys.EngShortName,
Companys.RegisterDate, Companys.BossName, Companys.ContactName, Companys.CompTel, 
Companys.ContactTel, Companys.CompFax, Companys.CompID, Companys.ContactEmail, Companys.CompAddress, 
Companys.CompUrl, Companys.TwitterUrl, Companys.FacebookUrl, Companys.InstagramUrl, Companys.SkypeUrl, 
Companys.LinkedinUrl, Companys.Latitude, Companys.Longitude, Companys.AboutusText, Companys.SupportText, 
Companys.ReturnText, Companys.ShippingText, Companys.PaymentText, Companys.Remark
FROM Companys 
LEFT OUTER JOIN vi_CodeCompany ON Companys.CodeNo = vi_CodeCompany.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeCompany.CodeName");
            return searchColumn;
        }

        /// <summary>
        /// 取得修改資料
        /// </summary>
        /// <param name="id">公司 Id</param>
        /// <param name="tab">欄位類別</param>
        public Companys GetEditor(int id, string tab)
        {
            var model = GetData(id);
            if (tab == "Aboutus") model.Remark = model.AboutusText;
            else if (tab == "Support") model.Remark = model.SupportText;
            else if (tab == "Return") model.Remark = model.ReturnText;
            else if (tab == "Shipping") model.Remark = model.ShippingText;
            else if (tab == "Payment") model.Remark = model.PaymentText;
            else model.Remark = "";
            return model;
        }

        /// <summary>
        /// 設定修改資料 
        /// </summary>
        /// <param name="model">修改資料</param>
        /// <param name="tab">欄位類別</param>
        public void SetEditor(Companys model, string tab)
        {
            string columnName = "";
            if (tab == "Aboutus") columnName = "AboutusText";
            else if (tab == "Support") columnName = "SupportText";
            else if (tab == "Return") columnName = "ReturnText";
            else if (tab == "Shipping") columnName = "ShippingText";
            else if (tab == "Payment") columnName = "PaymentText";

            using var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            string sql_query = $"UPDATE Companys SET {columnName} = @DataText WHERE Companys.Id = @Id";
            parm.Add("Id", model.Id);
            parm.Add("DataText", model.Remark);
            dpr.Execute(sql_query, parm);
        }
        /// <summary>
        /// 取得預設公司資料
        /// </summary>
        public Companys GetDefaultCompany()
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Companys.IsDefault = @IsDefault AND Companys.IsEnabled = @IsEnabled ";
            var parm = new DynamicParameters();
            parm.Add("IsDefault", true);
            parm.Add("IsEnabled", true);
            var model = dpr.ReadSingle<Companys>(sql_query, parm);
            if (model == null || model.Id == 0)
            {
                model = new Companys()
                {
                    IsDefault = true,
                    IsEnabled = true,
                    CompNo = "001",
                    CompName = "好好用公司",
                    CompTel = "02-12345678",
                    ContactEmail = "service@gmail.com",
                    CompAddress = "台北市中正區重慶南路一段122號"
                };
            }
            return model;
        }
    }
}