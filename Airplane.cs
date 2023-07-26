using GraviSharp;
using _3dSharp;

public class Airplane
{
    public Camera Camera { get; set; }
    public SolidBody SolidBody { get; set; }

    public Airplane(Camera camera)
    {
        Camera = camera;
        SolidBody.Position = Camera.Position;
        SolidBody.Angle = Camera.Angle;
    }

    

}