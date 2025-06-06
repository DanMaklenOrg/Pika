using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pika.Model;
using Pika.Repository;
using Pika.Service.Dto;

namespace Pika.Service.Controllers;

[ApiController]
[Route("game")]
public class GameController : ControllerBase
{
    private readonly IGameRepo _gameRepo;
    private readonly IUserStatsRepo _userStatsRepo;

    public GameController(IGameRepo gameRepo, IUserStatsRepo userStatsRepo)
    {
        _gameRepo = gameRepo;
        _userStatsRepo = userStatsRepo;
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

    [Authorize]
    [HttpGet("{gameId}/stats")]
    public async Task<UserStatsDto> GetStats(string gameId)
    {
        var userId = this.User.Identity!.Name!;
        var stats = await _userStatsRepo.Get(userId, gameId);
        return DtoMapper.ToDto(stats ?? new UserStats(userId, gameId));
    }

    [Authorize]
    [HttpPost("{gameId}/stats")]
    public async Task SetStats(string gameId, UserStatsDto statsDto)
    {
        var userId = this.User.Identity!.Name!;
        var stats = DtoMapper.FromDto(statsDto, userId, gameId);
        await _userStatsRepo.Create(stats);
    }
}
