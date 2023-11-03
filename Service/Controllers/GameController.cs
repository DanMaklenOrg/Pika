using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pika.DataLayer.Model;
using Pika.DataLayer.Repositories;
using Pika.Service.Dto;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly IGameRepo _gameDao;

    public GameController(IGameRepo gameDao)
    {
        _gameDao = gameDao;
    }

    [HttpGet("{gameId}")]
    public async Task<GameDto> Get(Guid gameId)
    {
        var game = await _gameDao.Get(gameId);
        return DtoMapper.ToDto(game);
    ;}


    [HttpGet("all")]
    public async Task<List<GameDto>> GetAll()
    {
        var allGames = await _gameDao.GetAll();
        return allGames.ConvertAll(DtoMapper.ToDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<GameDto> Add(string name)
    {
        var game = new GameDbModel { Name = name };
        await _gameDao.Create(game);
        return DtoMapper.ToDto(game);
    }
}
