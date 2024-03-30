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
        GamesRepository.start =
            GamesRepository.start == null ? new VectorDto() { X = 1, Y = 1 } : GamesRepository.start;
         
        var move = GetMove(userInput, GamesRepository.start);
        var game = TestData.AGameDto(GamesRepository.start, gameId);
        if (GamesRepository.Games.TryGetValue(gameId, out var game1))
        {
            game = game1;
        }
        var prev = new VectorDto(){X = GamesRepository.start.X, Y = GamesRepository.start.Y};
        if (!IsWall(game, move))
        {
            GamesRepository.start = move;
        }
        game.Cells.First(c => c.Type == "player").Pos = GamesRepository.start;
        moveBrick(game, move, prev);
        return Ok(game);
    }

    public void moveBrick(GameDto game, VectorDto pos, VectorDto prev)
    {
        var t = game.Cells.Any(c => c.Pos.X == pos.X && c.Pos.Y == pos.Y && c.Type == "box");
        if (t)
        {
            var box = game.Cells.First(c => c.Pos.X == pos.X && c.Pos.Y == pos.Y && c.Type == "box");
            if (prev.X < pos.X)
            {
                box.Pos.X++;
            }
            
            else if (prev.X > pos.X)
            {
                box.Pos.X--;
            }
            else if (prev.Y < pos.Y)
            {
                box.Pos.Y++;
            }
            else if (prev.Y > pos.Y)
            {
                box.Pos.Y--;
            }
        }
    }
    
    public bool IsWall(GameDto game, VectorDto pos)
    {
        var objects = new HashSet<string>(){"wall"};
        var b = game.Cells.Any(c => c.Pos.X == pos.X && c.Pos.Y == pos.Y && objects.Contains(c.Type));
        return b;
    }
    public VectorDto GetMove(UserInputDto userInput,VectorDto start)
    {
        var result = new VectorDto(){X = start.X, Y = start.Y};
        switch (userInput.KeyPressed)
        {
            case 'W' when result.Y > 0:
                result.Y--;
                break;
            case 'S':
                result.Y++;
                break;
            case 'A' when result.X > 0:
                result.X--;
                break;
            case 'D':
                result.X++;
                break;
        }

        return result;
    }
}