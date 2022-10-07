using System.Collections.Generic;

namespace SmartSchool.Models
{
    public class Disciplina
    {
        // Construtor padrão
        public Disciplina() { }

        // Costrutor com propriedades - campos obrigatórios
        public Disciplina(int id, string nome, int professorId, int cursoId)
        {
            this.Id = id;
            this.Nome = nome;
            this.ProfessorId = professorId;
            this.CursoId = cursoId;
        }

        // Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }

        public int? PreRequisitoId { get; set; } = null;
        public Disciplina PreRequisito { get; set; }

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}
