using UniversityEquipmentRental.Models;
using UniversityEquipmentRental.Services;

List<User> users = new();
List<Equipment> equipment = new();
List<Loan> loans = new();

PenaltyCalculator penaltyCalculator = new();
RentalService rentalService = new(users, equipment, loans, penaltyCalculator);
ReportService reportService = new(equipment, loans, users);

// DANE STARTOWE
SeedData();

RunMenu();

void RunMenu()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("===== MENU =====");
        Console.WriteLine("1. Pokaż cały sprzęt");
        Console.WriteLine("2. Pokaż dostępny sprzęt");
        Console.WriteLine("3. Dodaj użytkownika");
        Console.WriteLine("4. Dodaj sprzęt");
        Console.WriteLine("5. Wypożycz sprzęt");
        Console.WriteLine("6. Zwróć sprzęt");
        Console.WriteLine("7. Pokaż aktywne wypożyczenia użytkownika");
        Console.WriteLine("8. Pokaż przeterminowane wypożyczenia");
        Console.WriteLine("9. Raport");
        Console.WriteLine("0. Wyjście");
        Console.Write("Wybierz opcję: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowAllEquipment();
                break;
            case "2":
                ShowAvailableEquipment();
                break;
            case "3":
                AddUserMenu();
                break;
            case "4":
                AddEquipmentMenu();
                break;
            case "5":
                BorrowEquipmentMenu();
                break;
            case "6":
                ReturnEquipmentMenu();
                break;
            case "7":
                ShowActiveLoansForUserMenu();
                break;
            case "8":
                ShowOverdueLoans();
                break;
            case "9":
                ShowReport();
                break;
            case "0":
                Console.WriteLine("Koniec programu.");
                return;
            default:
                Console.WriteLine("Nieprawidłowa opcja.");
                break;
        }
    }
}

void SeedData()
{
    var student = new Student("Uladzimir", "Kuzmich", "s33258");
    var employee = new Employee("Adam", "Smyk", "s12345");

    rentalService.AddUser(student);
    rentalService.AddUser(employee);

    rentalService.AddEquipment(new Laptop("MacBook Pro", "Apple", 16, "M3"));
    rentalService.AddEquipment(new Camera("Sony Alpha A7 III", "Sony", 24, "Zoom Lens"));
    rentalService.AddEquipment(new Projector("Epson X1", "Epson", "1920x1080", 3500));
    rentalService.AddEquipment(new Laptop("MacBook Air", "Apple", 8, "M2"));
}

void ShowAllEquipment()
{
    Console.WriteLine();
    Console.WriteLine("===== CAŁY SPRZĘT =====");

    if (!equipment.Any())
    {
        Console.WriteLine("Brak sprzętu.");
        return;
    }

    foreach (var item in equipment)
    {
        Console.WriteLine(item);
    }
}

void ShowAvailableEquipment()
{
    Console.WriteLine();
    Console.WriteLine("===== DOSTĘPNY SPRZĘT =====");

    var available = equipment.Where(e => e.IsAvailable).ToList();

    if (!available.Any())
    {
        Console.WriteLine("Brak dostępnego sprzętu.");
        return;
    }

    foreach (var item in available)
    {
        Console.WriteLine(item);
    }
}

void AddUserMenu()
{
    Console.WriteLine();
    Console.WriteLine("===== DODAJ UŻYTKOWNIKA =====");
    Console.WriteLine("1. Student");
    Console.WriteLine("2. Employee");
    Console.Write("Wybierz typ użytkownika: ");
    string? userType = Console.ReadLine();

    Console.Write("Imię: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Nazwisko: ");
    string lastName = Console.ReadLine() ?? "";

    Console.Write("Numer/identyfikator: ");
    string code = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(code))
    {
        Console.WriteLine("Niepoprawne dane.");
        return;
    }

    User? user = userType switch
    {
        "1" => new Student(firstName, lastName, code),
        "2" => new Employee(firstName, lastName, code),
        _ => null
    };

    if (user == null)
    {
        Console.WriteLine("Niepoprawny typ użytkownika.");
        return;
    }

    Console.WriteLine(rentalService.AddUser(user).Message);
}

