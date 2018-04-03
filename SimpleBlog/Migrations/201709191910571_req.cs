namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class req : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "PostTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "PostIntro", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "PostContent", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Replies", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.Replies", "Content", c => c.String());
            AlterColumn("dbo.Comments", "Content", c => c.String());
            AlterColumn("dbo.Posts", "PostContent", c => c.String());
            AlterColumn("dbo.Posts", "PostIntro", c => c.String());
            AlterColumn("dbo.Posts", "PostTitle", c => c.String());
        }
    }
}
