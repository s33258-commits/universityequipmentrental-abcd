using UniversityEquipmentRental.Enums;

namespace UniversityEquipmentRental.Models;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public EquipmentStatus Status { get; private set; }

    protected Equipment(string name, string brand)
    {
        Id = _nextId++;
        Name = name;
        Brand = brand;
        Status = EquipmentStatus.Available;
    }

    public bool IsAvailable => Status == EquipmentStatus.Available;

    public void MarkAsBorrowed()
    {
        Status = EquipmentStatus.Borrowed;
    }

    public void MarkAsAvailable()
    {
        Status = EquipmentStatus.Available;
    }

    public void MarkAsUnavailable()
    {
        Status = EquipmentStatus.Unavailable;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Brand: {Brand}, Status: {Status}";
    }
}
