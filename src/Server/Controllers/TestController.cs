using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace Trailblazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IAsyncDocumentSession _session;

        public TestController(IAsyncDocumentSession session)
        {
            _session = session;
        }

        [HttpGet("")]
        public async Task<IActionResult> FetchData(CancellationToken cancellationToken)
        {
            var items = await _session.Query<FooEntity>(collectionName: "Foo").ToListAsync();
            return Json(items);
        }
    }

    public record FooEntity(string Name)
    {
    }
}
