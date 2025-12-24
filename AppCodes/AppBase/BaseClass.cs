using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

/// <summary>
/// 最底層類別(加入解構子呼叫GC)
/// </summary>
public class BaseClass : IDisposable
{
    #region 欄位(Field)
    private bool disposed = false;
    private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
    #endregion
    #region 解構功能
    /// <summary>
    /// 解構子,實現IDisposable中的Dispose方法
    /// </summary>
    public void Dispose()
    {
        //必須為true
        Dispose(true);
        //通知垃圾回收機制不再調用終端子（析構器）
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// 解構子
    /// </summary>
    /// <param name="disposing">disposing</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;
        //解構時要執行的其它程式
        if (disposing)
        {
            handle.Dispose();
        }
        //讓類別知道自己已經被釋放
        disposed = true;
    }
    #endregion
    #region 解構子
    /// <summary>
    /// BaseClass 解構子
    /// </summary>
    ~BaseClass()
    {
        //必須為false
        Dispose(false);
    }
    #endregion
}