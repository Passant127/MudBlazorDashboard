using BlazorApp1.Models.Article;

namespace BlazorApp1.Services.Articles;

public interface IArticlesService
{
    Task<IEnumerable<ArticlePreviewModel>> GetArticles();
}