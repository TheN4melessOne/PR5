using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR5
{
    public partial class MainWindow : Window
    {
        const int countDot = 30;
        List<double[]> dataList = new List<double[]>();
        DrawingGroup drawingGroup = new DrawingGroup();
        public MainWindow()
        {
            InitializeComponent();
            DataFill();
            Execute();
            Image1.Source = new DrawingImage(drawingGroup);
        }

        void DataFill()
        {
            double[] sin = new double[countDot + 1];
            double[] cos = new double[countDot + 1];
            for (int i = 0; i < sin.Length; i++)
            {
                double angle = Math.PI * 2 / countDot * i;//отметки на оси абсцисс(количество делений) - мера угла в радианах
                sin[i] = Math.Sin(angle);//принимает меру угла в радианах
                cos[i] = Math.Cos(angle);
            }
            dataList.Add(sin);
            dataList.Add(cos);
        }

        private void BackgroundFun()
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing();

            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(0,0,1,1);
            geometryDrawing.Geometry = rectangleGeometry;

            geometryDrawing.Brush = Brushes.Beige;
            geometryDrawing.Pen = new Pen(Brushes.Red, 0.005);

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void GridFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 1; i < 10; i++)
            {
                LineGeometry line = new LineGeometry(new Point(1.0, i * 0.1),
                    new Point(-0.1, i * 0.1));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);
            double[] dashes = { 1, 1, 1, 1, 1 };
            geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);
            geometryDrawing.Brush = Brushes.Beige;

            drawingGroup.Children.Add(geometryDrawing);
        }

        private void SinFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < dataList[0].Length - 1; i++)
            {
                LineGeometry line = new LineGeometry(new Point((double)i / countDot, 
                    .5 - (dataList[0][i] / 2.0)),
                    new Point((double)(i + 1) / countDot,
                    .5 - (dataList[0][i + 1] / 2.0)));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Blue, 0.005);
            drawingGroup.Children.Add(geometryDrawing);
        }

        private void CosFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < dataList[1].Length; i++)
            {
                EllipseGeometry ellipse = new EllipseGeometry(
                    new Point((double)i / (double)countDot, 
                    .5 - (dataList[1][i] / 2.0)), 0.01, 0.01);
                geometryGroup.Children.Add(ellipse);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Green, 0.005);
            drawingGroup.Children.Add(geometryDrawing);
        }

        private void MakerFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < 10; i++)
            {
                FormattedText formattedText = new FormattedText(
                    String.Format("{0,7:F}", 1 - i * 0.2),
                    CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                    new Typeface("Verdana"), 0.05, Brushes.Black);
                formattedText.SetFontWeight(FontWeights.Bold);

                Geometry geometry = formattedText.BuildGeometry(
                    new Point(-0.2, i * 0.1 - 0.03));
                geometryGroup.Children.Add(geometry);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);
            geometryDrawing.Brush = Brushes.LightGray;
            drawingGroup.Children.Add(geometryDrawing);
        }

        void Execute()
        {
            BackgroundFun();
            GridFun();
            SinFun();
            CosFun();
            MakerFun();
        }
    }
}
