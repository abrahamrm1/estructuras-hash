using System;
using System.Collections.Generic;
using System.IO;

class Programa
{
    private Dictionary<string, Persona> personas = new Dictionary<string, Persona>();

    public Programa()
    {
        CargarDatos();
    }

    public void AgregarPersona(Persona persona)
    {
        personas[persona.CUI] = persona;
        GuardarDatos();
    }

    public Persona BuscarPersona(string cui)
    {
        if (personas.TryGetValue(cui, out var persona))
        {
            return persona;
        }
        return null;
    }

    private void GuardarDatos()
    {
        using (StreamWriter sw = new StreamWriter("registros.txt"))
        {
            foreach (var persona in personas.Values)
            {
                sw.WriteLine($"{persona.CUI};{string.Join(",", persona.RegistrosVacunas)}");
            }
        }
    }

    private void CargarDatos()
    {
        if (File.Exists("registros.txt"))
        {
            var lineas = File.ReadAllLines("registros.txt");
            foreach (var linea in lineas)
            {
                var partes = linea.Split(';');
                var persona = new Persona(partes[0]);
                if (partes.Length > 1)
                {
                    persona.RegistrosVacunas.AddRange(partes[1].Split(','));
                }
                AgregarPersona(persona);
            }
        }
    }
}