void AddEquipmentMenu()
{
    Console.WriteLine();
    Console.WriteLine("===== DODAJ SPRZĘT =====");
    Console.WriteLine("1. Laptop");
    Console.WriteLine("2. Camera");
    Console.WriteLine("3. Projector");
    Console.Write("Wybierz typ sprzętu: ");
    string? equipmentType = Console.ReadLine();

    Console.Write("Nazwa: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Marka: ");
    string brand = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(brand))
    {
        Console.WriteLine("Niepoprawne dane.");
        return;
    }

    Equipment? newEquipment = null;

    switch (equipmentType)
    {
        case "1":
            Console.Write("RAM (GB): ");
            if (!int.TryParse(Console.ReadLine(), out int ram))
            {
                Console.WriteLine("Niepoprawny RAM.");
                return;
            }

            Console.Write("CPU: ");
            string cpu = Console.ReadLine() ?? "";
            newEquipment = new Laptop(name, brand, ram, cpu);
            break;

        case "2":
            Console.Write("Megapiksele: ");
            if (!int.TryParse(Console.ReadLine(), out int megapixels))
            {
                Console.WriteLine("Niepoprawna liczba megapikseli.");
                return;
            }

            Console.Write("Obiektyw: ");
            string lens = Console.ReadLine() ?? "";
            newEquipment = new Camera(name, brand, megapixels, lens);
            break;

        case "3":
            Console.Write("Rozdzielczość: ");
            string resolution = Console.ReadLine() ?? "";

            Console.Write("Jasność: ");
            if (!int.TryParse(Console.ReadLine(), out int brightness))
            {
                Console.WriteLine("Niepoprawna jasność.");
                return;
            }

            newEquipment = new Projector(name, brand, resolution, brightness);
            break;

        default:
            Console.WriteLine("Niepoprawny typ sprzętu.");
            return;
    }

    Console.WriteLine(rentalService.AddEquipment(newEquipment).Message);
}

void BorrowEquipmentMenu()
{
    Console.WriteLine();
    Console.WriteLine("===== WYPOŻYCZ SPRZĘT =====");

    if (!users.Any())
    {
        Console.WriteLine("Brak użytkowników.");
        return;
    }

    if (!equipment.Any())
    {
        Console.WriteLine("Brak sprzętu.");
        return;
    }

    Console.WriteLine("Użytkownicy:");
    foreach (var user in users)
    {
        Console.WriteLine($"ID: {user.Id}, {user.FirstName} {user.LastName}");
    }

    Console.WriteLine();
    Console.WriteLine("Sprzęt:");
    foreach (var item in equipment)
    {
        Console.WriteLine(item);
    }

    Console.Write("Podaj ID użytkownika: ");
    if (!int.TryParse(Console.ReadLine(), out int userId))
    {
        Console.WriteLine("Niepoprawne ID użytkownika.");
        return;
    }

    Console.Write("Podaj ID sprzętu: ");
    if (!int.TryParse(Console.ReadLine(), out int equipmentId))
    {
        Console.WriteLine("Niepoprawne ID sprzętu.");
        return;
    }

    Console.Write("Podaj liczbę dni wypożyczenia: ");
    if (!int.TryParse(Console.ReadLine(), out int days) || days <= 0)
    {
        Console.WriteLine("Niepoprawna liczba dni.");
        return;
    }

    Console.WriteLine(rentalService.BorrowEquipment(userId, equipmentId, days).Message);
}

void ReturnEquipmentMenu()
{
    Console.WriteLine();
    Console.WriteLine("===== ZWRÓĆ SPRZĘT =====");

    var activeLoans = loans.Where(l => l.IsActive).ToList();

    if (!activeLoans.Any())
    {
        Console.WriteLine("Brak aktywnych wypożyczeń.");
        return;
    }

    foreach (var loan in activeLoans)
    {
        Console.WriteLine(loan);
    }

    Console.Write("Podaj ID wypożyczenia do zwrotu: ");
    if (!int.TryParse(Console.ReadLine(), out int loanId))
    {
        Console.WriteLine("Niepoprawne ID wypożyczenia.");
        return;
    }

    Console.WriteLine(rentalService.ReturnEquipment(loanId).Message);
}

void ShowActiveLoansForUserMenu()
{
    Console.WriteLine();
    Console.WriteLine("===== AKTYWNE WYPOŻYCZENIA UŻYTKOWNIKA =====");

    if (!users.Any())
    {
        Console.WriteLine("Brak użytkowników.");
        return;
    }

    foreach (var user in users)
    {
        Console.WriteLine($"ID: {user.Id}, {user.FirstName} {user.LastName}");
    }

    Console.Write("Podaj ID użytkownika: ");
    if (!int.TryParse(Console.ReadLine(), out int userId))
    {
        Console.WriteLine("Niepoprawne ID użytkownika.");
        return;
    }

    var userLoans = loans.Where(l => l.Borrower.Id == userId && l.IsActive).ToList();

    if (!userLoans.Any())
    {
        Console.WriteLine("Brak aktywnych wypożyczeń dla tego użytkownika.");
        return;
    }

    foreach (var loan in userLoans)
    {
        Console.WriteLine(loan);
    }
}

void ShowOverdueLoans()
{
    Console.WriteLine();
    Console.WriteLine("===== PRZETERMINOWANE WYPOŻYCZENIA =====");

    var overdueLoans = loans.Where(l => l.IsOverdue).ToList();

    if (!overdueLoans.Any())
    {
        Console.WriteLine("Brak przeterminowanych wypożyczeń.");
        return;
    }

    foreach (var loan in overdueLoans)
    {
        Console.WriteLine(loan);
    }
}

void ShowReport()
{
    Console.WriteLine();
    Console.WriteLine(reportService.GenerateSummaryReport());
}