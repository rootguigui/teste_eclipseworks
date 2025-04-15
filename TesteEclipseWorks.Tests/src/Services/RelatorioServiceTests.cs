using Moq;
using FluentAssertions;
using AutoFixture;
using TesteEclipseWorks.Api.Services;
using TesteEclipseWorks.Api.Repositories.Interfaces;
using TesteEclipseWorks.Api.Models.Request.Relatorio;
using TesteEclipseWorks.Api.Models.Response.Relatorio;
using TesteEclipseWorks.Api.Externals;
using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Models.Enums;

namespace TesteEclipseWorks.Tests.Services
{
    public class RelatorioServiceTests
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;
        private readonly Mock<IUsuarioExternalApiService> _mockUsuarioExternalApiService;
        private readonly RelatorioService _service;
        private readonly Fixture _fixture;

        public RelatorioServiceTests()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();
            _mockUsuarioExternalApiService = new Mock<IUsuarioExternalApiService>();
            _service = new RelatorioService(_mockTarefaRepository.Object, _mockUsuarioExternalApiService.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ObterRelatorioDesempenhoUltimosTrintaDias_QuandoUsuarioNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var request = _fixture.Create<RelatorioUltimosTrintaDiasRequest>();
            
            _mockUsuarioExternalApiService.Setup(x => x.ObterUsuarioPorId(request.UsuarioId))
                .ReturnsAsync((Api.Models.Response.Usuario.UsuarioExternal)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ObterRelatorioDesempenhoUltimosTrintaDias(request));
        }

        [Fact]
        public async Task ObterRelatorioDesempenhoUltimosTrintaDias_QuandoUsuarioNaoEGerente_DeveLancarExcecao()
        {
            // Arrange
            var request = _fixture.Create<RelatorioUltimosTrintaDiasRequest>();
            var usuario = new Api.Models.Response.Usuario.UsuarioExternal
            {
                Id = request.UsuarioId,
                Nome = "Teste",
                Cargo = new Api.Models.Response.Usuario.CargoExternal { Nome = "Desenvolvedor" }
            };
            
            _mockUsuarioExternalApiService.Setup(x => x.ObterUsuarioPorId(request.UsuarioId))
                .ReturnsAsync(usuario);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ObterRelatorioDesempenhoUltimosTrintaDias(request));
        }

        [Fact]
        public async Task ObterRelatorioDesempenhoUltimosTrintaDias_QuandoNaoExistemTarefas_DeveRetornarListaVazia()
        {
            // Arrange
            var request = _fixture.Create<RelatorioUltimosTrintaDiasRequest>();
            var usuario = new Api.Models.Response.Usuario.UsuarioExternal
            {
                Id = request.UsuarioId,
                Nome = "Teste",
                Cargo = new Api.Models.Response.Usuario.CargoExternal { Nome = "Gerente" }
            };
            
            _mockUsuarioExternalApiService.Setup(x => x.ObterUsuarioPorId(request.UsuarioId))
                .ReturnsAsync(usuario);

            _mockTarefaRepository.Setup(x => x.GetAllByStatusAsync(
                It.IsAny<TarefaStatusEnum>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
                .ReturnsAsync(new List<Tarefa>());

            // Act
            var result = await _service.ObterRelatorioDesempenhoUltimosTrintaDias(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task ObterRelatorioDesempenhoUltimosTrintaDias_QuandoExistemTarefas_DeveRetornarRelatorioCorretamente()
        {
            // Arrange
            var request = _fixture.Create<RelatorioUltimosTrintaDiasRequest>();
            var usuario = new Api.Models.Response.Usuario.UsuarioExternal
            {
                Id = request.UsuarioId,
                Nome = "Teste",
                Cargo = new Api.Models.Response.Usuario.CargoExternal { Nome = "Gerente" }
            };

            var tarefas = new List<Tarefa>
            {
                new Tarefa { TarefaId = 1, UsuarioId = 1, ProjetoId = 1, Projeto = new Projeto { Nome = "Projeto 1" } },
                new Tarefa { TarefaId = 2, UsuarioId = 1, ProjetoId = 1, Projeto = new Projeto { Nome = "Projeto 1" } }
            };
            
            _mockUsuarioExternalApiService.Setup(x => x.ObterUsuarioPorId(request.UsuarioId))
                .ReturnsAsync(usuario);

            _mockTarefaRepository.Setup(x => x.GetAllByStatusAsync(
                It.IsAny<TarefaStatusEnum>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
                .ReturnsAsync(tarefas);

            _mockUsuarioExternalApiService.Setup(x => x.ObterUsuarioPorId(It.IsAny<int>()))
                .ReturnsAsync(usuario);

            // Act
            var result = await _service.ObterRelatorioDesempenhoUltimosTrintaDias(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().UsuarioId.Should().Be(1);
            result.First().ProjetoId.Should().Be(1);
            result.First().QuantidadeTarefas.Should().Be(2);
        }
    }
} 