using System;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games")]
public class GamesController : Controller
{
    [HttpPost]
    public IActionResult Index()
    {
        var guid = Guid.NewGuid();
        GamesRepository.Games.Add(guid, TestData.AGameDto(new VectorDto() {X = 4, Y = 3}, guid));
        return Ok(GamesRepository.Games[guid]);
    }
}