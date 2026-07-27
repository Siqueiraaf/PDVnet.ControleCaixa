# PDVnet.ControleCaixa

Sistema desktop desenvolvido em **C#** utilizando **WPF**, padrão **MVVM** e **Entity Framework Core** para gerenciamento de movimentações de caixa.

O projeto foi criado com o objetivo de ser simples e aplicar boas práticas de desenvolvimento, como separação de responsabilidades, arquitetura em camadas, injeção de dependências e persistência de dados utilizando SQL Server.

Próximo ao final desta documentação haverá uma explicação de `configuração` e `funcionalidades` da tela.

---

# Funcionalidades

* Cadastro de movimentações de caixa
* Edição de movimentações
* Exclusão de movimentações
* Listagem de todas as movimentações
* Controle de entradas e saídas
* Validação das informações antes da gravação
* Interface desenvolvida em WPF utilizando o padrão MVVM

---

# Tecnologias e Ferramentas Utilizadas

* C#
* .NET
* WPF
* MVVM
* CommunityToolkit.Mvvm
* Entity Framework Core
* SQL Server
* Dependency Injection
* SSMS
* Visual Studio
* Readme.so (https://readme.so/)

---

# Arquitetura

O projeto foi desenvolvido seguindo uma arquitetura em camadas, separando responsabilidades para facilitando manutenção, testes e evolução da aplicação.

```
PDVnet.ControleCaixa
│
├── PDVnet.ControleCaixa.Model
│
├── PDVnet.ControleCaixa.Business
│
├── PDVnet.ControleCaixa.Data
│
└── PDVnet.ControleCaixa.UI
```

---

# Estrutura do Projeto

## Model

```
PDVnet.ControleCaixa.Model
│
├── MovimentacaoCaixa.cs
│
└── Enums
    └── TipoMovimentacao.cs
```
Responsável pelas entidades e enums utilizados pela aplicação.

Exemplos:

* MovimentacaoCaixa
* TipoMovimentacao
* Categoria (caso utilizada)

Esta camada representa apenas os objetos de domínio da aplicação.

---

## Business

Contém toda a lógica de negócio.

Responsabilidades:

* Validar informações
* Aplicar regras de negócio
* Intermediar a comunicação entre a UI e o Repository

Estrutura:

```
PDVnet.ControleCaixa.Business
│
├── Exceptions
│   └── BusinessException.cs
│
├── Interfaces
│   └── IMovimentacaoService.cs
│
├── Services
│   └── MovimentacaoService.cs
│
└── Validators
    └── MovimentacaoValidator.cs
```

Exemplo de serviço:

```
IMovimentacaoService
```

Responsável por:

* Cadastrar movimentação
* Atualizar movimentação
* Excluir movimentação
* Listar movimentações

---

## Data

Camada responsável pela persistência dos dados.

Contém:

* DbContext
* Configurações do Entity Framework
* Repositories
* Migrations

Estrutura:

```
PDVnet.ControleCaixa.Data
│
├── DatabaseConnection.cs
│
├── Context
│   ├── PDVnetControleCaixaDbContext.cs
│   └── PDVnetControleCaixaDbContextFactory.cs
│
├── Interfaces
│   └── IMovimentacaoRepository.cs
│
├── Helpers
│   └── Log.cs
│
├── Mappings
│   └── MovimentacaoCaixaMap.cs
│
├── Migrations
│   ├── PDVnetControleCaixa-v1.cs
│   ├── PDVnetControleCaixa-v1.Designer.cs
│   └── PDVnetControleCaixaDbContextModelSnapshot.cs
│
└── Repositories
    └── MovimentacaoRepository.cs
```

Responsabilidades:

* Comunicação com o banco de dados
* Consultas
* Inserções
* Atualizações
* Exclusões

---

## UI

Camada responsável pela interface gráfica.

Foi construída utilizando o padrão **MVVM**, separando completamente a interface da Regra de negócio da aplicação.

Estrutura:

```
PDVnet.ControleCaixa.UI
│
├── Assets
│   └── cx.ico
│
├── Behaviors
│   └── NumericTextBoxBehavior.cs
│
├── Converters
│   └── StatusConverter.cs
│
├── Interfaces
│   └── IDialogService.cs
│
├── Resources
│   └── MovimentacaoOptions.cs
│
├── Services
│   └── DialogService.cs
│
├── Styles
│ 	├── ComboBoxStyles.xaml
│   └── ButtonStyles.xaml
│
├── ViewModels
│   ├── BaseViewModel.cs
│   ├── MainViewModel.cs
│   └── Movimentacao
│       ├── MovimentacaoFormViewModel.cs
│       ├── MovimentacaoCreateViewModel.cs
│       ├── MovimentacaoDeleteViewModel.cs
│       ├── MovimentacaoEditViewModel.cs
│       └── MovimentacaoListViewModel.cs
│
└── Views
    ├── MainWindow.xaml
    ├── MainWindow.xaml.cs
	├── MovimentacaoForm.xaml
    ├── MovimentacaoForm.cs
    └── Movimentacao
        ├── MovimentacaoCreate.xaml
        ├── MovimentacaoCreate.xaml.cs
        ├── MovimentacaoDelete.xaml
        ├── MovimentacaoDelete.xaml.cs
        ├── MovimentacaoEdit.xaml
        ├── MovimentacaoEdit.xaml.cs
        ├── MovimentacaoList.xaml
        └── MovimentacaoList.xaml.cs
```

### Views

Responsáveis apenas pela interface visual da aplicação.

### ViewModels

Responsáveis pela lógica das telas.

Utilizam:

* ObservableObject
* RelayCommand
* ObservableProperty

Fornecidos pelo:

```
CommunityToolkit.Mvvm
```

### DialogService

Responsável por abrir as janelas de:

* Cadastro
* Edição
* Exclusão

Mantendo a ViewModel desacoplada da View.

---

# Banco de Dados

O projeto utiliza **SQL Server** juntamente com o **Entity Framework Core**.

A criação e atualização do banco são realizadas através das Migrations(conforme a documentação).

Migration inicial:

```
PDVnetControleCaixa-v1
```

---

# Configuração

## 1. Clonar o projeto

```bash
git clone https://github.com/Siqueiraaf/PDVnet.ControleCaixa
```

---

## 2. Abrir no Visual Studio

Abra a solução:

```
PDVnet.ControleCaixa.sln
```

---

## 3. Configurar a Connection String (`App.config`)

O projeto utiliza **SQL Server** para armazenamento dos dados. Antes de executar a aplicação, é necessário configurar a conexão com o banco de dados.

No arquivo de configuração, informe sua conexão com o SQL Server.

*Caminho: PDVnet.ControleCaixa\PDVnet.ControleCaixa.UI\App.config*

    PDVnet.ControleCaixa
    └── PDVnet.ControleCaixa.UI
    └── App.config

Exemplo:

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<connectionStrings>
		<add name="PDVnetConnection"
			 connectionString="SUA_STRING_DE_CONEXAO_AQUI"
			 providerName="Microsoft.Data.SqlClient" />
	</connectionStrings>
</configuration>
```
Exemplo da connectionString utilizando SQL Server local:
```
<add name="PDVnetConnection"
	 connectionString="Server=.;Database=PDVnetControleCaixa;Trusted_Connection=True;TrustServerCertificate=True"
	 providerName="Microsoft.Data.SqlClient" />
```
---

## 4. Criar o banco de dados

#### Executando as migrations pelo Visual Studio

### No Visual Studio siga os passos:

1- Acesse o menu:

    Ferramentas
        ↓
    Gerenciador de Pacotes NuGet
        ↓
    Console do Gerenciador de Pacotes
    
2- No Console do Gerenciador de Pacotes, localize a opção:

    Projeto padrão

3- Altere o projeto padrão para:

    PDVnet.ControleCaixa.Data

A configuração deve ficar assim:

    Projeto padrão: PDVnet.ControleCaixa.Data

No terminal do **Package Manager Console** e execute:

```powershell
Update-Database
```

Caso seja necessário criar uma nova migration:

```powershell
Add-Migration PDVnetControleCaixa-v1 
```
A evolução da migration será apartir da adição da "-v" ex: v2, v3 e etc...

---

## 5. Executar

Pressione:

```
F5
```

ou

```
Ctrl + F5
```

---

# Fluxo da Aplicação

```
Usuário
    ↓
View (WPF)
    ↓
ViewModel
    ↓
Business Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

---

# Padrões Utilizados

* MVVM
* Repository Pattern
* Dependency Injection
* SOLID
* Entity Framework Core

---

# Telas
Para acessar os botões de `Excluir` ou `Editar` é preciso selecionar algum item da listagem da página principal.

Exemplo:
Para utilizar as opções **Editar** ou **Excluir**, é necessário primeiro selecionar uma movimentação na listagem da página principal.

A aplicação utiliza a seleção do item no DataGrid para identificar qual registro será alterado ou removido.
```
1. Acesse a tela principal:
    Movimentações

2. Selecione uma movimentação na lista:
    | Descrição       | Categoria | Tipo    | Valor  |
    | --------------- | --------- | ------- | ------ |
    | Venda Produto X | Vendas    | Entrada | 150,00 |


3. Após selecionar o registro, os botões estarão disponíveis para utilização:
[ Cadastrar ] [ Editar ] [ Excluir ]

```
---
# Testes Unitários

O projeto possui testes unitários para garantir o correto funcionamento das regras de negócio da aplicação.

Atualmente são testadas as validações da classe `MovimentacaoValidator`, incluindo:

- Descrição obrigatória.
- Categoria obrigatória.
- Valor maior que zero.
- Limite máximo de 200 caracteres para a descrição.
- Cenário de sucesso para uma movimentação válida.

### Executando os testes

Os testes unitários podem ser executados de duas formas:

#### Pelo terminal

Na raiz da solução, execute:

```bash
dotnet test
```

#### Pelo Visual Studio

1. Clique com o botão direito no projeto **`PDVnet.ControleCaixa.Tests`**.
2. Selecione **Executar Testes** (*Run Tests*).

Sinta-se à vontade para executar todos os cenários de teste disponíveis. Eles validam as principais regras de negócio da aplicação, garantindo o comportamento esperado para operações válidas e inválidas.

Se todos os testes forem executados com sucesso, será exibido um resultado semelhante a:

```text
Passed! - Failed: 0, Passed: X, Skipped: 0
```

## Logs da aplicação

O sistema possui um mecanismo simples de registro de logs para armazenar algumas operações realizadas como edição, exclusão de movimentações. E fornecem um histórico básico das operações executadas.

Optei por não utilizar bibliotecas como **NLog** ou **Serilog**, pois o objetivo do projeto é manter uma implementação simples, de fácil entendimento e com poucas dependências externas. Para as necessidades da aplicação, um arquivo de texto é suficiente para registrar os eventos de alterações importantes, sem adicionar complexidade desnecessária ao projeto.

Os registros de log foram implementados na camada **Data**, junto à infraestrutura de persistência. Essa abordagem permite registrar as informações completas das entidades durante as operações de edição e exclusão, sem criar dependências circulares entre os projetos **Business** e **Data**, preservando a organização da arquitetura da aplicação.

Durante as edições, o sistema registra apenas os campos que sofreram alterações, informando o valor anterior e o novo valor, o que facilita a auditoria e o acompanhamento das modificações realizadas.

Exemplo:

    [26/07/2026 12:24:31] EXCLUSÃO: Id=34 | Descrição=Compra medicamento | Categoria=Fornecedores |  Tipo=Saida | Valor=R$ 2.000,00 | Status=True
    [26/07/2026 12:24:50] EDIÇÃO: Id=32 | Descrição: 'Rf' -> 'Receita Federal' | Categoria:     'Despesas Fixas' -> 'Impostos'

**Caminho:** `PDVnet.ControleCaixa\PDVnet.ControleCaixa.UI\bin\Debug\net10.0-windows`

**Nome do arquivo:** `logs.txt`

