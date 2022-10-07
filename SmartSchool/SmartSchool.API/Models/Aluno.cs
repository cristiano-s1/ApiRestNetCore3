using System;
using System.Collections.Generic;

namespace SmartSchool.Models
{
    public class Aluno
    {
        // Construtor padrão
        public Aluno() { }

        // Costrutor com propriedades - campos obrigatórios
        public Aluno(int id, int matricula, string nome, string sobrenome, string telefone, DateTime dataNascimento)
        {
            this.Id = id;
            this.Matricula = matricula;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.DataNascimento = dataNascimento;
        }

        // Propriedades
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;

        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}
