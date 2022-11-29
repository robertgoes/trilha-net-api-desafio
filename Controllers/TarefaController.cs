using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

         [HttpPost]
         public IActionResult Criar(Tarefa tarefa)
         {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return Ok(tarefa);
         }

         [HttpGet("{id}")]
         public IActionResult ObterPorId(int id)
         {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();
            
            return Ok(tarefaBanco); 
         }

         [HttpGet("ObterTodos")]
         public IActionResult ObterTodos()
         {
            var tarefas = _context.Tarefas.ToList();

            return Ok(tarefas);
         }

         [HttpGet("ObterPorTitulo")]
         public IActionResult ObterPorTitulo(string titulo)
         {
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));

            if(tarefa == null)
                return NotFound();
            
            return Ok(tarefa);
         }

         [HttpGet("ObterPorData")]
         public IActionResult ObterPorData(DateTime data)
         {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);

            if(tarefa == null)
                return NotFound();
            
            return Ok(tarefa);
         }

         [HttpGet("ObterPorStatus")]
         public IActionResult ObterPorStatus(EnumStatusTarefa status)
         {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);

            if(tarefa == null)
                return NotFound();
            
            return Ok(tarefa);
         }

         [HttpDelete("{id}")]
         public IActionResult Deletar(int id)
         {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();
            
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
         }

         [HttpPut("{id}")]
         public IActionResult Atualizar(int id, Tarefa tarefa)
         {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();
            
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefaBanco);
         }

    }
}