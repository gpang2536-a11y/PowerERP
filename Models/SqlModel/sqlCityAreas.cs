namespace powererp.Models
{
    public class z_sqlCityAreas : DapperSql<CityAreas>
    {
        public z_sqlCityAreas()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "CityAreas.AreaName";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "CityAreas.AreaName";
            DropDownTextColumn = "CityAreas.AreaName";
            DropDownOrderColumn = "CityAreas.AreaName ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public List<CityAreas> GetDataList(string cityName, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<CityAreas>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE CityAreas.CityName = @BaseNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("BaseNo", cityName);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<CityAreas>(sql_query, parm);
            return model;
        }

        public List<CityAreas> GetCityAreaList(string cityName)
        {

            string str_query = GetSQLSelect();
            str_query += " WHERE CityAreas.CityName = @CityName ";
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("CityName", cityName);
            var model = dpr.ReadAll<CityAreas>(str_query, parm);
            return model;
        }

        public override List<SelectListItem> GetDropDownList(string cityName)
        {
            string str_query = "SELECT AreaName AS Value, AreaName AS Text FROM CityAreas WHERE CityName = @CityName ORDER BY AreaName";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("CityName", cityName);
            var model = dpr.ReadAll<SelectListItem>(str_query, parm);
            return model;
        }
    }
}
