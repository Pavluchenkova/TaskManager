namespace TasksManager.Infrastructure.DAL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tasks", "CategoryId", c => c.Guid());
            CreateIndex("dbo.Tasks", "CategoryId");
            AddForeignKey("dbo.Tasks", "CategoryId", "dbo.Categories", "Id");
            DropColumn("dbo.Tasks", "Category");

            Sql($"INSERT INTO dbo.Categories VALUES ('{Guid.NewGuid()}','Personal')");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Category", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tasks", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Tasks", new[] { "CategoryId" });
            DropColumn("dbo.Tasks", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
