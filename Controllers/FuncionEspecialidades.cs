using Microsoft.AspNetCore.Mvc;
using Data;
namespace Clases;

//Creamos el controlador
[ApiController]
[Route ("[Controller]")]

public class EspecialidadesController:ControllerBase
{
    private readonly DataContext _context;

    public EspecialidadesController(DataContext dataContext)

    {
        _context = dataContext;
    }

    [HttpGet ]
    public ActionResult<List<Especialidades>> Get()
    {
        List<Especialidades> especialidades =_context.Especialidad.OrderByDescending(x => x.nombreEspecialidad).ToList();
        //Revisar orden
       
        return   Ok(especialidades);
        
    }

    [HttpGet]
    [Route("nombreEspecialidad")] 
    public ActionResult<Especialidades> Get(string nombreEspecialidad)
    {
  List<Especialidades> especialidades =_context.Especialidad.Where(x=> x.nombreEspecialidad.Contains(nombreEspecialidad)).OrderByDescending(x=>x.nombreEspecialidad).ToList();
        //buscar por nombre   
        return especialidades == null? NotFound()
            : Ok(especialidades);
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Especialidades> Get(int id)
    {
    Especialidades especialidades = _context.Especialidad.Find(id);
        return especialidades == null? NotFound()
            : Ok(especialidades);
    }

    [HttpPost]
    public ActionResult<Especialidades> Post([FromBody] Especialidades especialidades)
    {
        Especialidades existingEspecialidadItems= _context.Especialidad.Find(especialidades.id);
        if (existingEspecialidadItems != null)
        {
            return Conflict("Ya existe esta especialidad");
        }
        _context.Especialidad.Add(especialidades);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + especialidades.id;
        return Created(resourceUrl, especialidades);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Especialidades> Update([FromBody] Especialidades especialidades, int id)
    {
        Especialidades especialidadToUpdate = _context.Especialidad.Find(id);
        if (especialidadToUpdate == null)
        {
            return NotFound("Paciente no encontrado");
        }
        especialidadToUpdate.nombreEspecialidad=especialidades.nombreEspecialidad;
        especialidadToUpdate.descripcion=especialidades.descripcion;
        especialidadToUpdate.rotativa=especialidades.rotativa;
        especialidadToUpdate.inicioEspecialidad=especialidades.inicioEspecialidad;
        especialidadToUpdate.salario=especialidades.salario;
        especialidadToUpdate.afiliados=especialidades.afiliados;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + especialidadToUpdate.nombreEspecialidad;
 
        return Created(resourceUrl, especialidadToUpdate);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        Especialidades especialidadToDelete = _context.Especialidad.Find(id);
        if (especialidadToDelete == null)
        {
            return NotFound("Especialidad no encontrada");
        }
        _context.Especialidad.Remove(especialidadToDelete);
        _context.SaveChanges();
        if (especialidadToDelete == null)
        {
            return NotFound();
        }
        return Ok(especialidadToDelete);
    }
}