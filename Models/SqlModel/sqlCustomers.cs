using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlCustomers : DapperSql<Customers>
    {
        public z_sqlCustomers()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Customers.CompNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Customers.CompNo";
            DropDownTextColumn = "Customers.ShortName";
            DropDownOrderColumn = "Customers.CompNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Customers.Id, Customers.IsDefault, Customers.IsEnabled, Customers.CodeNo, vi_CodeCustomer.CodeName, 
Customers.CompNo, Customers.CompName, Customers.ShortName, Customers.EngName, Customers.EngShortName,
Customers.RegisterDate, Customers.BossName, Customers.ContactName, Customers.CompTel, 
Customers.ContactTel, Customers.CompFax, Customers.CompID, Customers.ContactEmail, Customers.CompAddress, 
Customers.CompUrl, Customers.TwitterUrl, Customers.FacebookUrl, Customers.InstagramUrl, Customers.SkypeUrl, 
Customers.LinkedinUrl, Customers.Latitude, Customers.Longitude, Customers.AboutusText, Customers.SupportText, 
Customers.ReturnText, Customers.ShippingText, Customers.PaymentText, Customers.Remark
FROM Customers 
LEFT OUTER JOIN vi_CodeCustomer ON Customers.CodeNo = vi_CodeCustomer.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeCustomer.CodeName");
            return searchColumn;
        }
    }
}