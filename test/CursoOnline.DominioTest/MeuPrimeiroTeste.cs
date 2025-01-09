namespace CursoOnline.DominioTest;

public class MeuPrimeiroTeste
{
    [Fact(DisplayName = "Testar")]
    public void DeveAsVariaveisTeremOMesmoValor()
    {
        // AAA (Arrange / Act / Assert)
        
        //Arrange
        var variavel = 1;
        var variavel2 = 2;
        
        //Action
        variavel2 = variavel;
        
        // Assert
        Assert.Equal(variavel, variavel2);
        //Assert.True(variavel == variavel2);
    }
}