using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class CursoTest
{
    [Fact]
    public void DeveCriarCurso()
    {
        // Arrange
        var cursoEsperado = new
        {
            Nome = "Informatica Basica",
            PublicoAlvo = PublicoAlvo.Estudante,
            Valor = (double) 950,
            CargaHoraria = (double) 80
        };
        
        // Act 
        var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
        
        // Assert
        cursoEsperado.ToExpectedObject().ShouldMatch(curso);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)] // cada inlineData corresponde a 1 execucao com a variacao sendo o valor adicionado no mesmo
    public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
    {
        var cursoEsperado = new
        {
            Nome = "John Doe",
            PublicoAlvo = PublicoAlvo.Estudante,
            Valor = (double) 950,
            CargaHoraria = (double) 80
        };
        
        Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
            .ComMensagem("Nome Invalido");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void NaoDeveCursoTerUmaCargaHorariaMenorQueUm(double cargaHorariaInvalida)
    {
        var cursoEsperado = new
        {
            Nome = "John Doe",
            PublicoAlvo = PublicoAlvo.Estudante,
            Valor = (double) 950,
            CargaHoraria = (double) 999
        };
        
        Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
            .ComMensagem("Carga Horaria Invalida"); 
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void NaoDeveCursoTerUmValorMenorQueUm(double valorInvalido)
    {
        var cursoEsperado = new
        {
            Nome = "John Doe",
            PublicoAlvo = PublicoAlvo.Estudante,
            Valor = (double) 950,
            CargaHoraria = (double) 999
        };

        Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, valorInvalido))
            .ComMensagem("Valor Invalido");
    }
}

public enum PublicoAlvo {
    Estudante,
    Universitario,
    Empregado,
    Empreendedor
}

public class Curso
{
    public string Nome { get; private set; }
    public double CargaHoraria { get; private set; }
    public PublicoAlvo PublicoAlvo { get; private set; }
    public double Valor { get; private set; }

    public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Nome Invalido");
        }
        
        if (cargaHoraria < 1)
        {
            throw new ArgumentException("Carga Horaria Invalida");
        }
        
        if (valor < 1)
        {
            throw new ArgumentException("Valor Invalido");
        }
        
        Nome = nome;
        CargaHoraria = cargaHoraria;
        PublicoAlvo = publicoAlvo;
        Valor = valor;
    }
}