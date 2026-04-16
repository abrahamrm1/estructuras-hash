using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Vaccine
{
    public string Name { get; set; }
    public DateTime Date { get; set; }

    public Vaccine(string name, DateTime date)
    {
        Name = name;
        Date = date;
    }
}

public class Person
{
    public string CUI { get; set; }
    public string Name { get; set; }
    public List<Vaccine> Vaccines { get; set; }

    public Person(string cui, string name)
    {
        CUI = cui;
        Name = name;
        Vaccines = new List<Vaccine>();
    }
}

public class VaccineRegistry
{
    private Dictionary<string, Person> registry;
    private string filePath = "vaccines.json";

    public VaccineRegistry()
    {
        registry = new Dictionary<string, Person>();
        LoadFromFile();
    }

    public void AddPerson(string cui, string name)
    {
        if (!registry.ContainsKey(cui))
        {
            registry[cui] = new Person(cui, name);
            Console.WriteLine("Persona registrada exitosamente.");
        }
        else
        {
            Console.WriteLine("La persona con CUI " + cui + " ya existe.");
        }
    }

    public void AddVaccine(string cui, string vaccineName, DateTime date)
    {
        if (registry.ContainsKey(cui))
        {
            registry[cui].Vaccines.Add(new Vaccine(vaccineName, date));
            Console.WriteLine("Vacuna agregada exitosamente.");
        }
        else
        {
            Console.WriteLine("La persona con CUI " + cui + " no existe.");
        }
    }

    public void SearchPerson(string cui)
    {
        if (registry.ContainsKey(cui))
        {
            Person person = registry[cui];
            Console.WriteLine("CUI: " + person.CUI);
            Console.WriteLine("Nombre: " + person.Name);
            Console.WriteLine("Vacunas:");
            if (person.Vaccines.Count == 0)
            {
                Console.WriteLine("  Ninguna vacuna registrada.");
            }
            else
            {
                foreach (var vaccine in person.Vaccines)
                {
                    Console.WriteLine("  " + vaccine.Name + " - " + vaccine.Date.ToShortDateString());
                }
            }
        }
        else
        {
            Console.WriteLine("La persona con CUI " + cui + " no existe.");
        }
    }

    public void SaveToFile()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(registry, options);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar el archivo: " + ex.Message);
        }
    }

    private void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                registry = JsonSerializer.Deserialize<Dictionary<string, Person>>(json) ?? new Dictionary<string, Person>();
            }
            catch
            {
                registry = new Dictionary<string, Person>();
            }
        }
    }
}

partial class Program
{
}