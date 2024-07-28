namespace Blog.Domain.Events
{
    public record ArticleUpdatedEvent(Article article) : IDomainEvent;
}
