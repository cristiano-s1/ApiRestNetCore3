using System;

namespace SmartSchool.Models
{
    public class AlunoCurso
    {
        // Construtor padrão
        public AlunoCurso() { }

        // Costrutor com propriedades - campos obrigatórios
        public AlunoCurso(int alunoId, int cursoId)
        {
            this.AlunoId = alunoId;
            this.CursoId = cursoId;
        }

        // Propriedades
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
        public int? Nota { get; set; } = null;

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
