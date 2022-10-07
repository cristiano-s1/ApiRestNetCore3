using AutoMapper;
using SmartSchool.Models;
using SmartSchool.V1.Dtos;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Repository;
using System.Collections.Generic;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Versão 1 do meu controller de Professor.
    /// </summary>
    /// 
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : Controller
    {
        // Construtor para receber o contexto
        public readonly IBaseRepository _repository;
        public readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IBaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;     
        }

        /// <summary>
        /// Método responsavel por retornar apenas um único ProfessorDto
        /// </summary>
        /// <returns></returns>
        // api/V1/Professor/GetRegister
        [HttpGet("GetRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        /// <summary>
        /// Método responsável para retornar todos os Professor
        /// </summary>
        /// <returns></returns>
        //GET: api/V1/Professor
        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repository.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        /// <summary>
        /// Método responsável por retornar apenas um Professor por meio do código ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/V1/Professor/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O Professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        /// <summary>
        /// Método responsável por criar um Professor 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/V1/Professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repository.Add(professor);

            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("O Professor não encontrado");
        }

        /// <summary>
        /// Método responsável por atualizar um Professor 
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="model"></param>
        /// <returns></returns>
        // PUT: api/V1/Professor/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {

            var professor = _repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);

            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado");
        }

        /// <summary>
        /// Método responsável por atualizar um Professor 
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="model"></param>
        /// <returns></returns>
        // PATCH: api/V1/Professor/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);

            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado");
        }

        /// <summary>
        /// Método responsável por deletar um Professor 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/V1/Professor/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado");

            _repository.Delete(professor);

            if (_repository.SaveChanges())
            {
                return Ok("Professor deletado");
            }

            return BadRequest("Professor não foi deletado");
        }
    }
}
