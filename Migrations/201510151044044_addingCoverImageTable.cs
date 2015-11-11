namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingCoverImageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoverImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.Int(nullable: false),
                        Type = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CoverImages");
        }
    }
}
