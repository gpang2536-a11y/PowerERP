using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace powererp.Models;

public partial class dbEntities : DbContext
{
    public dbEntities(DbContextOptions<dbEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<Aablite2> Aablite2 { get; set; }

    public virtual DbSet<AboutUs> AboutUs { get; set; }

    public virtual DbSet<AboutUsDetails> AboutUsDetails { get; set; }

    public virtual DbSet<AddressBooks> AddressBooks { get; set; }

    public virtual DbSet<Applications> Applications { get; set; }

    public virtual DbSet<Calendars> Calendars { get; set; }

    public virtual DbSet<Carousels> Carousels { get; set; }

    public virtual DbSet<Carts> Carts { get; set; }

    public virtual DbSet<Categorys> Categorys { get; set; }

    public virtual DbSet<CityAreas> CityAreas { get; set; }

    public virtual DbSet<Citys> Citys { get; set; }

    public virtual DbSet<Clients> Clients { get; set; }

    public virtual DbSet<CloseDates> CloseDates { get; set; }

    public virtual DbSet<CodeBases> CodeBases { get; set; }

    public virtual DbSet<CodeDatas> CodeDatas { get; set; }

    public virtual DbSet<Companys> Companys { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Departments> Departments { get; set; }

    public virtual DbSet<EmployeeAgents> EmployeeAgents { get; set; }

    public virtual DbSet<EmployeeExperiences> EmployeeExperiences { get; set; }

    public virtual DbSet<EmployeeSchools> EmployeeSchools { get; set; }

    public virtual DbSet<Employees> Employees { get; set; }

    public virtual DbSet<ExtensionTables> ExtensionTables { get; set; }

    public virtual DbSet<Featureds> Featureds { get; set; }

    public virtual DbSet<FormDetail> FormDetail { get; set; }

    public virtual DbSet<FormMaster> FormMaster { get; set; }

    public virtual DbSet<ForumBoards> ForumBoards { get; set; }

    public virtual DbSet<Forums> Forums { get; set; }

    public virtual DbSet<InvDetails> InvDetails { get; set; }

    public virtual DbSet<InvMasters> InvMasters { get; set; }

    public virtual DbSet<Inventorys> Inventorys { get; set; }

    public virtual DbSet<InventorysDetail> InventorysDetail { get; set; }

    public virtual DbSet<InventorysType> InventorysType { get; set; }

    public virtual DbSet<Languages> Languages { get; set; }

    public virtual DbSet<Logs> Logs { get; set; }

    public virtual DbSet<MapPositions> MapPositions { get; set; }

    public virtual DbSet<Messages> Messages { get; set; }

    public virtual DbSet<Modules> Modules { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsLetter> NewsLetter { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<OrderDetails> OrderDetails { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<OrdersStatus> OrdersStatus { get; set; }

    public virtual DbSet<Overtimes> Overtimes { get; set; }

    public virtual DbSet<OvertimeTypes> OvertimeTypes { get; set; }

    public virtual DbSet<Payments> Payments { get; set; }

    public virtual DbSet<Photos> Photos { get; set; }

    public virtual DbSet<PricingDetails> PricingDetails { get; set; }

    public virtual DbSet<Pricings> Pricings { get; set; }

    public virtual DbSet<ProductFeatureds> ProductFeatureds { get; set; }

    public virtual DbSet<ProductInventorys> ProductInventorys { get; set; }

    public virtual DbSet<ProductPropertys> ProductPropertys { get; set; }

    public virtual DbSet<ProductStatus> ProductStatus { get; set; }

    public virtual DbSet<ProductTags> ProductTags { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<Programs> Programs { get; set; }

    public virtual DbSet<Promotions> Promotions { get; set; }

    public virtual DbSet<PropertyNames> PropertyNames { get; set; }

    public virtual DbSet<Propertys> Propertys { get; set; }

    public virtual DbSet<Questions> Questions { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Securitys> Securitys { get; set; }

    public virtual DbSet<Services> Services { get; set; }

    public virtual DbSet<Shippings> Shippings { get; set; }

    public virtual DbSet<TableRelations> TableRelations { get; set; }

    public virtual DbSet<Teams> Teams { get; set; }

    public virtual DbSet<Testimonials> Testimonials { get; set; }

    public virtual DbSet<TempUser> TempUser { get; set; }

    public virtual DbSet<Overtimes> Titles { get; set; }

    public virtual DbSet<TodoLists> TodoLists { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Vacations> Vacations { get; set; }

    public virtual DbSet<Vendors> Vendors { get; set; }

    public virtual DbSet<Warehouses> Warehouses { get; set; }

    public virtual DbSet<WorkflowDetails> WorkflowDetails { get; set; }

    public virtual DbSet<WorkflowMasters> WorkflowMasters { get; set; }

    public virtual DbSet<WorkflowRoleUsers> WorkflowRoleUsers { get; set; }

    public virtual DbSet<WorkflowRoles> WorkflowRoles { get; set; }

    public virtual DbSet<WorkflowRoutes> WorkflowRoutes { get; set; }

    public virtual DbSet<vi_CodeAddressBook> vi_CodeAddressBook { get; set; }

    public virtual DbSet<vi_CodeBrand> vi_CodeBrand { get; set; }

    public virtual DbSet<vi_CodeCalendar> vi_CodeCalendar { get; set; }

    public virtual DbSet<vi_CodeCloseDate> vi_CodeCloseDate { get; set; }

    public virtual DbSet<vi_CodeColor> vi_CodeColor { get; set; }

    public virtual DbSet<vi_CodeCompany> vi_CodeCompany { get; set; }

    public virtual DbSet<vi_CodeCustomer> vi_CodeCustomer { get; set; }

    public virtual DbSet<vi_CodeFormStatus> vi_CodeFormStatus { get; set; }

    public virtual DbSet<vi_CodeGender> vi_CodeGender { get; set; }

    public virtual DbSet<vi_CodeInventory> vi_CodeInventory { get; set; }

    public virtual DbSet<vi_CodeLog> vi_CodeLog { get; set; }

    public virtual DbSet<vi_CodeMaterial> vi_CodeMaterial { get; set; }

    public virtual DbSet<vi_CodeMember> vi_CodeMember { get; set; }

    public virtual DbSet<vi_CodeMessage> vi_CodeMessage { get; set; }

    public virtual DbSet<vi_CodeNews> vi_CodeNews { get; set; }

    public virtual DbSet<vi_CodeNotification> vi_CodeNotification { get; set; }

    public virtual DbSet<vi_CodePhoto> vi_CodePhoto { get; set; }

    public virtual DbSet<vi_CodeProgram> vi_CodeProgram { get; set; }

    public virtual DbSet<vi_CodeResignReason> vi_CodeResignReason { get; set; }

    public virtual DbSet<vi_CodeSheet> vi_CodeSheet { get; set; }

    public virtual DbSet<vi_CodeSize> vi_CodeSize { get; set; }

    public virtual DbSet<vi_CodeTarget> vi_CodeTarget { get; set; }

    public virtual DbSet<vi_CodeTax> vi_CodeTax { get; set; }

    public virtual DbSet<vi_CodeUser> vi_CodeUser { get; set; }

    public virtual DbSet<vi_CodeVacation> vi_CodeVacation { get; set; }

    public virtual DbSet<vi_CodeVendor> vi_CodeVendor { get; set; }

    public virtual DbSet<vi_CompCompany> vi_CompCompany { get; set; }

    public virtual DbSet<vi_CompCustomer> vi_CompCustomer { get; set; }

    public virtual DbSet<vi_CompVendor> vi_CompVendor { get; set; }

    public virtual DbSet<vi_SysIndex> vi_SysIndex { get; set; }

    public virtual DbSet<vi_SysTableColumn> vi_SysTableColumn { get; set; }

    public virtual DbSet<vi_TodoList> vi_TodoList { get; set; }

    public virtual DbSet<vi_UserCustomer> vi_UserCustomer { get; set; }

    public virtual DbSet<vi_UserMis> vi_UserMis { get; set; }

    public virtual DbSet<vi_UserOperator> vi_UserOperator { get; set; }

    public virtual DbSet<vi_UserTarget> vi_UserTarget { get; set; }

    public virtual DbSet<vi_UserUser> vi_UserUser { get; set; }

    public virtual DbSet<vi_UserVendor> vi_UserVendor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aablite2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Aablite");

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasDefaultValueSql("(space((0)))");
        });

        modelBuilder.Entity<AboutUs>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.HeaderName, "IX_AboutUs_name").IsClustered();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DetailText).HasMaxLength(500);
            entity.Property(e => e.HeaderName).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TitleName).HasMaxLength(500);
        });

        modelBuilder.Entity<AboutUsDetails>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.ItemName }, "IX_AboutUsDetails_sort_name").IsClustered();

            entity.Property(e => e.ItemName).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<AddressBooks>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.UserNo, e.CodeNo }, "IX_AddressBooks_uno_no").IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CompID).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(250);
            entity.Property(e => e.CompTel).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(50);
            entity.Property(e => e.FacebookID).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.InstagramID).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LineID).HasMaxLength(50);
            entity.Property(e => e.LinkedInID).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.TitleName).HasMaxLength(50);
            entity.Property(e => e.TwitterID).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Applications>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.AppName, "IX_Applications_name").IsClustered();

            entity.Property(e => e.AdminName).HasMaxLength(50);
            entity.Property(e => e.AppName).HasMaxLength(50);
            entity.Property(e => e.AppVersion).HasMaxLength(50);
            entity.Property(e => e.GoogleMapKey).HasMaxLength(50);
            entity.Property(e => e.LanguageNo).HasMaxLength(50);
            entity.Property(e => e.MailAppPassword).HasMaxLength(50);
            entity.Property(e => e.MailHostUrl).HasMaxLength(250);
            entity.Property(e => e.MailReceiverEmail).HasMaxLength(50);
            entity.Property(e => e.MailReceiverName).HasMaxLength(50);
            entity.Property(e => e.MailSenderEmail).HasMaxLength(50);
            entity.Property(e => e.MailSenderName).HasMaxLength(50);
            entity.Property(e => e.PowerBy).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShopName).HasMaxLength(50);
            entity.Property(e => e.WebSiteUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<Calendars>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.TargetCode, e.TargetNo, e.StartDate }, "IX_Calendars_code_target_date").IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ColorName).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasMaxLength(50);
            entity.Property(e => e.PlaceAddress).HasMaxLength(250);
            entity.Property(e => e.PlaceName).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ResourceText).HasMaxLength(500);
            entity.Property(e => e.RoomNo).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasMaxLength(50);
            entity.Property(e => e.SubjectName).HasMaxLength(50);
            entity.Property(e => e.TargetCode).HasMaxLength(50);
            entity.Property(e => e.TargetNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Carousels>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.HeaderName }, "IX_Carousels_sort_name").IsClustered();

            entity.Property(e => e.HeaderName).HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasMaxLength(250);
            entity.Property(e => e.MoreUrl).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Carts>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.LotNo, e.ProdNo }, "IX_Carts_lno_pno").IsClustered();

            entity.Property(e => e.CategoryName).HasMaxLength(250);
            entity.Property(e => e.CategoryNo).HasMaxLength(50);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.LotNo).HasMaxLength(50);
            entity.Property(e => e.MemberNo).HasMaxLength(50);
            entity.Property(e => e.ProdName).HasMaxLength(250);
            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.ProdSpec).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.VendorNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Categorys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable(tb => tb.HasTrigger("tr_Categorys"));

            entity.HasIndex(e => new { e.ParentNo, e.SortNo, e.CategoryNo }, "IX_Categorys_pno_sno_cno").IsClustered();

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.CategoryNo).HasMaxLength(50);
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RouteName).HasMaxLength(500);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<CityAreas>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.CityName, e.AreaName }, "IX_CityAreas_city_area").IsClustered();

            entity.Property(e => e.AreaName).HasMaxLength(50);
            entity.Property(e => e.CityName).HasMaxLength(50);
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Citys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.CityName }, "IX_Citys_sort_name").IsClustered();

            entity.Property(e => e.CityName).HasMaxLength(50);
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Clients>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.ClientName }, "IX_Clients_sort_name").IsClustered();

            entity.Property(e => e.ClientName).HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
            entity.Property(e => e.WebsiteUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<CloseDates>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.CodeNo, e.StartDate }, "IX_CloseDates_code_start")
                .IsDescending(false, true)
                .IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<CodeBases>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.IsAdmin, e.BaseNo }, "IX_CodeBases_admin_no");

            entity.HasIndex(e => e.BaseNo, "IX_CodeBases_no").IsClustered();

            entity.Property(e => e.BaseName).HasMaxLength(50);
            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<CodeDatas>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.BaseNo, e.ParentNo, e.SortNo, e.CodeNo }, "IX_BaseDatas_no_pno_sort_code").IsClustered();

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Companys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.CodeNo, e.CompNo }, "IX_Companys_code_no")
                .IsDescending(false, true)
                .IsClustered();

            entity.Property(e => e.BossName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CompAddress).HasMaxLength(250);
            entity.Property(e => e.CompFax).HasMaxLength(50);
            entity.Property(e => e.CompID).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(250);
            entity.Property(e => e.CompNo).HasMaxLength(50);
            entity.Property(e => e.CompTel).HasMaxLength(50);
            entity.Property(e => e.CompUrl).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(250);
            entity.Property(e => e.EngShortName).HasMaxLength(50);
            entity.Property(e => e.FacebookUrl).HasMaxLength(250);
            entity.Property(e => e.InstagramUrl).HasMaxLength(250);
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.LinkedinUrl).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.SkypeUrl).HasMaxLength(250);
            entity.Property(e => e.TwitterUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.CompNo, "IX_Customers_no")
            .IsUnique()
            .IsClustered();

            entity.Property(e => e.BossName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CompAddress).HasMaxLength(250);
            entity.Property(e => e.CompFax).HasMaxLength(50);
            entity.Property(e => e.CompID).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(250);
            entity.Property(e => e.CompNo).HasMaxLength(50);
            entity.Property(e => e.CompTel).HasMaxLength(50);
            entity.Property(e => e.CompUrl).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(250);
            entity.Property(e => e.EngShortName).HasMaxLength(50);
            entity.Property(e => e.FacebookUrl).HasMaxLength(250);
            entity.Property(e => e.InstagramUrl).HasMaxLength(250);
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.LinkedinUrl).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.SkypeUrl).HasMaxLength(250);
            entity.Property(e => e.TwitterUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<Departments>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.DeptNo, "IX_Departments_no")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.BossNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<EmployeeAgents>(entity =>
        {
            entity.Property(e => e.AgentNo).HasMaxLength(50);
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<EmployeeExperiences>(entity =>
        {
            entity.Property(e => e.BossName).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(50);
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.QuitReason).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TitleName).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeSchools>(entity =>
        {
            entity.Property(e => e.EducationName).HasMaxLength(50);
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SchoolName).HasMaxLength(50);
            entity.Property(e => e.SubjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.EmpNo, "IX_Employees_no").IsClustered();

            entity.Property(e => e.CityArea).HasMaxLength(50);
            entity.Property(e => e.CityName).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.EmpName).HasMaxLength(50);
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
        });

        modelBuilder.Entity<ExtensionTables>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ParentId, e.SortNo, e.ExtName }, "IX_ExtensionTables_pid_sort_name").IsClustered();

            entity.Property(e => e.ExtName).HasMaxLength(50);
            entity.Property(e => e.ExtNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Featureds>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.ProdNo }, "IX_Featureds_sort_pno").IsClustered();

            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<FormDetail>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.FormCode, e.FormNo }, "IX_FormDetail").IsClustered();

            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FormCode).HasMaxLength(50);
            entity.Property(e => e.FormNo).HasMaxLength(50);
            entity.Property(e => e.GuidNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.TargetName).HasMaxLength(50);
            entity.Property(e => e.TargetNo).HasMaxLength(50);
            entity.Property(e => e.TitleName).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
        });

        modelBuilder.Entity<FormMaster>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.FormCode, e.UserNo, e.FormNo }, "IX_FormMaster_code_user_no")
                .IsDescending(false, false, true)
                .IsClustered();

            entity.Property(e => e.ApproveNo).HasMaxLength(50);
            entity.Property(e => e.ApproveTime).HasColumnType("datetime");
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FormCode).HasMaxLength(50);
            entity.Property(e => e.FormNo).HasMaxLength(50);
            entity.Property(e => e.FormTime).HasColumnType("datetime");
            entity.Property(e => e.GuidNo).HasMaxLength(50);
            entity.Property(e => e.NextNo).HasMaxLength(50);
            entity.Property(e => e.NotifyKey).HasMaxLength(50);
            entity.Property(e => e.RejectNo).HasMaxLength(50);
            entity.Property(e => e.RejectTime).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SourceNo).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.StatusCode).HasMaxLength(50);
            entity.Property(e => e.TargetName).HasMaxLength(50);
            entity.Property(e => e.TargetNo).HasMaxLength(50);
            entity.Property(e => e.TitleName).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<ForumBoards>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.BoardNo }, "IX_ForumBoards_pid_sort_no").IsClustered();

            entity.Property(e => e.BoardName).HasMaxLength(250);
            entity.Property(e => e.BoardNo).HasMaxLength(50);
            entity.Property(e => e.GuidNo)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.IconName).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Forums>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.BoardNo, e.ParentGuid, e.SubjectDate }, "IX_Forums_pid_sort_name").IsClustered();

            entity.Property(e => e.BoardNo).HasMaxLength(50);
            entity.Property(e => e.GuidNo)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.ParentGuid).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ReplyGuid).HasMaxLength(50);
            entity.Property(e => e.SubjectDate).HasColumnType("datetime");
            entity.Property(e => e.SubjectName).HasMaxLength(250);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<InvDetails>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.WareHouseNo, e.ProductNo }, "IX_InvDetails_wno_pno").IsClustered();

            entity.Property(e => e.ProductNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.WareHouseNo).HasMaxLength(50);
        });

        modelBuilder.Entity<InvMasters>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.ProductNo, "IX_InvMasters_pno").IsClustered();

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.ProductNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Inventorys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.TypeNo, e.SheetNo }, "IX_Inventorys_tno_sno").IsClustered();

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.HandleNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SheetCode).HasMaxLength(50);
            entity.Property(e => e.SheetNo).HasMaxLength(50);
            entity.Property(e => e.TargetName).HasMaxLength(50);
            entity.Property(e => e.TargetNo).HasMaxLength(50);
            entity.Property(e => e.TypeNo).HasMaxLength(50);
            entity.Property(e => e.WarehouseNo).HasMaxLength(50);
        });

        modelBuilder.Entity<InventorysDetail>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ParentNo, e.ProductNo }, "IX_InventorysDetail_pno_pno").IsClustered();

            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.ProductNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<InventorysType>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.TypeNo, "IX_InventorysType_tno").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TypeName).HasMaxLength(50);
            entity.Property(e => e.TypeNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Languages>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.LangNo, "IX_Languages_no").IsClustered();

            entity.Property(e => e.LangName).HasMaxLength(50);
            entity.Property(e => e.LangNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Logs>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.KeyNo, "IX_Logs_KeyNo")
                .IsDescending()
                .IsClustered();

            entity.Property(e => e.KeyNo).HasMaxLength(50);
            entity.Property(e => e.LastTime).HasColumnType("datetime");
            entity.Property(e => e.LogCode).HasMaxLength(50);
            entity.Property(e => e.LogLevel).HasMaxLength(50);
            entity.Property(e => e.LogInterval).HasMaxLength(50);
            entity.Property(e => e.LogMessage).HasMaxLength(500);
            entity.Property(e => e.LogNo).HasMaxLength(50);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.LogTime).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TargetNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<MapPositions>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.TargetCode, e.TargetNo }, "IX_MapPositions_code_no").IsClustered();

            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TargetCode).HasMaxLength(50);
            entity.Property(e => e.TargetNo).HasMaxLength(50);
            entity.Property(e => e.TitleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Messages>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ReceiverNo, e.SendTime }, "IX_Messages_rno_time")
                .IsDescending(false, true, true)
                .IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.HeaderText).HasMaxLength(250);
            entity.Property(e => e.ReceiverNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SenderNo).HasMaxLength(50);
            entity.Property(e => e.SendTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Modules>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.RoleNo, e.SortNo, e.ModuleNo }, "IX_Modules_role_sort_no").IsClustered();

            entity.Property(e => e.IconName).HasMaxLength(50);
            entity.Property(e => e.ModuleName).HasMaxLength(50);
            entity.Property(e => e.ModuleNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.PublishDate, "IX_News_date")
                .IsDescending()
                .IsClustered();

            entity.HasIndex(e => new { e.CodeNo, e.PublishDate }, "IX_News_type_date").IsDescending(false, true);

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.HeaderName).HasMaxLength(50);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<NewsLetter>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.UserEmail, "IX_NewsLetter_email").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SubscribeDate).HasColumnType("datetime");
            entity.Property(e => e.UserEmail).HasMaxLength(50);
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ReceiverNo, e.SendDate, e.SendTime }, "IX_Notifications_rno_date_time")
                .IsDescending(false, true, true)
                .IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.HeaderText).HasMaxLength(250);
            entity.Property(e => e.ReceiverNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SenderNo).HasMaxLength(50);
            entity.Property(e => e.SourceNo).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderDetails>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ParentNo, e.ProdNo }, "IX_OrderDetails_sno").IsClustered();

            entity.Property(e => e.CategoryNo).HasMaxLength(50);
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.ProdName).HasMaxLength(250);
            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.ProdSpec).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.VendorNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.SheetNo, "IX_Orders_sno").IsClustered();

            entity.Property(e => e.CustName).HasMaxLength(50);
            entity.Property(e => e.CustNo).HasMaxLength(50);
            entity.Property(e => e.GuidNo).HasMaxLength(50);
            entity.Property(e => e.PaymentNo).HasMaxLength(50);
            entity.Property(e => e.ReceiverAddress).HasMaxLength(250);
            entity.Property(e => e.ReceiverEmail).HasMaxLength(50);
            entity.Property(e => e.ReceiverName).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SheetDate).HasColumnType("datetime");
            entity.Property(e => e.SheetNo).HasMaxLength(50);
            entity.Property(e => e.ShippingNo).HasMaxLength(50);
            entity.Property(e => e.StatusCode).HasMaxLength(50);
        });

        modelBuilder.Entity<OrdersStatus>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.StatusNo, "IX_OrdersStatus_sno").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.StatusName).HasMaxLength(50);
            entity.Property(e => e.StatusNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Overtimes>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(true);

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.SheetNo).HasMaxLength(50);
            entity.Property(e => e.SheetDate).HasColumnType("datetime");
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.ReasonText).HasMaxLength(50);
            entity.Property(e => e.TypeNo).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<OvertimeTypes>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(true);

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.TypeNo).HasMaxLength(50);
            entity.Property(e => e.TypeName).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Payments>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.PaymentNo, "IX_Payments_no").IsClustered();

            entity.Property(e => e.PaymentName).HasMaxLength(50);
            entity.Property(e => e.PaymentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Photos>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.CodeNo, e.FolderName }, "IX_Photos_type_folder").IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.FolderName).HasMaxLength(50);
            entity.Property(e => e.PhotoName).HasMaxLength(250);
            entity.Property(e => e.PriceName).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<PricingDetails>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.PricingNo, e.SortNo }, "IX_PricingDetails_pno_sort").IsClustered();

            entity.Property(e => e.ItemName).HasMaxLength(250);
            entity.Property(e => e.PricingNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Pricings>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.PricingNo }, "IX_Pricings_sort_no").IsClustered();

            entity.Property(e => e.CycleName).HasMaxLength(50);
            entity.Property(e => e.PricingName).HasMaxLength(50);
            entity.Property(e => e.PricingNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductFeatureds>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ProdNo, e.SortNo, e.FeaturedName }, "IX_ProductFeatureds_pno_sort_name").IsClustered();

            entity.Property(e => e.FeaturedName).HasMaxLength(50);
            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductInventorys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ProdNo, e.PropertyNo }, "IX_ProductInventorys_pno_propno").IsClustered();

            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.PropertyNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<ProductPropertys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ProdNo, e.PropertyNo }, "IX_ProductPropertys_pno_prodno").IsClustered();

            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.PropertyNo).HasMaxLength(50);
            entity.Property(e => e.PropertyValue).HasMaxLength(500);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<ProductStatus>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.StatusNo, "IX_ProductStatus_sno").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.StatusName).HasMaxLength(50);
            entity.Property(e => e.StatusNo).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductTags>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ProdNo, e.TagName }, "IX_ProductTags_pno_tname").IsClustered();

            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.ProdNo, "IX_Products_no").IsClustered();

            entity.Property(e => e.BarcodeNo).HasMaxLength(50);
            entity.Property(e => e.BrandName).HasMaxLength(50);
            entity.Property(e => e.BrandSeriesName).HasMaxLength(50);
            entity.Property(e => e.CategoryNo).HasMaxLength(50);
            entity.Property(e => e.ProdName).HasMaxLength(250);
            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.StatusNo).HasMaxLength(50);
            entity.Property(e => e.VendorNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Programs>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.ModuleNo, e.SortNo, e.PrgNo }, "IX_Programs_mno_sort_pno").IsClustered();

            entity.Property(e => e.ActionName).HasMaxLength(50);
            entity.Property(e => e.AreaName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ControllerName).HasMaxLength(50);
            entity.Property(e => e.ModuleNo).HasMaxLength(50);
            entity.Property(e => e.ParmValue).HasMaxLength(50);
            entity.Property(e => e.PrgName).HasMaxLength(50);
            entity.Property(e => e.PrgNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Promotions>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.StartTime, e.EndTime, e.ProdNo }, "IX_Promotions_stime_etime_pno")
                .IsDescending(true, true, false)
                .IsClustered();

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.ProdNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<PropertyNames>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.PropName, "IX_PropertyNames_name").IsClustered();

            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.PropName).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Propertys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.PropertyNo, "IX_Propertys_no").IsClustered();

            entity.Property(e => e.PropertyName).HasMaxLength(50);
            entity.Property(e => e.PropertyNo).HasMaxLength(50);
            entity.Property(e => e.PropertyValue).HasMaxLength(500);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Questions>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.SortNo, "IX_Questions_sno").IsClustered();

            entity.Property(e => e.AnswerText).HasMaxLength(500);
            entity.Property(e => e.QuestionText).HasMaxLength(500);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.RoleNo, "IX_Roles_no").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Securitys>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.RoleNo, e.TargetNo, e.ModuleNo, e.PrgNo }, "IX_Securitys_rno_tno_mno_pno").IsClustered();

            entity.Property(e => e.ModuleNo).HasMaxLength(50);
            entity.Property(e => e.PrgNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TargetNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Services>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.HeaderName }, "IX_Services_sort_name").IsClustered();

            entity.Property(e => e.DetailName).HasMaxLength(250);
            entity.Property(e => e.HeaderName).HasMaxLength(250);
            entity.Property(e => e.ImageUrl).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Shippings>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.ShippingNo, "IX_Shippings_no").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShippingName).HasMaxLength(50);
            entity.Property(e => e.ShippingNo).HasMaxLength(50);
        });

        modelBuilder.Entity<TableRelations>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.LeftColumnName, e.RightColumnName }, "IX_TableRelations").IsClustered();

            entity.Property(e => e.LeftColumnName).HasMaxLength(50);
            entity.Property(e => e.LeftDataType).HasMaxLength(50);
            entity.Property(e => e.LeftTableName).HasMaxLength(50);
            entity.Property(e => e.RelationName).HasMaxLength(250);
            entity.Property(e => e.RelationType).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RightColumnName).HasMaxLength(50);
            entity.Property(e => e.RightDataType).HasMaxLength(50);
            entity.Property(e => e.RightTableName).HasMaxLength(50);
        });

        modelBuilder.Entity<Teams>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.SortNo, e.TeamNo }, "IX_Teams_sort_no").IsClustered();

            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(50);
            entity.Property(e => e.FacebookUrl).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.InstagramUrl).HasMaxLength(50);
            entity.Property(e => e.LinkedinUrl).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SkypeUrl).HasMaxLength(50);
            entity.Property(e => e.SortNo).HasMaxLength(50);
            entity.Property(e => e.TeamName).HasMaxLength(50);
            entity.Property(e => e.TeamNo).HasMaxLength(50);
            entity.Property(e => e.TitleName).HasMaxLength(50);
            entity.Property(e => e.TwitterUrl).HasMaxLength(50);
        });

        modelBuilder.Entity<Testimonials>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(true);

            entity.Property(e => e.SendDate).HasColumnType("datetime");
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.TitleName).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<TempUser>(entity =>
        {
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Titles>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.TitleNo, "IX_Titles_no")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.TitleName).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
        });

        modelBuilder.Entity<TodoLists>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.UserNo, e.DeadlineDate }, "IX_TodoLists_uno_date")
                .IsDescending(false, true)
                .IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.DeadlineDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.TitleName).HasMaxLength(250);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.UserNo, "IX_Users_no").IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.NotifyPassword).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.ValidateCode).HasMaxLength(250);
        });

        modelBuilder.Entity<Vacations>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.VacYear, e.StartDate }, "IX_Vacations_no")
                .IsDescending(true, false)
                .IsClustered();

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
        });

        modelBuilder.Entity<Vendors>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.CompNo, "IX_Vendors_no")
            .IsUnique()
            .IsClustered();

            entity.Property(e => e.BossName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CompAddress).HasMaxLength(250);
            entity.Property(e => e.CompFax).HasMaxLength(50);
            entity.Property(e => e.CompID).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(250);
            entity.Property(e => e.CompNo).HasMaxLength(50);
            entity.Property(e => e.CompTel).HasMaxLength(50);
            entity.Property(e => e.CompUrl).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(250);
            entity.Property(e => e.EngShortName).HasMaxLength(50);
            entity.Property(e => e.FacebookUrl).HasMaxLength(250);
            entity.Property(e => e.InstagramUrl).HasMaxLength(250);
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.LinkedinUrl).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.SkypeUrl).HasMaxLength(250);
            entity.Property(e => e.TwitterUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<Warehouses>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.WarehouseNo, "IX_Warehouses_wno").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.WarehouseName).HasMaxLength(50);
            entity.Property(e => e.WarehouseNo).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkflowDetails>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.MasterGuidNo, e.RouteGuidNo, e.RouteOrder }, "IX_WorkflowDetails_mguid_rguid_rorder").IsClustered();

            entity.Property(e => e.AgentName).HasMaxLength(50);
            entity.Property(e => e.AgentNo).HasMaxLength(50);
            entity.Property(e => e.AgentReadTime).HasColumnType("datetime");
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.GuidNo)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.MasterGuidNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.RouteGuidNo).HasMaxLength(50);
            entity.Property(e => e.RouteOrder).HasMaxLength(50);
            entity.Property(e => e.SignComment).HasMaxLength(250);
            entity.Property(e => e.SignTime).HasColumnType("datetime");
            entity.Property(e => e.SignUserName).HasMaxLength(50);
            entity.Property(e => e.SignUserNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.UserReadTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<WorkflowMasters>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.FlowGuidNo, e.SheetNo, e.StartTime }, "IX_WorkflowMasters_fguid_fno_stime").IsClustered();

            entity.Property(e => e.DeadlineTime).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FlowGuidNo).HasMaxLength(50);
            entity.Property(e => e.GuidNo)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SheetName).HasMaxLength(50);
            entity.Property(e => e.SheetNo).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkflowRoleUsers>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_RoleUsers")
                .IsClustered(false);

            entity.HasIndex(e => new { e.RoleNo, e.UserNo }, "IX_WorkflowRoleUsers_rno_uno").IsClustered();

            entity.Property(e => e.AgentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkflowRoles>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.RoleNo, "IX_WorkflowRoles_no").IsClustered();

            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkflowRoutes>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.PrgNo, e.RouteOrder }, "IX_WorkflowRoutes_pno_rorder").IsClustered();

            entity.Property(e => e.PrgNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.RouteOrder).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeAddressBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeAddressBook");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeBrand>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeBrand");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeCalendar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeCalendar");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeCloseDate>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeCloseDate");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeColor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeColor");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeCompany>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeCompany");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeCustomer");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeFormStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeFormStatus");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeGender>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeGender");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeInventory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeInventory");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeLog");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeMaterial>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeMaterial");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeMember>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeMember");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeMessage>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeMessage");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeNews>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeNews");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeNotification>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeNotification");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodePhoto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodePhoto");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeProgram>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeProgram");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeResignReason>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeResignReason");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeSheet>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeSheet");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeSize>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeSize");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeTarget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeTarget");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeTax>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeTax");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeUser");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeVacation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeVacation");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CodeVendor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CodeVendor");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_CompCompany>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CompCompany");

            entity.Property(e => e.BossName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CompAddress).HasMaxLength(250);
            entity.Property(e => e.CompFax).HasMaxLength(50);
            entity.Property(e => e.CompID).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(250);
            entity.Property(e => e.CompNo).HasMaxLength(50);
            entity.Property(e => e.CompTel).HasMaxLength(50);
            entity.Property(e => e.CompUrl).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(250);
            entity.Property(e => e.EngShortName).HasMaxLength(50);
            entity.Property(e => e.FacebookUrl).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InstagramUrl).HasMaxLength(250);
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.LinkedinUrl).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.SkypeUrl).HasMaxLength(250);
            entity.Property(e => e.TwitterUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_CompCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CompCustomer");

            entity.Property(e => e.BossName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CompAddress).HasMaxLength(250);
            entity.Property(e => e.CompFax).HasMaxLength(50);
            entity.Property(e => e.CompID).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(250);
            entity.Property(e => e.CompNo).HasMaxLength(50);
            entity.Property(e => e.CompTel).HasMaxLength(50);
            entity.Property(e => e.CompUrl).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(250);
            entity.Property(e => e.EngShortName).HasMaxLength(50);
            entity.Property(e => e.FacebookUrl).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InstagramUrl).HasMaxLength(250);
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.LinkedinUrl).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.SkypeUrl).HasMaxLength(250);
            entity.Property(e => e.TwitterUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_CompVendor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_CompVendor");

            entity.Property(e => e.BossName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CompAddress).HasMaxLength(250);
            entity.Property(e => e.CompFax).HasMaxLength(50);
            entity.Property(e => e.CompID).HasMaxLength(50);
            entity.Property(e => e.CompName).HasMaxLength(250);
            entity.Property(e => e.CompNo).HasMaxLength(50);
            entity.Property(e => e.CompTel).HasMaxLength(50);
            entity.Property(e => e.CompUrl).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.EngName).HasMaxLength(250);
            entity.Property(e => e.EngShortName).HasMaxLength(50);
            entity.Property(e => e.FacebookUrl).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InstagramUrl).HasMaxLength(250);
            entity.Property(e => e.Latitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.LinkedinUrl).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasColumnType("decimal(20, 15)");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.SkypeUrl).HasMaxLength(250);
            entity.Property(e => e.TwitterUrl).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_SysIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_SysIndex");

            entity.Property(e => e.IndexName).HasMaxLength(128);
            entity.Property(e => e.IsClustered)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.IsPrimaryKey)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.IsUnique)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.TableName).HasMaxLength(128);
        });

        modelBuilder.Entity<vi_SysTableColumn>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_SysTableColumn");

            entity.Property(e => e.ColumnName).HasMaxLength(128);
            entity.Property(e => e.Comments).HasColumnType("sql_variant");
            entity.Property(e => e.DataType).HasMaxLength(128);
            entity.Property(e => e.DataTypeName).HasMaxLength(160);
            entity.Property(e => e.DefaultValue).HasMaxLength(4000);
            entity.Property(e => e.IsIdentity)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.IsNullable)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SchemaName).HasMaxLength(128);
            entity.Property(e => e.TableName).HasMaxLength(128);
            entity.Property(e => e.TableType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<vi_TodoList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_TodoList");

            entity.Property(e => e.BaseNo).HasMaxLength(50);
            entity.Property(e => e.CodeName).HasMaxLength(50);
            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.CodeValue).HasMaxLength(250);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ParentNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.SortNo).HasMaxLength(50);
        });

        modelBuilder.Entity<vi_UserCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_UserCustomer");

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.ValidateCode).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_UserMis>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_UserMis");

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.ValidateCode).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_UserOperator>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_UserOperator");

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.ValidateCode).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_UserTarget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_UserTarget");

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.ValidateCode).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_UserUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_UserUser");

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.ValidateCode).HasMaxLength(250);
        });

        modelBuilder.Entity<vi_UserVendor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vi_UserVendor");

            entity.Property(e => e.CodeNo).HasMaxLength(50);
            entity.Property(e => e.ContactAddress).HasMaxLength(250);
            entity.Property(e => e.ContactEmail).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.DeptNo).HasMaxLength(50);
            entity.Property(e => e.GenderCode).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Remark).HasMaxLength(250);
            entity.Property(e => e.RoleNo).HasMaxLength(50);
            entity.Property(e => e.TitleNo).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserNo).HasMaxLength(50);
            entity.Property(e => e.ValidateCode).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
