public class FolderService : BaseClass
{
    public string FolderName { get; set; } = "";
    public string FolderPath { get; set; } = "";
    // 判斷是否在本機執行
    public bool IsDesignTime
    {
        get
        {
            // 檢查是否為本機執行環境
            string serverName = System.Environment.MachineName;
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;

            // 本機開發環境通常包含 bin\Debug 或 bin\Release 路徑
            return currentPath.Contains("\\bin\\Debug") ||
                   currentPath.Contains("\\bin\\Release") ||
                   serverName.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
                   System.Diagnostics.Debugger.IsAttached;
        }
    }
    public string FullFolderPath
    {
        get
        {
            string folderPath = FolderPath;
            if (FolderPath.IndexOf("~/") >= 0)
            {
                // 取得目前專案資料夾目錄路徑
                string fileNameOnServer = Directory.GetCurrentDirectory();
                if (IsDesignTime)
                {
                    // 在設計階段，使用相對路徑
                    fileNameOnServer = AppDomain.CurrentDomain.BaseDirectory;
                    int index = fileNameOnServer.IndexOf("\\bin\\");
                    fileNameOnServer = fileNameOnServer.Substring(0, index);
                }
                fileNameOnServer = Path.Combine(fileNameOnServer, "wwwroot");
                folderPath = folderPath.Replace("~/", "");
                folderPath = folderPath.Replace("/", "\\");
                folderPath = Path.Combine(fileNameOnServer, folderPath);
            }
            return Path.Combine(folderPath, FolderName);
        }
    }
    public List<FileInfo> GetFileInfos()
    {
        List<FileInfo> fileInfos = new List<FileInfo>();
        if (Directory.Exists(FullFolderPath))
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(FullFolderPath);
            fileInfos.AddRange(directoryInfo.GetFiles());
        }
        return fileInfos;
    }

    public FolderService()
    {
        Init("", "");
    }

    public FolderService(string folderName)
    {
        Init("", folderName);
    }

    public FolderService(string pathName, string folderName)
    {
        Init(pathName, folderName);
    }

    private void Init(string pathName, string folderName)
    {
        FolderPath = pathName;
        FolderName = folderName;
    }
    public void DeleteFolder()
    {
        if (Directory.Exists(FullFolderPath))
        {
            Directory.Delete(FullFolderPath, true);
        }
    }

    public void CreateFolder()
    {
        if (!Directory.Exists(FullFolderPath))
        {
            Directory.CreateDirectory(FullFolderPath);
        }
    }

    /// <summary>
    /// 根據檔案副檔名取得 MIME 類型
    /// </summary>
    /// <param name="fileName">檔案名稱</param>
    /// <returns>MIME 類型</returns>
    public string GetContentType(string fileName)
    {
        string extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".txt" => "text/plain",
            ".zip" => "application/zip",
            ".rar" => "application/x-rar-compressed",
            _ => "application/octet-stream"
        };
    }
}