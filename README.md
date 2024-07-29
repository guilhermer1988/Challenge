## Como iniciar esse projeto localmente.

Para iniciar o projeto em mode de desenvolvimento, primeiramente é necessario ter o Docker instalado em seu computador. 
Segue o link da documentação para instalação em sua máquina https://www.docker.com/ 

Após ter instalado o Docker, é necessário realizar o clone deste projeto.
Realizado o clone do projeto basta entrar na pasta e digitar o comando abaixo:

```bash
docker compose up --build -d
```

Esse comando irá levantar um container com o banco de dados e as Apis e ferramentas desenvolvida neste projeto para acesso localmente.

Com o container iniciado as seguintes url's serão disponibilizadas:
* http://localhost:49160/swagger/index.html - TaskManager

Existem 2 usuarios cadastrados com os UserNames a seguir:
'usuarioAdmin' que contém o privilegio de "gerente"
'usuario' que NÃO contém o privilegio de "gerente"
utilize esses userNames para inserir, atualizar e remover registros 


## Fase 2: Refinamento 
> Qual o público alvo da aplicação?
> Qual o volume de dados esperado, incluindo histórico? há preocupação com performance e escalabilidade?
> O sistema irá integrar com outros sistemas?
> Como irá criar outros usuarios?
> Como devem ser tratados exclusão em cascata para histórico e comentários?
> Os relatórios devem ser entregues em formato específico como pdf, excel?
> Os relatórios devem ser gerados em tempo real ou poderá ser criado uma rotina para entrega via email por exemplo?

## Fase 3: Final
> Se o projeto crescer, pode ser benéfico adotar uma arquitetura de microserviços. Isso permitirá a escalabilidade independente de diferentes partes do sistema e facilita a manutenção.
> Visto que relatórios sempre envolvem uma grande massa de dados, seria interessante a implementação de um serviço orientado a eventos para gerar e entregar estes relatórios e não onerar a aplicação.
> Os testes unitários é um ponto que deve ser melhorado.
> Testes automatizados para incluir integração utilizando ferramentas de CI/CD.
> Implementação de uma interface gráfica amigável e as mensagens de feedback.
> Implementação de gerenciamento de erros e recuperação de falhas.