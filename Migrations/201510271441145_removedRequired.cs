namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "AuthorLastName", c => c.String());
            AlterColumn("dbo.Books", "AuthorFirstName", c => c.String());
            AlterColumn("dbo.Books", "AuthorPlaceOfBirth", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "AuthorPlaceOfBirth", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorFirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorLastName", c => c.String(nullable: false));
        }
    }
}
