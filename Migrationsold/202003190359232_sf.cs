namespace IdeaCollectorSH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "IdeaID", "dbo.Ideas");
            DropForeignKey("dbo.Comments", "StaffID", "dbo.Staffs");
            DropForeignKey("dbo.Staffs", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Ideas", "StaffID", "dbo.Staffs");
            DropForeignKey("dbo.Documents", "IdeaID", "dbo.Ideas");
            DropIndex("dbo.Ideas", new[] { "StaffID" });
            DropIndex("dbo.Comments", new[] { "IdeaID" });
            DropIndex("dbo.Comments", new[] { "StaffID" });
            DropIndex("dbo.Staffs", new[] { "DepartmentID" });
            DropIndex("dbo.Documents", new[] { "IdeaID" });
            DropTable("dbo.Ideas");
            DropTable("dbo.Comments");
            DropTable("dbo.Staffs");
            DropTable("dbo.Departments");
            DropTable("dbo.Documents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentID = c.Int(nullable: false, identity: true),
                        IdeaID = c.Int(),
                        DocumentName = c.String(),
                    })
                .PrimaryKey(t => t.DocumentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        DepartmentID = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(),
                        Password = c.String(),
                        PhoneNumber = c.Int(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.StaffID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        SubmitDate = c.DateTime(nullable: false),
                        IdeaID = c.Int(),
                        StaffID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId);
            
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        IdeaID = c.Int(nullable: false, identity: true),
                        IdeaTitle = c.String(),
                        SubmitDate = c.DateTime(nullable: false),
                        Category = c.String(),
                        Price = c.String(),
                        TUp = c.Int(),
                        TDown = c.Int(),
                        ExpiryDate = c.DateTime(),
                        ViewCount = c.Int(),
                        StaffID = c.Int(),
                        IdeaDescription = c.String(),
                    })
                .PrimaryKey(t => t.IdeaID);
            
            CreateIndex("dbo.Documents", "IdeaID");
            CreateIndex("dbo.Staffs", "DepartmentID");
            CreateIndex("dbo.Comments", "StaffID");
            CreateIndex("dbo.Comments", "IdeaID");
            CreateIndex("dbo.Ideas", "StaffID");
            AddForeignKey("dbo.Documents", "IdeaID", "dbo.Ideas", "IdeaID");
            AddForeignKey("dbo.Ideas", "StaffID", "dbo.Staffs", "StaffID");
            AddForeignKey("dbo.Staffs", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "StaffID", "dbo.Staffs", "StaffID");
            AddForeignKey("dbo.Comments", "IdeaID", "dbo.Ideas", "IdeaID");
        }
    }
}
