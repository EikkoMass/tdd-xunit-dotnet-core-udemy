using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class CursoTest
{
    [Fact]
    public void DeveCriarCurso()
    {
        // Arrange
        var nome = "Informatica Basica";
        string publicoAlvo = "Estudantes";
        double valor = 950;
        double cargaHoraria = 80;
        
        // Act 
        var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);
        
        // Assert
        Assert.Equal(nome, curso.Nome);
        Assert.Equal(cargaHoraria, curso.CargaHoraria);
        Assert.Equal(publicoAlvo, curso.PublicoAlvo);
        Assert.Equal(valor, curso.Valor);
    }
}

public class Curso
{
    public string Nome { get; private set; }
    public double CargaHoraria { get; private set; }
    public string PublicoAlvo { get; private set; }
    public double Valor { get; private set; }

    public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
    {
        // throw new Exception();
        
        Nome = nome;
        CargaHoraria = cargaHoraria;
        PublicoAlvo = publicoAlvo;
        Valor = valor;
    }
}