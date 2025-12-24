using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlVendors : DapperSql<Vendors>
    {
        public z_sqlVendors()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Vendors.CompNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Vendors.CompNo";
            DropDownTextColumn = "Vendors.ShortName";
            DropDownOrderColumn = "Vendors.CompNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Vendors.Id, Vendors.IsDefault, Vendors.IsEnabled, Vendors.CodeNo, vi_CodeVendor.CodeName, 
Vendors.CompNo, Vendors.CompName, Vendors.ShortName, Vendors.EngName, Vendors.EngShortName,
Vendors.RegisterDate, Vendors.BossName, Vendors.ContactName, Vendors.CompTel, 
Vendors.ContactTel, Vendors.CompFax, Vendors.CompID, Vendors.ContactEmail, Vendors.CompAddress, 
Vendors.CompUrl, Vendors.TwitterUrl, Vendors.FacebookUrl, Vendors.InstagramUrl, Vendors.SkypeUrl, 
Vendors.LinkedinUrl, Vendors.Latitude, Vendors.Longitude, Vendors.AboutusText, Vendors.SupportText, 
Vendors.ReturnText, Vendors.ShippingText, Vendors.PaymentText, Vendors.Remark
FROM Vendors 
LEFT OUTER JOIN vi_CodeVendor ON Vendors.CodeNo = vi_CodeVendor.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeVendor.CodeName");
            return searchColumn;
        }
    }
}