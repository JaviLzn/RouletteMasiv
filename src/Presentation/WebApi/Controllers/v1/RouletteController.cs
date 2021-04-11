using Application.Features.Roulettes.Commands.CreateRoulette;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RouletteController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateRouletteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
