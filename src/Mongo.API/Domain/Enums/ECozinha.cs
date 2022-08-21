namespace Mongo.API.Domain.Enums
{
    public enum ECozinha
    {
        Brasileira = 1,
        Italiana = 2,
        Arabe = 3,
        Japonesa = 4,
        FastFood = 5
    }

    public static class ECozinhaHelper
    {
        public static ECozinha ConverterDeInteiro(int valor)
        {
            if(Enum.TryParse(valor.ToString(), out ECozinha cozinha))
                return cozinha;

            throw new ArgumentOutOfRangeException("valor", valor, "O valor não é válido para o tipo ECozinha");
        }
    }
}