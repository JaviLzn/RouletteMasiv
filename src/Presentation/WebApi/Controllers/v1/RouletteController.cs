using Application.Features.Roulettes.Commands.BetRoulette;
using Application.Features.Roulettes.Commands.CreateRoulette;
using Application.Features.Roulettes.Commands.EndingRoulette;
using Application.Features.Roulettes.Commands.OpeningRoulette;
using Application.Features.Roulettes.Queries.GetAllRoulettes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RouletteController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateRouletteResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync()
        {
            return Ok(await Mediator.Send(new CreateRouletteCommand()));
        }

        [HttpPost]
        [Route("{id}/Opening")]
        [ProducesResponseType(typeof(OpeningRouletteResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostOpeningAsync(string id)
        {
            return Ok(await Mediator.Send(new OpeningRouletteCommand() { RouletteId = id }));
        }

        [HttpPost]
        [Route("{id}/Bet")]
        [ProducesResponseType(typeof(BetRouletteResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostBetAsync(string id, [FromHeader(Name = "UserId")] string userId, BetRouletteRequest request)
        {
            return Ok(await Mediator.Send(new BetRouletteCommand() { RouletteId = id, UserId = userId, Amount = request.Amount, Color = request.Color, Number = request.Number }));
        }

        [HttpPost]
        [Route("{id}/Ending")]
        [ProducesResponseType(typeof(EndingRouletteResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostEndingAsync(string id)
        {
            return Ok(await Mediator.Send(new EndingRouletteCommand() { RouletteId = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator.Send(new GetAllRoulettesQuery() {}));
        }
    }
}
