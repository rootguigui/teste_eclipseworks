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
using TesteEclipseWorks.Api.Models.Response.Relatorio;
using TesteEclipseWorks.Api.Models.Request.Relatorio;
using Microsoft.AspNetCore.Http;

namespace TesteEclipseWorks.Tests.src.Controllers
{
    public class RelatorioControllerTests
    {
        private readonly Mock<IRelatorioService> _mockRelatorioService;
        private readonly RelatorioController _controller;
        private readonly Mock<HttpContext> _httpContextMock = new();
        private readonly Mock<HttpRequest> _httpRequestMock = new();

        public RelatorioControllerTests()
        {
            _mockRelatorioService = new Mock<IRelatorioService>();
            _controller = new RelatorioController(_mockRelatorioService.Object);
            _controller.ControllerContext.HttpContext = CreateHttpContextMock();
        }

        private HttpContext CreateHttpContextMock()
        {
            _httpRequestMock.Setup(set => set.Path).Returns("/relatorios");
            _httpContextMock.Setup(set => set.Request).Returns(_httpRequestMock.Object);
            return _httpContextMock.Object;
        }

        [Fact]
        public async Task GetRelatorioProjetos_DeveRetornarRelatorioDeProjetos()
        {
            // Arrange
            var relatorio = new RelatorioDesempenhoResponse
            {
                ProjetoId = 1,
                ProjetoNome = "Projeto Teste",
                QuantidadeTarefas = 2,
                UsuarioId = 1,
                UsuarioNome = "Usuário Teste"
            };

            _mockRelatorioService.Setup(s => s.ObterRelatorioDesempenhoUltimosTrintaDias(It.IsAny<RelatorioUltimosTrintaDiasRequest>()))
                .ReturnsAsync(new List<RelatorioDesempenhoResponse> { relatorio });

            // Act
            var result = await _controller.ObterRelatorioDesempenhoUltimosTrintaDias(new RelatorioUltimosTrintaDiasRequest());

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<RelatorioDesempenhoResponse>>>();
            var okResult = result.Result as ObjectResult;
            okResult?.Value.Should().BeEquivalentTo(new List<RelatorioDesempenhoResponse> { relatorio });
        }

        [Fact]
        public async Task GetRelatorioTarefas_DeveRetornarRelatorioDeTarefas()
        {
            // Arrange
            var relatorio = new RelatorioDesempenhoResponse
            {
                ProjetoId = 1,
                ProjetoNome = "Projeto Teste",
                QuantidadeTarefas = 3,
                UsuarioId = 1,
                UsuarioNome = "Usuário Teste"
            };

            _mockRelatorioService.Setup(s => s.ObterRelatorioDesempenhoUltimosTrintaDias(It.IsAny<RelatorioUltimosTrintaDiasRequest>()))
                .ReturnsAsync(new List<RelatorioDesempenhoResponse> { relatorio });

            // Act
            var result = await _controller.ObterRelatorioDesempenhoUltimosTrintaDias(new RelatorioUltimosTrintaDiasRequest());

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<RelatorioDesempenhoResponse>>>();
            var okResult = result.Result as ObjectResult;
            okResult?.Value.Should().BeEquivalentTo(new List<RelatorioDesempenhoResponse> { relatorio });
        }

        [Fact]
        public async Task GetRelatorioTarefasPorProjeto_QuandoProjetoExiste_DeveRetornarRelatorio()
        {
            // Arrange
            var relatorio = new RelatorioDesempenhoResponse
            {
                ProjetoId = 1,
                ProjetoNome = "Projeto Teste",
                QuantidadeTarefas = 2,
                UsuarioId = 1,
                UsuarioNome = "Usuário Teste"
            };

            _mockRelatorioService.Setup(s => s.ObterRelatorioDesempenhoUltimosTrintaDias(It.IsAny<RelatorioUltimosTrintaDiasRequest>()))
                .ReturnsAsync(new List<RelatorioDesempenhoResponse> { relatorio });

            // Act
            var result = await _controller.ObterRelatorioDesempenhoUltimosTrintaDias(new RelatorioUltimosTrintaDiasRequest());

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<RelatorioDesempenhoResponse>>>();
            var okResult = result.Result as ObjectResult;
            okResult?.Value.Should().BeEquivalentTo(new List<RelatorioDesempenhoResponse> { relatorio });
        }
    }
}