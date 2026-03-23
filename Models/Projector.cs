namespace UniversityEquipmentRental.Models;

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public int BrightnessLumens { get; set; }

    public Projector(string name, string brand, string resolution, int brightnessLumens)
        : base(name, brand)
    {
        Resolution = resolution;
        BrightnessLumens = brightnessLumens;
    }

    public override string ToString()
    {
        return base.ToString() + $", Type: Projector, Resolution: {Resolution}, Brightness: {BrightnessLumens} lm";
    }
}
