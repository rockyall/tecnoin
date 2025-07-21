using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prueba1.Models;
using prueba1.Services;

namespace prueba1.Controllers;

public class ClienteController : Controller
{
    public DALService dalService { get; set; }
    public ClienteController()
    {
        dalService = new DALService();
    }
    // GET
    public IActionResult Index()
    {
        try
        {
            dalService = new DALService();
            var rawData = dalService.GetAll();
            var clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(rawData);

            return View(clientes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(ClienteViewModel cliente)   
    {
        if(cliente == null) return BadRequest();
        if (ModelState.IsValid)
        {
            var rawData = dalService.Create(cliente);
            return RedirectToAction("Index");
        }
        
        return View(cliente);
    }

    public IActionResult Edit(int clienteid)
    {
        try
        {
            var rawData = dalService.GetByID(clienteid);
            var clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(rawData);

            return View(clientes.FirstOrDefault());
        }
        catch (Exception e)
        {
            return View();
        }
    }
    
    [HttpPost]
    public IActionResult Edit(int id, ClienteViewModel cliente)
    {
        try
        {
            if(cliente == null) return BadRequest();
            if (ModelState.IsValid)
            {
                dalService.Update(id, cliente);
                return RedirectToAction("Index");
            }
            
            return View(cliente);
        }
        catch (Exception e)
        {
            return View();
        }
    }

    public IActionResult Delete(int clienteid)
    {
        if (clienteid == 0)
        {
            ViewData["DeletionMessageResponse"] = TempData["DeletionMessageResponse"];
        }
        
        var rawData = dalService.GetByID(clienteid);
        var clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(rawData);

        return View(clientes.FirstOrDefault() ?? new ClienteViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id, ClienteViewModel cliente)
    {
        try
        {
            var status = dalService.Delete(id);
            TempData["DeletionMessageResponse"] = status;
            return RedirectToAction("Delete");
        }
        catch (Exception e)
        {
            TempData["DeletionMessageResponse"] = "Ocurrió un error al borrar el cliente.";
            return RedirectToAction("Delete");
        }
    }
}