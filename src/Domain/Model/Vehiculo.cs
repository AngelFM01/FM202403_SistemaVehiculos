namespace Domain.Model;

public class Vehiculo
{
    public long VehiculoId { get; set; }
    public long MarcaId { get; set; }
    public string Modelo { get; set; } = null!;
    public int Anio { get; set; }
    public int CantidadPuertas { get; set; }

    // Cada Vehiculo pertenece a 1 Marca
    public Marca Marca { get; set; } = null!;

    // 1 Vehiculo "es contenido en" 1 Venta
    public Venta? Venta { get; set; }
}
