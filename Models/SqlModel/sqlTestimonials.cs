using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlTestimonials : DapperSql<Testimonials>
    {
        public z_sqlTestimonials()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Testimonials.SendDate";
            DefaultOrderByDirection = "DESC";
            DropDownValueColumn = "Testimonials.Id";
            DropDownTextColumn = "Testimonials.Id";
            DropDownOrderColumn = "Testimonials.Id ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}