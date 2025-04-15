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


## Refinamento do Projeto

### Perguntas para o Product Owner

#### Sobre Tarefas
1. Qual é o número máximo de tarefas que um projeto pode ter?
4. Existe alguma regra para priorização de tarefas?
5. Uma tarefa pode ser atribuída a mais de um usuário simultaneamente?

#### Sobre Projetos
1. Quais informações são obrigatórias para a criação de um projeto?
3. Existe algum limite de tempo para a duração de um projeto?
4. Como devemos lidar com projetos atrasados (que ultrapassaram a data de término)?

#### Sobre Usuários e Permissões
1. Quais são os diferentes perfis de usuário no sistema (gerente, desenvolvedor, etc.)?
3. Um usuário pode estar em múltiplos projetos ao mesmo tempo?
4. Existe algum limite de tarefas que um usuário pode ter atribuídas simultaneamente?

#### Sobre Relatórios
1. Além do relatório de desempenho dos últimos 30 dias, quais outros relatórios são necessários?
2. Quais métricas específicas devem ser incluídas nos relatórios?
3. Os relatórios precisam ser exportados em algum formato específico (PDF, Excel)?

#### Sobre Integrações
1. O sistema precisa se integrar com outras ferramentas (como sistemas de notificação ou calendários)?


## Melhorias para Próximas Fases

### Arquitetura e Infraestrutura
1. Implementar cache distribuído para melhorar performance
2. Adicionar monitoramento e logging com Elasticsearch, Kibana e Prometheus
3. Configurar CI/CD completo com testes automatizados e deploy contínuo
4. Implementar arquitetura de microsserviços para melhor escalabilidade
5. Adicionar uma Arquitetura CQRS, Utilizar Aspire.

### Segurança
1. Implementar autenticação com OAuth 2.0 e OpenID Connect
2. Adicionar autenticação de dois fatores (2FA)
3. Implementar auditoria completa de ações no sistema
4. Melhorar políticas de permissões

### Integrações
1. Integração com ferramentas de comunicação (Slack, Microsoft Teams)
2. Integração com calendários (Google Calendar, Outlook)
3. Webhooks para sistemas externos
4. API pública com documentação completa para integrações de terceiros

