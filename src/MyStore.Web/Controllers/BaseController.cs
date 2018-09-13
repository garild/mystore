using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Messages;

namespace MyStore.Web.Controllers
{
    [Route("[controller]")]
    public abstract class BaseController : Controller
    {
        private readonly IDispatcher _dispatcher;

        protected BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected IActionResult Single<T>(T model)
        {
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        protected async Task SendAsync<T>(T command) where T : ICommand
            => await _dispatcher.SendAsync(command);

        protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => await _dispatcher.QueryAsync(query);
    }
}