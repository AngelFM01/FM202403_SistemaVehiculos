namespace Domain.Model;

public class Venta
{
    public long VentaId { get; set; }
    public long VehiculoId { get; set; }
    public double TotalVenta { get; set; }
    public int Cantidad { get; set; }

    // 1 Venta "contiene" 1 Vehiculo
    public Vehiculo Vehiculo { get; set; } = null!;
}
