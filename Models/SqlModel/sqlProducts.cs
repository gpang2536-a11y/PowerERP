using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlProducts : DapperSql<Products>
    {
        public z_sqlProducts()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Products.ProdNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Products.ProdNo";
            DropDownTextColumn = "Products.ProdName";
            DropDownOrderColumn = "Products.ProdNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT  Products.Id, Products.IsEnabled, Products.IsInventory, Products.IsShowPhoto, Products.ProdNo, 
Products.ProdName, Products.BrandName, Products.BrandSeriesName, Products.BarcodeNo, Products.StatusNo, 
ProductStatus.StatusName, Products.VendorNo, Vendors.ShortName AS VendorName, Products.CategoryNo, 
Categorys.CategoryName, Products.CostPrice, Products.MarketPrice, Products.SalePrice, Products.DiscountPrice, 
Products.InventoryQty, Products.ContentText, Products.SpecificationText, Products.Remark
FROM Products 
LEFT OUTER JOIN Categorys ON Products.CategoryNo = Categorys.CategoryNo 
LEFT OUTER JOIN Vendors ON Products.VendorNo = Vendors.CompNo 
LEFT OUTER JOIN ProductStatus ON Products.StatusNo = ProductStatus.StatusNo
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("ProductStatus.StatusName");
            searchColumn.Add("Vendors.ShortName");
            searchColumn.Add("Categorys.CategoryName");
            return searchColumn;
        }
    }
}