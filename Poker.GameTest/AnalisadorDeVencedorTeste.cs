using Poker.Game;
using Xunit;

namespace Poker.GameTest;

public class AnalisadorDeVencedorTeste
{
    private readonly AnalisadorDeVencedor _analisador;
    
    public AnalisadorDeVencedorTeste()
    {
        _analisador = new AnalisadorDeVencedor();
    }
    
    [Theory]
    [InlineData("2O,4C,3P,6C,7C", "3O,5C,2E,9C,7P", "Segundo Jogador")]
    [InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,7C", "Primeiro Jogador")]
    [InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,10E", "Segundo Jogador")]
    [InlineData("3O,5C,2E,9C,VP", "2O,4C,3P,6C,AE", "Segundo Jogador")]
    public void DeveAnalisarVencedorQuandoTiverMaiorCarta(string cartasDoPrimeiroJogadorString, string cartasDoSegundoJogadorString, string vencedorEsperado)
    {
        var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorString.Split(',').ToList();
        var cartasDoSegundoJogador = cartasDoSegundoJogadorString.Split(',').ToList();

        var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);
        
        Assert.Equal(vencedorEsperado, vencedor);
    }

    [Theory]
    [InlineData("2O,2C,3P,6C,7C", "3O,5C,2E,9C,7P", "Primeiro Jogador")]
    [InlineData("3O,5C,2E,9C,7P", "2O,2C,3P,6C,7C", "Segundo Jogador")]
    [InlineData("2O,2C,3P,6C,7C","DO,DC,2E,9C,7P", "Segundo Jogador")]
    [InlineData("DO,DC,2E,9C,7P", "2O,2C,3P,6C,7C", "Primeiro Jogador")]

    public void DeveAnalisarVencedorQuandoTiverUmParDeCartasDoMesmoValor(string cartasDoPrimeiroJogadorString,
        string cartasDoSegundoJogadorString, string vencedorEsperado)
    {
        var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorString.Split(',').ToList();
        var cartasDoSegundoJogador = cartasDoSegundoJogadorString.Split(',').ToList();

        var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);
        
        Assert.Equal(vencedorEsperado, vencedor);
    }
    
    [Theory]
    [InlineData("2O,2C,3P,6C,7C", "3O,5C,2E,9C,7P", "Primeiro Jogador")]
    [InlineData("RO,2C,3P,RC,7C", "3O,5C,RE,AC,RP", "Segundo Jogador")]

    public void DeveAnalisarVencedorQuandoDoisJogadoresEstaoEmpatadosEmParSendoVencedorOQueTemMaiorCarta(string cartasDoPrimeiroJogadorString,
        string cartasDoSegundoJogadorString, string vencedorEsperado)
    {
        var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorString.Split(',').ToList();
        var cartasDoSegundoJogador = cartasDoSegundoJogadorString.Split(',').ToList();

        var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);
        
        Assert.Equal(vencedorEsperado, vencedor);
    }
}