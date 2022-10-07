using AutoMapper;
using SmartSchool.Helpers;
using SmartSchool.Models;
using SmartSchool.V1.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SmartSchool.API.Repository;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Versão 1 do meu controller de Aluno.
    /// </summary>
    /// 
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        // Construtor para receber o contexto       
        private readonly IBaseRepository _repository;
        public readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AlunoController(IBaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Método responsavel por retornar apenas um único AlunoDto
        /// </summary>
        /// <returns></returns>
        // api/V1/Aluno/GetRegister
        [HttpGet("GetRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }

        /// <summary>
        /// Método responsável para retornar todos os meus alunos
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por retornar apenas um Aluno por meio do código ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/V1/Aluno/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            // Dto
            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        /// <summary>
        /// Método responsável por criar um Aluno 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por atualizar um Aluno 
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por atualizar um Aluno 
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável por deletar um Aluno 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
