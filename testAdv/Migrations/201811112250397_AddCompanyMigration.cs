namespace testAdv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Author", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Author", c => c.Guid(nullable: false));
        }
    }
}
