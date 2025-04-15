using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TesteEclipseWorks.Api.Controllers;
using TesteEclipseWorks.Api.Models;
using TesteEclipseWorks.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteEclipseWorks.Api.Services.Interfaces;
using TesteEclipseWorks.Api.Models.Response.Tarefa;
using TesteEclipseWorks.Api.Models.Request.Tarefa;
using Microsoft.AspNetCore.Http;

namespace TesteEclipseWorks.Tests.src.Controllers
{
    public class TarefaControllerTests
    {
        private readonly Mock<ITarefaService> _mockTarefaService;
        private readonly TarefaController _controller;
        private readonly Mock<HttpContext> _httpContextMock = new();
        private readonly Mock<HttpRequest> _httpRequestMock = new();

        public TarefaControllerTests()
        {
            _mockTarefaService = new Mock<ITarefaService>();
            _controller = new TarefaController(_mockTarefaService.Object);
            _controller.ControllerContext.HttpContext = CreateHttpContextMock();
        }

        private HttpContext CreateHttpContextMock()
        {
            _httpRequestMock.Setup(set => set.Path).Returns("/tarefas");
            _httpContextMock.Setup(set => set.Request).Returns(_httpRequestMock.Object);
            return _httpContextMock.Object;
        }

        [Fact]
        public async Task GetTarefas_DeveRetornarListaDeTarefas()
        {
            // Arrange
            var tarefas = new List<TarefaResponse>
            {
                new TarefaResponse { TarefaId = 1, Titulo = "Tarefa 1" },
                new TarefaResponse { TarefaId = 2, Titulo = "Tarefa 2" }
            };

            _mockTarefaService.Setup(s => s.ObterTodosPorProjetoIdAsync(It.IsAny<int>()))
                .ReturnsAsync(tarefas);

            // Act
            var result = await _controller.ObterTodosPorProjetoIdAsync(1);

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<TarefaResponse>>>();
            var okResult = result.Result as ObjectResult;
            okResult?.Value.Should().BeEquivalentTo(tarefas);
        }

        [Fact]
        public async Task GetTarefa_QuandoTarefaExiste_DeveRetornarTarefa()
        {
            // Arrange
            var tarefa = new TarefaResponse { TarefaId = 1, Titulo = "Tarefa Teste" };
            _mockTarefaService.Setup(s => s.ObterPorIdAsync(1))
                .ReturnsAsync(tarefa);

            // Act
            var result = await _controller.ObterPorIdAsync(1);

            // Assert
            result.Should().BeOfType<ActionResult<TarefaResponse>>();
            var okResult = result.Result as ObjectResult;
            okResult?.Value.Should().BeEquivalentTo(tarefa);
        }

        [Fact]
        public async Task GetTarefa_QuandoTarefaNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _mockTarefaService.Setup(s => s.ObterPorIdAsync(999))
                .ReturnsAsync((TarefaResponse)null);

            // Act
            var result = await _controller.ObterPorIdAsync(999);

            // Assert
            result.Should().BeOfType<ActionResult<TarefaResponse>?>();
        }

        [Fact]
        public async Task CreateTarefa_DeveCriarTarefaERetornarCreated()
        {
            // Arrange
            var novaTarefa = new TarefaRequest { Titulo = "Nova Tarefa" };
            var tarefaCriada = new TarefaResponse { TarefaId = 1, Titulo = "Nova Tarefa" };

            _mockTarefaService.Setup(s => s.CriarAsync(novaTarefa))
                .ReturnsAsync(tarefaCriada);

            // Act
            var result = await _controller.CriarAsync(novaTarefa);

            // Assert
            result.Should().BeOfType<ActionResult<TarefaResponse>>();
            var createdResult = result.Result as ObjectResult;
            createdResult?.Value.Should().BeEquivalentTo(tarefaCriada);
        }

        [Fact]
        public async Task UpdateTarefa_QuandoTarefaExiste_DeveAtualizarERetornarNoContent()
        {
            // Arrange
            var tarefaAtualizada = new TarefaRequest { Titulo = "Tarefa Atualizada" };
            var tarefaAtualizadaResponse = new TarefaResponse { TarefaId = 1, Titulo = "Tarefa Atualizada" };
            _mockTarefaService.Setup(s => s.AtualizarAsync(1, tarefaAtualizada))
                .ReturnsAsync(tarefaAtualizadaResponse);

            // Act
            var result = await _controller.AtualizarAsync(1, tarefaAtualizada);

            // Assert
            result.Should().BeOfType<ActionResult<TarefaResponse>>();
        }


        [Fact]
        public async Task DeleteTarefa_QuandoTarefaExiste_DeveDeletarERetornarNoContent()
        {
            // Arrange
            _mockTarefaService.Setup(s => s.DeletarAsync(1, It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeletarAsync(1, "Comentário de teste");

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}