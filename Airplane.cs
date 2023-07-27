using GraviSharp;
using _3dSharp;
using System;

public class Airplane
{
    public Camera Camera { get; set; }
    public SolidBody SolidBody { get; set; } = new SolidBody(new Point3d(0, 0, 0), true, false);
    public bool[] Moves = new bool[8];
    // thrust, revthrust, Lyaw, Ryaw, Upitch, Dpitch, Lroll, Rroll

    public double Thrust { get; set; } = 10*10;
    public double YawSpeed { get; set; } = (1.0/3.0);
    public double PitchSpeed { get; set; } = (3.0/3.0);
    public double RollSpeed { get; set; } = (5.0/3.0);

    public double[] MoveParams { get; set; } = new double[4];

    public Airplane(Camera camera)
    {
        Camera = camera;
        SolidBody.Position = Camera.Position;
        SolidBody.Angle = Camera.Angle;
        MoveParams = new double[4] {Thrust, YawSpeed, PitchSpeed, RollSpeed};
        SolidBody.Time = DateTime.Now;
    }

    public void Update()
    {
        SolidBody.Update(Moves, MoveParams);
        UpdateCamera();
    }

    public void Move(byte move, bool state)
    {
        Moves[move] = state;
        Update();
    }

    public void UpdateCamera()
    {
        Camera.MoveTo(SolidBody.Position);
        Camera.Angle = SolidBody.Angle;
        Camera.CameraView.Refresh();
    }

}