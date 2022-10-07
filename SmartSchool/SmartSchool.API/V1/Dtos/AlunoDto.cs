using System;

namespace SmartSchool.V1.Dtos
{
    public class AlunoDto
    {
        // Deixar somente as propriedades que deseja retornar valor a API
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataInicial { get; set; }
        public bool Ativo { get; set; }
    }
}
