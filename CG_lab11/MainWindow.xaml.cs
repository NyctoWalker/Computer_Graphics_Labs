using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CG_lab11
{
    public partial class MainWindow : Window
    {


        private Vector3D _lookDirection; // Вектор в направлении взгляда
        private Vector3D _rightVector; // Вектор справа от направления взгляда
        private Vector3D _upVector; // Вектор сверху от направления взгляда

        private Point _lastMousePosition;
        private const double RotationSensitivity = 0.05;

        public MainWindow()
        {


            InitializeComponent();
            SetupInputHandlers();

            // CreateCuboid(new Point3D(3, 5, 0), 4, 8, 1);
            //TestPolygon();
            CreateStaircase(new Point3D(0, 0, 0), 12, 2, 8, 10, 12);

        }

        #region AddGeometry

        private void TestPolygon()
        {
            var visual = new ModelVisual3D();
            var mesh = new MeshGeometry3D();

            mesh.Positions.Add(new Point3D(3, 0, 0));
            mesh.Positions.Add(new Point3D(6, 0, 6));
            mesh.Positions.Add(new Point3D(3.2, 1, 1));

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(0);

            var material = new DiffuseMaterial(Brushes.Red);

            var geometryModel = new GeometryModel3D(mesh, material);

            var modelGroup = new Model3DGroup();
            modelGroup.Children.Add(geometryModel);

            visual.Content = modelGroup;

            viewport.Children.Add(visual);
        }

        private void CreateCuboid(Point3D cPoint, double width_x, double height_y, double length_z)
        {
            var geometry = GenerateCuboidGeometry(GenerateCuboidPoints(cPoint, width_x, height_y, length_z));

            var mesh = new MeshGeometry3D();
            mesh.Positions = geometry.Positions;
            mesh.TriangleIndices = geometry.TriangleIndices;

            var material = new DiffuseMaterial(Brushes.Red);
            material.AmbientColor = Colors.White;
            var geometryModel = new GeometryModel3D(mesh, material);

            var modelGroup = new Model3DGroup();
            modelGroup.Children.Add(geometryModel);

            var modelVisual = new ModelVisual3D();
            modelVisual.Content = modelGroup;

            viewport.Children.Add(modelVisual);
        }

        private List<Point3D> GenerateCuboidPoints(Point3D cPoint, double width_x, double height_y, double length_z)
        {
            double w = width_x / 2d;
            double h = height_y / 2d;
            double l = length_z / 2d;

            double cx = cPoint.X;
            double cy = cPoint.Y;
            double cz = cPoint.Z;

            List<Point3D> pts = new List<Point3D>{
                new Point3D(cx - w, cy - h, cz - l),
                new Point3D(cx + w, cy - h, cz - l),
                new Point3D(cx - w, cy + h, cz - l),
                new Point3D(cx + w, cy + h, cz - l),

                new Point3D(cx - w, cy - h, cz + l),
                new Point3D(cx + w, cy - h, cz + l),
                new Point3D(cx - w, cy + h, cz + l),
                new Point3D(cx + w, cy + h, cz + l),
            };

            return pts;
        }

        private MeshGeometry3D GenerateCuboidGeometry(List<Point3D> pts)
        {
            var mesh = new MeshGeometry3D();

            // 8 точек
            foreach (Point3D p in pts)
                mesh.Positions.Add(p);

            // 12 треугольных полигонов
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(1); // Верх
            mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(2); // Восточная грань
            mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(6);

            mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(4); // Южная грань
            mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(7); mesh.TriangleIndices.Add(5); // Западная грань
            mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(7);

            mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(3); // Северная грань
            mesh.TriangleIndices.Add(3); mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(6); // Низ
            mesh.TriangleIndices.Add(7); mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(5);

            // 6*3 нормалей
            var frontNormal = new Vector3D(0, 0, mesh.Positions[0].Z);
            var backNormal = new Vector3D(0, 0, mesh.Positions[4].Z);
            var leftNormal = new Vector3D(mesh.Positions[0].X, 0, 0);
            var rightNormal = new Vector3D(mesh.Positions[1].X, 0, 0);
            var topNormal = new Vector3D(0, mesh.Positions[2].Y, 0);
            var bottomNormal = new Vector3D(0, mesh.Positions[0].Y, 0);

            mesh.Normals.Add(frontNormal); mesh.Normals.Add(frontNormal); mesh.Normals.Add(frontNormal);
            mesh.Normals.Add(backNormal); mesh.Normals.Add(backNormal); mesh.Normals.Add(backNormal);
            mesh.Normals.Add(leftNormal); mesh.Normals.Add(leftNormal); mesh.Normals.Add(leftNormal);
            mesh.Normals.Add(rightNormal); mesh.Normals.Add(rightNormal); mesh.Normals.Add(rightNormal);
            mesh.Normals.Add(topNormal); mesh.Normals.Add(topNormal); mesh.Normals.Add(topNormal);
            mesh.Normals.Add(bottomNormal); mesh.Normals.Add(bottomNormal); mesh.Normals.Add(bottomNormal);

            return mesh;
        }

        #endregion

        #region Stairs

        private void CreateStaircase(Point3D startPoint, int width_x, int height_y, int length_z, int numLevels, int numStairsPerLevel)
        {
            double platformWidth = width_x;
            double platformDepth = length_z;
            double platformHeight = height_y;

            double stairWidth = platformWidth / 2;
            double stairDepth = platformDepth / 2;
            double stairHeight = platformHeight / 2;

            Point3D currentPoint = startPoint;

            for (int i = 0; i < numLevels; i++)
            {
                int rightSide = i % 2;

                Point3D platformCenter = new Point3D(currentPoint.X, currentPoint.Y - (i+1) * stairHeight * (numStairsPerLevel-4), currentPoint.Z - rightSide * stairDepth * numStairsPerLevel);
                CreateCuboid(platformCenter, platformWidth, platformHeight, platformDepth);

                int rightLeftShift = rightSide * 2 - 1;
                AddLightSource(new Point3D(platformCenter.X, platformCenter.Y + platformHeight * 3, platformCenter.Z));
                // CreateCuboid(new Point3D(platformCenter.X, platformCenter.Y + platformHeight * 3, platformCenter.Z), 1, 1, 1);

                Point3D stairStartPoint = new Point3D(platformCenter.X + rightLeftShift * platformWidth / 4, platformCenter.Y - platformHeight, platformCenter.Z);

                for (int j = 0; j < (numStairsPerLevel - 2); j++)
                {
                    Point3D stairCenter = new Point3D(stairStartPoint.X, stairStartPoint.Y - stairHeight * (j-1.25), stairStartPoint.Z + rightLeftShift * stairDepth * (j+1.5));
                    CreateCuboid(stairCenter, stairWidth, stairHeight, stairDepth);
                }

                currentPoint = new Point3D(currentPoint.X, currentPoint.Y - platformHeight - stairHeight, currentPoint.Z);
            }
        }

        private void AddLightSource(Point3D position)
        {
            PointLight light = new PointLight();
            var visual = new ModelVisual3D();

            light.Color = Colors.White;
            light.Position = position;
            light.Range = 50;
            // light.Direction = new Vector3D(0, -1, 0);

            visual.Content = light;

            viewport.Children.Add(visual);
        }

        #endregion

        #region CameraControls
        private void SetupInputHandlers()
        {
            PreviewKeyDown += OnPreviewKeyDown;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
        }



        private void UpdateVectors()
        {
            _lookDirection = new Vector3D(
                _camera.LookDirection.X,
                _camera.LookDirection.Y,
                _camera.LookDirection.Z
            );
            _rightVector = Vector3D.CrossProduct(_lookDirection, new Vector3D(0, 1, 0));
            _upVector = Vector3D.CrossProduct(_lookDirection, _rightVector);
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W or Key.Up:
                    MoveForward();
                    break;
                case Key.S or Key.Down:
                    MoveBackward();
                    break;
                case Key.A or Key.Left:
                    MoveLeft();
                    break;
                case Key.D or Key.Right:
                    MoveRight();
                    break;
                case Key.Q:
                    MoveUp();
                    break;
                case Key.E:
                    MoveDown();
                    break;
            }
        }

        private void MoveForward()
        {
            Point3D position = _camera.Position;
            position += _lookDirection * 0.2;
            _camera.Position = position;
            UpdateVectors();
        }

        private void MoveBackward()
        {
            Point3D position = _camera.Position;
            position -= _lookDirection * 0.2;
            _camera.Position = position;
            UpdateVectors();
        }

        private void MoveLeft()
        {
            Point3D position = _camera.Position;
            position -= _rightVector * 0.2;
            _camera.Position = position;
            UpdateVectors();
        }

        private void MoveRight()
        {
            Point3D position = _camera.Position;
            position += _rightVector * 0.2;
            _camera.Position = position;
            UpdateVectors();
        }

        private void MoveUp()
        {
            Point3D position = _camera.Position;
            position += _upVector * 0.2;
            _camera.Position = position;
            UpdateVectors();
        }

        private void MoveDown()
        {
            Point3D position = _camera.Position;
            position -= _upVector * 0.2;
            _camera.Position = position;
            UpdateVectors();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _lastMousePosition = Mouse.GetPosition(this);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                Point currentPosition = Mouse.GetPosition(this);

                double deltaX = currentPosition.X - _lastMousePosition.X;
                double deltaY = currentPosition.Y - _lastMousePosition.Y;

                RotateCamera(deltaX * RotationSensitivity, deltaY * RotationSensitivity);

                _lastMousePosition = currentPosition;
            }
        }

        private void RotateCamera(double deltaX, double deltaY)
        {
            Vector3D rotationAxisX = new Vector3D(1, 0, 0); // (up/down)
            Vector3D rotationAxisY = new Vector3D(0, 1, 0); // (left/right)

            Quaternion rotationQuaternionX = new Quaternion(rotationAxisX, deltaY);
            Quaternion rotationQuaternionY = new Quaternion(rotationAxisY, deltaX);

            Vector3D direction = new Vector3D(_camera.LookDirection.X, _camera.LookDirection.Y, _camera.LookDirection.Z);

            direction = Multiply(direction, rotationQuaternionY);
            direction = Multiply(direction, rotationQuaternionX);

            _camera.LookDirection = direction;

            UpdateVectors();
        }

        private static Vector3D Multiply(Vector3D vector, Quaternion quaternion)
        {
            double x = vector.X;
            double y = vector.Y;
            double z = vector.Z;
            double qx = quaternion.X;
            double qy = quaternion.Y;
            double qz = quaternion.Z;
            double qw = quaternion.W;

            return new Vector3D(
                x * (qw * qw + qx * qx - qy * qy - qz * qz) +
                y * (2 * qx * qy - 2 * qw * qz) +
                z * (2 * qx * qz + 2 * qw * qy),

                x * (2 * qx * qy + 2 * qw * qz) +
                y * (qw * qw - qx * qx + qy * qy - qz * qz) +
                z * (2 * qy * qz - 2 * qw * qx),

                x * (2 * qx * qz - 2 * qw * qy) +
                y * (2 * qy * qz + 2 * qw * qx) +
                z * (qw * qw - qx * qx - qy * qy + qz * qz)
            );
        }
        #endregion
    }
}
