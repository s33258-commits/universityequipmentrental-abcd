namespace UniversityEquipmentRental.Models;

public class Loan
{
    private static int _nextId = 1;

    public int Id { get; }
    public User Borrower { get; }
    public Equipment Equipment { get; }
    public DateTime BorrowDate { get; }
    public int DurationDays { get; }
    public DateTime DueDate => BorrowDate.AddDays(DurationDays);
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public Loan(User borrower, Equipment equipment, DateTime borrowDate, int durationDays)
    {
        Id = _nextId++;
        Borrower = borrower;
        Equipment = equipment;
        BorrowDate = borrowDate;
        DurationDays = durationDays;
        Penalty = 0;
    }

    public bool IsActive => ReturnDate == null;
    public bool IsReturnedOnTime => ReturnDate != null && ReturnDate <= DueDate;
    public bool IsOverdue => IsActive && DateTime.Now > DueDate;

    public void ReturnEquipment(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }

    public override string ToString()
    {
        string returnInfo = ReturnDate == null
            ? "Not returned"
            : $"Returned: {ReturnDate:yyyy-MM-dd HH:mm}, Penalty: {Penalty} PLN";

        return $"Loan ID: {Id}, User: {Borrower.FirstName} {Borrower.LastName}, Equipment: {Equipment.Name}, " +
               $"Borrowed: {BorrowDate:yyyy-MM-dd HH:mm}, Due: {DueDate:yyyy-MM-dd HH:mm}, {returnInfo}";
    }
}
