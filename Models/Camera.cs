namespace UniversityEquipmentRental.Models;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public string LensType { get; set; }

    public Camera(string name, string brand, int megapixels, string lensType)
        : base(name, brand)
    {
        Megapixels = megapixels;
        LensType = lensType;
    }

    public override string ToString()
    {
        return base.ToString() + $", Type: Camera, Megapixels: {Megapixels} MP, Lens: {LensType}";
    }
}
