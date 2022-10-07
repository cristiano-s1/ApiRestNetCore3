using System;

namespace SmartSchool.V1.Dtos
{
    /// <summary>
    /// Este é o Dto de Aluno para Registrar.
    /// </summary>
    public class ProfessorRegistrarDto
    {
        /// <summary>
        /// Identificar a chave do banco de dados.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Chave do Aluno, para outros negócios na Instituição.
        /// </summary>
        public int Registro { get; set; }

        /// <summary>
        /// Nome é o Primeiro nome e o Sobrenome do Aluno.
        /// </summary>
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInicial { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;

    }
}
