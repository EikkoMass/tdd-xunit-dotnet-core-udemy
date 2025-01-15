namespace Poker.Game;

public class Carta
{
    public string Valor { get; private set; }
    public string Naipe { get; private set; }
    public int Peso { get; private set; }


    public Carta(string carta)
    {
        Naipe = carta.Substring(carta.Length - 1);
        Valor = carta.Replace(Naipe, string.Empty);
        
        if (Naipe != "O" && Naipe != "C" && Naipe != "P" && Naipe != "E")
        {
            throw new Exception("Valor da naipe invalido");
        }
        
        ConverterParaPesoDaCarta(Valor);
        
        if(Peso < 2 || Peso > 14)
            throw new Exception("Valor da carta invalida");
    }

    private void ConverterParaPesoDaCarta(string valorDaCarta)
    {
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

        Peso = valor;
    }

}