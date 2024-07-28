
namespace Blog.Domain.Events
{
    public record ArticleCreatedEvent(Article article) : IDomainEvent;
}
