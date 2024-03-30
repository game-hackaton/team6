using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games/{gameId}/moves")]
public class MovesController : Controller
{
    [HttpPost]
    public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
    {
        var game = TestData.AGameDto(userInput.ClickedPos ?? new VectorDto {X = 1, Y = 1}, gameId);
        
        if (userInput.ClickedPos != null && !IsWall(game,userInput.ClickedPos))
        {
            GamesRepository.start = userInput.ClickedPos;
        }
        game.Cells.First(c => c.Type == "player").Pos = GamesRepository.start;
        game = TestData.AGameDto(GamesRepository.start, gameId);
        

        return Ok(game);
    }

    public bool IsWall(GameDto game, VectorDto pos)
    {
        var objects = new HashSet<string>{"wall", "box"};
        var b = game.Cells.Any(c => c.Pos.X == pos.X && c.Pos.Y == pos.Y && objects.Contains(c.Type));
        return b;
    }
}