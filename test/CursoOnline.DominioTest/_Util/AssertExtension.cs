using CursoOnline.Dominio._Base;
using Xunit;

namespace CursoOnline.DominioTest._Util;

public static class AssertExtension
{
    public static void ComMensagem(this ExcecaoDeDominio exception, string mensagem)
    {
        
        Assert.True(exception.MensagensDeErro.Contains(mensagem), "Esperava mensagem: " + mensagem);
    }
}