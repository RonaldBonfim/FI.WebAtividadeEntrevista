Teste Prático – Vaga de Desenvolvedor
Este repositório contém a solução desenvolvida para o teste prático técnico para avaliação de conhecimentos na vaga de desenvolvedor.

Objetivo
O objetivo deste teste é avaliar o conhecimento técnico e a lógica de desenvolvimento do candidato, com base em requisitos específicos aplicados a um sistema de cadastro de clientes.

Requisitos Técnicos
Visual Studio 2022 (preferencialmente com os seguintes pacotes instalados):

Pacote de direcionamento do .NET Framework 4.8

SDK do .NET Framework 4.8

SQL Server Express 2019 LocalDB

Funcionalidades Implementadas
1. Campo CPF para Cliente
Adição do campo CPF na tela de cadastro/edição de clientes.

Formatação aplicada no padrão 999.999.999-99.

Validação de CPF (verificação de dígitos verificadores).

Preenchimento obrigatório.

Verificação de unicidade: não permite CPF duplicado no banco.

Alteração do banco de dados: adição do campo CPF na tabela CLIENTES.

2. Cadastro de Beneficiários
Inclusão do botão Beneficiários na tela de clientes.

Abertura de um pop-up para inclusão e manutenção dos beneficiários.

Campos no pop-up:

CPF do beneficiário (com validação e formatação)

Nome do beneficiário

Grid com lista de beneficiários vinculados ao cliente.

Funcionalidades de adicionar, editar e excluir beneficiários.

Validação para impedir beneficiários com CPF repetido para o mesmo cliente.

Os beneficiários são salvos no banco ao confirmar o cadastro do cliente.

Alteração do banco de dados: criação da tabela BENEFICIARIOS com os campos:

ID

CPF

NOME

IDCLIENTE

Banco de Dados
A base de dados utilizada está localizada no diretório App_Data da aplicação. A estrutura foi adaptada conforme os requisitos, com os seguintes ajustes:

Tabela CLIENTES: adicionado campo CPF.

Tabela BENEFICIARIOS: criada para relacionar beneficiários aos clientes.

Considerações Finais
Este projeto foi desenvolvido com foco em clareza, organização e aderência aos padrões do sistema base. Todas as validações de CPF seguem os critérios estabelecidos no teste, garantindo integridade e consistência dos dados.
