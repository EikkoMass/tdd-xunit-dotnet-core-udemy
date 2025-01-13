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
    
    public CursoTest(ITestOutputHelper output)
    {
        _output = output;
        _output.WriteLine("Construtor sendo executado");
        var faker = new Faker();

        _nome = faker.Random.Word();
        _cargaHoraria = faker.Random.Double(50, 1000);
        _valor = faker.Random.Double(100, 1000);
        _publicoAlvo = PublicoAlvo.Estudante;
        _descricao = faker.Lorem.Paragraph();
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
}