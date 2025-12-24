public class ImageService : BaseClass
{
    /// <summary>
    /// 許可的檔案副檔名
    /// </summary>
    /// <value></value>
    public string AllowedExtensions { get; set; }

    /// <summary>
    /// 最大檔案大小 (MB)
    /// </summary>
    public int MaxFileSizeMB { get; set; }

    /// <summary>
    /// 錯誤訊息 
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// 建構子
    /// </summary>
    public ImageService()
    {
        AllowedExtensions = ".jpg,.jpeg,.png,.gif,.webp";
        MaxFileSizeMB = 5;
        ErrorMessage = "";
    }

    /// <summary>
    /// 檢查檔案是否存在 
    /// </summary>
    /// <param name="filePath">檔案路徑, 例如: ~/images/users/U001.jpg</param>
    /// <returns></returns>
    public bool FileExists(string filePath)
    {
        string str_file_path = filePath.Replace("~/", "");
        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", str_file_path);
        return File.Exists(filePath);
    }

    /// <summary>
    /// 產生唯一的檔案名稱
    /// </summary>
    /// <param name="originalFileName">原始檔案名稱</param>
    public static string GetUniqueFileName(string originalFileName)
    {
        //除理瀏覽器會在緩存圖片問題
        var uniqueFileName = originalFileName + $"?t={DateTime.Now.ToString("yyyyMMddHHmmssff")}";
        return uniqueFileName;
    }

    /// <summary>
    /// 上傳圖片 (通用型)
    /// </summary>
    /// <param name="file">上傳的檔案</param>
    /// <param name="folderName">資料夾名稱 (例如: "news")</param>
    /// <param name="fileName">檔案名稱 (空白則使用亂數檔名)</param>
    /// <returns>錯誤訊息 (空白表示成功)</returns>
    public string UploadImage(IFormFile file, string folderName, string fileName = "")
    {
        ErrorMessage = "";
        try
        {
            // 驗證檔案是否存在
            if (file == null || file.Length == 0) ErrorMessage = "未選擇檔案";
            if (!string.IsNullOrEmpty(ErrorMessage)) return ErrorMessage;

            // 驗證檔案類型
            var allowedExtensions = AllowedExtensions.Split(',').ToList();
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension)) ErrorMessage = $"不支援的檔案格式，請上傳 {AllowedExtensions} 格式";
            if (!string.IsNullOrEmpty(ErrorMessage)) return ErrorMessage;

            // 驗證檔案大小 (限制 5MB)
            if (file.Length > MaxFileSizeMB * 1024 * 1024) ErrorMessage = $"檔案大小不可超過 {MaxFileSizeMB}MB";
            if (!string.IsNullOrEmpty(ErrorMessage)) return ErrorMessage;

            // 建立儲存目錄
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // 決定檔案名稱
            string uniqueFileName;
            if (string.IsNullOrEmpty(fileName))
            {
                // 使用亂數檔名 (時間戳記 + GUID)
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                uniqueFileName = $"{timestamp}_{Guid.NewGuid():N}{extension}";
            }
            else
            {
                // 使用傳入的檔名，確保有副檔名
                uniqueFileName = fileName.Contains('.') ? fileName : $"{fileName}{extension}";
            }

            var filePath = Path.Combine(uploadPath, uniqueFileName);

            // 刪除已存在檔案
            if (File.Exists(filePath)) File.Delete(filePath);

            // 儲存檔案
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // 回傳空白表示成功
            ErrorMessage = "";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"上傳失敗: {ex.Message}";
        }
        return ErrorMessage;
    }

    /// <summary>
    /// CKEditor 5 上傳圖片
    /// </summary>
    /// <param name="file">上傳的檔案</param>
    /// <param name="folderName">資料夾名稱 (例如: "news")</param>
    /// <param name="fileName">檔案名稱 (空白則使用亂數檔名)</param>
    /// <returns>JSON 格式回傳結果</returns>
    public async Task<object> CKEditorUploadImageAsync(IFormFile file, string folderName, string fileName = "")
    {
        ErrorMessage = "";
        try
        {
            // 驗證檔案是否存在
            if (file == null || file.Length == 0)
            {
                return new { success = false, error = "未選擇檔案" };
            }

            // 驗證檔案類型
            var allowedExtensions = AllowedExtensions.Split(',').ToList();
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension)) ErrorMessage = $"不支援的檔案格式，請上傳 {AllowedExtensions} 格式";
            if (!string.IsNullOrEmpty(ErrorMessage)) return ErrorMessage;

            // 驗證檔案大小 (限制 5MB)
            if (file.Length > MaxFileSizeMB * 1024 * 1024) ErrorMessage = $"檔案大小不可超過 {MaxFileSizeMB}MB";
            if (!string.IsNullOrEmpty(ErrorMessage)) return ErrorMessage;

            // 建立儲存目錄
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            // 決定檔案名稱
            string uniqueFileName;
            if (string.IsNullOrEmpty(fileName))
            {
                // 使用亂數檔名 (時間戳記 + GUID)
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                uniqueFileName = $"{timestamp}_{Guid.NewGuid():N}{extension}";
            }
            else
            {
                // 使用傳入的檔名,確保有副檔名
                uniqueFileName = fileName.Contains('.') ? fileName : $"{fileName}{extension}";
            }

            var filePath = Path.Combine(uploadPath, uniqueFileName);

            // 儲存檔案
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 回傳成功結果
            var imageUrl = $"/images/{folderName}/{uniqueFileName}";
            return new
            {
                success = true,
                url = imageUrl,
                fileName = uniqueFileName,
                fileSize = file.Length,
                uploadTime = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            ErrorMessage = $"上傳失敗: {ex.Message}";
            return new { success = false, error = ErrorMessage };
        }
    }
}