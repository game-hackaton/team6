using thegame.Models;

namespace thegame.Services;

public class MapSerializer
{
    public CellDto ParseToCell(char character, string id, VectorDto vectorDto)
    {
        return character switch
        {
            'P' => new CellDto(id, vectorDto, "player", "", 10),
            'O' => new CellDto(id, vectorDto, "wall", "", 0),
            'B' => new CellDto(id, vectorDto, "box", "", 10),
            'T' => new CellDto(id, vectorDto, "target", "", 0),
            _ => new CellDto(id, vectorDto, "sokoban", "", 0)
        };
    }
}