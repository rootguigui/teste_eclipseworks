# API de Gerenciamento de Tarefas e Projetos

Este projeto é uma API para gerenciamento de tarefas e projetos, desenvolvida com ASP.NET Core.

## Endpoints Principais

### Tarefas
- **GET /api/tarefas**: Lista todas as tarefas
- **GET /api/tarefas/{id}**: Obtém uma tarefa específica
- **POST /api/tarefas**: Cria uma nova tarefa
- **PUT /api/tarefas/{id}**: Atualiza uma tarefa existente
- **DELETE /api/tarefas/{id}**: Remove uma tarefa

### Projetos
- **GET /api/projetos**: Lista todos os projetos
- **GET /api/projetos/{id}**: Obtém um projeto específico
- **POST /api/projetos**: Cria um novo projeto
- **PUT /api/projetos/{id}**: Atualiza um projeto existente
- **DELETE /api/projetos/{id}**: Remove um projeto

### Relatórios
- **POST /api/relatorios/desempenho**: Gera relatório de desempenho dos últimos 30 dias

## Como Executar o Projeto

### Usando Docker Compose

1. Certifique-se de ter o Docker e o Docker Compose instalados
2. Na pasta raiz do projeto, execute:

```bash
# Construir e iniciar os containers
docker-compose up -d

# Executar as migrações do banco de dados
docker-compose exec api dotnet ef database update
```
### Requisitos
- Docker
- Docker Compose
- .NET Core SDK (para desenvolvimento local)
