using Xunit;

namespace CursoOnline.DominioTest._Util;

public static class AssertExtension
{
    public static void ComMensagem(this ArgumentException exception, string mensagem)
    {
        
        Assert.True(exception.Message == mensagem, "Esperava mensagem: " + mensagem);
    }
}