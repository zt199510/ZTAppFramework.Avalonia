using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.LogicalTree;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZTAppFramework.Template.Enums;
using static Avalonia.OpenGL.GlInterface;

namespace ZTAppFramework.Template.Controls
{
    public class ImageCore : ContentControl
    {
        CompositeDisposable _disposables;

        #region 内部成员
        public Grid grid_all = null;

        public Canvas _CenterCanvas = null;

        public Image _imageCenter = null;

        /// <summary> 用于鼠标按下拖动图片移动效果 </summary>
        public ContentControl grid_Mouse_drag = null;

        public ScrollViewer _svDefault = null;

        public Grid rootGrid = null;

        public Viewbox vb = null;

        TransformGroup tfGroup;
        #endregion

        /// <summary>
        /// Defines the <see cref="ImageSource"/> property.
        /// </summary>
        public static readonly StyledProperty<IImage> ImageSourceProperty =
            AvaloniaProperty.Register<ImageCore, IImage>(nameof(ImageSource), null);

        /// <summary>
        /// Comment
        /// </summary>
        public IImage ImageSource
        {
            get { return GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }



     


        /// <summary>
        /// Defines the <see cref="Scale"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ScaleProperty =
            AvaloniaProperty.Register<ImageCore, double>(nameof(Scale), 1.0);

        /// <summary>
        /// Comment
        /// </summary>
        public double Scale
        {
            get { return GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }



        /// <summary>
        /// Defines the <see cref="IsMaxScaled"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsMaxScaledProperty =
            AvaloniaProperty.Register<ImageCore, bool>(nameof(IsMaxScaled), false);

        /// <summary>
        /// 是否放到最大
        /// </summary>
        public bool IsMaxScaled
        {
            get { return GetValue(IsMaxScaledProperty); }
            set { SetValue(IsMaxScaledProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsMinScaled"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsMinScaledProperty =
            AvaloniaProperty.Register<ImageCore, bool>(nameof(IsMinScaled), false);

        /// <summary>
        /// 是否放到最小
        /// </summary>
        public bool IsMinScaled
        {
            get { return GetValue(IsMinScaledProperty); }
            set { SetValue(IsMinScaledProperty, value); }
        }

        #region 属性
        /// <summary> 滚轮放大倍数 </summary>
        public double WheelScale { get; set; } = 0.1;

        /// <summary> 设置最大放大倍数 </summary>
        public int MaxScale { get; set; } = 25;

        double hOffSetRate = 0;//滚动条横向位置横向百分比

        double vOffSetRate = 0;//滚动条位置纵向百分比

        /// <summary> 图片的宽度 </summary>
        public double imgWidth;

        /// <summary> 图片的高度 </summary>
        public double imgHeight;

        //double ScrollableWidth;
        //double ScrollableHeight;
        #endregion

        List<IBehavior> behaviors = new List<IBehavior>();

        public ImageCore()
        {
            //  Do：改变窗口自适应大小
        
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            grid_all = e.NameScope.Find<Grid>("PART_Grid_All");

            grid_Mouse_drag = e.NameScope.Find<ContentControl>("PART_Grid_Mouse_Drag");

            _svDefault = e.NameScope.Find<ScrollViewer>("PART_ScrollView_Default");

            rootGrid = e.NameScope.Find<Grid>("PART_Grid_Root");

            vb = e.NameScope.Find<Viewbox>("PART_ViewBox_Default");

            _CenterCanvas = e.NameScope.Find<Canvas>("PART_CenterCanvas");

            _imageCenter = e.NameScope.Find<Image>("PART_ImageCenter");

       
            //_svDefault.PointerWheelChanged += (s, e) =>
            //{
            //    var extent = _svDefault.Extent;
            //    var viewport = _svDefault.Viewport;

            //    if (extent.Width > viewport.Width)
            //        ScrollableWidth = extent.Width - viewport.Width;
            //    else
            //        ScrollableWidth = 0;

            //    if (extent.Height > viewport.Height)
            //        ScrollableHeight = extent.Height - viewport.Height;
            //    else
            //        ScrollableHeight = 0;
            //};
       
            _disposables = new CompositeDisposable()
            {
                this.WhenAnyValue(x=>x.ImageSource).Where(x => x != null).Subscribe(x =>
                {
                    _imageCenter.Source=x;
                    SetFullImage();

                }),
                this.WhenAnyValue(x => x.Scale).Subscribe(x =>
                {
                    Scale = x < 0 ? 0 : x;
                    RefreshImageByScale();
                })
            };

            InitWidthHeight();

            SetFullImage();

            this.behaviors.ForEach(l => l.RegisterBehavior());
            PropertyChanged += ImageCore_PropertyChanged;
        }

        private void ImageCore_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == Control.BoundsProperty)
            {
                this.SetFullImage();
            }
        }

        #region 通用方法


        //  Do ：初始化宽度高度
        void InitWidthHeight()
        {
            InvalidateMeasure();
            imgWidth = this.grid_Mouse_drag.Bounds.Width;
            imgHeight = this.grid_Mouse_drag.Bounds.Height;
        }

        //  Do ：初始化设置平铺
        /// <summary> 设置初始图片为平铺整个控件 </summary>
        void SetFullImage()
        {
            this.InitWidthHeight();
            if (imgWidth == 0 || imgHeight == 0)
                return;

            IsMinScaled = true;

            RefreshImageByScale();

            Scale = this.GetFullScale();
        }

        /// <summary> 当Scale改变时刷新图片大小 </summary>
        public void RefreshImageByScale()
        {
            GetOffSetRate();

            if (imgWidth < 0 || imgHeight < 0) return;

            vb.Width = Scale * imgWidth;
            vb.Height = Scale * imgHeight;
            SetOffSetByRate();

        }

        /// <summary> 当Scale变化时获取更新前水平和垂直位移 </summary>
        public void GetOffSetRate()
        {
            if (_svDefault.Bounds.Width > 0)
            {
                if (_svDefault.Offset.X != 0)
                    hOffSetRate = _svDefault.Offset.X / _svDefault.Bounds.Width;
            }
            if (_svDefault.Bounds.Height > 0)
            {
                if (_svDefault.Offset.Y != 0)
                    vOffSetRate = _svDefault.Offset.Y / _svDefault.Bounds.Height;
            }
        }

        /// <summary> 当Scale变化时设置更新后水平和垂直位移 </summary>
        internal void SetOffSetByRate()
        {
            InvalidateMeasure();
            if (_svDefault.Bounds.Width > 0)
            {
                double hOffSet = hOffSetRate * _svDefault.Bounds.Width;
                _svDefault.Offset = new Vector(hOffSet, _svDefault.Offset.Y);
            }
            if (_svDefault.Bounds.Height > 0)
            {
                double vOffSet = vOffSetRate * _svDefault.Bounds.Height;
                _svDefault.Offset = new Vector(_svDefault.Offset.X, vOffSet);
            }
        }

        /// <summary> 获取适应屏幕大小的范围 </summary>
        public double GetFullScale()
        {
            double result = _svDefault.Bounds.Width / imgWidth;

            result = Math.Min(result, _svDefault.Bounds.Height / imgHeight);

            return result - 0.01;
        }

        public void ReleaseBehaviors()
        {
            this.behaviors.ForEach(l => l.ReleaseBehavior());
        }
        public void AddBehaviors<T>(T Behavior) where T : IBehavior
        {
            this.behaviors.Add(Behavior);
        }

        #endregion


        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            ReleaseBehaviors();
            PropertyChanged -= ImageCore_PropertyChanged;
            _disposables?.Dispose();//释放 
        }
    }
}
