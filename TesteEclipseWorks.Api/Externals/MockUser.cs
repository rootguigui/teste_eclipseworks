using System.Diagnostics.CodeAnalysis;
using TesteEclipseWorks.Api.Models.Response.Usuario;

namespace TesteEclipseWorks.Api.Externals;
[ExcludeFromCodeCoverage]
public static class MockUser
{
    public static IEnumerable<UsuarioExternal> GetMockUser()
    {
        return new List<UsuarioExternal>
        {
            new UsuarioExternal
            {
                Id = 1,
                Nome = "John Doe",
                Email = "john.doe@example.com",
                Cargo = new CargoExternal
                {
                    Id = 1,
                    Nome = "Desenvolvedor"
                },
            },
            new UsuarioExternal
            {
                Id = 2,
                Nome = "Jane Doe",
                Email = "jane.doe@example.com",
                Cargo = new CargoExternal
                {
                    Id = 1,
                    Nome = "Desenvolvedor"
                },
            },
            new UsuarioExternal
            {
                Id = 3,
                Nome = "Lilian",
                Email = "lilian@example.com",
                Cargo = new CargoExternal
                {
                    Id = 2,
                    Nome = "Gerente"
                },
            }
        };
    }

    public static IEnumerable<CargoExternal> GetMockCargo()
    {
        return new List<CargoExternal>
        {
            new CargoExternal { Id = 1, Nome = "Desenvolvedor" },
            new CargoExternal { Id = 2, Nome = "Gerente" },
        };
    }
}
