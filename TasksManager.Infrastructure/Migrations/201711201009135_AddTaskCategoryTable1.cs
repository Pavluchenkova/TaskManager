namespace TasksManager.Infrastructure.DAL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskCategoryTable1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tasks", name: "CategoryId", newName: "TaskCategoryId");
            RenameIndex(table: "dbo.Tasks", name: "IX_CategoryId", newName: "IX_TaskCategoryId");
            AddColumn("dbo.TaskCategories", "CategoryName", c => c.String());
            DropColumn("dbo.TaskCategories", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskCategories", "Category", c => c.String());
            DropColumn("dbo.TaskCategories", "CategoryName");
            RenameIndex(table: "dbo.Tasks", name: "IX_TaskCategoryId", newName: "IX_CategoryId");
            RenameColumn(table: "dbo.Tasks", name: "TaskCategoryId", newName: "CategoryId");
        }
    }
}
