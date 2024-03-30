using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace thegame.Services;

public class TestData
{
    public static readonly string[] Map = { "OOOOOOOOOO", 
                                 "OOOXXXOOOO", 
                                 "OXXXBXXBXO", 
                                 "OXBXPBXXXO", 
                                 "OOOBBOOOOO", 
                                 "OOOXXTTOOO", 
                                 "OOOTTTTOOO", 
                                 "OOOOOOOOOO" };
    
    public static readonly string[] Map1 = { "OOOOOOOOOO", 
        "OOOXXXOOOO", 
        "OXXXBXXBXO", 
        "OXBXPBXXXO", 
        "OOOBBOOOOO", 
        "OOOXOTTOOO", 
        "OOOTTOTOOO", 
        "OOOOOOOOOO" };
    public static GameDto AGameDto(VectorDto movingObjectPosition, Guid gameId)
    {
        var width = Map[0].Length;
        var height = Map.Length;
        var boxes = 0;
        var serializer = new MapSerializer();

        var testCells = new List<CellDto>();
        
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                if (Map[y][x] != 'P')
                    testCells.Add(serializer.ParseToCell(Map[y][x], y + " " + x, new VectorDto {X = x, Y = y}));
                else testCells.Add(serializer.ParseToCell(Map[y][x], y + " " + x, movingObjectPosition));
            }

        boxes = testCells.Count(x => x.Type == "box");
        
        return new GameDto(testCells.ToArray(), true, false, width, height, gameId, false, 0);
    }
    
    public static GameDto AGameDto1(VectorDto movingObjectPosition, Guid gameId)
    {
        var width = Map1[0].Length;
        var height = Map1.Length;
        var boxes = 0;
        var serializer = new MapSerializer();

        var testCells = new List<CellDto>();
        
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            if (Map[y][x] != 'P')
                testCells.Add(serializer.ParseToCell(Map1[y][x], y + " " + x, new VectorDto {X = x, Y = y}));
            else testCells.Add(serializer.ParseToCell(Map1[y][x], y + " " + x, movingObjectPosition));
        }

        boxes = testCells.Count(x => x.Type == "box");
        
        return new GameDto(testCells.ToArray(), true, false, width, height, gameId, false, 0);
    }
}
