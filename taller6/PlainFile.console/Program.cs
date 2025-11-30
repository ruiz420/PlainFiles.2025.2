using PlainFiles.Core;

public class Program
{
    private static void Main(string[] args)
    {
        UserManager UM = new UserManager();
        string? user = Login(UM);

        if (user == null)
        {
            Console.WriteLine("Access denied.");
            return;
        }

        PersonManager PM = new PersonManager(user);
        Menu(PM);
    }

    private static string? Login(UserManager UM)
    {
        int attempts = 0;
        string username = "";

        while (attempts < 3)
        {
            Console.Write("User: ");
            username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            string result = UM.ValidateLogin(username, password);

            if (result == "ok")
            {
                Console.WriteLine("\nLogin successful.\n");
                return username;
            }

            if (result == "blocked")
            {
                Console.WriteLine("User is blocked.");
                break;
            }

            attempts++;
            Console.WriteLine("Invalid credentials.\n");
        }

        Console.WriteLine("Too many attempts. User blocked.");
        UM.BlockUser(username);

        return null;
    }

    private static void Menu(PersonManager PM)
    {
        string option;

        do
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine("1. Mostrar personas");
            Console.WriteLine("2. Agregar persona");
            Console.WriteLine("3. Guardar cambios");
            Console.WriteLine("4. Editar persona");
            Console.WriteLine("5. Eliminar persona");
            Console.WriteLine("6. Reporte por ciudad");
            Console.WriteLine("0. Salir");
            Console.WriteLine("===============================");


            Console.Write("Option: ");
            option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    PM.ShowPeople();
                    break;

                case "2":
                    AddPerson(PM);
                    break;

                case "3":
                    PM.SavePeople();
                    Console.WriteLine("Changes saved.");
                    break;

                case "4":
                    EditPerson(PM);
                    break;

                case "5":
                    DeletePerson(PM);
                    break;

                case "6":
                    PM.ReportByCity();
                    break;

                case "0":
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        while (option != "0");
    }

    private static void AddPerson(PersonManager PM)
    {
        try
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            if (PM.FindById(id) != null)
            {
                Console.WriteLine("That ID already exists.");
                return;
            }

            Console.Write("Name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Lastname: ");
            string lastname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastname))
            {
                Console.WriteLine("Lastname cannot be empty.");
                return;
            }

            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            if (!phone.All(char.IsDigit))
            {
                Console.WriteLine("Phone must contain only numbers.");
                return;
            }

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("Balance: ");
            double balance = double.Parse(Console.ReadLine());
            if (balance <= 0)
            {
                Console.WriteLine("Balance must be positive.");
                return;
            }

            Person p = new Person
            {
                Id = id,
                Name = name,
                Lastname = lastname,
                Phone = phone,
                City = city,
                Balance = balance
            };

            PM.AddPerson(p);
            Console.WriteLine("Person added.");
        }
        catch
        {
            Console.WriteLine("Invalid data.");
        }
    }

    private static void EditPerson(PersonManager PM)
    {
        Console.Write("ID to edit: ");
        int id = int.Parse(Console.ReadLine());

        Person? p = PM.FindById(id);

        if (p == null)
        {
            Console.WriteLine("ID not found.");
            return;
        }

        Console.Write($"Name ({p.Name}): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
            p.Name = name;

        Console.Write($"Lastname ({p.Lastname}): ");
        string lastname = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lastname))
            p.Lastname = lastname;

        Console.Write($"Phone ({p.Phone}): ");
        string phone = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(phone))
        {
            if (!phone.All(char.IsDigit))
            {
                Console.WriteLine("Phone must contain only numbers.");
                return;
            }
            p.Phone = phone;
        }

        Console.Write($"City ({p.City}): ");
        string city = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(city))
            p.City = city;

        Console.Write($"Balance ({p.Balance}): ");
        string bal = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(bal))
        {
            double newBal = double.Parse(bal);
            if (newBal <= 0)
            {
                Console.WriteLine("Balance must be positive.");
                return;
            }
            p.Balance = newBal;
        }

        Console.WriteLine("Data updated.");
    }

    private static void DeletePerson(PersonManager PM)
    {
        Console.Write("ID to delete: ");
        int id = int.Parse(Console.ReadLine());

        Person? p = PM.FindById(id);

        if (p == null)
        {
            Console.WriteLine("ID not found.");
            return;
        }

        Console.WriteLine($"Person: {p.Name} {p.Lastname}");
        Console.Write("Delete? (y/n): ");
        string r = Console.ReadLine();

        if (r.ToLower() == "y")
        {
            PM.DeletePerson(id);
            Console.WriteLine("Deleted.");
        }
    }
}
