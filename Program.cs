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

Pen pen = new Pen(Color.Black);

Point3d cameraPos = new Point3d(0, 0, 0);
double FOV = 800;
Ratio ratio = new Ratio(16 * 100, 9 * 100, 720);
Angle angle = new Angle(0, 0, 0);
double ratioScale = 1;
Camera camera = new Camera(cameraPos, FOV, angle, ratio, ratioScale);

Airplane plane = new Airplane(camera);

// _3dSharp.Panel panel = new _3dSharp.Panel(new Point3d(1000,10000,-1000), new Point3d(-1000,10000,1000));

Cube cube = new Cube(new Point3d(9000, -1000, -1000), new Point3d(11000, 1000, 1000));
Cube cube2 = new Cube(new Point3d(12000, -1000, 2000), new Point3d(14000, 1000, 4000));
Cube cube3 = new Cube(new Point3d(12000, -1000, -2000), new Point3d(14000, 1000, -4000));
Cube cube4 = new Cube(new Point3d(6000, -1000, 2000), new Point3d(8000, 1000, 4000));
Cube cube5 = new Cube(new Point3d(6000, -1000, -2000), new Point3d(8000, 1000, -4000));

Cube cube6 = new Cube(new Point3d(-1000, 9000, -1000), new Point3d(1000, 11000, 1000));

tm.Tick += delegate
{
    List<Triangle2d> triangles = Scene.BruteRender(camera);

    var drawFont = new Font("Arial", 10);

    PointF drawPoint = new PointF(150.0F, 150.0F);
    PointF drawPoint2 = new PointF(150.0F, 175.0F);
    PointF drawPoint3 = new PointF(150.0F, 200.0F);
    PointF drawPoint4 = new PointF(150.0F, 225.0F);
    PointF drawPoint5 = new PointF(150.0F, 250.0F);
    PointF drawPoint6 = new PointF(150.0F, 275.0F);
    PointF drawPoint7 = new PointF(150.0F, 300.0F);

    g.Clear(Color.White);

    g.DrawString($"Camera FOV: {camera.CameraView.FOVpoint}", drawFont, Brushes.Black, drawPoint);
    g.DrawString($"{triangles.Count} Triangles Rendered", drawFont, Brushes.Black, drawPoint2);
    g.DrawString($"Camera angles: {camera.CameraView.Angle.yaw}, {camera.CameraView.Angle.pitch}, {camera.CameraView.Angle.roll}", drawFont, Brushes.Black, drawPoint3);
    g.DrawString($"Camera position: {camera.Position}", drawFont, Brushes.Black, drawPoint4);
    g.DrawString($"Edge points: P0: ({camera.CameraView.Points[0]}) / P1: ({camera.CameraView.Points[1]})", drawFont, Brushes.Black, drawPoint5);
    g.DrawString($"Edge Points: P2: ({camera.CameraView.Points[2]}) / P3: ({camera.CameraView.Points[3]})", drawFont, Brushes.Black, drawPoint6);

    foreach (var triangle in triangles)
    {
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
    if (e.KeyCode == Keys.Left)
        camera.YawAdd(-1);
    if (e.KeyCode == Keys.Right)
        camera.YawAdd(1);
    if (e.KeyCode == Keys.Up)
        camera.PitchAdd(1);
    if (e.KeyCode == Keys.Down)
        camera.PitchAdd(-1);
    if (e.KeyCode == Keys.M)
        camera.RollAdd(-1);
    if (e.KeyCode == Keys.N)
        camera.RollAdd(1);
    if (e.KeyCode == Keys.W)
        camera.Translate(50, 0, 0);
    if (e.KeyCode == Keys.S)
        camera.Translate(-50, 0, 0);
    if (e.KeyCode == Keys.A)
        camera.Translate(0, 0, -50);
    if (e.KeyCode == Keys.D)
        camera.Translate(0, 0, 50);
    if (e.KeyCode == Keys.Z)
        camera.Translate(0, -50, 0);
    if (e.KeyCode == Keys.Space)
        camera.Translate(0, 50, 0);
};

Application.Run(form);