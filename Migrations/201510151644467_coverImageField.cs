namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coverImageField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CoverImages", "ImageId", "dbo.Books");
            DropIndex("dbo.CoverImages", new[] { "ImageId" });
            DropTable("dbo.CoverImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CoverImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false),
                        Name = c.String(),
                        Size = c.Long(nullable: false),
                        Type = c.String(),
                        FileContent = c.Binary(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateIndex("dbo.CoverImages", "ImageId");
            AddForeignKey("dbo.CoverImages", "ImageId", "dbo.Books", "Id");
        }
    }
}
