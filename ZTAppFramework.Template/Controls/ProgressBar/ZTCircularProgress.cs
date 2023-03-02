using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls
{
    public class ZTCircularProgress : ContentControl
    {

        private int _pathFigureWidth;
        private int _pathFigureHeight;
        private Thickness _pathFigureMargin;
        private Point _pathFigureStartPoint;
        private Point _arcSegmentPoint;
        private Size _arcSegmentSize;
        private bool _arcSegmentIsLargeArc;
        private double _radius;

        public static readonly StyledProperty<IBrush> StrokeBrushProperty =
         AvaloniaProperty.Register<ZTCircularProgress, IBrush>(nameof(StrokeBrush));

        public static readonly StyledProperty<int> StrokeThicknessProperty =
            AvaloniaProperty.Register<ZTCircularProgress, int>(nameof(StrokeThickness), 5);

        public static readonly StyledProperty<double> ProgressValueProperty =
            AvaloniaProperty.Register<ZTCircularProgress, double>(
                nameof(ProgressValue), 1);


        public static readonly StyledProperty<bool> IsIndeterminateProperty =
            AvaloniaProperty.Register<ZTCircularProgress, bool>(
                nameof(IsIndeterminate), false);


        public static readonly DirectProperty<ZTCircularProgress, int> PathFigureWidthProperty =
            AvaloniaProperty.RegisterDirect<ZTCircularProgress, int>(
                nameof(PathFigureWidth),
                o => o.PathFigureWidth,
                (o, v) => o.PathFigureWidth = v);

        public static readonly DirectProperty<ZTCircularProgress, int> PathFigureHeightProperty =
            AvaloniaProperty.RegisterDirect<ZTCircularProgress, int>(
                nameof(PathFigureHeight),
                o => o.PathFigureHeight,
                (o, v) => o.PathFigureHeight = v);

        public static readonly DirectProperty<ZTCircularProgress, Thickness> PathFigureMarginProperty =
            AvaloniaProperty.RegisterDirect<ZTCircularProgress, Thickness>(
                nameof(PathFigureMargin),
                o => o.PathFigureMargin,
                (o, v) => o.PathFigureMargin = v);

        public static readonly DirectProperty<ZTCircularProgress, Point> PathFigureStartPointProperty =
            AvaloniaProperty.RegisterDirect<ZTCircularProgress, Point>(
                nameof(PathFigureStartPoint),
                o => o.PathFigureStartPoint,
                (o, v) => o.PathFigureStartPoint = v);

        public static readonly DirectProperty<ZTCircularProgress, Point> ArcSegmentPointProperty =
            AvaloniaProperty.RegisterDirect<ZTCircularProgress, Point>(
                nameof(ArcSegmentPoint),
                o => o.ArcSegmentPoint,
                (o, v) => o.ArcSegmentPoint = v);

        public static readonly DirectProperty<ZTCircularProgress, Size> ArcSegmentSizeProperty =
            AvaloniaProperty.RegisterDirect<ZTCircularProgress, Size>(
                nameof(ArcSegmentSize),
                o => o.ArcSegmentSize,
                (o, v) => o.ArcSegmentSize = v);

        public static readonly DirectProperty<ZTCircularProgress, bool> ArcSegmentIsLargeArcProperty =
            AvaloniaProperty.RegisterDirect<ZTCircularProgress, bool>(
                nameof(ArcSegmentIsLargeArc),
                o => o.ArcSegmentIsLargeArc,
                (o, v) => o.ArcSegmentIsLargeArc = v);

        public IBrush StrokeBrush
        {
            get => GetValue(StrokeBrushProperty);
            set => SetValue(StrokeBrushProperty, value);
        }

        public int StrokeThickness
        {
            get => GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public double ProgressValue
        {
            get => GetValue(ProgressValueProperty);
            set => SetValue(ProgressValueProperty, value);
        }

        public bool IsIndeterminate
        {
            get => GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }


        public int PathFigureWidth
        {
            get => _pathFigureWidth;
            private set => SetAndRaise(PathFigureWidthProperty, ref _pathFigureWidth, value);
        }

        public int PathFigureHeight
        {
            get => _pathFigureHeight;
            private set => SetAndRaise(PathFigureHeightProperty, ref _pathFigureHeight, value);
        }

        public Thickness PathFigureMargin
        {
            get => _pathFigureMargin;
            private set => SetAndRaise(PathFigureMarginProperty, ref _pathFigureMargin, value);
        }

        public Point PathFigureStartPoint
        {
            get => _pathFigureStartPoint;
            private set => SetAndRaise(PathFigureStartPointProperty, ref _pathFigureStartPoint, value);
        }

        public Point ArcSegmentPoint
        {
            get => _arcSegmentPoint;
            private set => SetAndRaise(ArcSegmentPointProperty, ref _arcSegmentPoint, value);
        }

        public Size ArcSegmentSize
        {
            get => _arcSegmentSize;
            private set => SetAndRaise(ArcSegmentSizeProperty, ref _arcSegmentSize, value);
        }

        public bool ArcSegmentIsLargeArc
        {
            get => _arcSegmentIsLargeArc;
            private set => SetAndRaise(ArcSegmentIsLargeArcProperty, ref _arcSegmentIsLargeArc, value);
        }

        public ZTCircularProgress()
        {
            _pathFigureMargin = new Thickness();
            _pathFigureStartPoint = new Point();
            _arcSegmentPoint = new Point();
            _arcSegmentSize = new Size();
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == StrokeBrushProperty ||
                e.Property == StrokeThicknessProperty ||
                e.Property == ProgressValueProperty)
            {
                RenderArc();
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            _radius = availableSize.Height / 2;
            _radius -= StrokeThickness;
            RenderArc();
            return new Size(_radius * 2, _radius * 2);
        }

        private void RenderArc()
        {
            double percentage = ProgressValue / 100;

            var angle = percentage * 360;

            var startPoint = new Point(_radius, 0);
            var endPoint = ComputeCartesianCoordinate(angle, _radius);
            endPoint += new Point(_radius, _radius);

            PathFigureWidth = (int)_radius * 2 + StrokeThickness;
            PathFigureHeight = (int)_radius * 2 + StrokeThickness;
            PathFigureMargin = new Thickness(StrokeThickness, StrokeThickness, 0, 0);

            var largeArc = angle > 180.0;

            var outerArcSize = new Size(_radius, _radius);


            PathFigureStartPoint = startPoint;

            if (Math.Abs(startPoint.X - Math.Round(endPoint.X)) < 0.01 &&
                Math.Abs(startPoint.Y - Math.Round(endPoint.Y)) < 0.01)
            {
                endPoint -= new Point(0.01, 0);
            }

            ArcSegmentPoint = endPoint;
            ArcSegmentSize = outerArcSize;
            ArcSegmentIsLargeArc = largeArc;
        }

        private static Point ComputeCartesianCoordinate(double angle, double radius)
        {
            var angleRad = (Math.PI / 180.0) * (angle - 90);

            var x = radius * Math.Cos(angleRad);
            var y = radius * Math.Sin(angleRad);

            return new Point(x, y);
        }

    }
}
