using System;
using System.Collections.Generic;

namespace SmartSchool.Models
{
    public class Professor
    {
        // Construtor padrão
        public Professor() { }

        // Costrutor com propriedades - campos obrigatórios
        public Professor(int id, int registro, string nome, string sobrenome)
        {
            this.Id = id;
            this.Registro = registro;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
        }

        // Propriedades
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;

        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
