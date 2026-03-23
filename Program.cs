using UniversityEquipmentRental.Models;
using UniversityEquipmentRental.Services;

List<User> users = new();
List<Equipment> equipment = new();
List<Loan> loans = new();

PenaltyCalculator penaltyCalculator = new();
RentalService rentalService = new(users, equipment, loans, penaltyCalculator);
ReportService reportService = new(equipment, loans, users);

var student = new Student("Uladzimir", "Kuzmich", "s33258");
var employee = new Employee("Adam", "Smyk", "	s12345");

Console.WriteLine(rentalService.AddUser(student).Message);
Console.WriteLine(rentalService.AddUser(employee).Message);

var laptop = new Laptop("Dell XPS 15", "Dell", 16, "Intel i7");
var camera = new Camera("Canon EOS", "Canon", 24, "Zoom");
var projector = new Projector("Epson X1", "Epson", "1920x1080", 3500);

Console.WriteLine(rentalService.AddEquipment(laptop).Message);
Console.WriteLine(rentalService.AddEquipment(camera).Message);
Console.WriteLine(rentalService.AddEquipment(projector).Message);

Console.WriteLine();

Console.WriteLine(rentalService.BorrowEquipment(student.Id, laptop.Id, 7).Message);
Console.WriteLine(rentalService.BorrowEquipment(student.Id, camera.Id, 5).Message);
Console.WriteLine(rentalService.BorrowEquipment(student.Id, projector.Id, 3).Message);

Console.WriteLine();

Console.WriteLine("ACTIVE LOANS:");
foreach (var loan in rentalService.GetAllLoans())
{
    Console.WriteLine(loan);
}

Console.WriteLine();

Console.WriteLine(rentalService.ReturnEquipment(1).Message);

Console.WriteLine();
Console.WriteLine(reportService.GenerateSummaryReport());