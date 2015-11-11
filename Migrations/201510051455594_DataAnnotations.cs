namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorLastName", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorFirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "AuthorPlaceOfBirth", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Genre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Genre", c => c.String());
            AlterColumn("dbo.Books", "AuthorPlaceOfBirth", c => c.String());
            AlterColumn("dbo.Books", "AuthorFirstName", c => c.String());
            AlterColumn("dbo.Books", "AuthorLastName", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
