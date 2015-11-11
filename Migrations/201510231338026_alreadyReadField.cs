namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alreadyReadField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AlreadyRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "AlreadyRead");
        }
    }
}
