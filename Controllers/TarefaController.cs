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

[HttpGet]
[Route("listar")]
public IActionResult Listar()
{
    try
    {
        var tarefasComCategoria = _context.Tarefas
            .Join(_context.Categorias,
                  tarefa => tarefa.CategoriaId,
                  categoria => categoria.CategoriaId,
                  (tarefa, categoria) => new
                  {
                      TarefaId = tarefa.TarefaId,
                      Titulo = tarefa.Titulo,
                      Descricao = tarefa.Descricao,
                      CriadoEm = tarefa.CriadoEm,
                      Categoria = categoria, 
                      CategoriaId = tarefa.CategoriaId,
                      Status = tarefa.Status,
                      StatusId = tarefa.StatusId
                  })
            .ToList();

        return Ok(tarefasComCategoria);
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
    public IActionResult Atualizar(int id,  [FromBody] Tarefa tarefa)
    {
        try
        {
        Tarefa? tarefaExistente = _context.Tarefas.FirstOrDefault(x => x.TarefaId == id);

            if (tarefaExistente != null && tarefaExistente.StatusId == 3)
            {
                tarefaExistente.Titulo = tarefa.Titulo;
                tarefaExistente.Descricao = tarefa.Descricao;
                tarefaExistente.CategoriaId = tarefa.CategoriaId;
                tarefaExistente.StatusId = 1;

                _context.Tarefas.Update(tarefaExistente);
                _context.SaveChanges();
                Console.WriteLine("Id da tarefa", tarefaExistente.StatusId);

                return Ok();
            }
            if (tarefaExistente != null && tarefaExistente.StatusId == 1)
            {
                tarefaExistente.Titulo = tarefa.Titulo;
                tarefaExistente.Descricao = tarefa.Descricao;
                tarefaExistente.CategoriaId = tarefa.CategoriaId;
                tarefaExistente.StatusId = 2;

                _context.Tarefas.Update(tarefaExistente);
                _context.SaveChanges();
                Console.WriteLine("Id da tarefa", tarefaExistente.StatusId);

                return Ok();
            }
                        if (tarefaExistente != null && tarefaExistente.StatusId == 2)
            {
                tarefaExistente.Titulo = tarefa.Titulo;
                tarefaExistente.Descricao = tarefa.Descricao;
                tarefaExistente.CategoriaId = tarefa.CategoriaId;
                tarefaExistente.StatusId = 3;

                _context.Tarefas.Update(tarefaExistente);
                _context.SaveChanges();
                Console.WriteLine("Id da tarefa", tarefaExistente.StatusId);

                return Ok();
            }

                
                _context.SaveChanges();
                return Ok(tarefaExistente);

                
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
