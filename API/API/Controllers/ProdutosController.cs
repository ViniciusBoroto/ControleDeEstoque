using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/produtos")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repo;
    public ProdutosController(IProdutoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
    {
        return Ok(await _repo.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> GetById(int id)
    {
        var produto = await _repo.GetByIdAsync(id);
        if (produto is null) return NotFound();
        return Ok(produto);
    }

    [HttpPost("")]
    public async Task<ActionResult<ProdutoViewModel>> Create(ProdutoInputModel produto)
    {
        return Ok(_repo.CreateAsync(produto));
    }
}
