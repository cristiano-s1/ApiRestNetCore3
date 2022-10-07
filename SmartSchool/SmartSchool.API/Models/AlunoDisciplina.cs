using System;

namespace SmartSchool.Models
{
    public class AlunoDisciplina
    {
        // Construtor padrão
        public AlunoDisciplina() { }

        // Costrutor com propriedades - campos obrigatórios
        public AlunoDisciplina(int alunoId, int disciplinaId)
        {
            this.AlunoId = alunoId;
            this.DisciplinaId = disciplinaId;
        }

        // Propriedades
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
        public int? Nota { get; set; } = null;

        public int AlunoId { get; set; } 
        public Aluno Aluno { get; set; }

        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
