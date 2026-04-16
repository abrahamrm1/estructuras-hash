// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

partial class Program
{
    static void Main(string[] args)
    {
        VaccineRegistry registry = new VaccineRegistry();

        while (true)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Registrar persona");
            Console.WriteLine("2. Agregar vacuna");
            Console.WriteLine("3. Buscar persona por CUI");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Ingrese CUI: ");
                    string cui = Console.ReadLine();
                    Console.Write("Ingrese nombre: ");
                    string name = Console.ReadLine();
                    registry.AddPerson(cui, name);
                    break;
                case "2":
                    Console.Write("Ingrese CUI: ");
                    cui = Console.ReadLine();
                    Console.Write("Ingrese nombre de la vacuna: ");
                    string vaccineName = Console.ReadLine();
                    Console.Write("Ingrese fecha (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    {
                        registry.AddVaccine(cui, vaccineName, date);
                    }
                    else
                    {
                        Console.WriteLine("Fecha inválida.");
                    }
                    break;
                case "3":
                    Console.Write("Ingrese CUI: ");
                    cui = Console.ReadLine();
                    registry.SearchPerson(cui);
                    break;
                case "4":
                    registry.SaveToFile();
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}
