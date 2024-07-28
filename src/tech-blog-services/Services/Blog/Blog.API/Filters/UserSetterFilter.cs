using Blog.API.Extensions;
using Blog.Application.Helpers;

namespace Blog.API.Filters
{
    public class UserSetterFilter : IEndpointFilter
    {
        public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var userContext = context.HttpContext.RequestServices.GetService<IUserContext>();

            var user = context.HttpContext.User;

            userContext.SetUser(user.GetUserId(), user.GetUserRole());

            return next(context);
        }
    }
}
