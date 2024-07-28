
namespace Blog.Application.Exceptions
{
    public class ArticleNotFoundException : NotFoundException
    {
        public ArticleNotFoundException(Guid id) : base("Article", id)
        {
        }
    }
}
