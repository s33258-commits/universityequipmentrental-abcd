namespace UniversityEquipmentRental.Models;

public class Employee : User
{
    public string EmployeeNumber { get; set; }

    public Employee(string firstName, string lastName, string employeeNumber)
        : base(firstName, lastName)
    {
        EmployeeNumber = employeeNumber;
    }

    public override string UserType => "Employee";
    public override int MaxActiveLoans => 5;

    public override string ToString()
    {
        return base.ToString() + $", Employee Number: {EmployeeNumber}";
    }
}
