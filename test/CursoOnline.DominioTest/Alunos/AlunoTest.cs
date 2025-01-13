using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Enums;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Alunos;

public class AlunoTest
{
    private readonly string _nome;
    private readonly string _cpf;
    private readonly string _email;
    private readonly PublicoAlvo _publicoAlvo;
    private readonly Faker _faker;
    
    public AlunoTest()
    {
        _faker = new Faker();

        _nome = _faker.Person.FirstName;
        _cpf = _faker.Person.Cpf();
        _email = _faker.Person.Email;
        _publicoAlvo = PublicoAlvo.Estudante;
    }
    
    [Fact]
    public void DeveCriarAluno()
    {
        // Arrange
        var alunoEsperado = new
        {
            Nome = _nome,
            Cpf = _cpf,
            PublicoAlvo = _publicoAlvo,
            Email = _email,
        };
        
        // Act 
        var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Email, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo);
        
        // Assert
        alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveAlunoTerUmNomeInvalido(string nomeInvalido)
    {
        
        Assert.Throws<ExcecaoDeDominio>(() => 
                AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
            .ComMensagem(Resource.NomeInvalido);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Cpf Invalido")]
    [InlineData("00000000000")]
    public void NaoDeveAlunoTerUmCpfInvalido(string cpfInvalido)
    {
        
        Assert.Throws<ExcecaoDeDominio>(() => 
                AlunoBuilder.Novo().ComCpf(cpfInvalido).Build())
            .ComMensagem(Resource.CpfInvalido);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Email Invalido")]
    [InlineData("email@invalido")]
    public void NaoDeveAlunoTerUmEmailInvalido(string emailInvalido)
    {
        
        Assert.Throws<ExcecaoDeDominio>(() => 
                AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
            .ComMensagem(Resource.EmailInvalido);
    }

    [Fact]
    public void DeveAlterarNome()
    {
        var nomeEsperado = _faker.Person.FirstName;
        var aluno = AlunoBuilder.Novo().Build();
        
        aluno.AlterarNome(nomeEsperado);
        
        Assert.Equal(nomeEsperado, aluno.Nome);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
    {
        var aluno = AlunoBuilder.Novo().Build();
        
        Assert.Throws<ExcecaoDeDominio>(() => 
                aluno.AlterarNome(nomeInvalido))
            .ComMensagem(Resource.NomeInvalido);
    }
    
}