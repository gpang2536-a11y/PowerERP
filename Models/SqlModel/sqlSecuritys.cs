namespace powererp.Models
{
    public class z_sqlSecuritys : DapperSql<Securitys>
    {
        public override string DefaultOrderByColumn { get { return "Securitys.TargetNo,Securitys.PrgNo"; } }
        public override string DefaultOrderByDirection { get { return "ASC,ASC"; } }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Securitys.Id, Securitys.RoleNo, Roles.RoleName, Securitys.TargetNo, Users.UserName AS TargetName, 
Securitys.ModuleNo, Modules.ModuleName, Securitys.PrgNo, Programs.PrgName, Securitys.IsAdd, Securitys.IsEdit, 
Securitys.IsDelete, Securitys.IsConfirm, Securitys.IsUndo, Securitys.IsCancel, Securitys.IsUpload, 
Securitys.IsDownload, Securitys.IsPrint, Securitys.Remark
FROM Securitys 
LEFT OUTER JOIN Programs ON Securitys.PrgNo = Programs.PrgNo 
LEFT OUTER JOIN Modules ON Securitys.ModuleNo = Modules.ModuleNo 
LEFT OUTER JOIN Users ON Securitys.TargetNo = Users.UserNo 
LEFT OUTER JOIN Roles ON Securitys.RoleNo = Roles.RoleNo 
";
            return str_query;
        }

        public List<Securitys> GetPrgDataList(string baseNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Securitys>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE Securitys.PrgNo = @BaseNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("BaseNo", baseNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Securitys>(sql_query, parm);
            return model;
        }

        public List<Securitys> GetUserDataList(string baseNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Securitys>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE Securitys.TargetNo = @BaseNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("BaseNo", baseNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Securitys>(sql_query, parm);
            return model;
        }

        /// <summary>
        /// 設定目前使用者程式權限
        /// </summary>
        /// <param name="codeNo">權限對象代號</param>
        /// <returns></returns>
        public void SetSecuritys(string codeNo = "")
        {
            var sec = new FormSecurity();
            string str_code = string.IsNullOrEmpty(codeNo) ? SessionService.SecurityCodeNo : codeNo;
            //除錯模式無權限問題
            if (AppService.DebugMode)
            {
                sec.IsDisplay = true;
                sec.IsAdd = true;
                sec.IsEdit = true;
                sec.IsDelete = true;
                sec.IsConfirm = true;
                sec.IsUndo = true;
                sec.IsCancel = true;
                sec.IsUpload = true;
                sec.IsDownload = true;
                sec.IsPrint = true;
            }
            else
            {
                string sql_query = GetSQLSelect();
                sql_query += " WHERE Securitys.TargetNo = @targetNo AND Securitys.PrgNo = @prgNo";
                string str_prg_no = SessionService.PrgNo + str_code;
                var parm = new DynamicParameters();
                parm.Add("@targetNo", SessionService.SecurityTargetNo);
                parm.Add("@prgNo", str_prg_no);
                var model = dpr.ReadSingle<Securitys>(sql_query, parm);
                bool isNull = model == null ? true : false;
                if (isNull)
                {
                    var parm1 = new DynamicParameters();
                    parm.Add("@targetNo", SessionService.SecurityTargetNo);
                    parm.Add("@prgNo", SessionService.PrgNo);
                    model = dpr.ReadSingle<Securitys>(sql_query, parm);
                }

                if (model == null)
                {
                    sec.IsDisplay = false;
                    sec.IsAdd = false;
                    sec.IsEdit = false;
                    sec.IsDelete = false;
                    sec.IsConfirm = false;
                    sec.IsUndo = false;
                    sec.IsCancel = false;
                    sec.IsUpload = false;
                    sec.IsDownload = false;
                    sec.IsPrint = false;
                }
                else
                {
                    sec.IsDisplay = true;
                    sec.IsAdd = (model.IsAdd) ? true : false;
                    sec.IsEdit = (model.IsEdit) ? true : false;
                    sec.IsDelete = (model.IsDelete) ? true : false;
                    sec.IsConfirm = (model.IsConfirm) ? true : false;
                    sec.IsUndo = (model.IsUndo) ? true : false;
                    sec.IsCancel = (model.IsCancel) ? true : false;
                    sec.IsUpload = (model.IsUpload) ? true : false;
                    sec.IsDownload = (model.IsDownload) ? true : false;
                    sec.IsPrint = (model.IsPrint) ? true : false;
                }
            }
            sec.IsAddEdit = (sec.IsAdd || sec.IsEdit);
            sec.IsAddEditDelete = (sec.IsAdd || sec.IsEdit || sec.IsDelete);
            SessionService.FormSecurity = sec;
            return;
        }

        /// <summary>
        /// 取得使用模組權限列畏
        /// </summary>
        /// <param name="roleNo">角色編號</param>
        /// <param name="target_no">對象編號</param>
        /// <returns></returns>
        public List<Modules> GetMenuModules(string roleNo = "", string target_no = "")
        {
            string str_query = "";
            if (string.IsNullOrEmpty(roleNo)) roleNo = SessionService.RoleNo;
            if (string.IsNullOrEmpty(target_no)) target_no = SessionService.UserNo;
            var parm = new DynamicParameters();
            if (target_no == "Debug")
            {
                str_query = @"
SELECT Modules.ModuleNo, Modules.ModuleName, Modules.IconName 
FROM Modules 
WHERE Modules.RoleNo = @RoleNo AND Modules.IsEnabled = @IsEnabled 
ORDER BY Modules.SortNo , Modules.ModuleNo
";
                parm.Add("@RoleNo", "User");
            }
            else
            {
                str_query = @"
SELECT DISTINCT Securitys.ModuleNo, Modules.ModuleName, Modules.IconName 
FROM Securitys 
LEFT OUTER JOIN Modules ON Securitys.ModuleNo = Modules.ModuleNo 
WHERE Securitys.RoleNo = @RoleNo AND Securitys.TargetNo = @TargetNo AND Modules.IsEnabled = @IsEnabled
ORDER BY Securitys.ModuleNo
";
                parm.Add("@RoleNo", roleNo);
                parm.Add("@TargetNo", target_no);
            }
            parm.Add("@IsEnabled", true);
            var models = dpr.ReadAll<Modules>(str_query, parm);
            return models;
        }

        /// <summary>
        /// 取得使用模組權限列畏
        /// </summary>
        /// <param name="moduleNo">模組編號</param>
        /// <param name="roleNo">角色編號</param>
        /// <param name="target_no">對象編號</param>
        /// <returns></returns>
        public List<Programs> GetMenuPrograms(string moduleNo, string roleNo = "", string target_no = "")
        {
            string str_query = "";
            if (string.IsNullOrEmpty(roleNo)) roleNo = SessionService.RoleNo;
            if (string.IsNullOrEmpty(target_no)) target_no = SessionService.UserNo;
            var parm = new DynamicParameters();
            if (target_no == "Debug")
            {
                str_query = @"
SELECT Programs.Id, Programs.IsEnabled, Programs.IsPageSize, Programs.IsSearch, Programs.RoleNo, 
Programs.LockNo, Programs.ModuleNo, Programs.SortNo, Programs.PrgNo, Programs.PrgName, Programs.CodeNo, 
vi_CodeProgram.CodeValue AS IconName,Programs.AreaName, Programs.ControllerName, Programs.ActionName, 
Programs.ParmValue, Programs.Remark 
FROM Programs 
LEFT OUTER JOIN vi_CodeProgram ON Programs.CodeNo = vi_CodeProgram.CodeNo 
WHERE (Programs.ModuleNo = @ModuleNo) AND (Programs.IsEnabled = @IsEnabled) 
ORDER BY Programs.PrgNo 
";
            }
            else
            {
                str_query = @"
SELECT Programs.Id, Programs.IsEnabled, Programs.IsPageSize, Programs.IsSearch, Programs.RoleNo, 
Programs.LockNo, Programs.ModuleNo, Programs.SortNo, Programs.PrgNo, Programs.PrgName, Programs.CodeNo, 
vi_CodeProgram.CodeValue AS IconName,Programs.AreaName, Programs.ControllerName, Programs.ActionName, 
Programs.ParmValue, Programs.Remark 
FROM Programs 
LEFT OUTER JOIN Securitys ON Securitys.PrgNo = Programs.PrgNo 
LEFT OUTER JOIN vi_CodeProgram ON Programs.CodeNo = vi_CodeProgram.CodeNo 
WHERE (Securitys.RoleNo = @RoleNo) AND (Securitys.TargetNo = @TargetNo) AND 
(Securitys.ModuleNo = @ModuleNo) AND (Programs.IsEnabled = @IsEnabled) 
ORDER BY Securitys.PrgNo 
";
                parm.Add("@TargetNo", target_no);
                parm.Add("@RoleNo", roleNo);
            }
            parm.Add("@ModuleNo", moduleNo);
            parm.Add("@IsEnabled", true);
            var models = dpr.ReadAll<Programs>(str_query, parm);
            return models;
        }
    }
}