using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Enums;
using Xunit;

namespace CursoOnline.DominioTest.PublicosAlvo;

public class ConversorDePublicoAlvoTest
{
    private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();
    
    [Theory]
    [InlineData(PublicoAlvo.Empregado, "Empregado")]
    [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
    [InlineData(PublicoAlvo.Estudante, "Estudante")]
    [InlineData(PublicoAlvo.Universitario, "Universitario")]
    public void DeveConverterPublicoAlvo(PublicoAlvo publicoAlvo, string publicoAlvoEmString)
    {
        var publicoAlvoConvertido = _conversor.Converter(publicoAlvoEmString);
        
        Assert.Equal(publicoAlvo, publicoAlvoConvertido);
    }

    [Fact]
    public void NaoDeveConverterQuandoPublicoAlvoInvalido()
    {
        string publicoAlvoInvalido = "Invalido";
        Assert.Throws<ExcecaoDeDominio>(() => _conversor.Converter(publicoAlvoInvalido));
    }
}