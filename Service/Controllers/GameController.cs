using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pika.DataLayer.Model;
using Pika.DataLayer.Repositories;
using Pika.Service.Dto;

namespace Pika.Service.Controllers;

[ApiController]
[Route("game")]
public class GameController : ControllerBase
{
    private readonly IGameRepo _gameRepo;

    public GameController(IGameRepo gameRepo)
    {
        _gameRepo = gameRepo;
    }

    [HttpGet("{gameId}")]
    public async Task<GameDto> Get(string gameId)
    {
        var game = await _gameRepo.Get(gameId);
        if (game is null) throw new Exception("Game Not Found");
        return DtoMapper.ToDto(game);
    }


    [HttpGet("all")]
    public async Task<List<GameSummaryDto>> GetAll()
    {
        var allGames = await _gameRepo.GetAll();
        return allGames.ConvertAll(DtoMapper.ToSummaryDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<GameSummaryDto> Add(string name, string id, string? version)
    {
        if (IdUtilities.IsValidId(id)) throw new ArgumentException("Invalid Id Format", nameof(id));
        // TODO: resolve race condition here
        if (await _gameRepo.Get(id) != null) throw new Exception("Id already exist");
        var game = new GameDbModel
        {
            Id = id,
            Name = name,
            Version = version ?? "0.0.0",
        };
        await _gameRepo.Create(game);
        return DtoMapper.ToSummaryDto(game);
    }
}
