using TesteEclipseWorks.Api.Models.Response.Usuario;

namespace TesteEclipseWorks.Api.Externals;

public interface IUsuarioExternalApiService
{
    Task<UsuarioExternal?> ObterUsuarioPorId(int id);
    Task<IEnumerable<CargoExternal>> ObterCargos();
}
