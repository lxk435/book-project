namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKeys : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CoverImages");
            AlterColumn("dbo.CoverImages", "ImageId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CoverImages", "ImageId");
            CreateIndex("dbo.CoverImages", "ImageId");
            AddForeignKey("dbo.CoverImages", "ImageId", "dbo.Books", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoverImages", "ImageId", "dbo.Books");
            DropIndex("dbo.CoverImages", new[] { "ImageId" });
            DropPrimaryKey("dbo.CoverImages");
            AlterColumn("dbo.CoverImages", "ImageId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CoverImages", "ImageId");
        }
    }
}
