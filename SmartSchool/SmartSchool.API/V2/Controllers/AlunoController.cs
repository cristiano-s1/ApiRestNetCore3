using AutoMapper;
using SmartSchool.Helpers;
using SmartSchool.Models;
using SmartSchool.V1.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SmartSchool.API.Repository;

namespace SmartSchool.API.V2.Controllers
{
    /// <summary>
    /// Versão 2 do meu controller de Aluno.
    /// </summary>
    /// 
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        // Construtor para receber o contexto       
        private readonly IBaseRepository _repository;
        public readonly IMapper _mapper;

        public AlunoController(IBaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/V1/Aluno
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams) // Passando como parametro a classe de paginação
        {
            var alunos = await _repository.GetAllAlunosAsync(pageParams, true);

            // Trazendo as propriedades do mapeamento Dto
            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            // Puxando o método do Extensions
            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        // POST: api/V1/Aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            // Recebendo como parametro a model AlunoDto e mapeando para model Aluno
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);

            if (_repository.SaveChanges())
            {
                // Created é um método que vai retornar o código 201
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno)); // mapeando Aluno para model AlunoDto
            }

            return BadRequest("Aluno não cadastrado");
        }

        // PUT: api/V1/Aluno/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            // Mapear os dois model
            _mapper.Map(model, aluno);

            _repository.Update(aluno);

            if (_repository.SaveChanges())
            {
                // Created é um método que vai retornar o código 201
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno)); // mapeando Aluno para model AlunoDto
            }

            return BadRequest("Aluno não atualizado");
        }

        // PATCH: api/V1/Aluno/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            // Mapear os dois model
            _mapper.Map(model, aluno);

            _repository.Update(aluno);

            if (_repository.SaveChanges())
            {
                // Created é um método que vai retornar o código 201
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno)); // mapeando Aluno para model AlunoDto
            }

            return BadRequest("Aluno não atualizado");
        }

        // DELETE: api/V1/Aluno/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repository.Delete(aluno);

            if (_repository.SaveChanges())
            {
                return Ok("Aluno deletado");
            }

            return BadRequest("Aluno não foi deletado");
        }
    }
}
