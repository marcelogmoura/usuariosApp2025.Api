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

namespace UsuariosApp.Tests
{
    public class UsuariosTests
    {
        [Fact]
        public void Criar_Usuario_Com_Sucesso()
        {

            var request = new Faker<CriarUsuarioRequestDto>()
                .RuleFor(dto => dto.Nome, faker => faker.Person.FullName)
                .RuleFor(dto => dto.Email, faker => faker.Internet.Email())
                .RuleFor(dto => dto.Senha, "1234")
                .Generate();

            var jsonRequest = new StringContent
                (JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");
            
            var client = new WebApplicationFactory<Program>()
                .CreateClient();              

            var response = client.PostAsync("/api/usuarios/criar", jsonRequest).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var result = response.Content.ReadAsStringAsync().Result;
            
            result.Should().Contain("Usuário cadastrado com sucesso");
        }
    }
}
