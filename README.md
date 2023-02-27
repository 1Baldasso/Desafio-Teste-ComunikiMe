# Teste Comuniki Me

## Tecnologias Utilizadas

- ASP.Net Core
- Entity Framework Core
- Arquitetura MVC (Model View Controller)

## Como Executar o Projeto

### Requisitos para Execução
- Git
- Visual Studio
- SQL Server

1 - Crie uma pastar para receber o projeto.

2 - Abra o terminal na pasta.

3 - Execute o Comando 
```
git clone https://github.com/1Baldasso/Desafio-Teste-ComunikiMe
```

4 - Abra o arquivo ```ComunikiMeTeste.sln```

5 - No Visual Studio clique com o botão direito na Solução e clique na opção:

![image](https://user-images.githubusercontent.com/82400557/221455715-a4904488-c463-43cb-aecf-6e52e317de72.png)

6 - Após o download dos pacotes finalizar, volte ao terminal e execute os comandos:
```
cd Desafio-Teste-ComunikiMe
```
```
dotnet ef database update
```

7 - Após a criação do banco de dados, ainda no terminal execute o comando
```
dotnet run --project ComunikMe.WebAPI
```

8 - Defina o Projeto ComunikiMe.WebApp como projeto de inicialização clicando com o botão direito no projeto e na opção:

![image](https://user-images.githubusercontent.com/82400557/221456323-e4e0b435-8394-47ca-81f9-855b5a39a13d.png)

9 - Execute o projeto.

10 - Uma janela do navegador deve abrir, se isso não acontecer você pode acessar através da URL: ```localhost:7265```

11 - Você pode acessar todos os endpoints da API através da URL: ```https://localhost:7200/swagger```

## Importante

- O primeiro usuário criado será definido como usuário administrador, todos os seguintes serão convidados.
