namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class u3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "UserId");
        }
    }
}
