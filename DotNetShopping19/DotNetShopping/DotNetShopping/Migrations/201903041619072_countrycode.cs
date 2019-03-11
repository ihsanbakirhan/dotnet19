namespace DotNetShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countrycode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "Code", c => c.String());
            AddColumn("dbo.States", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.States", "Code");
            DropColumn("dbo.Countries", "Code");
        }
    }
}
