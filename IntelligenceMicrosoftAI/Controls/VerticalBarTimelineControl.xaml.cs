

using Microsoft.ProjectOxford.Common.Contract;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IntelligenceMicrosoftAI.Controls
{
    public enum BarType
    {
        Centered,
        UpDown
    }

    public enum WrapBehavior
    {
        Clear,
        Slide
    }

    public sealed partial class VerticalBarTimelineControl : UserControl
    {
        static SolidColorBrush DefaultBarColor = new SolidColorBrush(Color.FromArgb(0xaa, 0xff, 0xff, 0xff));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
            "Header",
            typeof(string),
            typeof(VerticalBarTimelineControl),
            new PropertyMetadata("")
            );

        public static readonly DependencyProperty BarTypeProperty =
            DependencyProperty.Register(
            "BarType",
            typeof(BarType),
            typeof(VerticalBarTimelineControl),
            new PropertyMetadata(BarType.UpDown)
            );

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public BarType BarType
        {
            get { return (BarType)GetValue(BarTypeProperty); }
            set { SetValue(BarTypeProperty, value); }
        }

        public VerticalBarTimelineControl()
        {
            this.InitializeComponent();
        }

        private double leftMargin;
        public void DrawDataPoint(double value, Brush barColor = null, Image toolTip = null, WrapBehavior wrapBehavior = WrapBehavior.Clear)
        {
            if (leftMargin >= graph.ActualWidth)
            {
                if (wrapBehavior == WrapBehavior.Clear)
                {
                    leftMargin = 0;
                    graph.Children.Clear();
                }
                else
                {
                    // Remove first element and shift all the others to the left by 1
                    graph.Children.RemoveAt(0);

                    double widthPerChild = 6;
                    for (int i = 0; i < graph.Children.Count; i++)
                    {
                        (graph.Children[i] as Control).Margin = new Thickness(widthPerChild * i, 0, 0, 0);
                    }

                    leftMargin -= widthPerChild;

                    // Remove 20% element of the elements (from the beginning) and shift all the others to the left by that ammount
                    //int removeCount = graph.Children.Count / 5;
                    //for (int i = 0; i < removeCount; i++)
                    //{
                    //    graph.Children.RemoveAt(0);
                    //}

                    //double widthPerChild = 6;
                    //for (int i = 0; i < graph.Children.Count; i++)
                    //{
                    //    (graph.Children[i] as Control).Margin = new Thickness(widthPerChild * i, 0, 0, 0);
                    //}

                    //leftMargin = (graph.Children.Count - 1) * widthPerChild;
                }
            }

            Control bar;
            if (this.BarType == BarType.UpDown)
            {
                var upDownBar = new UpDownVerticalBarControl();
                upDownBar.DrawDataPoint(value, barColor != null ? barColor : DefaultBarColor, toolTip);

                bar = upDownBar;
            }
            else
            {
                var centeredBar = new CenteredVerticalBarControl();
                centeredBar.DrawDataPoint(value, barColor != null ? barColor : DefaultBarColor, toolTip);

                bar = centeredBar;
            }

            bar.Width = 4;
            bar.HorizontalAlignment = HorizontalAlignment.Left;
            bar.Margin = new Thickness(leftMargin += (bar.Width + 2), 0, 0, 0);

            graph.Children.Add(bar);
        }

        public void Clear()
        {
            leftMargin = 0;
            graph.Children.Clear();
        }
    }
}
