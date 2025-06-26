using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using UsuariosApp.Domain.Models.Dtos;

namespace UsuariosApp.Tests;

public class AutenticarUsuariosTest
{
    [Fact]
    public void Autenticar_Usuario_Com_Sucesso()
    {

        #region Criando um usuário na API

        var requestCriarUsuario = new Faker<CriarUsuarioRequestDto>()
            .RuleFor(dto => dto.Nome, faker => faker.Person.FullName)
            .RuleFor(dto => dto.Email, faker => faker.Internet.Email())
            .RuleFor(dto => dto.Senha, "1234")
            .Generate();


        var jsonCriarUsuario = new StringContent(JsonConvert.SerializeObject(requestCriarUsuario),
            Encoding.UTF8, "application/json");
        
        var client = new WebApplicationFactory<Program>().CreateClient();

        var resultCriarUsuario = client.PostAsync("/api/usuarios/criar", jsonCriarUsuario)?.Result;

        resultCriarUsuario?.StatusCode.Should().Be(HttpStatusCode.Created);
        #endregion


        #region Autenticando o usuário
        var requestAutenticarUsuario = new AutenticarUsuarioRequestDto
        {
            Email = requestCriarUsuario.Email,
            Senha = requestCriarUsuario.Senha
        };

        var jsonAutenticarUsuario = new StringContent(JsonConvert.SerializeObject(requestAutenticarUsuario),
            Encoding.UTF8, "application/json");

        var resultAutenticarUsuario = client.PostAsync("/api/usuarios/autenticar", jsonAutenticarUsuario)?.Result;
        
        resultAutenticarUsuario?.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = resultAutenticarUsuario?.Content.ReadAsStringAsync()?.Result;

        response.Should().Contain("Usuário autenticado com sucesso");
        #endregion

    }

    [Fact]
    public void Acesso_Negado_De_Usuario()
    {
        var requestAutenticarUsuario = new AutenticarUsuarioRequestDto
        {
            Email = "teste@email.com",
            Senha = "Teste@2025"
        };

        var jsonAutenticarUsuario = new StringContent(JsonConvert.SerializeObject(requestAutenticarUsuario),
            Encoding.UTF8, "application/json");

        var client = new WebApplicationFactory<Program>().CreateClient();

        var resultAutenticarUsuario = client.PostAsync("/api/usuarios/autenticar",
            jsonAutenticarUsuario)?.Result;

        resultAutenticarUsuario?.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var response = resultAutenticarUsuario?.Content.ReadAsStringAsync()?.Result;

        response.Should().Contain("Acesso negado. Usuário não encontrado.");
    }
}

