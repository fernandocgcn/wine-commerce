# Wine Commerce

Exemplo de uma API RESTful para uma loja de vinhos, que ao longo dos anos, guardou dados de seus clientes e um histórico de compras (disponibilizados também por uma API)  
Funcionalidades:
* 1 - Liste os clientes ordenados pelo maior valor total em compras.
* 2 - Mostre o cliente com maior compra única no último ano (2016).
* 3 - Liste os clientes mais fiéis.
* 4 - Recomende um vinho para um determinado cliente a partir do histórico de compras.

![](/misc/ClassModel.png)

![](/misc/ComponentDiagram.png)

## Tecnologias Utilizadas

### Back-End (C# - .NET Core)

* src/WFDomain - Biblioteca do Domínio, Lógica do Negócio e Modelo de Dados do Projeto (baseado nos JSONs retornados)

### Front-End (C# - .NET Core - API RESTful)

* src/WCApiRest - API RESTful, utilizando os padrões "MVC" e "Dependency Injection" para Inversão de Controle

## Screenshots da Aplicação

![](/misc/screenshots/01.png)
![](/misc/screenshots/02.png)
![](/misc/screenshots/03.png)
![](/misc/screenshots/04.png)
