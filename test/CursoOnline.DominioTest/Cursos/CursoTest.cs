using ExpectedObjects;
using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
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
            PublicoAlvo = "Estudantes",
            Valor = (double) 950,
            CargaHoraria = (double) 80
        };
        
        // Act 
        var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
        
        // Assert
        cursoEsperado.ToExpectedObject().ShouldMatch(curso);
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