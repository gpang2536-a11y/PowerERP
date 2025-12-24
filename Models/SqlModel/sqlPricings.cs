using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlPricings : DapperSql<Pricings>
    {
        public z_sqlPricings()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Pricings.SortNo,Pricings.PricingNo";
            DefaultOrderByDirection = "ASC,ASC";
            DropDownValueColumn = "Pricings.PricingNo";
            DropDownTextColumn = "Pricings.PricingName";
            DropDownOrderColumn = "Pricings.SortNo ASC,Pricings.PricingNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public void SetParent(int id, int page)
        {
            var model = GetData(id);
            SessionService.ParentId = id;
            SessionService.ParentNo = model.PricingNo;
            SessionService.ParentName = model.PricingName;
            SessionService.ParentPage = page;
        }
    }
}
