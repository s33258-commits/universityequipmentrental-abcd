using UniversityEquipmentRental.Models;
using UniversityEquipmentRental.Services;

List<User> users = new();
List<Equipment> equipment = new();
List<Loan> loans = new();

PenaltyCalculator penaltyCalculator = new();
RentalService rentalService = new(users, equipment, loans, penaltyCalculator);
ReportService reportService = new(equipment, loans, users);

// USERS
var student = new Student("Uladzimir", "Kuzmich", "s33258");
var employee = new Employee("Adam", "Smyk", "s12345");

Console.WriteLine("ADDING USERS...");
Console.WriteLine(rentalService.AddUser(student).Message);
Console.WriteLine(rentalService.AddUser(employee).Message);

// EQUIPMENT
var laptop = new Laptop("MacBook Pro", "Apple", 16, "M3");
var camera = new Camera("Sony Alpha A7 III", "Sony", 24, "Zoom Lens");
var projector = new Projector("Epson X1", "Epson", "1920x1080", 3500);
var laptop2 = new Laptop("MacBook Air", "Apple", 8, "M2");

Console.WriteLine();
Console.WriteLine("ADDING EQUIPMENT...");
Console.WriteLine(rentalService.AddEquipment(laptop).Message);
Console.WriteLine(rentalService.AddEquipment(camera).Message);
Console.WriteLine(rentalService.AddEquipment(projector).Message);
Console.WriteLine(rentalService.AddEquipment(laptop2).Message);

// ALL EQUIPMENT
Console.WriteLine();
Console.WriteLine("ALL EQUIPMENT:");
foreach (var item in equipment)
{
    Console.WriteLine(item);
}

// AVAILABLE EQUIPMENT
Console.WriteLine();
Console.WriteLine("AVAILABLE EQUIPMENT:");
foreach (var item in equipment.Where(e => e.IsAvailable))
{
    Console.WriteLine(item);
}

// CORRECT BORROWING
Console.WriteLine();
Console.WriteLine("BORROWING EQUIPMENT...");
Console.WriteLine(rentalService.BorrowEquipment(student.Id, laptop.Id, 7).Message);
Console.WriteLine(rentalService.BorrowEquipment(student.Id, camera.Id, 5).Message);

// INVALID OPERATION - LIMIT EXCEEDED
Console.WriteLine();
Console.WriteLine("INVALID BORROW ATTEMPT (LIMIT EXCEEDED):");
Console.WriteLine(rentalService.BorrowEquipment(student.Id, projector.Id, 3).Message);

// MARK EQUIPMENT AS UNAVAILABLE
projector.MarkAsUnavailable();

Console.WriteLine();
Console.WriteLine("INVALID BORROW ATTEMPT (UNAVAILABLE EQUIPMENT):");
Console.WriteLine(rentalService.BorrowEquipment(employee.Id, projector.Id, 3).Message);

// ACTIVE LOANS FOR STUDENT
Console.WriteLine();
Console.WriteLine("ACTIVE LOANS FOR STUDENT:");
foreach (var loan in loans.Where(l => l.Borrower.Id == student.Id && l.IsActive))
{
    Console.WriteLine(loan);
}

// RETURN ON TIME
Console.WriteLine();
Console.WriteLine("RETURN ON TIME:");
Console.WriteLine(rentalService.ReturnEquipment(1).Message);

// CREATE OVERDUE LOAN FOR DEMO
Console.WriteLine();
Console.WriteLine("CREATING OVERDUE LOAN FOR DEMO...");

laptop2.MarkAsBorrowed();
var overdueLoan = new Loan(employee, laptop2, DateTime.Now.AddDays(-10), 5);
loans.Add(overdueLoan);

// OVERDUE LOANS
Console.WriteLine();
Console.WriteLine("OVERDUE LOANS:");
foreach (var loan in loans.Where(l => l.IsOverdue))
{
    Console.WriteLine(loan);
}

// RETURN LATE WITH PENALTY
Console.WriteLine();
Console.WriteLine("LATE RETURN WITH PENALTY:");
Console.WriteLine(rentalService.ReturnEquipment(overdueLoan.Id).Message);

// FINAL REPORT
Console.WriteLine();
Console.WriteLine("FINAL REPORT:");
Console.WriteLine(reportService.GenerateSummaryReport());