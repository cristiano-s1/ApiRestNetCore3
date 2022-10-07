using AutoMapper;
using SmartSchool.V1.Dtos;
using SmartSchool.Models;
using SmartSchool.Helpers;

namespace SmartSchool.V1.Profiles
{

    // Classe que vai fazer o mapeamento entre os Dtos e os Dominios.
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            //Criar mapeamento
            CreateMap<Aluno, AlunoDto>()

                // Alimentar um objeto especifico
                .ForMember(
                    destino => destino.Nome,
                    pegar => pegar.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )

                // // Alimentar um objeto especifico e implementar um método da Helpers
                .ForMember(
                    destino => destino.Idade,
                    pegar => pegar.MapFrom(src => src.DataNascimento.GetCurrentAge())
                );

            // Criar mapeamento inverso (Obrigatório) 
            CreateMap<AlunoDto, Aluno>();



            /*--------------------------------------------------------------------------------*/
            //Criar mapeamento
            CreateMap<Professor, ProfessorDto>()
                // Alimentar um objeto especifico
                .ForMember(

                    destino => destino.Nome,
                    pegar => pegar.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                );

            // Criar mapeamento inverso (Obrigatório) 
            CreateMap<ProfessorDto, Professor>();



            /*--------------------------------------------------------------------------------*/
            // Nesse caso podemos criar o mapeamento inverso direto porque tem as mesmas propriedades na Model e Dto
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();



            /*--------------------------------------------------------------------------------*/
            // Nesse caso podemos criar o mapeamento inverso direto porque tem as mesmas propriedades na Model e Dto
            CreateMap<Professor, ProfessorDto>().ReverseMap();
        }
    }
}
