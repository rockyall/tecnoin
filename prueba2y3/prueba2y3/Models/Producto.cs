namespace prueba2y3.Models;

public class Producto
{
    public int? Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public double Precio { get; set; }
    public int CantidadEnStock { get; set; }
}