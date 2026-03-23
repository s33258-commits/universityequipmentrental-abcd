namespace UniversityEquipmentRental.Services;

public class PenaltyCalculator
{
    private const decimal PenaltyPerDay = 10m;

    public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate <= dueDate)
            return 0;

        int lateDays = (returnDate.Date - dueDate.Date).Days;
        return lateDays * PenaltyPerDay;
    }
}
