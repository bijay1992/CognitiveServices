

using IntelligenceMicrosoftAI.Views;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IntelligenceMicrosoftAI.Controls
{
    public sealed partial class OverallStatsControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
            "HeaderText",
            typeof(string),
            typeof(OverallStatsControl),
            new PropertyMetadata("Total Faces")
            );

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, (string)value); }
        }

        public static readonly DependencyProperty SubHeaderTextProperty =
            DependencyProperty.Register(
            "SubHeaderText",
            typeof(string),
            typeof(OverallStatsControl),
            new PropertyMetadata("")
            );

        public string SubHeaderText
        {
            get { return (string)GetValue(SubHeaderTextProperty); }
            set { SetValue(SubHeaderTextProperty, (string)value); }
        }

        public static readonly DependencyProperty SubHeaderVisibilityProperty =
            DependencyProperty.Register(
            "SubHeaderVisibility",
            typeof(Visibility),
            typeof(OverallStatsControl),
            new PropertyMetadata(Visibility.Collapsed)
            );

        public Visibility SubHeaderVisibility
        {
            get { return (Visibility)GetValue(SubHeaderVisibilityProperty); }
            set { SetValue(SubHeaderVisibilityProperty, (Visibility)value); }
        }

        public OverallStatsControl()
        {
            this.InitializeComponent();
        }

        public void UpdateData(DemographicsData data)
        {
            this.facesProcessedTextBlock.Text = data.Visitors.Sum(v => v.Count).ToString();
            this.uniqueFacesCountTextBlock.Text = data.Visitors.Count.ToString();
        }
    }
}
