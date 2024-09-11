# TechsysLog - Sistema de Gestão de Pedidos e Entregas

Este repositório contém o projeto **TechsysLog**, desenvolvido para otimizar a gestão de pedidos e entregas, com uma arquitetura robusta e escalável, tanto no frontend quanto no backend.

## 1 - Tecnologias do Frontend
- Angular
- Angular Material
- SignalR (comunicação em tempo real)
- Validações nativas
- Modais para interações
- Guards para controle de login
- Gestão de token JWT no LocalStorage (implementação simples e expansível)
- **Linguagem de Desenvolvimento:** pt-BR

## 2 - Tecnologias do Backend
- .NET 8
- CQRS
- MongoDB
- FluentValidation para validação de comandos
- RestSharp para chamadas externas (ViaCEP)
- Polly para resiliência de chamadas
- SOLID e DDD conforme necessário
- **Linguagem de Desenvolvimento:** en-US

## 3 - Como Executar?

### 3.1 - Backend
- Configurar a instância desejada do MongoDB.
- Executar a aplicação via `dotnet run`.

### 3.2 - Frontend
- Navegar até a pasta do frontend.
- Executar `npm install` para instalar as dependências.
- Configurar `environment.ts` com as referências ao backend.
- Executar `ng serve` para iniciar o frontend.

## 4 - Considerações Finais

Este projeto adota uma abordagem prática comum em alguns sistemas legados, onde o frontend foi desenvolvido em **português (pt-BR)** e o backend em **inglês (en-US)**. Essa separação reflete as necessidades de cada camada e facilita a integração com diferentes equipes e ambientes.

O **SignalR** foi implementado para atualizações em tempo real, tanto para novos pedidos quanto para entregas. 

Em relação à autenticação JWT, foi utilizada uma abordagem simples, com um usuário padrão configurado (login: `admin`, senha: `admin`), permitindo expansões futuras. Esse mecanismo pode ser facilmente atualizado para realizar autenticações em um banco de dados ou por meio de um servidor de autenticação, como o Active Directory (AD).

O sistema é flexível, preparado para futuras melhorias, e oferece uma base sólida para ambientes de produção.
