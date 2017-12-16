namespace TasksManager.Infrastructure.DAL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultCategories : DbMigration
    {
        public override void Up()
        {
            Sql($"INSERT INTO Categories VALUES ('{Guid.NewGuid()}','Work')");
            Sql($"INSERT INTO Categories VALUES ('{Guid.NewGuid()}','Study')");
        }
        
        public override void Down()
        {
        }
    }
}
