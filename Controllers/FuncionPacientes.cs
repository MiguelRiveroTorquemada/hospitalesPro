using Microsoft.AspNetCore.Mvc;
using Data;
namespace Clases;

//Creamos el controlador
[ApiController]
[Route ("[Controller]")]

public class PacientesController:ControllerBase
{
    private readonly DataContext _context;

    public PacientesController(DataContext dataContext)

    {
        _context = dataContext;
    }

    [HttpGet ]
    public ActionResult<List<Pacientes>> Get()
    {
        List<Pacientes> pacientes =_context.Paciente.OrderByDescending(x => x.nombre).ToList();
        //Revisar orden
       
        return   Ok(pacientes);
        
    }
    [HttpGet]
    [Route("fechaIngreso")] 
    public ActionResult<Pacientes> Get(DateTime fecha_ingreso)
    {
  List<Pacientes> pacientes =_context.Paciente.Where(x=> x.fecha_ingreso==fecha_ingreso).ToList();
        //buscar por nombre   
        return pacientes == null? NotFound()
            : Ok(pacientes);
    }
    [HttpGet]
    [Route("nombre")] 
    public ActionResult<Pacientes> Get(string nombre)
    {
  List<Pacientes> pacientes =_context.Paciente.Where(x=> x.nombre.Contains(nombre)).OrderBy(x=>x.nombre).ToList();
        //buscar por nombre   
        return pacientes == null? NotFound()
            : Ok(pacientes);
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Pacientes> Get(int id)
    {
    Pacientes pacientes = _context.Paciente.Find(id);
        return pacientes == null? NotFound()
            : Ok(pacientes);
    }
          [HttpGet]
    [Route("baja")] 
    public ActionResult<Pacientes> Get1(bool alta_baja)
    {
  List<Pacientes> paciente =_context.Paciente.Where(x=> x.alta_baja==alta_baja).ToList();
        //buscar por nombre   
        return paciente == null? NotFound()
            : Ok(paciente);
    }

    [HttpPost]
    public ActionResult<Pacientes> Post([FromBody] Pacientes pacientes)
    {
        Pacientes existingPacienteItems= _context.Paciente.Find(pacientes.id);
        if (existingPacienteItems != null)
        {
            return Conflict("Ya existe este paciente");
        }
        _context.Paciente.Add(pacientes);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + pacientes.id;
        return Created(resourceUrl, pacientes);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Pacientes> Update([FromBody] Pacientes pacientes, int id)
    {
        Pacientes pacienteToUpdate = _context.Paciente.Find(id);
        if (pacienteToUpdate == null)
        {
            return NotFound("Paciente no encontrado");
        }
        pacienteToUpdate.nombre=pacientes.nombre;
        pacienteToUpdate.apellido=pacientes.apellido;
        pacienteToUpdate.alta_baja=pacientes.alta_baja;
        pacienteToUpdate.fecha_ingreso=pacientes.fecha_ingreso;
        pacienteToUpdate.peso=pacientes.peso;
        pacienteToUpdate.edad=pacientes.edad;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + pacienteToUpdate.nombre;
 
        return Created(resourceUrl, pacienteToUpdate);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        Pacientes pacienteToDelete = _context.Paciente.Find(id);
        if (pacienteToDelete == null)
        {
            return NotFound("Paciente no encontrado");
        }
        _context.Paciente.Remove(pacienteToDelete);
        _context.SaveChanges();
        if (pacienteToDelete == null)
        {
            return NotFound();
        }
        return Ok(pacienteToDelete);
    }
}