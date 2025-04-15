using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TesteEclipseWorks.Api.Services.Interfaces;
using TesteEclipseWorks.Api.Controllers;
using TesteEclipseWorks.Api.Models.Response.Projeto;
using TesteEclipseWorks.Api.Models.Request.Projeto;
using Microsoft.AspNetCore.Http;

namespace TesteEclipseWorks.Tests.src.Controllers
{
    public class ProjetoControllerTests
    {
        private readonly Mock<IProjetoService> _mockProjetoService;
        private readonly ProjetoController _controller;
        private readonly Mock<HttpContext> _httpContextMock = new();
        private readonly Mock<HttpRequest> _httpRequestMock = new();

        public ProjetoControllerTests()
        {
            _mockProjetoService = new Mock<IProjetoService>();
            _controller = new ProjetoController(_mockProjetoService.Object);
            _controller.ControllerContext.HttpContext = CreateHttpContextMock();
        }

        private HttpContext CreateHttpContextMock()
        {
            _httpRequestMock.Setup(set => set.Path).Returns("/projetos");
            _httpContextMock.Setup(set => set.Request).Returns(_httpRequestMock.Object);
            return _httpContextMock.Object;
        }

        [Fact]
        public async Task GetProjetos_DeveRetornarListaDeProjetos()
        {
            // Arrange
            var projetos = new List<ProjetoResponse>
            {
                new ProjetoResponse { ProjetoId = 1, Nome = "Projeto 1" },
                new ProjetoResponse { ProjetoId = 2, Nome = "Projeto 2" }
            };

            _mockProjetoService.Setup(s => s.ObterTodosAsync()).ReturnsAsync(projetos);

            // Act
            var result = await _controller.ObterTodosAsync();

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<ProjetoResponse>>>();
            var okResult = result.Result as ObjectResult;
            okResult?.Value.Should().BeEquivalentTo(projetos);
        }

        [Fact]
        public async Task GetProjeto_QuandoProjetoExiste_DeveRetornarProjeto()
        {
            // Arrange
            var projeto = new ProjetoResponse { ProjetoId = 1, Nome = "Projeto Teste" };
            _mockProjetoService.Setup(s => s.ObterPorIdAsync(1))
                .ReturnsAsync(projeto);

            // Act
            var result = await _controller.ObterPorIdAsync(1);

            // Assert
            result.Should().BeOfType<ActionResult<ProjetoResponse>>();
            var okResult = result.Result as ObjectResult;
            okResult?.Value.Should().BeEquivalentTo(projeto);
        }

        [Fact]
        public async Task GetProjeto_QuandoProjetoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _mockProjetoService.Setup(s => s.ObterPorIdAsync(999))
                .ReturnsAsync((ProjetoResponse)null);

            // Act
            var result = await _controller.ObterPorIdAsync(999);

            // Assert
            result.Should().BeOfType<ActionResult<ProjetoResponse>?>();
        }

        [Fact]
        public async Task CreateProjeto_DeveCriarProjetoERetornarCreated()
        {
            // Arrange
            var novoProjeto = new ProjetoRequest { Nome = "Novo Projeto" };
            var projetoCriado = new ProjetoResponse { ProjetoId = 1, Nome = "Novo Projeto" };

            _mockProjetoService.Setup(s => s.CriarAsync(novoProjeto))
                .ReturnsAsync(projetoCriado);

            // Act
            var result = await _controller.CriarAsync(novoProjeto);

            // Assert
            result.Should().BeOfType<ActionResult<ProjetoResponse>>();
            var createdResult = result.Result as ObjectResult;
            createdResult?.Value.Should().BeEquivalentTo(projetoCriado);
        }

        [Fact]
        public async Task DeleteProjeto_QuandoProjetoExiste_DeveDeletarERetornarNoContent()
        {
            // Arrange
            _mockProjetoService.Setup(s => s.DeletarAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeletarAsync(1);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}