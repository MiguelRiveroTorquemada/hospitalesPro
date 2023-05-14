namespace Clases;

public class Trabajadores
{
    public int id { get;set; }
    public String nombre { get;set; }
    public String email { get;set; }
    public bool baja { get; set; }
    public DateTime inicioTrabajo { get;set; }
    public decimal salario { get;set; }
    public int edad { get;set; }

public virtual ICollection<Especialidades>?Especialidades{get;set;}
    
 
    public String Form()
    {
        return nombre + " " + email + " " + baja.ToString() + " " + inicioTrabajo.ToString() + " " + salario.ToString() + " " + edad.ToString();
    }
}