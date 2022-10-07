using SmartSchool.Helpers;
using SmartSchool.Models;
using System.Threading.Tasks;

namespace SmartSchool.API.Repository
{
    public interface IBaseRepository
    {
        // Todos os métodos vai ter como parâmetro um tipo classe
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();


        //Chamada dos métodos IRepository

        // Forma Asincrona (Herda de PageList)
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);

        // Forma Sincrona
        Aluno[] GetAllAlunos(bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

        // Lista de professor
        Professor[] GetAllProfessores(bool includeAlunos = false);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
        Professor GetProfessorById(int professorId, bool includeProfessor = false);
    }
}
