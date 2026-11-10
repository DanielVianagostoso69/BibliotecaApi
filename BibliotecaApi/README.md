# Sistema de Gerenciamento de Biblioteca OO

## Integrantes
- Daniel Von Randow Viana.

## Descrição do sistema
Web API para gerenciamento de materiais de uma biblioteca. O projeto demonstra herança, abstração, encapsulamento, polimorfismo, interfaces, Repository Pattern e injeção de dependência com Singleton. Os dados são armazenados em memória por meio de `Dictionary<int, MaterialBiblioteca>`.

## Tecnologias utilizadas
- C#
- .NET 10 / ASP.NET Core Web API
- Swagger (Swashbuckle)
- Visual Studio Code ou Visual Studio

## Como executar
1. Instale o SDK do .NET 10.
2. Abra a pasta `BibliotecaApi` no terminal.
3. Restaure as dependências e execute:

   ```bash
   dotnet restore
   dotnet run
   ```

4. Acesse o Swagger em [https://localhost:7184/swagger](https://localhost:7184/swagger) (ou use a URL/porta exibida no terminal).

> O projeto usa armazenamento em memória. Os dados cadastrados durante a execução são mantidos enquanto a aplicação estiver aberta e voltam aos exemplos iniciais quando ela reiniciar.

## Principais endpoints

| Método | Rota | Função |
|---|---|---|
| GET | `/api/Materiais` | Lista os materiais |
| GET | `/api/Materiais/{id}` | Busca por ID |
| POST | `/api/Materiais/livro` | Cadastra livro |
| POST | `/api/Materiais/revista` | Cadastra revista |
| PUT | `/api/Materiais/livro/{id}` | Atualiza livro |
| PUT | `/api/Materiais/revista/{id}` | Atualiza revista |
| DELETE | `/api/Materiais/{id}` | Exclui material |
| GET | `/api/Materiais/{id}/descricao` | Exibe descrição e prazo polimórfico |
| POST | `/api/Materiais/{id}/emprestar` | Tenta emprestar o material |

## Exemplos de testes

### Listar materiais
```http
GET /api/Materiais
```
O repositório inicia com o livro de ID `1` e a revista de ID `2`.

### Cadastrar livro
```http
POST /api/Materiais/livro
Content-Type: application/json

{
  "titulo": "O Programador Pragmático",
  "anoPublicacao": 1999,
  "autor": "Andrew Hunt",
  "paginas": 352
}
```

### Cadastrar revista
```http
POST /api/Materiais/revista
Content-Type: application/json

{
  "titulo": "Revista de Tecnologia",
  "anoPublicacao": 2026,
  "edicao": 20,
  "mesPublicacao": 11
}
```

### Verificar polimorfismo
- `GET /api/Materiais/1/descricao` deve retornar prazo de 14 dias para o livro.
- `GET /api/Materiais/2/descricao` deve retornar prazo de 7 dias para a revista.

### Verificar interface de empréstimo
- `POST /api/Materiais/1/emprestar` deve retornar `200 OK`.
- `POST /api/Materiais/2/emprestar` deve retornar `400 Bad Request`, pois `Revista` não implementa `IEmprestimo`.

### Exemplos de validação e erros
- `GET /api/Materiais/999` → `404 Not Found`.
- Livro sem título ou com `paginas` igual a `0` → `400 Bad Request`.
- Revista com `edicao` igual a `0` ou `mesPublicacao` igual a `13` → `400 Bad Request`.
- Exclusão de material existente → `204 No Content`.
- Exclusão de ID inexistente → `404 Not Found`.

### Teste de geração de IDs
Cadastre vários materiais e observe os IDs. Exclua o material com o maior ID e cadastre outro: o novo ID continuará crescente e não será repetido durante a execução da aplicação.

## Organização do projeto
```text
BibliotecaApi/
├── Controllers/
│   └── MateriaisController.cs
├── Models/
│   ├── MaterialBiblioteca.cs
│   ├── Livro.cs
│   ├── Revista.cs
│   └── IEmprestimo.cs
├── Repositories/
│   ├── IMaterialRepository.cs
│   └── MaterialRepository.cs
├── Properties/
│   └── launchSettings.json
├── Program.cs
├── BibliotecaApi.csproj
└── README.md
```

## Observações
- `MaterialBiblioteca` é abstrata; o Repository armazena objetos concretos usando o tipo-base.
- `Livro` implementa `IEmprestimo`; `Revista` não implementa.
- O Repository é registrado como Singleton no `Program.cs`.
- O ID é gerado pelo Repository, não deve ser enviado no JSON de cadastro.
- O CORS está configurado com a política `PermitirFrontend`.
