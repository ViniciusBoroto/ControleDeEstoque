using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repo;
    public CategoriasController(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> GetAllAsync()
    {
        return Ok(await _repo.GetAllAsync());
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaViewModel>> GetByIdAsync(int id)
    {
        var categoria = await _repo.GetByIdAsync(id);
        if (categoria is null)
            return NotFound();

        return categoria;
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> CreateAsync(CategoriaInputModel categoria)
    {
        var resp = await _repo.CreateAsync(categoria);
        if (resp is null) return BadRequest();
        return Ok(resp);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaViewModel>> UpdateAsync(int id, CategoriaInputModel categoria)
    {
        var response = await _repo.UpdateAsync(id, categoria);
        if (response is null) return NotFound();
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaViewModel>> DeleteAsync(int id)
    {
        return Ok(await _repo.DeleteByIdAsync(id));
    }

    [HttpGet("{id:int}/produtos")]
    public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> GetByCategoriaAsync(int id)
    {
        return Ok(await _repo.GetByCategoriaAsync(id));
    }
}
