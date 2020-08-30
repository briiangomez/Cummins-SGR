using SGI.DAL.Contracts;
using SGI.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SGI.DAL.Concrete
{
    public class ArticleManager : IManager<Article>
    {
        private readonly IDapperManager _dapperManager;

        public ArticleManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(Article article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Title", article.Title, DbType.String);
            dbPara.Add("IdCategory", article.Category?.IdCategory);

            var articleId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return articleId;
        }

        public Task<Article> GetById(int id)
        {
            string query = "select A.IdArticle, A.Title, C.IdCategory, C.Name from [Article] A INNER JOIN [Category] C " +
                           $"ON A.IdCategory = C.IdCategory where IdArticle = {id}";

            var article = Task.FromResult(_dapperManager.Get<Article, Category>
                                            (query,
                                            (article, category) =>
                                            {
                                                article.Category = category;                                                 
                                                return article;
                                            }, "IdCategory",
                                            null, commandType: CommandType.Text));
            return article;
        }

        public Task<int> Delete(int id)
        {
            var deleteArticle = Task.FromResult(_dapperManager.Execute($"Delete [Article] where IdArticle = {id}", null,
                    commandType: CommandType.Text));

            return deleteArticle;
        }

        public Task<int> Count(string search)
        {
            var totArticle = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [Article] WHERE Title like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totArticle;
        }

        public Task<List<Article>> ListAll()
        {
            var articles = Task.FromResult(_dapperManager.GetAll<Article>
                            ($"SELECT * FROM [Article]", null, commandType: CommandType.Text));
            return articles;
        }


        public Task<List<Article>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            string query = $"SELECT A.IdArticle, A.Title, A.IdCategory, C.Name FROM [Article] A INNER JOIN [Category] C" +
                        $" ON A.IdCategory = C.IdCategory" +
                        $" WHERE Title like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";

            var articles = Task.FromResult(_dapperManager.GetAll<Article, Category>
                                            (query,
                                            (article, category) =>
                                            {
                                                article.Category = category;
                                                return article;
                                            }, "IdCategory",
                                            null, commandType: CommandType.Text));
            return articles;
        }

        public Task<int> Update(Article article)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("IdArticle", article.IdArticle);
            dbPara.Add("Title", article.Title, DbType.String);
            dbPara.Add("IdCategory", article.Category?.IdCategory);

            var updateArticle = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_Article]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }


    }
}
