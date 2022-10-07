using System;
using System.IO;
using System.Reflection;
using SmartSchool.API.Context;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSchool.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        // Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {
            #region CONEXÃO COM BANCO DE DADOS
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );
            #endregion

            #region CONTROLLES
            // Adicionar as controllers ao serviço
            // Irá ignorar o loop infinito dos relacionamento dos modelos
            services.AddControllers()
                    .AddNewtonsoftJson(
                        opt => opt.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            #endregion

            #region AUTO MAPPER
            // Adicionar pacote AutoMapper ao serviço
            // Fazer o mapeamento entre os Dtos e os Dominios
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            #region INJEÇÃO DE DEPENDÊNCIA
            //Injetar o repository dentro das controllers com obstração do contexto por meio da interface.
            services.AddScoped<IBaseRepository, BaseRepository>();
            #endregion

            #region SWAGGER

            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc(
            //            "SmartSchoolAPI",
            //            new Microsoft.OpenApi.Models.OpenApiInfo()
            //            {
            //                Title = "SmartSchool API", //Titulo
            //                Version = "1.0"
            //            });
            //});

            //Adicionando Versionamento da API ao serviço(Configurações de versão de API)
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true; // Substituir o numero da versão na URL

            }).AddApiVersioning(options =>
            {
                // Versão padrão quando não definida
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });


            //Deixar as versões de API dinâmicas
           var apiProviderDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                // Deixar a rota da api versionavel:
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {

                    // Documentação Open Id
                    options.SwaggerDoc(
                        description.GroupName,

                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            // Configurações:
                            Title = "SmartSchool API",
                            Version = description.ApiVersion.ToString(),
                            TermsOfService = new Uri("http://SeusTermosDeUso.com"), // Colocar o termo de uso do seu site
                            Description = "A descrição da WebAPI do SmartSchool",
                            License = new Microsoft.OpenApi.Models.OpenApiLicense
                            {
                                Name = "SmartSchool License",
                                Url = new Uri("http://mit.com") // Url da sua api, tem que ser uma url válida se não dá erro.
                            },

                            Contact = new Microsoft.OpenApi.Models.OpenApiContact
                            {
                                // Contato da Empresa
                                Name = "Cristiano Campos de Souza",
                                Email = "cristiano.souzac@hotmail.com.br",
                                Url = new Uri("https://www.linkedin.com/in/cristiano-s1/")

                            }
                        });

                }

                // Configuração para aparecer os comentários xml das controllers no Swagger
                // Criar arquivo xml do Assembly
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                // Passando o endereço onde vai pegar o arquivo xml gerado (Vai ser criado no diretório base)
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                // Passando para options
                options.IncludeXmlComments(xmlCommentsFullPath);
            });

            #endregion
        }

        // Use este método para configurar o pipeline de solicitação HTTP.
        // Passar como paremetro 'apiProviderDescription' para configurar versionamento da api
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiProviderDescription)
        {
            //Se estiver no ambiente de desenvolvimento e aparecer um erro mostra uma Exception na pagina
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region SWAGGER

            //app.UseSwagger()

            //   // Configurando url para do Swagger (localhost:61091/index.html)
            //   .UseSwaggerUI(options =>
            //   {
            //       options.SwaggerEndpoint($"/swagger/SmartSchoolAPI/swagger.json", "SmartSchoolAPI");

            //       options.RoutePrefix = "";
            //   });

            app.UseSwagger()
               // Configurando url para do Swagger (localhost:61091/index.html)
               .UseSwaggerUI(options =>
               {
                   // Deixar rota da api versionavel
                   foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                   {
                       options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant());
                   }

                   options.RoutePrefix = "";
               });

            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
