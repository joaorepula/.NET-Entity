using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers;

[Route("api/tarefa")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly AppDataContext _context;

    public TarefaController(AppDataContext context) =>
        _context = context;

    // GET: api/tarefa/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).ToList();
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("listarNaoConcluidas")]
    public IActionResult TarefasNaoConcluidas()
    {
        try
        {
        List<Tarefa> tarefas = _context.Tarefas.Where(t => t.StatusId == 1 || t.StatusId == 2).ToList();
        return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("listarConcluidas")]
    public IActionResult TarefasConcludias()
    {
        try
        {
        List<Tarefa> tarefas = _context.Tarefas.Where(t => t.StatusId == 3).ToList();
        return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch]
    [Route("atualizar/{id}")]
    public IActionResult Atualizar(int id)
    {
        try
        {
            Tarefa tarefaExistente = _context.Tarefas.Find(id) ?? throw new InvalidOperationException($"Tarefa com id {id} não encontrado");

                 
            if (tarefaExistente != null)
            {
                if(tarefaExistente.StatusId == 1){
                    tarefaExistente.StatusId = 2;
                     _context.SaveChanges();
                    return Ok(tarefaExistente);
                }else if(tarefaExistente.StatusId == 2){
                    tarefaExistente.StatusId = 2;
                    _context.SaveChanges();
                    return Ok(tarefaExistente);
                }else if(tarefaExistente.StatusId == 3){
                    tarefaExistente.StatusId = 1;
                    _context.SaveChanges();
                    return Ok(tarefaExistente);
                }
                
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    // POST: api/tarefa/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Tarefa tarefa)
    {
        try
        {
            Categoria? categoria = _context.Categorias.Find(tarefa.CategoriaId);
            if (categoria == null)
            {
                return NotFound();
            }
            tarefa.Categoria = categoria;
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
