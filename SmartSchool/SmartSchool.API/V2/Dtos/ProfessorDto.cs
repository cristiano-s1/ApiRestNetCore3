﻿using System;

namespace SmartSchool.V2.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInicial { get; set; }
        public bool Ativo { get; set; }

    }
}
