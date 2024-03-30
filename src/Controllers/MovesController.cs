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
        if (!GamesRepository.Games.TryGetValue(gameId, out var game))
            return NotFound();
        
        var player = game.Cells.First(c => c.Type == "player");
        var move = GetMove(userInput, player.Pos);
        
        moveBrick(game, move, player.Pos);
        
        if (!IsWall(game, move))
        {
            player.Pos = move;
        }
        
        return Ok(game);
    }

    public void moveBrick(GameDto game, VectorDto pos, VectorDto prev)
    {
        var next = new VectorDto() { X = pos.X - prev.X, Y = pos.Y - prev.Y };
        var t = game.Cells.Any(c => c.Pos.X == pos.X && c.Pos.Y == pos.Y && (c.Type == "box" || c.Type == "boxOnTarget"));
        if (t && !IsWall(game, new VectorDto() {X = pos.X + next.X, Y = pos.Y + next.Y}))
        {
            var box = game.Cells.First(c => c.Pos.X == pos.X && c.Pos.Y == pos.Y && (c.Type == "box" || c.Type == "boxOnTarget"));
            if (prev.X < pos.X)
            {
                prev.X++;
                box.Pos.X++;
            }
            
            else if (prev.X > pos.X)
            {
                prev.X--;
                box.Pos.X--;
            }
            else if (prev.Y < pos.Y)
            {
                prev.Y++;
                box.Pos.Y++;
            }
            else if (prev.Y > pos.Y)
            {
                prev.Y--;
                box.Pos.Y--;
            }

            if (game.Cells.Any(c => c.Type == "target" && c.Pos.X == box.Pos.X && c.Pos.Y == box.Pos.Y))
            {
                box.Type = "boxOnTarget";
            }
            else
            {
                box.Type = "box";
            }
        }
    }
    
    public bool IsWall(GameDto game, VectorDto pos)
    {
        var objects = new HashSet<string>(){"wall", "box", "boxOnTarget"};
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