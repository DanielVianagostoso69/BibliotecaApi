using BibliotecaApi.Models;

namespace BibliotecaApi.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly Dictionary<int, MaterialBiblioteca> _materiais = new();
    private int _ultimoIdGerado = 2;

    public MaterialRepository()
    {
        var livro = new Livro
        {
            Id = 1,
            Titulo = "Clean Code",
            AnoPublicacao = 2008,
            Autor = "Robert C. Martin",
            Paginas = 464
        };

        var revista = new Revista
        {
            Id = 2,
            Titulo = "Tecnologia e Inovação",
            AnoPublicacao = 2026,
            Edicao = 15,
            MesPublicacao = 11
        };

        _materiais.Add(livro.Id, livro);
        _materiais.Add(revista.Id, revista);
    }

    public IEnumerable<MaterialBiblioteca> ObterTodos()
    {
        return _materiais.Values;
    }

    public MaterialBiblioteca? ObterPorId(int id)
    {
        return _materiais.TryGetValue(id, out var material) ? material : null;
    }

    public MaterialBiblioteca Adicionar(MaterialBiblioteca material)
    {
        // Mantém a geração crescente mesmo se o último registro for excluído.
        material.Id = Math.Max(GerarNovoId(), _ultimoIdGerado + 1);
        _ultimoIdGerado = material.Id;
        _materiais.Add(material.Id, material);
        return material;
    }

    public bool Atualizar(int id, MaterialBiblioteca material)
    {
        if (!_materiais.ContainsKey(id))
            return false;

        material.Id = id;
        _materiais[id] = material;
        return true;
    }

    public bool Remover(int id)
    {
        return _materiais.Remove(id);
    }

    private int GerarNovoId()
    {
        if (_materiais.Count == 0)
            return 1;

        return _materiais.Keys.Max() + 1;
    }
}
