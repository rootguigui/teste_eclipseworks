using TesteEclipseWorks.Api.Models.Response.Usuario;

namespace TesteEclipseWorks.Api.Externals;

public class UsuarioExternalApiService : IUsuarioExternalApiService
{
    public async Task<UsuarioExternal?> ObterUsuarioPorId(int id)
    {
        return await Task.FromResult(MockUser.GetMockUser().FirstOrDefault(u => u.Id == id));
    }

    public async Task<IEnumerable<CargoExternal>> ObterCargos()
    {
        return await Task.FromResult(MockUser.GetMockCargo());
    }

}
