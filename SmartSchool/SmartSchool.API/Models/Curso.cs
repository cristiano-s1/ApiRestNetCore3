using System.Collections.Generic;

namespace SmartSchool.Models
{
    public class Curso
    {
        // Construtor padrão
        public Curso(){ }

        // Costrutor com propriedades - campos obrigatórios
        public Curso(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        // Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }

        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
