using BlazorApp1.Models.Article;
using System.Net.Http.Json;

namespace BlazorApp1.Services.Articles;

public class ArticlesService : IArticlesService
{
    private const string UriRequest = "sample-data/articles.json";
    private readonly HttpClient _htt;

    public ArticlesService(HttpClient htt)
    {
        _htt = htt;
    }

    public async Task<IEnumerable<ArticlePreviewModel>> GetArticles()
    {
        var articles = await _htt.GetFromJsonAsync<IEnumerable<ArticlePreviewModel>>(UriRequest);
        return articles ?? throw new InvalidOperationException();
    }
}