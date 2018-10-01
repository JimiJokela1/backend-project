using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace backend_project
{
    public class SameNamePlayerResult : ObjectResult
    {
        public SameNamePlayerResult() : base(null)
        { }

        public SameNamePlayerResult(object value) : base(value) 
        { }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = StatusCode ?? 400;
            return Task.CompletedTask;
        }
    }
}