namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctFileContentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CoverImages", "FileContent", c => c.Binary());
            AlterColumn("dbo.CoverImages", "Size", c => c.Long(nullable: false));
            AlterColumn("dbo.CoverImages", "ModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CoverImages", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CoverImages", "Size", c => c.Int(nullable: false));
            DropColumn("dbo.CoverImages", "FileContent");
        }
    }
}
