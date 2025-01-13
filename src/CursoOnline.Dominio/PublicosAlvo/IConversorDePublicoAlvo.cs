using CursoOnline.Dominio.Enums;

namespace CursoOnline.DominioTest.PublicosAlvo;

public interface IConversorDePublicoAlvo
{
    PublicoAlvo Converter(string publicoAlvo);
}