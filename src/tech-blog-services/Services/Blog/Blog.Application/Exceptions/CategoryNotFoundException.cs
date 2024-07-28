
namespace Blog.Application.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(Guid id) : base("Category", id)
        {
        }
    }
}
