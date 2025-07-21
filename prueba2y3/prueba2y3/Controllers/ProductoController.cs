using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prueba2y3.Models;
using prueba2y3.Services;

namespace prueba2y3.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase
{
    public DALService ctx { get; set; }
    
    public ProductoController()
    {
        ctx = new DALService();
    }
    
    
    [HttpGet("/api/productos")]
    public async Task<ActionResult<List<Producto>>> GetProductos()
    {
        try
        {
            var json = ctx.GetAll();
            var resp = JsonConvert.DeserializeObject<List<Producto>>(json);
            return resp;
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
    
    [HttpGet("/api/productos/{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        try
        {
            var json = ctx.GetByID(id);
            var resp = JsonConvert.DeserializeObject<List<Producto>>(json);
            return resp.FirstOrDefault() ?? new Producto();
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
    
    [HttpPost("/api/productos")]
    public async Task<ActionResult<string>> Create([FromBody] Producto producto)
    {
        try
        {
            if(producto == null) return BadRequest("El producto no puede ser invalido");
            var json = ctx.Create(producto);
            return json;
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
    
    [HttpPut("/api/productos/{id}")]
    public async Task<ActionResult<string>> Edit(int id, [FromBody] Producto producto)
    {
        try
        {
            if(producto == null) return BadRequest("El producto no puede ser invalido");
            producto.Id = id;
            var json = ctx.Update(producto);
            return json;
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
    
    [HttpDelete("/api/productos/{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
        try
        {
            var json = ctx.Delete(id);
            return json;
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
}