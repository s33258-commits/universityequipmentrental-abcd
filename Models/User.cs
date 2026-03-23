namespace UniversityEquipmentRental.Models;

public abstract class User
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    protected User(string firstName, string lastName)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
    }

    public abstract string UserType { get; }
    public abstract int MaxActiveLoans { get; }

    public override string ToString()
    {
        return $"ID: {Id}, {FirstName} {LastName}, Type: {UserType}, Limit: {MaxActiveLoans}";
    }
}
