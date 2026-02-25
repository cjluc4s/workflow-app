# 🚀 WorkflowApp

Sistema web de workflow para gestão e aprovação de solicitações, desenvolvido em ASP.NET Core MVC com autenticação via Identity, controle de acesso por Roles e histórico de auditoria.

---

## 📋 Sobre o Projeto

O WorkflowApp é uma aplicação web que simula um sistema corporativo interno de criação e aprovação de solicitações, com separação clara de responsabilidades entre perfis de usuário.

O sistema foi desenvolvido com foco em:

- Arquitetura organizada
- Controle de acesso por perfil
- Rastreabilidade de alterações
- Dashboard gerencial
- Boas práticas com Entity Framework Core

---

## 🔐 Controle de Acesso

O sistema possui dois perfis principais:

### 👤 Solicitante
- Criar solicitações
- Visualizar apenas suas solicitações
- Acompanhar status
- Visualizar histórico de alterações

### 👨‍💼 Aprovador
- Visualizar todas as solicitações pendentes
- Aprovar ou rejeitar solicitações
- Visualizar histórico completo
- Dashboard com dados globais

---

## 🔄 Workflow

Cada solicitação possui um ciclo de vida:

Pendente → Aprovado
→ Rejeitado

Todas as alterações de status são registradas em um histórico de auditoria, contendo:

- Status anterior
- Novo status
- Usuário responsável
- Data e hora da alteração

---

## 📊 Dashboard

O sistema possui dashboard condicional por perfil:

- Usuário comum → visualiza apenas seus dados
- Aprovador → visualiza dados globais

Indicadores exibidos:

- Total Geral
- Pendentes
- Aprovadas
- Rejeitadas

---

## 🏗 Tecnologias Utilizadas

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- ASP.NET Core Identity
- SQLite
- Bootstrap 5
- LINQ
- Razor Views

---

## 🧠 Conceitos Aplicados

- Autenticação e Autorização por Roles
- Separação de responsabilidades
- ViewModels
- Controle de acesso por perfil
- Auditoria de alterações
- Consultas agregadas com LINQ
- Migrations com EF Core
- Versionamento com Git

---

## 📁 Estrutura Simplificada

Controllers/
Models/
Data/
Views/
Migrations/

---

## 🚀 Como Executar

1. Clone o repositório
2. Execute:

dotnet restore

dotnet ef database update

dotnet run

3. Acesse via navegador

---

## 📌 Próximas Evoluções Técnicas

- Refatoração do Status para enum
- Camada de Services para regras de negócio
- Paginação e filtros avançados
- Melhorias de UX
- Deploy em ambiente cloud

---

## 👨‍💻 Autor

Desenvolvido por Lucas.
Estudante de Ciência da Computação.
Estagiário em desenvolvimento de sistemas.
