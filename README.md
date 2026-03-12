# 🚀 WorkflowApp

Sistema web de workflow para criação e aprovação de solicitações, desenvolvido com **ASP.NET Core MVC**, autenticação via **ASP.NET Identity** e controle de acesso por **Roles**.

O projeto simula um sistema corporativo interno onde usuários criam solicitações e aprovadores podem analisá-las, mantendo **histórico completo de alterações**.

---

## ⚙️ Funcionalidades

- Autenticação com ASP.NET Identity
- Controle de acesso por Roles (Solicitante / Aprovador)
- Criação de solicitações
- Aprovação e rejeição de solicitações
- Histórico de alterações (auditoria)
- Dashboard com indicadores
- Separação de regras de negócio com **Service Layer**

---

## 🏗 Tecnologias

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- ASP.NET Core Identity
- SQLite
- Bootstrap 5
- LINQ
- Git

---

## 🧠 Conceitos Aplicados

- Arquitetura em camadas
- Service Layer
- Enum para controle de Status
- Autenticação e autorização
- Auditoria de alterações
- Migrations com EF Core

---

## 🚀 Executar o Projeto

```bash
git clone https://github.com/cjluc4s/workflow-app.git
dotnet restore
dotnet ef database update
dotnet run
