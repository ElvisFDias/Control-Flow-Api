using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Control_Flow_Api.Model
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
    }

    public class Cidade
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }
    }

    public class Resultado
    {
        public Resultado() : this(false) { }

        public Resultado(bool success)
        {
            Sucesso = success;
        }

        public bool Sucesso { get; set; }
    }

    public class CadastroUsuarioException : Exception { }
    public class NomeDuplicadoException : CadastroUsuarioException { }

    public class EmailDuplicadoException : CadastroUsuarioException { }

    public class CidadeInvalidaException : CadastroUsuarioException { }

    public class CEPInvalidoException : CadastroUsuarioException { }

}
