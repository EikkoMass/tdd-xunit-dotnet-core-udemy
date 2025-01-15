namespace Poker.Game;

public class AnalisadorDeVencedor
{
   public string Analisar(List<string> maoPrimeiroJogador, List<string> maoSegundoJogador)
   {
       var cartasDuplicadasDoPrimeiroJogador = maoPrimeiroJogador
           .Select(carta => ConverterParaValorDaCarta(carta))
           .GroupBy(valorDaCarta => valorDaCarta)
           .Where(grupo => grupo.Count() > 1);
 
       var cartasDuplicadasDoSegundoJogador = maoSegundoJogador
           .Select(carta => ConverterParaValorDaCarta(carta))
           .GroupBy(valorDaCarta => valorDaCarta)
           .Where(grupo => grupo.Count() > 1);

       if (cartasDuplicadasDoPrimeiroJogador != null && cartasDuplicadasDoPrimeiroJogador.Any() &&
           cartasDuplicadasDoSegundoJogador != null && cartasDuplicadasDoSegundoJogador.Any())
       {
           var maiorPardeCartasPrimeiroJogador = cartasDuplicadasDoPrimeiroJogador.Select(valor => valor.Key).OrderBy(valor => valor).Max();
           var maiorPardeCartasSegundoJogador = cartasDuplicadasDoSegundoJogador.Select(valor => valor.Key).OrderBy(valor => valor).Max();

           if (maiorPardeCartasPrimeiroJogador > maiorPardeCartasSegundoJogador)
           {
               return "Primeiro Jogador";
           } else if (maiorPardeCartasSegundoJogador > maiorPardeCartasPrimeiroJogador)
           {
               return "Segundo Jogador";
           }
       } 
       else if (cartasDuplicadasDoPrimeiroJogador != null && cartasDuplicadasDoPrimeiroJogador.Any())
       {
           return "Primeiro Jogador";
       } 
       else if (cartasDuplicadasDoSegundoJogador != null && cartasDuplicadasDoSegundoJogador.Any())
       {
           return "Segundo Jogador";
       }
       
      var maiorCartaDoPrimeiroJogador = maoPrimeiroJogador.Select(carta => ConverterParaValorDaCarta(carta))
          .OrderBy(valorDaCarta => valorDaCarta)
          .Max();
      
      var maiorCartaDoSegundoJogador = maoSegundoJogador.Select(carta => ConverterParaValorDaCarta(carta))
          .OrderBy(valorDaCarta => valorDaCarta)
          .Max();
      
      return (maiorCartaDoPrimeiroJogador > maiorCartaDoSegundoJogador ? "Primeiro" : "Segundo") + " Jogador";
   }

   private int ConverterParaValorDaCarta(string carta)
   {
       var valorDaCarta = carta.Substring(0, carta.Length - 1);

       if (!int.TryParse(valorDaCarta, out var valor))
       {
           switch (valorDaCarta)
           {
               case "V":
                   valor = 11;
                   break;
               case "D":
                   valor = 12;
                   break;
               case "R":
                   valor = 13;
                   break;
               case "A":
                   valor = 14;
                   break;
           }
       }
       
       return valor;
   }
}