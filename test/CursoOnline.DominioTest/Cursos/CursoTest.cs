using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos;

public class CursoTest : IDisposable
{

    private readonly ITestOutputHelper _output;
    
    private readonly string _nome;
    private readonly string _descricao;
    private readonly double _cargaHoraria;
    private readonly double _valor;
    private readonly PublicoAlvo _publicoAlvo;
    private readonly Faker _faker;
    
    public CursoTest(ITestOutputHelper output)
    {
        _output = output;
        _output.WriteLine("Construtor sendo executado");
        _faker = new Faker();

        _nome = _faker.Random.Word();
        _cargaHoraria = _faker.Random.Double(50, 1000);
        _valor = _faker.Random.Double(100, 1000);
        _publicoAlvo = PublicoAlvo.Estudante;
        _descricao = _faker.Lorem.Paragraph();
    }
    
    public void Dispose()
    {
        _output.WriteLine("Dispose sendo executado");
    }
    
    [Fact]
    public void DeveCriarCurso()
    {
        // Arrange
        var cursoEsperado = new
        {
            Nome = _nome,
            Descricao = _descricao,
            PublicoAlvo = _publicoAlvo,
            Valor = _valor,
            CargaHoraria = _cargaHoraria,
        };
        
        // Act 
        var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
        
        // Assert
        cursoEsperado.ToExpectedObject().ShouldMatch(curso);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)] // cada inlineData corresponde a 1 execucao com a variacao sendo o valor adicionado no mesmo
    public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
    {
        
        Assert.Throws<ExcecaoDeDominio>(() => 
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
            .ComMensagem("Nome Invalido");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void NaoDeveCursoTerUmaCargaHorariaMenorQueUm(double cargaHorariaInvalida)
    {
        Assert.Throws<ExcecaoDeDominio>(() => 
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
            .ComMensagem("Carga Horaria Invalida"); 
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void NaoDeveCursoTerUmValorMenorQueUm(double valorInvalido)
    {
        Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
            .ComMensagem("Valor Invalido");
    }

    [Fact]
    public void DeveAlterarNome()
    {
        var nomeEsperado = _faker.Person.FirstName;
        var curso = CursoBuilder.Novo().Build();
        
        curso.AlterarNome(nomeEsperado);
        
        Assert.Equal(nomeEsperado, curso.Nome);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
    {
        var curso = CursoBuilder.Novo().Build();
        
        Assert.Throws<ExcecaoDeDominio>(() => 
                curso.AlterarNome(nomeInvalido))
            .ComMensagem("Nome Invalido");
    }
    
    [Fact]
    public void DeveAlterarCargaHoraria()
    {
        var cargaHorariaEsperada = 20.5;
        var curso = CursoBuilder.Novo().Build();
        
        curso.AlterarCargaHoraria(cargaHorariaEsperada);
        
        Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHorariaInvalida)
    {
        var curso = CursoBuilder.Novo().Build();
        
        Assert.Throws<ExcecaoDeDominio>(() =>
                curso.AlterarCargaHoraria(cargaHorariaInvalida))
            .ComMensagem("Carga Horaria Invalida");
    }
    
    [Fact]
    public void DeveAlterarValor()
    {
        var valor = 234.99;
        var curso = CursoBuilder.Novo().Build();
        
        curso.AlterarValor(valor);
        
        Assert.Equal(valor, curso.Valor);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void NaoDeveAlterarComValorInvalido(double valorInvalido)
    {
        var curso = CursoBuilder.Novo().Build();
        
        Assert.Throws<ExcecaoDeDominio>(() =>
                curso.AlterarValor(valorInvalido))
            .ComMensagem("Valor Invalido");
    }
}