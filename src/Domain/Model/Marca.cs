namespace Domain.Model;

public class Marca
{
    public long MarcaId { get; set; }
    public string Nombre { get; set; } = null!;

    // 1 Marca "Tiene" 1..* Vehiculo
    public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
