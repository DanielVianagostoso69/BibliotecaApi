using System.Text.Json.Serialization;

namespace BibliotecaApi.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "tipoMaterial")]
[JsonDerivedType(typeof(Livro), "livro")]
[JsonDerivedType(typeof(Revista), "revista")]
public abstract class MaterialBiblioteca
{
    public int Id { get; internal set; }
    public string Titulo { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; }

    public abstract int CalcularPrazo();

    public virtual string Descricao()
    {
        return $"Material: {Titulo} - Ano: {AnoPublicacao}";
    }
}
