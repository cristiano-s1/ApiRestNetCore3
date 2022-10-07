using System.Linq;
using SmartSchool.Models;
using SmartSchool.Helpers;
using System.Threading.Tasks;
using SmartSchool.API.Context;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.API.Repository
{
    public class BaseRepository : IBaseRepository
    {
        // Construtor para receber o contexto
        private readonly SmartContext _context;

        public BaseRepository(SmartContext context)
        {
            _context = context;
        }

        #region CRUD GENÉRICO
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
        #endregion

        #region ALUNOS
        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            // select * from Aluno
            IQueryable<Aluno> query = _context.Alunos;

            // Se for incluir professor
            if (includeProfessor)
            {
                // join
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            // order by id desc
            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            // select * from Aluno
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                // join
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            // where order by id desc
            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            // select * from Aluno
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                // join
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            // where order by id desc
            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.AlunosDisciplinas
                         .Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        // Método para paginação
        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            // select * from Aluno
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                // join
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            // order by id desc
            query = query.AsNoTracking().OrderBy(a => a.Id);

            // retorna a paginação
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
        #endregion

        #region PROFESSORES
        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            // select * from Professor
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                // join
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            // order by id desc
            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeProfessor = false)
        {
            // select * from Professor
            IQueryable<Professor> query = _context.Professores;

            if (includeProfessor)
            {
                // join
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            // where order by id desc
            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(professor => professor.Id == professorId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            // select * from Professor
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                // join
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            // where order by id desc
            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                         .Where(aluno => aluno.Disciplinas.Any(
                            d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)
                         ));

            return query.ToArray();
        }
        #endregion
    }
}
