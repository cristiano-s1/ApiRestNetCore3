using System;

namespace SmartSchool.V2.Dtos
{
    public class AlunoRegistrarDto
    {
        // Deixar somente as propriedades que deseja retornar o valor a API
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;

    }
}
