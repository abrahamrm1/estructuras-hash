using System;
using System.Collections.Generic;
using System.IO;

class Programa
{
    private const int TAMANO_TABLA = 100;
    private List<Persona>[] tablaHash = new List<Persona>[TAMANO_TABLA];

    public Programa()
    {
        for (int i = 0; i < TAMANO_TABLA; i++)
        {
            tablaHash[i] = new List<Persona>();
        }
        CargarDatos();
    }

    private int Hash(string cui)
    {
        return int.Parse(cui) % TAMANO_TABLA;
    }

    public void AgregarPersona(Persona persona)
    {
        int indice = Hash(persona.CUI);
        tablaHash[indice].Add(persona);
        GuardarDatos();
    }

    public Persona BuscarPersona(string cui)
    {
        int indice = Hash(cui);
        foreach (var persona in tablaHash[indice])
        {
            if (persona.CUI == cui)
            {
                return persona;
            }
        }
        return null;
    }

    private void GuardarDatos()
    {
        using (StreamWriter sw = new StreamWriter("registros.txt"))
        {
            foreach (var lista in tablaHash)
            {
                foreach (var persona in lista)
                {
                    sw.WriteLine($"{persona.CUI};{string.Join(",", persona.RegistrosVacunas)}");
                }
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
