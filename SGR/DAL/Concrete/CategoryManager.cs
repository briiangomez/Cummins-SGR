using SGR.DAL.Contracts;
using SGR.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SGR.DAL.Concrete
{
    public class CategoryManager : IManager<Category>
    {
        private readonly IDapperManager _dapperManager;

        public CategoryManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(Category category)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Name", category.Name, DbType.String);
            dbPara.Add("Description", category.Description, DbType.String);

            var categoryId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_Category]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return categoryId;
        }

        public Task<Category> GetById(int id)
        {
            var category = Task.FromResult(_dapperManager.Get<Category>($"select * from [Category] where IdCategory = {id}", null,
                    commandType: CommandType.Text));
            return category;
        }

        public Task<int> Delete(int id)
        {
            var deleteCategory = Task.FromResult(_dapperManager.Execute($"Delete [Category] where IdCategory = {id}", null,
                    commandType: CommandType.Text));
            return deleteCategory;
        }

        public Task<int> Count(string search)
        {
            var totCategory = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [Category] WHERE Title like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totCategory;
        }

        public Task<List<Category>> ListAll()
        {
            var categories = Task.FromResult(_dapperManager.GetAll<Category>
                ($"SELECT * FROM [Category]", null, commandType: CommandType.Text));
            return categories;
        }

        public Task<List<Category>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var categories = Task.FromResult(_dapperManager.GetAll<Category>
                ($"SELECT * FROM [Category] WHERE Title like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return categories;
        }

        public Task<int> Update(Category category)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Name", category.Name, DbType.String);
            dbPara.Add("Description", category.Description, DbType.String);

            var updateCategory = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_Category]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateCategory;
        }
    }
}
