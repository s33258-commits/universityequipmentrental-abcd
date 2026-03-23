using UniversityEquipmentRental.Models;

namespace UniversityEquipmentRental.Services;

public class ReportService
{
    private readonly List<Equipment> _equipment;
    private readonly List<Loan> _loans;
    private readonly List<User> _users;

    public ReportService(List<Equipment> equipment, List<Loan> loans, List<User> users)
    {
        _equipment = equipment;
        _loans = loans;
        _users = users;
    }

    public string GenerateSummaryReport()
    {
        int totalEquipment = _equipment.Count;
        int availableEquipment = _equipment.Count(e => e.IsAvailable);
        int unavailableEquipment = _equipment.Count(e => !e.IsAvailable);
        int activeLoans = _loans.Count(l => l.IsActive);
        int overdueLoans = _loans.Count(l => l.IsOverdue);

        return
$@"===== RENTAL SUMMARY REPORT =====
Users: {_users.Count}
Total equipment: {totalEquipment}
Available equipment: {availableEquipment}
Unavailable or borrowed equipment: {unavailableEquipment}
Active loans: {activeLoans}
Overdue loans: {overdueLoans}
=================================";
    }
}
