# LeveInvestimentos - Sistema de Gestão de Tarefas

Um sistema corporativo moderno para a gestão e atribuição de tarefas, projetado sob os princípios de **Clean Architecture**, empregando **ASP.NET Core Razor Pages**, **Entity Framework Core**, e **UIkit** para fornecer uma interface fluida, responsiva e com design premium.

## Principais Funcionalidades Implementadas

*   **Autenticação Segura & Sessões:** Login protegido com criptografia de senha (`PasswordHasher`), sistema persistente "Lembrar de Mim" via Cookies (duração de 30 dias), controle Anti-Cache rigoroso evitando brechas no botão "Voltar" do navegador, além de encerramento absoluto da sessão no Logout.

*   **Controle de Perfil e Acesso:** Sistema dual com papéis de acesso: `Gestor` (Manager) e `Usuário Comum` (Common). Restrição severa de rotas via `[Authorize(Policy)]` impedindo a navegação e execução de ações não autorizadas (bloqueio no Backend e ocultação no Frontend).
*   **Gestão Completa de Usuários (Exclusivo para Gestores):** Permite listar toda a equipe e realizar operações CRUD (Criar, Editar, Excluir).
*   **Controle e Fluxo de Tarefas:** 
    *   *Gestor:* Pode criar tarefas e atribuí-las a qualquer usuário. Possui acesso à guia exclusiva "Criadas por Mim" e controle irrestrito para Editar e Excluir qualquer tarefa no sistema.
    *   *Usuário Comum:* Acesso restrito à visualização de "Minhas Tarefas" (atribuídas a ele), podendo apenas ver os detalhes ou concluir a atividade.
*   **Design Premium e UX:** Interface rica em detalhes, uso de *Glassmorphism* no cabeçalho e cards, paleta de cores unificada (vermelho escuro corporativo), formulários elegantes (com "Revelar Senha") e integração viacep automatizada.

## Tecnologias e Arquitetura

O software foi desenvolvido separando rigidamente as responsabilidades:
*   **Domain:** Entidades de domínio (`AppTask`, `User`), Enums de Status/Roles e interfaces fundamentais (Puro, sem bibliotecas externas).
*   **Application:** Interfaces de Casos de Uso e DTOs, padronizando a comunicação.
*   **Infrastructure:** Serviços concretos (`TaskService`, `UserService`), contexto de banco de dados (`ApplicationDbContext`) mapeado em Entity Framework Core e *Seeder* inicial.
*   **Web (Apresentação):** Sistema baseado em Razor Pages focado na simplicidade, protegido com as "Policies" do AspNet Identity e renderizado com HTML/CSS Vanilla + UIkit.

## Como Testar a Aplicação

O ecossistema é preparado para facilitar os testes, executando a criação das tabelas e injeção do usuário administrador automaticamente durante o boot (`DbSeeder`).

### 1. Pré-Requisitos
*   .NET SDK instalado na máquina.
*   SQL Server (Express ou LocalDB), que é instanciado por padrão através da string de conexão no `appsettings.json`.

### 2. Inicializando

No seu terminal, a partir da pasta raiz do repositório, acesse a pasta da aplicação Web:
```bash
cd LeveInvestimentos.Web
```

Em seguida, compile e inicie o projeto:
```bash
dotnet run
```
*Nota: A rotina de Migrations validará e construirá o esquema do banco de dados na hora, se necessário.*

Abra no navegador a URL servida pelo terminal (Ex: `http://localhost:5175`).

### 3. Contas de Acesso (Login de Teste)

Para realizar o primeiro acesso sem precisar manipular o banco de dados manualmente, uma conta raiz de Gestor é criada pelo `DbSeeder`:

*   **E-mail:** `ti@leveinvestimentos.com.br`
*   **Senha:** `teste123`

## Autor

Desenvolvido por **Wagner Santos** como solução para desafio técnico.