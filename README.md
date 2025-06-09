# Desafio Técnico: Desenvolvedor (.NET C# + Angular)

## Contexto

O desafio consiste na construção de uma aplicação web para simular um processo de pedidos em um ecommerce simplificado. O objetivo é avaliar sua capacidade técnica, decisões arquiteturais e domínio das ferramentas modernas do ecossistema .NET e Angular. O prazo para a entrega é até **dia 09/06/2025**.

A entrega será feita via **Pull Request** sobre um **fork** deste repositório.

## Descrição Geral

O sistema simula um e-commerce com:

- Listagem de produtos (pré-cadastrados no banco)
- Criação de pedidos (compra de um ou mais produtos)
- Processamento assíncrono dos pedidos via fila com MassTransit
- Tela de acompanhamento do status dos pedidos

## Funcionalidades e Requisitos Técnicos

### Backend (C# com .Net Framework 4.x ou .NET 6+)

- Web API e Consumer construída com C# e ASP.NET
- Produtos devem ser pré-cadastrados no banco, não é necessário implementar CRUD completo
- Criação de pedidos pela API (usuário envia um ou mais itens de produto, quantidade, etc.)
- Uso de **MassTransit** com mensageria (RabbitMQ) para simular o processamento dos pedidos
- Processamento assíncrono: ao receber o pedido, ele deve ser enfileirado e processado por um consumer
- Durante o processamento, o status do pedido deve evoluir em etapas: `Recebido` -> `Em Processamento` -> `Concluído` ou `Falhou`. O status `Falhou` pode ser usado para simular uma falha no processamento do pedido, permitindo testar mecanismos como retry ou circuit breaker.
- Persistência de pedidos e produtos em banco de dados (SQL Server ou Postgres)

### Frontend (Angular 13+)

- Tela para listar produtos disponíveis (chamada à API)
- Tela para criar pedido selecionando produto(s) e quantidade(s)
- Tela para listar pedidos realizados e acompanhar o status em tempo real ou por refresh manual (polling)
- Gerenciamento básico de estado (com ou sem biblioteca externa, como NGXS ou NgRx) será um diferencial
- Boas práticas de estrutura de componentes e organização de módulos

### Simulação do Processamento Assíncrono

- Após o pedido ser enfileirado, o consumer deve simular o processamento em etapas:
  - Esperar 30 segundos -> atualizar status para `Em Processamento`
  - Esperar mais 90 segundos -> com base em uma chance aleatória de 50%, atualizar status para `Concluído` ou `Falhou`
- As atualizações devem ser persistidas no banco para que possam ser consultadas na tela de pedidos

### Observações Técnicas

- **Não é necessário login/autenticação**
- **É necessário que, caso ocorra uma falha (`Falhou`) durante o processamento, a mensagem seja reenviada automaticamente para a fila para reprocessamento (mecanismo de retry) após 60 segundos**. A mensagem pode falhar no máximo 3 vezes consecutivas. Após a terceira falha, o pedido deve ser considerado com status final `Falhou`, sem novas tentativas.
- Pode usar quaisquer bibliotecas/frameworks auxiliares para facilitar o desenvolvimento, tanto no back como no front, mas se fizer seus próprios componentes do Angular será um diferencial
- Documentação da API com Swagger não é obrigatória, mas será um diferencial também

## Testes Automatizados

### Backend

- É obrigatório incluir testes automatizados no backend (.NET), com foco em testes de unidade e/ou testes de integração.

### Frontend

- Não é obrigatório incluir testes automatizados no frontend (Angular), mas a presença de testes de unidade (Jest preferencialmente) ou testes de componentes/e2e (Cypress preferencialmente) será considerada um diferencial positivo.

## Critérios de Avaliação

- **Organização do código**: clareza, separação de responsabilidades, arquitetura
- **Funcionalidade**: sistema funcionando de ponta a ponta, sem bugs e vulnerabilidades
- **Qualidade do frontend**: UX básica, componentização, integração com backend
- **Documentação**: README, com no mínimo, instruções claras de como subir o projeto (backend e frontend)
- **Uso de boas práticas**: tanto em Angular quanto em .NET

## Extra - Ambiente de Execução com Docker

Recomenda-se a inclusão de um `docker-compose.yml` para facilitar a execução local da aplicação. O compose deve contemplar:

- Web API e Consumer
- RabbitMQ (para mensageria com MassTransit)
- Banco de dados (SQL Server/PostgreSQL)

O `README.md` deve conter instruções de como subir todos os serviços com `docker-compose up`, incluindo as portas expostas e qualquer configuração necessária para testes locais.

## Instruções de Entrega

1. Faça um fork deste repositório
2. Desenvolva sua solução nesse fork
3. Ao finalizar, crie um Pull Request para o repositório original
4. Garanta que todas as instruções de execução estejam descritas no `README.md`
5. Mesmo que não consiga ou não dê tempo de fazer tudo que está descrito no desafio, recomendamos que faça a pull request mesmo assim

Boa sorte!
