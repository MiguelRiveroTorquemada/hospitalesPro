using Microsoft.AspNetCore.Mvc;
using Data;
namespace Clases;

//Creamos el controlador
[ApiController]
[Route ("[Controller]")]

public class TrabajadoresController:ControllerBase
{
    private readonly DataContext _context;

    public TrabajadoresController(DataContext dataContext)

    {
        _context = dataContext;
    }

    [HttpGet ]
    public ActionResult<List<Trabajadores>> Get()
    {
        List<Trabajadores> trabajadores =_context.Trabajadores.OrderByDescending(x => x.nombre).ToList();
        //Revisar orden
       
        return   Ok(trabajadores);
    }

    [HttpGet]
    [Route("nombre")] 
    public ActionResult<Trabajadores> Get(string nombre)
    {
  List<Trabajadores> trabajadores =_context.Trabajadores.Where(x=> x.nombre.Contains(nombre)).OrderBy(x=>x.nombre).ToList();
        //buscar por nombre   
        return trabajadores == null? NotFound()
            : Ok(trabajadores);
    }
        [HttpGet]
    [Route("baja")] 
    public ActionResult<Trabajadores> Get1(bool baja)
    {
  List<Trabajadores> trabajadores =_context.Trabajadores.Where(x=> x.baja==baja).ToList();
        //buscar por nombre   
        return trabajadores == null? NotFound()
            : Ok(trabajadores);
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Trabajadores> Get(int id)
    {
    Trabajadores trabajadores = _context.Trabajadores.Find(id);
        return trabajadores == null? NotFound()
            : Ok(trabajadores);
    }

    [HttpPost]
    public ActionResult<Trabajadores> Post([FromBody] Trabajadores trabajadores)
    {
        Trabajadores existingTrabajadorItems= _context.Trabajadores.Find(trabajadores.id);
        if (existingTrabajadorItems != null)
        {
            return Conflict("Ya existe este trabajador");
        }
        _context.Trabajadores.Add(trabajadores);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + trabajadores.id;
        return Created(resourceUrl, trabajadores);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Trabajadores> Update([FromBody] Trabajadores trabajadores, int id)
    {
        Trabajadores trabajadoresToUpdate = _context.Trabajadores.Find(id);
        if (trabajadoresToUpdate == null)
        {
            return NotFound("Paciente no encontrado");
        }
        trabajadoresToUpdate.nombre=trabajadores.nombre;
        trabajadoresToUpdate.email=trabajadores.email;
        trabajadoresToUpdate.baja=trabajadores.baja;
        trabajadoresToUpdate.inicioTrabajo=trabajadores.inicioTrabajo;
        trabajadoresToUpdate.salario=trabajadores.salario;
        trabajadoresToUpdate.edad=trabajadores.edad;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + trabajadoresToUpdate.nombre;
 
        return Created(resourceUrl, trabajadoresToUpdate);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        Trabajadores trabajadorToDelete = _context.Trabajadores.Find(id);
        if (trabajadorToDelete == null)
        {
            return NotFound("Trabajador no encontrado");
        }
        _context.Trabajadores.Remove(trabajadorToDelete);
        _context.SaveChanges();
        if (trabajadorToDelete == null)
        {
            return NotFound();
        }
        return Ok(trabajadorToDelete);
    }
}