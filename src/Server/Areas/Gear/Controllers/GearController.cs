using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trailblazor.Domain.Common;
using Trailblazor.Server.Services;
using Trailblazor.Shared.Models;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Areas.Gear.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GearController : Controller
    {
        private readonly IDataService<GearListViewModel> _gearListService;

        public GearController(IDataService<GearListViewModel> gearListService)
        {
            _gearListService = gearListService;
        }

        [HttpGet("GearList")]
        public async Task<IActionResult> GetGearLists(BaseQueryOptions queryOptions, CancellationToken cancellationToken)
        {
            var result = await _gearListService.GetAll(queryOptions, cancellationToken);

            return Ok(result);
        }

        [HttpGet("GearList/{gearListId}")]
        public async Task<IActionResult> GetGearListById(Guid gearListId, CancellationToken cancellationToken)
        {
            var result = await _gearListService.GetById(gearListId, cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("GearList")]
        public async Task<IActionResult> SaveGearList(GearListViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel is null)
                return BadRequest("View model is null or invalid.");

            await _gearListService.Save(viewModel, cancellationToken);

            return Ok();
        }
    }
}
