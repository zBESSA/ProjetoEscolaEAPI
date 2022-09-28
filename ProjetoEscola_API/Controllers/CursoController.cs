using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEscola_API.Data;
using ProjetoEscola_API.Models;

namespace ProjetoEscola_API.Controllers{
    [Route("api/[controller]")]
    [ApiController]

    public class CursoController : ControllerBase{
        private EscolaContext _context;

        public CursoController(EscolaContext context){
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Curso>> GetAll(){
            return _context.Curso.ToList();
        }

        [HttpGet("{CursoId}")]
        public ActionResult<List<Curso>> Get(int CursoID){
            try{
                var result = _context.Curso.Find(CursoID);
                if(result == null){
                    return NotFound();
                }
                return Ok(result);
            }
            catch{
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Falha no acesso ao banco de dados.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> post(Curso model){
            try{
                _context.Curso.Add(model);
                if (await _context.SaveChangesAsync() == 1){
                    return Ok();
                }
            }
            catch{
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                "Falha no acesso ao banco de dados.");
            }
            return BadRequest();
        }
    }
}
