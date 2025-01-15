using ExpectedObjects;
using Poker.Game;
using Xunit;

namespace Poker.GameTest;

public class CartaTeste
{
    [Fact]
    public void DeveCriarUmaCarta()
    {
        const string valorEsperado = "A";
        const string naipeEsperado = "O";
        
        var cartaEsperada = new
        {
            Valor = valorEsperado,
            Naipe = naipeEsperado,
        };

        var carta = new Carta(cartaEsperada.Valor + cartaEsperada.Naipe);
        
        cartaEsperada.ToExpectedObject().ShouldMatch(carta);
    }
    
    [Theory]
    [InlineData("V", 11)]
    [InlineData("D", 12)]
    [InlineData("R", 13)]
    [InlineData("A", 14)]
    public void DeveCriarUmaCartaComPeso(string valorEsperado, int pesoEsperado)
    {
        var carta = new Carta(valorEsperado + "E");
        Assert.Equal(pesoEsperado, carta.Peso);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("-1")]
    [InlineData("15")]
    public void DeveValidarValorCarta(string valorDaCartaInvalido)
    {
        var mensagemDeErro = Assert.Throws<Exception>(() => new Carta(valorDaCartaInvalido + "O")).Message;
        Assert.Equal(mensagemDeErro, "Valor da carta invalida");
    }
    
    [Theory]
    [InlineData("A")]
    [InlineData("Z")]
    public void DeveValidarNaipeCarta(string naipeDaCartaInvalido)
    {
        var mensagemDeErro = Assert.Throws<Exception>(() => new Carta("2" + naipeDaCartaInvalido)).Message;
        Assert.Equal(mensagemDeErro, "Valor da naipe invalido");
    }
}