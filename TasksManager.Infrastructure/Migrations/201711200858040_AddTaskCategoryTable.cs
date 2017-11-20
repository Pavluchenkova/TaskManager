namespace TasksManager.Infrastructure.DAL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tasks", "CategoryId", c => c.Guid());
            CreateIndex("dbo.Tasks", "CategoryId");
            AddForeignKey("dbo.Tasks", "CategoryId", "dbo.TaskCategories", "Id");
            DropColumn("dbo.Tasks", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Category", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tasks", "CategoryId", "dbo.TaskCategories");
            DropIndex("dbo.Tasks", new[] { "CategoryId" });
            DropColumn("dbo.Tasks", "CategoryId");
            DropTable("dbo.TaskCategories");
        }
    }
}
