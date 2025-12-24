using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlPricingDetails : DapperSql<PricingDetails>
    {
        public z_sqlPricingDetails()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "PricingDetails.SortNo,PricingDetails.ItemName";
            DefaultOrderByDirection = "ASC,ASC";
            DropDownValueColumn = "PricingDetails.ItemName";
            DropDownTextColumn = "PricingDetails.ItemName";
            DropDownOrderColumn = "PricingDetails.SortNo ASC,PricingDetails.ItemName ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public List<PricingDetails> GetDataList(string baseNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<PricingDetails>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE PricingNo = @BaseNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("BaseNo", baseNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<PricingDetails>(sql_query, parm);
            return model;
        }

        public override List<PricingDetails> GetDataList(string pricingNo)
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE PricingNo=@PricingNo ";
            sql_query += " ORDER BY " + DropDownOrderColumn;
            var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            parm.Add("@PricingNo", pricingNo);
            var model = dpr.ReadAll<PricingDetails>(sql_query, parm);
            return model;
        }
    }
}
