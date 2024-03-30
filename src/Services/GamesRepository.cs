using System;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public class GamesRepository
{
    public static VectorDto start;

    public static Dictionary<Guid, GameDto> Games = new Dictionary<Guid, GameDto>();
}