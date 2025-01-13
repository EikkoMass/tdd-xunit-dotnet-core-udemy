using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.DominioTest.PublicosAlvo;

public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
{
    public PublicoAlvo Converter(string publicoAlvo)
    {
        ValidadorDeRegra.Novo()
            .Quando(!Enum.TryParse<PublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
            .DispararExcecaoSeExistir();
            
        return publicoAlvoConvertido;
    }
}