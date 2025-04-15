using Moq;
using FluentAssertions;
using AutoFixture;
using TesteEclipseWorks.Api.Services;
using TesteEclipseWorks.Api.Repositories.Interfaces;
using TesteEclipseWorks.Api.Models.Request.Projeto;
using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Models.Mappers;

namespace TesteEclipseWorks.Tests.Services
{
    public class ProjetoServiceTests
    {
        private readonly Mock<IProjetoRepository> _mockProjetoRepository;
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;
        private readonly ProjetoService _service;
        private readonly Fixture _fixture;

        public ProjetoServiceTests()
        {
            _mockProjetoRepository = new Mock<IProjetoRepository>();
            _mockTarefaRepository = new Mock<ITarefaRepository>();
            _service = new ProjetoService(_mockProjetoRepository.Object, _mockTarefaRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task DeletarAsync_QuandoProjetoExisteESemTarefasPendentes_DeveDeletarComSucesso()
        {
            // Arrange
            var projetoId = _fixture.Create<int>();
            var projeto = new Projeto
            {
                ProjetoId = projetoId,
                Nome = "Projeto Teste",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(10)
            };
            
            _mockProjetoRepository.Setup(x => x.GetByIdAsync(projetoId))
                .ReturnsAsync(projeto);
            
            _mockTarefaRepository.Setup(x => x.TemTarefasPendentesAsync(projetoId))
                .ReturnsAsync(false);

            _mockProjetoRepository.Setup(x => x.DeleteAsync(projeto.ProjetoId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeletarAsync(projetoId);

            // Assert
            result.Should().BeTrue();
            _mockProjetoRepository.Verify(x => x.DeleteAsync(projeto.ProjetoId), Times.Once);
        }

        [Fact]
        public async Task DeletarAsync_QuandoProjetoNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var projetoId = _fixture.Create<int>();
            
            _mockProjetoRepository.Setup(x => x.GetByIdAsync(projetoId))
                .ReturnsAsync((Projeto)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.DeletarAsync(projetoId));
        }

        [Fact]
        public async Task DeletarAsync_QuandoProjetoTemTarefasPendentes_DeveLancarExcecao()
        {
            // Arrange
            var projetoId = _fixture.Create<int>();
            var projeto = new Projeto
            {
                ProjetoId = projetoId,
                Nome = "Projeto Teste",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(10)
            };
            
            _mockProjetoRepository.Setup(x => x.GetByIdAsync(projetoId))
                .ReturnsAsync(projeto);
            
            _mockTarefaRepository.Setup(x => x.TemTarefasPendentesAsync(projetoId))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.DeletarAsync(projetoId));
        }

        [Fact]
        public async Task ObterPorIdAsync_QuandoProjetoExiste_DeveRetornarProjetoResponse()
        {
            // Arrange
            var projetoId = _fixture.Create<int>();
            var projeto = new Projeto
            {
                ProjetoId = projetoId,
                Nome = "Projeto Teste",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(10)
            };
            
            _mockProjetoRepository.Setup(x => x.GetByIdAsync(projetoId))
                .ReturnsAsync(projeto);

            // Act
            var result = await _service.ObterPorIdAsync(projetoId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(projeto.ToResponse());
        }

        [Fact]
        public async Task ObterPorIdAsync_QuandoProjetoNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var projetoId = _fixture.Create<int>();
            
            _mockProjetoRepository.Setup(x => x.GetByIdAsync(projetoId))
                .ReturnsAsync((Projeto)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ObterPorIdAsync(projetoId));
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarListaDeProjetosResponse()
        {
            // Arrange
            var projetos = new List<Projeto>
            {
                new Projeto { ProjetoId = 1, Nome = "Projeto 1", DataInicio = DateTime.Now, DataFim = DateTime.Now.AddDays(10) },
                new Projeto { ProjetoId = 2, Nome = "Projeto 2", DataInicio = DateTime.Now, DataFim = DateTime.Now.AddDays(10) },
                new Projeto { ProjetoId = 3, Nome = "Projeto 3", DataInicio = DateTime.Now, DataFim = DateTime.Now.AddDays(10) }
            };
            
            _mockProjetoRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(projetos);

            // Act
            var result = await _service.ObterTodosAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(projetos.ToResponse());
        }

        [Fact]
        public async Task CriarAsync_DeveCriarProjetoERetornarProjetoResponse()
        {
            // Arrange
            var request = _fixture.Create<ProjetoRequest>();
            var projeto = request.ToEntity();
            
            _mockProjetoRepository.Setup(x => x.AddAsync(It.IsAny<Projeto>()))
                .ReturnsAsync(projeto);
            // Act
            var result = await _service.CriarAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(projeto.ToResponse());
            _mockProjetoRepository.Verify(x => x.AddAsync(It.IsAny<Projeto>()), Times.Once);
        }
    }
} 