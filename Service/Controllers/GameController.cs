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
    private readonly IGameProgressRepo _gameProgressRepo;

    public GameController(IGameRepo gameRepo, IGameProgressRepo gameProgressRepo)
    {
        _gameRepo = gameRepo;
        _gameProgressRepo = gameProgressRepo;
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
    [HttpGet("{gameId}/progress")]
    public async Task<GameProgressDto> GetProgress(string gameId)
    {
        var userId = this.User.Identity!.Name!;
        var progress = await _gameProgressRepo.Get(userId, gameId);
        return DtoMapper.ToDto(progress ?? new GameProgress(userId, gameId));
    }

    [Authorize]
    [HttpPost("{gameId}/progress")]
    public async Task<ActionResult> SetProgress(string gameId, GameProgressDto dto)
    {
        var userId = this.User.Identity!.Name!;
        if (userId != dto.UserId || gameId != dto.Game) return ValidationProblem();
        var progress = DtoMapper.FromDto(dto);
        await _gameProgressRepo.Create(progress);
        return Ok();
    }
}
