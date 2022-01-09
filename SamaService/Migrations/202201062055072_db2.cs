namespace SamaService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYears",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    YearsName = c.String(nullable: false, maxLength: 250),
                    YearsStart = c.DateTime(nullable: false),
                    YearsFinish = c.DateTime(nullable: false),
                    Enabled = c.Boolean(nullable: false),
                    Description = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Registereds",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    AcademicYearID_FK = c.Int(nullable: false),
                    SchoolID_FK = c.Int(nullable: false),
                    ClassRoomID_FK = c.Int(nullable: false),
                    StudentID_FK = c.Int(nullable: false),
                    Enabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassRooms", t => t.ClassRoomID_FK)
                .ForeignKey("dbo.Schools", t => t.SchoolID_FK)
                .ForeignKey("dbo.Students", t => t.StudentID_FK)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearID_FK)
                .Index(t => t.AcademicYearID_FK)
                .Index(t => t.SchoolID_FK)
                .Index(t => t.ClassRoomID_FK)
                .Index(t => t.StudentID_FK);

            CreateTable(
                "dbo.ClassRooms",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ClassName = c.String(nullable: false, maxLength: 250),
                    ClassLevelID_FK = c.Int(nullable: false),
                    ClassRegisterDate = c.DateTime(nullable: false),
                    ClassRoomEnable = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassLevels", t => t.ClassLevelID_FK)
                .Index(t => t.ClassLevelID_FK);

            CreateTable(
                "dbo.ClassLevels",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ClassLevelTitle = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Schools",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    SchoolName = c.String(nullable: false, maxLength: 250),
                    SchoolAddress = c.String(maxLength: 250),
                    SchoolTel = c.String(maxLength: 15),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Students",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FName = c.String(nullable: false, maxLength: 50),
                    LName = c.String(nullable: false, maxLength: 100),
                    FullName = c.String(maxLength: 151),
                    StudentCode = c.String(maxLength: 20),
                    StudentNatinalCode = c.String(maxLength: 11),
                    FatherName = c.String(nullable: false, maxLength: 50),
                    MotherName = c.String(maxLength: 50),
                    HomePhone = c.String(maxLength: 11),
                    FatherPhone = c.String(maxLength: 11),
                    MotherPhone = c.String(maxLength: 11),
                    SMS = c.String(nullable: false, maxLength: 11),
                    BrithDate = c.DateTime(nullable: false, storeType: "date"),
                    RegDate = c.DateTime(nullable: false),
                    Enabled = c.Boolean(nullable: false),
                    Picture = c.Binary(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.BirthRegisters",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    StudentID_FK = c.Int(nullable: false),
                    Registered = c.DateTime(nullable: false, storeType: "date"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID_FK)
                .Index(t => t.StudentID_FK);

            CreateTable(
                "dbo.StudentTAGs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    TagID_FK = c.Int(nullable: false),
                    StudentID_FK = c.Int(nullable: false),
                    Enabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tags", t => t.TagID_FK)
                .ForeignKey("dbo.Students", t => t.StudentID_FK)
                .Index(t => t.TagID_FK)
                .Index(t => t.StudentID_FK);

            CreateTable(
                "dbo.Tags",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    TagID_HEX = c.String(nullable: false, maxLength: 50),
                    Enabled = c.Boolean(nullable: false),
                    DeleteTAG = c.Boolean(nullable: false),
                    CartView = c.String(maxLength: 10),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.TagRecorders",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    TagID = c.String(nullable: false, maxLength: 50),
                    DateTimeRegister = c.DateTime(nullable: false),
                    MysqlID = c.Int(nullable: false),
                    SMS = c.Boolean(nullable: false),
                    Type = c.Boolean(nullable: false),
                    Enables = c.Boolean(nullable: false),
                    TagID_FK = c.Int(),
                    StudentID_FK = c.Int(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false, maxLength: 50),
                    FName = c.String(nullable: false, maxLength: 50),
                    LName = c.String(nullable: false, maxLength: 50),
                    Mobile = c.String(nullable: false, maxLength: 12),
                    Password = c.String(nullable: false, maxLength: 120),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Registereds", "AcademicYearID_FK", "dbo.AcademicYears");
            DropForeignKey("dbo.StudentTAGs", "StudentID_FK", "dbo.Students");
            DropForeignKey("dbo.StudentTAGs", "TagID_FK", "dbo.Tags");
            DropForeignKey("dbo.Registereds", "StudentID_FK", "dbo.Students");
            DropForeignKey("dbo.BirthRegisters", "StudentID_FK", "dbo.Students");
            DropForeignKey("dbo.Registereds", "SchoolID_FK", "dbo.Schools");
            DropForeignKey("dbo.Registereds", "ClassRoomID_FK", "dbo.ClassRooms");
            DropForeignKey("dbo.ClassRooms", "ClassLevelID_FK", "dbo.ClassLevels");
            DropIndex("dbo.StudentTAGs", new[] { "StudentID_FK" });
            DropIndex("dbo.StudentTAGs", new[] { "TagID_FK" });
            DropIndex("dbo.BirthRegisters", new[] { "StudentID_FK" });
            DropIndex("dbo.ClassRooms", new[] { "ClassLevelID_FK" });
            DropIndex("dbo.Registereds", new[] { "StudentID_FK" });
            DropIndex("dbo.Registereds", new[] { "ClassRoomID_FK" });
            DropIndex("dbo.Registereds", new[] { "SchoolID_FK" });
            DropIndex("dbo.Registereds", new[] { "AcademicYearID_FK" });
            DropTable("dbo.Users");
            DropTable("dbo.TagRecorders");
            DropTable("dbo.Tags");
            DropTable("dbo.StudentTAGs");
            DropTable("dbo.BirthRegisters");
            DropTable("dbo.Students");
            DropTable("dbo.Schools");
            DropTable("dbo.ClassLevels");
            DropTable("dbo.ClassRooms");
            DropTable("dbo.Registereds");
            DropTable("dbo.AcademicYears");
        }
    }
}
