using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using _3dSharp;

ApplicationConfiguration.Initialize();

var form = new Form();

form.Size = new Size(1280, 720);

// form.WindowState = FormWindowState.Maximized;

Graphics g = null;
Bitmap bmp = null;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

var tm = new Timer();
tm.Interval = 5;

var drawFont = new Font("Arial", 10);
Pen pen = new Pen(Color.Black);
SolidBrush solidBrush = new SolidBrush(Color.LightBlue);
SolidBrush floorBrush = new SolidBrush(Color.Green);

Point3d cameraPos = new Point3d(0, 0, 0);
double FOV = 800;
Ratio ratio = new Ratio(16 * 100, 9 * 100, 720);
Angle angle = new Angle(0, 0, 0);
double ratioScale = 1;
Camera camera = new Camera(cameraPos, FOV, angle, ratio, ratioScale);

Airplane plane = new Airplane(camera);

Random rd = new Random();

for (int i = 1; i < 21; i++)
{
    int yBase = rd.Next(-3000, 1500);
    int zBase = rd.Next(-3000, 1500);

    // yBase *= 1+(i/10);
    // zBase *= 1+(i/10);

    Cube P1L = new Cube(new Point3d(i*5000, yBase, zBase), new Point3d((i*5000)+100, yBase+1500, zBase+100));
    Cube P1R = new Cube(new Point3d(i*5000, yBase, zBase+1500), new Point3d((i*5000)+100, yBase+1500, zBase+1400));
    Cube P1B = new Cube(new Point3d(i*5000, yBase, zBase), new Point3d((i*5000)+100, yBase+100, zBase+1500));
    Cube P1T = new Cube(new Point3d(i*5000, yBase+1400, zBase), new Point3d((i*5000)+100, yBase+1500, zBase+1500));

    Floor floor = new Floor(new Point3d((i-1)*5000, -3500, -5000), new Point3d((i*5000)-2500, -3500, 0));
    Floor floor2 = new Floor(new Point3d((i-1)*5000, -3500, 0), new Point3d((i*5000)-2500, -3500, 5000));
    Floor floor3 = new Floor(new Point3d(((i-1)*5000)+2500, -3500, -5000), new Point3d(i*5000, -3500, 0));
    Floor floor4 = new Floor(new Point3d(((i-1)*5000)+2500, -3500, 0), new Point3d(i*5000, -3500, 5000));

}

tm.Tick += delegate
{
    plane.Update();

    List<Triangle2d> triangles = Scene.BruteRender(camera);

    List<Triangle2d> floor = Scene.RenderFloor(camera);

    g.Clear(Color.White);

    foreach (var triangle in floor)
    {
        g.FillPolygon(floorBrush, triangle.points);
    }

    foreach (var triangle in triangles)
    {
        g.FillPolygon(solidBrush, triangle.points);
        g.DrawPolygon(new Pen(Color.Black), triangle.points);
    }

    pb.Refresh();
};

form.Load += delegate
{
    bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);
    g.Clear(Color.White);
    pb.Image = bmp;
    tm.Start();
};

form.KeyPreview = true;
form.KeyDown += (s, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
    if (e.KeyCode == Keys.Up)
        plane.Move(0, true);
    if (e.KeyCode == Keys.Down)
        plane.Move(1, true);
    if (e.KeyCode == Keys.E)
        plane.Move(2, true);
    if (e.KeyCode == Keys.Q)
        plane.Move(3, true);
    if (e.KeyCode == Keys.S)
        plane.Move(4, true);
    if (e.KeyCode == Keys.W)
        plane.Move(5, true);
    if (e.KeyCode == Keys.A)
        plane.Move(6, true);
    if (e.KeyCode == Keys.D)
        plane.Move(7, true);
};

form.KeyUp += (s, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
    if (e.KeyCode == Keys.Up)
        plane.Move(0, false);
    if (e.KeyCode == Keys.Down)
        plane.Move(1, false);
    if (e.KeyCode == Keys.E)
        plane.Move(2, false);
    if (e.KeyCode == Keys.Q)
        plane.Move(3, false);
    if (e.KeyCode == Keys.S)
        plane.Move(4, false);
    if (e.KeyCode == Keys.W)
        plane.Move(5, false);
    if (e.KeyCode == Keys.A)
        plane.Move(6, false);
    if (e.KeyCode == Keys.D)
        plane.Move(7, false);
};

Application.Run(form);