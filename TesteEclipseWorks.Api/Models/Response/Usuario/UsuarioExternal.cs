namespace TesteEclipseWorks.Api.Models.Response.Usuario;

public class UsuarioExternal
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public CargoExternal Cargo { get; set; } = new CargoExternal();
}
