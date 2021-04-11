using Application.Features.Roulettes.Commands.CreateRoulette;
using Application.Features.Roulettes.Commands.OpeningRoulette;
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
        public async Task<IActionResult> Post()
        {
            return Ok(await Mediator.Send(new CreateRouletteCommand()));
        }

        [HttpPost]
        [Route("{id}/opening")]
        [ProducesResponseType(typeof(OpeningRouletteResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostOpening(string id)
        {
            return Ok(await Mediator.Send(new OpeningRouletteCommand() { Id = id }));
        }
    }
}
