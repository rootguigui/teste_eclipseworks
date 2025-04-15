using Moq;
using FluentAssertions;
using AutoFixture;
using TesteEclipseWorks.Api.Services;
using TesteEclipseWorks.Api.Repositories.Interfaces;
using TesteEclipseWorks.Api.Models.Request.Tarefa;
using TesteEclipseWorks.Api.Models.Response.Tarefa;
using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Models.Enums;

namespace TesteEclipseWorks.Tests.Services
{
    public class TarefaServiceTests
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;
        private readonly Mock<ITarefaHistoricoRepository> _mockTarefaHistoricoRepository;
        private readonly TarefaService _service;
        private readonly Fixture _fixture;

        public TarefaServiceTests()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();
            _mockTarefaHistoricoRepository = new Mock<ITarefaHistoricoRepository>();
            _service = new TarefaService(_mockTarefaRepository.Object, _mockTarefaHistoricoRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AtualizarAsync_QuandoTarefaNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var tarefaId = _fixture.Create<int>();
            var request = _fixture.Create<TarefaRequest>();
            
            _mockTarefaRepository.Setup(x => x.GetByIdAsync(tarefaId))
                .ReturnsAsync((Tarefa)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AtualizarAsync(tarefaId, request));
        }

        [Fact]
        public async Task AtualizarAsync_QuandoTarefaExiste_DeveAtualizarComSucesso()
        {
            // Arrange
            var tarefaId = _fixture.Create<int>();
            var request = _fixture.Create<TarefaRequest>();
            var tarefa = new Tarefa
            {
                TarefaId = tarefaId,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Status = request.Status,
                ProjetoId = request.ProjetoId
            };
            
            _mockTarefaRepository.Setup(x => x.GetByIdAsync(tarefaId))
                .ReturnsAsync(tarefa);

            _mockTarefaRepository.Setup(x => x.UpdateAsync(It.IsAny<Tarefa>()))
                .ReturnsAsync(tarefa);

            _mockTarefaHistoricoRepository.Setup(x => x.AddAsync(It.IsAny<TarefaHistorico>()))
                .ReturnsAsync(new TarefaHistorico());

            // Act
            var result = await _service.AtualizarAsync(tarefaId, request);

            // Assert
            result.Should().NotBeNull();
            _mockTarefaRepository.Verify(x => x.UpdateAsync(It.IsAny<Tarefa>()), Times.Once);
            _mockTarefaHistoricoRepository.Verify(x => x.AddAsync(It.IsAny<TarefaHistorico>()), Times.Once);
        }

        [Fact]
        public async Task CriarAsync_QuandoProjetoNaoTemMaximoTarefas_DeveCriarComSucesso()
        {
            // Arrange
            var request = _fixture.Create<TarefaRequest>();
            var tarefas = new List<Tarefa>
            {
                new Tarefa { TarefaId = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1", Status = TarefaStatusEnum.Pendente, ProjetoId = request.ProjetoId },
                new Tarefa { TarefaId = 2, Titulo = "Tarefa 2", Descricao = "Descrição 2", Status = TarefaStatusEnum.Pendente, ProjetoId = request.ProjetoId }
            };
            var novaTarefa = new Tarefa
            {
                TarefaId = 3,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Status = TarefaStatusEnum.Pendente,
                ProjetoId = request.ProjetoId
            };
            
            _mockTarefaRepository.Setup(x => x.GetAllByProjetoIdAsync(request.ProjetoId))
                .ReturnsAsync(tarefas);

            _mockTarefaRepository.Setup(x => x.AddAsync(It.IsAny<Tarefa>()))
                .ReturnsAsync(novaTarefa);

            // Act
            var result = await _service.CriarAsync(request);

            // Assert
            result.Should().NotBeNull();
            _mockTarefaRepository.Verify(x => x.AddAsync(It.IsAny<Tarefa>()), Times.Once);
        }

        [Fact]
        public async Task DeletarAsync_QuandoTarefaNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var tarefaId = _fixture.Create<int>();
            var comentario = _fixture.Create<string>();
            
            _mockTarefaRepository.Setup(x => x.GetByIdAsync(tarefaId))
                .ReturnsAsync((Tarefa)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.DeletarAsync(tarefaId, comentario));
        }

        [Fact]
        public async Task DeletarAsync_QuandoTarefaExiste_DeveDeletarComSucesso()
        {
            // Arrange
            var tarefaId = _fixture.Create<int>();
            var comentario = _fixture.Create<string>();
            var tarefa = new Tarefa
            {
                TarefaId = tarefaId,
                Titulo = "Tarefa 1",
                Descricao = "Descrição 1",
                Status = TarefaStatusEnum.Pendente,
                ProjetoId = 1
            };
            
            _mockTarefaRepository.Setup(x => x.GetByIdAsync(tarefaId))
                .ReturnsAsync(tarefa);

            _mockTarefaRepository.Setup(x => x.DeleteAsync(tarefaId))
                .ReturnsAsync(true);

            _mockTarefaHistoricoRepository.Setup(x => x.AddAsync(It.IsAny<TarefaHistorico>()))
                .ReturnsAsync(new TarefaHistorico());

            // Act
            var result = await _service.DeletarAsync(tarefaId, comentario);

            // Assert
            result.Should().BeTrue();
            _mockTarefaRepository.Verify(x => x.DeleteAsync(tarefaId), Times.Once);
            _mockTarefaHistoricoRepository.Verify(x => x.AddAsync(It.IsAny<TarefaHistorico>()), Times.Once);
        }

        [Fact]
        public async Task ObterPorIdAsync_QuandoTarefaExiste_DeveRetornarTarefaResponse()
        {
            // Arrange
            var tarefaId = _fixture.Create<int>();
            var tarefa = new Tarefa
            {
                TarefaId = tarefaId,
                Titulo = "Tarefa 1",
                Descricao = "Descrição 1",
                Status = TarefaStatusEnum.Pendente,
                ProjetoId = 1
            };
            
            _mockTarefaRepository.Setup(x => x.GetByIdAsync(tarefaId))
                .ReturnsAsync(tarefa);

            // Act
            var result = await _service.ObterPorIdAsync(tarefaId);

            // Assert
            result.Should().NotBeNull();
            result.TarefaId.Should().Be(tarefaId);
        }

        [Fact]
        public async Task ObterTodosPorProjetoIdAsync_DeveRetornarListaDeTarefasResponse()
        {
            // Arrange
            var projetoId = _fixture.Create<int>();
            var tarefas = new List<Tarefa>
            {
                new Tarefa { TarefaId = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1", Status = TarefaStatusEnum.Pendente, ProjetoId = projetoId },
                new Tarefa { TarefaId = 2, Titulo = "Tarefa 2", Descricao = "Descrição 2", Status = TarefaStatusEnum.Pendente, ProjetoId = projetoId },
                new Tarefa { TarefaId = 3, Titulo = "Tarefa 3", Descricao = "Descrição 3", Status = TarefaStatusEnum.Pendente, ProjetoId = projetoId }
            };
            
            _mockTarefaRepository.Setup(x => x.GetAllByProjetoIdAsync(projetoId))
                .ReturnsAsync(tarefas);

            // Act
            var result = await _service.ObterTodosPorProjetoIdAsync(projetoId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }
    }
} 