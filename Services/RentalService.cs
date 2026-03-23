using UniversityEquipmentRental.Enums;
using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class RentalService
{
    private readonly List<User> _users;
    private readonly List<Equipment> _equipment;
    private readonly List<Loan> _loans;
    private readonly PenaltyCalculator _penaltyCalculator;

    public RentalService(List<User> users, List<Equipment> equipment, List<Loan> loans, PenaltyCalculator penaltyCalculator)
    {
        _users = users;
        _equipment = equipment;
        _loans = loans;
        _penaltyCalculator = penaltyCalculator;
    }

    public OperationResult AddUser(User user)
    {
        _users.Add(user);
        return OperationResult.Ok($"User added: {user.FirstName} {user.LastName}");
    }

    public OperationResult AddEquipment(Equipment item)
    {
        _equipment.Add(item);
        return OperationResult.Ok($"Equipment added: {item.Name}");
    }

    public OperationResult BorrowEquipment(int userId, int equipmentId, int durationDays)
    {
        User? user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
            return OperationResult.Fail("User not found.");

        Equipment? item = _equipment.FirstOrDefault(e => e.Id == equipmentId);
        if (item == null)
            return OperationResult.Fail("Equipment not found.");

        if (item.Status == EquipmentStatus.Unavailable)
            return OperationResult.Fail("Equipment is unavailable.");

        if (!item.IsAvailable)
            return OperationResult.Fail("Equipment is already borrowed.");

        int activeLoansCount = _loans.Count(l => l.Borrower.Id == userId && l.IsActive);
        if (activeLoansCount >= user.MaxActiveLoans)
            return OperationResult.Fail($"User exceeded the limit of {user.MaxActiveLoans} active loans.");

        Loan loan = new Loan(user, item, DateTime.Now, durationDays);
        _loans.Add(loan);
        item.MarkAsBorrowed();

        return OperationResult.Ok($"Equipment '{item.Name}' borrowed by {user.FirstName} {user.LastName}.");
    }

    public OperationResult ReturnEquipment(int loanId)
    {
        Loan? loan = _loans.FirstOrDefault(l => l.Id == loanId);
        if (loan == null)
            return OperationResult.Fail("Loan not found.");

        if (!loan.IsActive)
            return OperationResult.Fail("This equipment has already been returned.");

        DateTime returnDate = DateTime.Now;
        decimal penalty = _penaltyCalculator.CalculatePenalty(loan.DueDate, returnDate);

        loan.ReturnEquipment(returnDate, penalty);
        loan.Equipment.MarkAsAvailable();

        if (penalty > 0)
            return OperationResult.Ok($"Equipment returned with penalty: {penalty} PLN.");

        return OperationResult.Ok("Equipment returned on time.");
    }

    public OperationResult MarkEquipmentAsUnavailable(int equipmentId)
    {
        Equipment? item = _equipment.FirstOrDefault(e => e.Id == equipmentId);
        if (item == null)
            return OperationResult.Fail("Equipment not found.");

        if (item.Status == EquipmentStatus.Borrowed)
            return OperationResult.Fail("Borrowed equipment cannot be marked as unavailable.");

        item.MarkAsUnavailable();
        return OperationResult.Ok($"Equipment '{item.Name}' marked as unavailable.");
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipment;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipment.Where(e => e.IsAvailable).ToList();
    }

    public List<Loan> GetActiveLoansForUser(int userId)
    {
        return _loans.Where(l => l.Borrower.Id == userId && l.IsActive).ToList();
    }

    public List<Loan> GetOverdueLoans()
    {
        return _loans.Where(l => l.IsOverdue).ToList();
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }

    public List<Loan> GetAllLoans()
    {
        return _loans;
    }
}
