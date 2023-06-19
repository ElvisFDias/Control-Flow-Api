using Control_Flow_Api.Model;
using System.Xml.Linq;

namespace Control_Flow_Api.Services
{
    public class UserService
    {
        private readonly UsuarioRepositorio usuarioRepo;
        private readonly LocalizacaoRepositorio localizacaoRepo;

        public UserService(UsuarioRepositorio usuarioRepo, LocalizacaoRepositorio localizacaoRepo)
        {
            this.usuarioRepo = usuarioRepo;
            this.localizacaoRepo = localizacaoRepo;
        }
        public async Task CriarUsuario_ComException(Usuario usuario)
        {
            if(await usuarioRepo.BuscarPorNome(usuario.Nome) is not null) 
                throw new NomeDuplicadoException();

            if (await usuarioRepo.BuscarPorEmail(usuario.Email) is not null)
                throw new EmailDuplicadoException();

            var cidade = await localizacaoRepo.BuscarCidade(usuario.Cidade);
            if (cidade is null)
                throw new CidadeInvalidaException();

            if (!await localizacaoRepo.ValidarCEP(usuario.CEP))
                throw new CEPInvalidoException();

            await usuarioRepo.Criar(usuario);
        }

        public async Task<Resultado> CriarUsuario_SemExcecao(Usuario user)
        {
            if (await usuarioRepo.BuscarPorNome(user.Nome) is not null)
                return new Resultado();

            if (await usuarioRepo.BuscarPorEmail(user.Email) is not null)
                return new Resultado();

            var city = await localizacaoRepo.BuscarCidade(user.Cidade);
            if (city is null)
                return new Resultado();

            if (!await localizacaoRepo.ValidarCEP(user.CEP))
                return new Resultado();

            await usuarioRepo.Criar(user);

            return new Resultado(true);
            
        }

    }

    public class UsuarioRepositorio
    {
        public async Task<Usuario?> BuscarPorEmail(string email)
        {
            if(email.Equals("email_exist@test.com", StringComparison.OrdinalIgnoreCase))
                return new Usuario() { Email = email };

            return null;
        }

        public async Task<Usuario?> BuscarPorNome(string name)
        {
            if (name.Equals("name_exist", StringComparison.OrdinalIgnoreCase))
                return new Usuario() { Nome = name };

            return null;
        }

        public async Task Criar(Usuario user)
        {
            return;
        }
    }

    public class LocalizacaoRepositorio
    {
        public async Task<Cidade?> BuscarCidade(string name)
        {
            if (name.Equals("city_exist", StringComparison.OrdinalIgnoreCase))
                return new Cidade() { Nome = name };

            return null;
        }

        public async Task<bool> ValidarCEP(string zipcode)
        {
            if (zipcode.Equals("123456", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

    }

}
