# API Churrasco Trinca
<p align="center">API RESTful desenvolvida em C# .Net Core 5, com o intuito de gerenciar uma agenda de churrasco</p>

# Requisitos da API:
Incluir um novo churrasco com data, descrição e observações adicionais;
Adicionar e remover participantes (colocando o seu valor de contribuição);
Colocar um valor sugerido por usuário de contribuição (valor com e sem bebida inclusa);
Visualizar os detalhes do churrasco, total de participantes e valor arrecadado.

#A API FOI CONSTRUIDA COM Autenticação JWT e conceitos de Refresh Token

# Requisitos
Para execução do projeto, é necessário instalação do .Net 5. O passo-a-passo abaixo foi feito com base no Visial Studio 2019.

# DataBase
No projeto foi utilizado um banco de dados SQLSERVER executado localmente.

# Para executar o projeto
Clone o repositório ou faça download;
Para iniciar a aplicação clique no projeto pricipal com o botão direito do mouse, selecione "Set as Startup Project" e na aba superior da IDE clique em Run.
Caso tenha que gerar Migrations para criar o banco, execute sob a camada de Infrastructure
# EndPoints
Para ver a lista de chamadas REST disponíveis, seus parametros, códigos de resposta HTTP, e tipo de retorno, inicie a aplicação e acesse: https://localhost:44395/swagger/

# Testes
Para executar os testes abra a aba de testes do Visual Studio 2019 e clique Run. Isso fará com que todos os testes de integração implementados sejam executados.


#IMPORTANTE
Caso não queira baixar e executar os códigos você pode apenas acessar: https://osvaldo2109241000.bateaquihost.com.br/swagger/index.html e vazer os testes que quiser.
