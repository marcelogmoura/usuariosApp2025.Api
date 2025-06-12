using Azure.Core;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Dtos;


namespace UsuarioApp.Tests
{
    public class UsuariosTest
    {
        [Fact]
        public void Criar_Usuario_Com_Sucesso()
        {
            var request = new Faker<CriarUsuarioRequestDto>()
                .RuleFor(dto => dto.Nome, faker => faker.Person.FullName)
                .RuleFor(dto => dto.Email, faker => faker.Internet.Email())
                .RuleFor(dto => dto.Senha, "Teste2025")
                .Generate();

            var jsonRequest = new StringContent
            (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            //criando a requisição / solicitação para a API

            var client = new WebApplicationFactory<Program>()
            .CreateClient();
            //executando o cadastro do usuário e capturando a resposta

            var response = client.PostAsync("/api/usuarios/criar", jsonRequest).Result;
            //verificando se a API retornou resposta HTTP 201 (CREATED)

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
        