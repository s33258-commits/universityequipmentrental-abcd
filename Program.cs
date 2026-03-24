using UniversityEquipmentRental.Models;
using UniversityEquipmentRental.Services;

List<User> users = new();
List<Equipment> equipment = new();
List<Loan> loans = new();

PenaltyCalculator penaltyCalculator = new();
RentalService rentalService = new(users, equipment, loans, penaltyCalculator);
ReportService reportService = new(equipment, loans, users);


var student = new Student("Uladzimir", "Kuzmich", "s33258");
var employee = new Employee("Adam", "Smyk", "s12345");

Console.WriteLine("ADDING USERS...");
Console.WriteLine(rentalService.AddUser(student).Message);
Console.WriteLine(rentalService.AddUser(employee).Message);
var laptop = new Laptop("MacBook NEO", "Apple", 16, "NEO");
var camera = new Camera("Sony Alpha t9", "Sony", 24, "Zoom Lens");
var projector = new Projector("Epson X1", "Epson", "1920x1080", 3500);
var laptop2 = new Laptop("MacBook Air", "Apple", 8, "M2");

Console.WriteLine();
Console.WriteLine("ADDING EQUIPMENT...");
Console.WriteLine(rentalService.AddEquipment(laptop).Message);
Console.WriteLine(rentalService.AddEquipment(camera).Message);
Console.WriteLine(rentalService.AddEquipment(projector).Message);
Console.WriteLine(rentalService.AddEquipment(laptop2).Message);


Console.WriteLine();
Console.WriteLine("ALL EQUIPMENT:");
foreach (var item in equipment)
{
    Console.WriteLine(item);
}

Console.WriteLine();
Console.WriteLine("AVAILABLE EQUIPMENT:");
foreach (var item in equipment.Where(e => e.Status == UniversityEquipmentRental.Enums.EquipmentStatus.Available))
{
    Console.WriteLine(item);
}


Console.WriteLine();
Console.WriteLine("BORROWING EQUIPMENT...");
Console.WriteLine(rentalService.BorrowEquipment(student.Id, laptop.Id, 7).Message);
Console.WriteLine(rentalService.BorrowEquipment(student.Id, camera.Id, 5).Message);


Console.WriteLine();
Console.WriteLine("INVALID BORROW ATTEMPT (LIMIT EXCEEDED):");
Console.WriteLine(rentalService.BorrowEquipment(student.Id, projector.Id, 3).Message);


projector.Status = UniversityEquipmentRental.Enums.EquipmentStatus.Unavailable;

Console.WriteLine();
Console.WriteLine("INVALID BORROW ATTEMPT (UNAVAILABLE EQUIPMENT):");
Console.WriteLine(rentalService.BorrowEquipment(employee.Id, projector.Id, 3).Message);


Console.WriteLine();
Console.WriteLine("ACTIVE LOANS FOR STUDENT:");
foreach (var loan in loans.Where(l => l.User.Id == student.Id && !l.IsReturned))
{
    Console.WriteLine(loan);
}


Console.WriteLine();
Console.WriteLine("RETURN ON TIME:");
Console.WriteLine(rentalService.ReturnEquipment(1).Message);


Console.WriteLine();
Console.WriteLine("CREATING OVERDUE LOAN FOR DEMO...");
Console.WriteLine(rentalService.BorrowEquipment(employee.Id, laptop2.Id, 1).Message);


var overdueLoan = loans.Last();
overdueLoan.BorrowDate = DateTime.Now.AddDays(-10);
overdueLoan.DueDate = DateTime.Now.AddDays(-5);


Console.WriteLine();
Console.WriteLine("OVERDUE LOANS:");
foreach (var loan in loans.Where(l => !l.IsReturned && l.DueDate < DateTime.Now))
{
    Console.WriteLine(loan);
}


Console.WriteLine();
Console.WriteLine("LATE RETURN WITH PENALTY:");
Console.WriteLine(rentalService.ReturnEquipment(overdueLoan.Id).Message);


Console.WriteLine();
Console.WriteLine("FINAL REPORT:");
Console.WriteLine(reportService.GenerateSummaryReport());
