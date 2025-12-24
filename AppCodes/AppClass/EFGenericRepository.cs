using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

/// <summary>
/// 實作 Entity Framework Generic Repository 的 Class。
/// </summary>
/// <typeparam name="TEntity">EF Model 裡面的Type</typeparam>
public class EFGenericRepository<TEntity> : BaseClass where TEntity : class
{
    #region 欄位(Field)
    protected readonly DbContext Context;
    #endregion
    #region 建構子(Constructor)
    /// <summary>
    /// 建構EF一個Entity的Repository，需傳入此Entity的Context。
    /// </summary>
    /// <param name="inContext">Entity所在的Context</param>
    public EFGenericRepository(DbContext inContext)
    {
        Context = inContext;
    }
    /// <summary>
    /// 建構EF一個Entity的Repository，預設使用 dbEntities 為 Entity的Contex
    /// </summary>
    public EFGenericRepository()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("dbconn");
        var optionsBuilder = new DbContextOptionsBuilder<dbEntities>();
        optionsBuilder.UseSqlServer(connectionString);
        Context = new dbEntities(optionsBuilder.Options);
    }
    #endregion
    #region CRUD 查詢功能
    /// <summary>
    /// 取得第一筆符合條件的內容 (同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <param name="tracking">是否追蹤</param>
    /// <returns>第一筆符合條件的資料</returns>
    public TEntity ReadSingle(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
    {
        if (tracking) return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        return Context.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefault();
    }

    /// <summary>
    /// 取得第一筆符合條件的內容 (非同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <param name="tracking">是否追蹤</param>
    /// <returns>第一筆符合條件的資料</returns>
    public async Task<TEntity> ReadSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
    {
        if (tracking) return await Context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        return await Context.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (同步)
    /// </summary>
    /// <param name="tracking">是否追蹤</param>
    /// <returns>Entity 全部筆數的資料</returns>
    public List<TEntity> ReadAll(bool tracking = true)
    {
        if (tracking) return Context.Set<TEntity>().AsQueryable().ToList();
        return Context.Set<TEntity>().AsNoTracking().AsQueryable().ToList();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (非同步)
    /// </summary>
    /// <param name="tracking">是否追蹤</param>
    /// <returns>Entity 全部筆數的資料</returns>
    public async Task<List<TEntity>> ReadAllAsync(bool tracking = true)
    {
        if (tracking) return await Context.Set<TEntity>().ToListAsync();
        return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <param name="tracking">是否追蹤</param>
    /// <returns>Entity 全部筆數的資料</returns>
    public List<TEntity> ReadAll(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
    {
        if (tracking) return Context.Set<TEntity>().Where(predicate).AsQueryable().ToList();
        return Context.Set<TEntity>().AsNoTracking().Where(predicate).AsQueryable().ToList();
    }

    /// <summary>
    /// 取得 Entity 全部筆數資料 (非同步)
    /// </summary>
    /// <param name="predicate">要取得的 Where 條件式</param>
    /// <param name="tracking">是否追蹤</param>
    /// <returns>Entity 全部筆數的資料</returns>
    public async Task<List<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
    {
        if (tracking) return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        return await Context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// 新增一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要新增的內容</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public void Insert(TEntity entity, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Added;
        if (saveChange) SaveChanges();
    }
    #endregion
    #region CRUD 新增、更新、刪除功能
    /// <summary>
    /// 新增一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要新增的 Entity</param>
    public void Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// 新增一筆資料 (非同步)
    /// </summary>
    /// <param name="entity">要新增的 Entity</param>
    /// <returns></returns>
    public async Task CreateAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    /// <summary>
    /// 新增一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要新增的內容</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public async Task InsertAsync(TEntity entity, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Added;
        if (saveChange) await SaveChangesAsync();
    }

    /// <summary>
    /// 更新一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public void Update(TEntity entity, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Modified;
        if (saveChange) SaveChanges();
    }

    /// <summary>
    /// 更新一筆資料 (同步)
    /// </summary>
    /// <param name="entity">要更新的內容</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public async Task UpdateAsync(TEntity entity, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Modified;
        if (saveChange) await SaveChangesAsync();
    }

    /// <summary>
    /// 更新一筆Entity的內容。只更新有指定的Property。
    /// </summary>
    /// <param name="entity">要更新的內容。</param>
    /// <param name="updateProperties">需要更新的欄位。</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public void Update(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Unchanged;

        if (updateProperties != null)
        {
            foreach (var property in updateProperties)
            {
                Context.Entry<TEntity>(entity).Property(property).IsModified = true;
            }
        }

        if (saveChange) SaveChanges();
    }

    /// <summary>
    /// 更新一筆Entity的內容。只更新有指定的Property。
    /// </summary>
    /// <param name="entity">要更新的內容。</param>
    /// <param name="updateProperties">需要更新的欄位。</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, object>>[] updateProperties, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Unchanged;

        if (updateProperties != null)
        {
            foreach (var property in updateProperties)
            {
                Context.Entry<TEntity>(entity).Property(property).IsModified = true;
            }
        }

        if (saveChange) await SaveChangesAsync();
    }

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public void Delete(TEntity entity, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        if (saveChange) SaveChanges();
    }

    /// <summary>
    /// 刪除一筆資料內容。
    /// </summary>
    /// <param name="entity">要被刪除的Entity。</param>
    /// <param name="saveChange">是否執行後存檔</param>
    public async Task DeleteAsync(TEntity entity, bool saveChange = false)
    {
        Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        if (saveChange) await SaveChangesAsync();
    }
    #endregion
    #region 存檔功能
    /// <summary>
    /// 儲存異動。
    /// </summary>
    public string SaveChanges()
    {
        string str_message = "";
        try
        {
            Context.SaveChanges();
        }
        catch (Exception ex) { str_message = ex.Message; }
        return str_message;
    }

    /// <summary>
    /// 儲存異動。
    /// </summary>
    public async Task<string> SaveChangesAsync()
    {
        string str_message = "";
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (Exception ex) { str_message = ex.Message; }
        return str_message;
    }
    #endregion
}