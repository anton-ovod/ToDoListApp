using ToDoListApplication.Providers;

namespace ToDoListApplication.Middlewares
{
    public class StorageTypeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly StorageTypeProvider _storageTypeProvider;

        public StorageTypeMiddleware(RequestDelegate next, StorageTypeProvider storageTypeProvider)
        {
            _next = next;
            _storageTypeProvider = storageTypeProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //context.Items["Storage-Type"] = _storageTypeProvider.StorageType;
            if(string.IsNullOrEmpty(context.Request.Headers["Storage-Type"]))
                context.Request.Headers["Storage-Type"] = _storageTypeProvider.StorageType;
            await _next(context);
        }
    }
}
