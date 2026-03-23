namespace UniversityEquipmentRental.Models;

public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string Processor { get; set; }

    public Laptop(string name, string brand, int ramGb, string processor)
        : base(name, brand)
    {
        RamGb = ramGb;
        Processor = processor;
    }

    public override string ToString()
    {
        return base.ToString() + $", Type: Laptop, RAM: {RamGb} GB, CPU: {Processor}";
    }
}
