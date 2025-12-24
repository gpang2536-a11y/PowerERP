using Dapper;
using iText.Kernel.Crypto.Securityhandler;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// 使用 Dapper 執行 SQL 指令的類別
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class DapperSql<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TEntity> : BaseClass where TEntity : class
{
    #region 建構子(Constructor)
    /// <summary>
    /// 建構子
    /// </summary>
    public DapperSql()
    {
        OrderByColumn = SessionService.SortColumn;
        OrderByDirection = SessionService.SortDirection;
        if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
        if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
    }
    #endregion
    #region 屬性(Property)
    /// <summary>
    /// Dapper 物件
    /// </summary>
    /// <returns></returns>
    public DapperRepository dpr = new DapperRepository();
    /// <summary>
    /// Entity Object
    /// </summary>
    /// <returns></returns>
    public TEntity EntityObject => (TEntity)Activator.CreateInstance(typeof(TEntity)) ?? throw new InvalidOperationException($"Cannot create instance of type {typeof(TEntity).Name}");
    /// <summary>
    /// Entity Name
    /// </summary>
    /// <returns></returns>
    public string EntityName { get { return typeof(TEntity).Name; } }
    /// <summary>
    /// 影響筆數
    /// </summary>
    /// <value></value>
    public int AffectedRows { get; set; } = 0;
    /// <summary>
    /// 連線字串名稱
    /// </summary>
    public string ConnName { get; set; } = "dbconn";
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public string ErrorMessage { get; set; } = "";
    /// <summary>
    /// 預設 SQL 排序指令
    /// </summary>
    public virtual string DefaultOrderByColumn { get; set; } = "";
    /// <summary>
    /// 預設 SQL 排序方式指令
    /// </summary>
    public virtual string DefaultOrderByDirection { get; set; } = "";
    public virtual string DropDownValueColumn { get; set; } = "";
    public virtual string DropDownTextColumn { get; set; } = "";
    public virtual string DropDownOrderColumn { get; set; } = "";
    /// <summary>
    /// OrderBy 排序指令
    /// </summary>
    /// <value></value>
    public string OrderByColumn { get; set; } = "";
    /// <summary>
    /// OrderBy 排序方式
    /// </summary>
    public string OrderByDirection { get; set; } = "";
    /// <summary>
    /// 模糊搜尋的欄位集合
    /// </summary>
    public List<string> SearchColumns { get; set; } = new List<string>();
    #endregion
    #region SQL 查詢指令相關的函數
    /// <summary>
    /// 為 SQL 查詢指令加入資料列編號欄位
    /// </summary>
    /// <param name="sqlQuery">SQL SELECT 查詢指令</param>
    public string AddRowNoColumn(string sqlQuery)
    {
        string str_orderby = GetSQLOrderBy();
        string str_rowno = $"SELECT ROW_NUMBER() OVER({str_orderby}) AS RowNo, ";
        sqlQuery = sqlQuery.Trim();
        string str_query = sqlQuery.Substring(6, sqlQuery.Length - 6);
        str_query = str_rowno + " " + str_query;
        return str_query;
    }
    /// <summary>
    /// 取得與設定 SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLSelect()
    {
        //自動由表格 Class 產生 SQL 查詢指令
        string str_query = "";
        str_query = dpr.GetSQLSelectCommand(EntityObject);
        return str_query;
    }
    /// <summary>
    /// 取得與設定明細 SQL 查詢欄位及表格指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLDetailSelect()
    {
        //自動由表格 Class 產生 SQL 查詢指令
        string str_query = "";
        str_query = dpr.GetSQLSelectCommand(EntityObject);
        return str_query;
    }
    /// <summary>
    /// 取得刪除詳細資料的 SQL 指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLDetailDelete()
    {
        string str_query = "";
        return str_query;
    }
    /// <summary>
    /// 取得與設定 SQL 查詢條件式指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLWhere()
    {
        string str_query = "";
        return str_query;
    }
    /// <summary>
    /// 取得與設定 SQL 查詢排序指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLOrderBy()
    {
        string str_query = " ORDER BY ";
        if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
        if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        if (OrderByDirection.IndexOf(',') == 0)
        {
            str_query += OrderByColumn;
            if (!string.IsNullOrEmpty(OrderByDirection)) str_query += $" {OrderByDirection}";
        }
        else
        {
            List<string> lst_column = OrderByColumn.Split(',').ToList();
            List<string> lst_order = OrderByDirection.Split(',').ToList();
            if (lst_column.Count == lst_order.Count)
            {
                for (int i = 0; i < lst_column.Count; i++)
                {
                    str_query += lst_column[i];
                    str_query += " ";
                    str_query += lst_order[i];
                    if (i < lst_column.Count - 1) str_query += ", ";
                }
            }
            else
            {
                str_query += OrderByColumn;
                if (!string.IsNullOrEmpty(OrderByDirection)) str_query += $" {OrderByDirection}";
            }
        }
        return str_query;

    }
    #endregion
    #region SQL 增刪改指令相關的函數
    /// <summary>
    /// 取得與設定 SQL 新增指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLInsert()
    {
        //自動由表格 Class 產生 Insert 查詢指令
        return dpr.GetSQLInsertCommand(EntityObject);
    }
    /// <summary>
    /// 取得與設定 SQL 刪除指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLDelete()
    {
        //自動由表格 Class 產生 Delete 查詢指令
        return dpr.GetSQLDeleteCommand(EntityObject);
    }
    /// <summary>
    /// 取得與設定 SQL 修改指令
    /// </summary>
    /// <returns></returns>
    public virtual string GetSQLUpdate()
    {
        //自動由表格 Class 產生 Update 查詢指令
        return dpr.GetSQLUpdateCommand(EntityObject);
    }
    #endregion
    #region 資料查詢相關的函數
    /// <summary>
    /// 取得與設定模糊搜尋的欄位集合
    /// </summary>
    /// <returns></returns>
    public virtual List<string> GetSearchColumns()
    {
        //由系統自動取得文字欄位的集合
        SearchColumns = dpr.GetStringColumnList(EntityObject);
        return SearchColumns;
    }
    /// <summary>
    ///  取得與設定下拉式選單資料集
    /// </summary>
    /// <param name="sqlQuery">SQL 語法</param>
    /// <returns></returns>
    public virtual List<SelectListItem> GetDropDownList(string sqlQuery)
    {
        var model = dpr.ReadAll<SelectListItem>(sqlQuery);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    /// <summary>
    ///  取得與設定下拉式選單資料集
    /// </summary>
    /// <param name="sqlQuery">SQL 語法</param>
    /// <param name="parm">SQL 參數</param>
    /// <returns></returns>
    public virtual List<SelectListItem> GetDropDownList(string sqlQuery, DynamicParameters parm)
    {
        var model = dpr.ReadAll<SelectListItem>(sqlQuery, parm);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    /// <summary>
    /// 取得與設定下拉式選單資料集
    /// </summary>
    /// <param name="textIncludeValue">顯示欄位名稱是否顯示資料欄位</param>
    /// <returns></returns>
    public virtual List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
    {
        string str_query = "SELECT ";
        if (textIncludeValue) str_query += $"{DropDownValueColumn} + ' ' + ";
        str_query += $"{DropDownTextColumn} AS Text , {DropDownValueColumn} AS Value FROM {EntityName} ORDER BY {DropDownOrderColumn}";
        var model = dpr.ReadAll<SelectListItem>(str_query);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    /// <summary>
    /// 取得與設定下拉式選單資料集
    /// </summary>
    /// <param name="valueColumn">資料欄位名稱</param>
    /// <param name="textColumn">顯示欄位名稱</param>
    /// <param name="orderColumn">排序欄位名稱</param>/// 
    /// <param name="textIncludeValue">顯示欄位名稱是否顯示資料欄位</param>
    /// <returns></returns>
    public virtual List<SelectListItem> GetDropDownList(string valueColumn, string textColumn, string orderColumn, bool textIncludeValue = false)
    {
        string str_query = "SELECT ";
        if (textIncludeValue) str_query += $"{valueColumn} + ' ' + ";
        str_query += $"{textColumn} AS Text , {valueColumn} AS Value FROM {EntityName} ORDER BY {orderColumn}";
        var model = dpr.ReadAll<SelectListItem>(str_query);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    #endregion
    #region 取得資料相關的函數(同步呼叫)
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual TEntity GetData(int id)
    {
        var model = (TEntity)Activator.CreateInstance(typeof(TEntity));
        if (id > 0)
        {
            string sql_query = GetSQLSelect();
            sql_query += dpr.GetSQLSelectWhereById(model);
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);
            model = dpr.ReadSingle<TEntity>(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
        }
        return model;
    }
    /// <summary>
    /// 取得單筆資料(同步呼叫)
    /// </summary>
    /// <param name="valueNo">要查詢的欄位值</param>
    /// <returns></returns>
    public virtual TEntity GetData(string valueNo)
    {
        string sql_query = GetSQLSelect();
        sql_query += $" WHERE {DropDownValueColumn} = @ValueNo";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("ValueNo", valueNo);
        var model = dpr.ReadSingle<TEntity>(sql_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList()
    {
        DynamicParameters parm = new DynamicParameters();
        var model = GetDataList(parm, "");
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(DynamicParameters parm)
    {
        var model = GetDataList(parm, "");
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(string searchString)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = GetDataList(parm, searchString);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(同步呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual List<TEntity> GetDataList(DynamicParameters parm, string searchString)
    {
        List<string> searchColumns = GetSearchColumns();
        var model = new List<TEntity>();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString) && searchColumns.Count() > 0)
            sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
        sql_query += GetSQLOrderBy();
        if (parm.ParameterNames.Count() > 0)
            model = dpr.ReadAll<TEntity>(sql_query, parm);
        else
            model = dpr.ReadAll<TEntity>(sql_query);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    /// <summary>
    /// 取得表單表頭單筆資料(同步呼叫)
    /// </summary>
    /// <param name="id">第幾筆</param>
    /// <param name="condition">條件式</param>
    /// <returns></returns>
    public virtual TEntity GetDataMaster(int id, string condition = "")
    {
        var model = (TEntity)Activator.CreateInstance(typeof(TEntity));
        if (id > 0)
        {
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += dpr.GetSQLSelectWhereById(model);
            if (!string.IsNullOrEmpty(condition))
            {
                if (sql_query.IndexOf(" WHERE ") > 0)
                    sql_query += " AND " + condition;
                else
                    sql_query += " WHERE " + condition;
            }
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);
            model = dpr.ReadSingle<TEntity>(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
        }
        return model;
    }

    /// <summary>
    /// 以 BaseNo 設定目前的記錄值
    /// </summary>
    /// <param name="baseno"></param>
    public virtual void SetBaseNo(string baseno)
    {

    }
    #endregion
    #region 取得資料相關的函數(非同步呼叫)
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetDataAsync(int id)
    {
        var model = (TEntity)Activator.CreateInstance(typeof(TEntity));
        if (id > 0)
        {
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += dpr.GetSQLSelectWhereById(model);
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);
            model = await dpr.ReadSingleAsync<TEntity>(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
        }
        return model;
    }
    /// <summary>
    /// 取得單筆資料(非同步呼叫)
    /// </summary>
    /// <param name="valueNo">要查詢的欄位值</param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetDataAsync(string valueNo)
    {
        string sql_query = GetSQLSelect();
        sql_query += $" WHERE {EntityName}.{DropDownValueColumn} = @ValueNo";
        DynamicParameters parm = new DynamicParameters();
        parm.Add("ValueNo", valueNo);
        var model = await dpr.ReadSingleAsync<TEntity>(sql_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步不分頁呼叫)
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync()
    {
        DynamicParameters parm = new DynamicParameters();
        var model = await GetDataListAsync(parm, "");
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步不分頁呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(DynamicParameters parm)
    {
        var model = await GetDataListAsync(parm, "");
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步不分頁呼叫)
    /// </summary>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(string searchString)
    {
        DynamicParameters parm = new DynamicParameters();
        var model = await GetDataListAsync(parm, searchString);
        return model;
    }
    /// <summary>
    /// 取得多筆資料(非同步不分頁呼叫)
    /// </summary>
    /// <param name="parm">參數</param>
    /// <param name="searchString">模糊搜尋文字</param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetDataListAsync(DynamicParameters parm, string searchString)
    {
        List<string> searchColumns = GetSearchColumns();
        var model = new List<TEntity>();
        string sql_query = GetSQLSelect();
        string sql_where = GetSQLWhere();
        sql_query += sql_where;
        if (!string.IsNullOrEmpty(searchString))
            sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
        sql_query += GetSQLOrderBy();
        if (parm.ParameterNames.Count() > 0)
            model = await dpr.ReadAllAsync<TEntity>(sql_query, parm);
        else
            model = await dpr.ReadAllAsync<TEntity>(sql_query);
        ErrorMessage = dpr.ErrorMessage;
        return model;
    }
    #endregion
    #region 資料增刪改相關的函數(同步呼叫)
    /// <summary>
    /// 新增或修改資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <param name="id">Key 欄位值</param>
    public virtual int CreateEdit(TEntity model, int id = 0)
    {
        if (id == 0)
            AffectedRows = Create(model);
        else
            AffectedRows = Edit(model);
        return AffectedRows;
    }
    /// <summary>
    /// 新增資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public virtual int Create(TEntity model)
    {
        string str_query = dpr.GetSQLInsertCommand(model);
        DynamicParameters parm = dpr.GetSQLInsertParameters(model);
        AffectedRows = dpr.Execute(str_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return AffectedRows;
    }
    /// <summary>
    /// 更新資料(同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    public virtual int Edit(TEntity model)
    {
        string str_query = dpr.GetSQLUpdateCommand(model);
        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);
        AffectedRows = dpr.Execute(str_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return AffectedRows;
    }
    /// <summary>
    /// 刪除資料(同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    public virtual int Delete(int id = 0)
    {
        string str_query = dpr.GetSQLDeleteCommand(EntityObject);
        DynamicParameters parm = dpr.GetSQLDeleteParameters(EntityObject, id);
        AffectedRows = dpr.Execute(str_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return AffectedRows;
    }
    /// <summary>
    /// 刪除明細資料(同步呼叫)
    /// </summary>
    /// <param name="parmColumn">參數欄位名稱</param>
    /// <param name="parmValue">參數值</param>
    public virtual int DeleteDetail(string parmColumn, object parmValue)
    {
        string str_query = GetSQLDetailDelete();
        var parm = new DynamicParameters();
        parm.Add(parmColumn, parmValue);
        AffectedRows = dpr.Execute(str_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return AffectedRows;
    }
    #endregion
    #region 資料增刪改相關的函數(非同步呼叫)
    /// <summary>
    /// 新增或修改資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual async Task<int> CreateEditAsync(TEntity model, int id = 0)
    {
        if (id == 0)
            AffectedRows = await CreateAsync(model);
        else
            AffectedRows = await EditAsync(model);
        return AffectedRows;
    }
    /// <summary>
    /// 新增資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public virtual async Task<int> CreateAsync(TEntity model)
    {
        string str_query = dpr.GetSQLInsertCommand(model);
        DynamicParameters parm = dpr.GetSQLInsertParameters(model);
        AffectedRows = await dpr.ExecuteAsync(str_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return AffectedRows;
    }
    /// <summary>
    /// 更新資料(非同步呼叫)
    /// </summary>
    /// <param name="model">資料模型</param>
    /// <returns></returns>
    public virtual async Task<int> EditAsync(TEntity model)
    {
        string str_query = dpr.GetSQLUpdateCommand(model);
        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);
        AffectedRows = await dpr.ExecuteAsync(str_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return AffectedRows;
    }
    /// <summary>
    /// 刪除資料(非同步呼叫)
    /// </summary>
    /// <param name="id">Key 欄位值</param>
    /// <returns></returns>
    public virtual async Task<int> DeleteAsync(int id = 0)
    {
        string str_query = dpr.GetSQLDeleteCommand(EntityObject);
        DynamicParameters parm = dpr.GetSQLDeleteParameters(EntityObject, id);
        AffectedRows = await dpr.ExecuteAsync(str_query, parm);
        ErrorMessage = dpr.ErrorMessage;
        return AffectedRows;
    }
    #endregion
    #region 表單表頭與表單明細相關函數
    /// <summary>
    /// 取得第一筆資料
    /// </summary>
    /// <returns></returns>
    public virtual void SetFirstData()
    {
        var models = GetAllData();
        SessionService.PageMaster = (models.Count() > 0) ? 1 : 0;
        SessionService.PageCountMaster = models.Count();
        SetMasterPage();
    }

    /// <summary>
    /// 取得前一筆資料
    /// </summary>
    /// <returns></returns>
    public virtual void SetPriorData()
    {
        var models = GetAllData();
        SessionService.PageMaster -= 1;
        SessionService.PageCountMaster = models.Count();
        if (SessionService.PageMaster < 1) SessionService.PageMaster = 1;
        SetMasterPage();
    }

    /// <summary>
    /// 取得下一筆資料
    /// </summary>
    /// <returns></returns>
    public virtual void SetNextData()
    {
        var models = GetAllData();
        SessionService.PageMaster += 1;
        SessionService.PageCountMaster = models.Count();
        if (SessionService.PageMaster > SessionService.PageCountMaster) SessionService.PageMaster = SessionService.PageCountMaster;
        SetMasterPage();
    }

    /// <summary>
    /// 取得最後一筆資料
    /// </summary>
    /// <returns></returns>
    public void SetLastData()
    {
        var models = GetAllData();
        SessionService.PageMaster = models.Count();
        SessionService.PageCountMaster = models.Count();
        SetMasterPage();
    }

    /// <summary>
    /// 設定表頭為第幾筆及總筆數
    /// </summary>
    public virtual void SetMasterPage()
    {

    }

    public List<TEntity> GetAllData()
    {
        string str_sql = GetSQLSelect();
        str_sql += GetSQLWhere();
        str_sql += GetSQLOrderBy();
        var models = dpr.ReadAll<TEntity>(str_sql);
        return models.ToList();
    }
    #endregion
    #region 其它相關函數(公用)
    /// <summary>
    /// 檢查是否有重覆輸入值
    /// </summary>
    /// <param name="column">欄位名稱</param>
    /// <returns></returns>
    public string IsDuplicated(string column)
    {
        string str_message = "";
        if (dpr.IsDuplicated(EntityObject, column)) str_message = dpr.GetDuplicateMessage(EntityObject, column);
        return str_message;
    }
    #endregion
    #region 其它相關函數(私用)
    /// <summary>
    /// 取得真實排序欄位
    /// </summary>
    /// <param name="column">欄位名稱</param>
    /// <returns></returns>
    private string GetSqlOrderByColumn(string column = "")
    {
        string str_value = "";
        if (!string.IsNullOrEmpty(column))
        {
            string str_select = GetSQLSelect().Trim().Replace("\r\n", " ");
            string str_upper = str_select.ToUpper();
            int int_index = str_upper.IndexOf(" FROM ");
            if (int_index > 0)
            {
                str_select = str_select.Substring(6, int_index - 7).Trim();
                var cols = str_select.Split(',');
                foreach (var col in cols)
                {
                    string str_col_name = "";
                    string str_alias = "";
                    string str_col = col.Trim();
                    if (str_col.IndexOf(" AS ") > 0)
                    {
                        str_col_name = str_col.Substring(0, str_col.IndexOf(" AS "));
                        str_alias = str_col.Substring(str_col_name.Length + 4, str_col.Length - str_col_name.Length - 4);
                        if (str_alias.ToUpper() == column.ToUpper()) { str_value = str_col_name; break; }
                    }
                    else
                    {
                        if (str_col.IndexOf(".") > 0) str_col = str_col.Split(".")[1];
                        if (str_col.ToUpper() == column.ToUpper()) { str_value = column; break; }
                    }
                }
            }
        }
        return str_value;
    }
    #endregion
}