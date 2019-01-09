

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IntelligenceMicrosoftAI.Controls
{
    public sealed partial class SentimentDistributionControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
            "HeaderText",
            typeof(string),
            typeof(SentimentDistributionControl),
            new PropertyMetadata("Distribution")
            );

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, (string)value); }
        }

        public SentimentDistributionControl()
        {
            this.InitializeComponent();
        }

        public void UpdateData(IEnumerable<double> sentiments)
        {
            if (sentiments.Any())
            {
                this.chartHostGrid.Visibility = Visibility.Visible;

                // group at one decimal point precision
                var sentimentGroups = sentiments.GroupBy(s => Math.Round(s, 1));
                int largestGroupSize = sentimentGroups.OrderByDescending(g => g.Count()).First().Count();

                var barCharts = this.chartGrid.Children.Where(c => typeof(VerticalBarWithValueControl) == c.GetType()).Cast<VerticalBarWithValueControl>().ToArray();

                for (int i = 0; i < barCharts.Length; i++)
                {
                    var group = sentimentGroups.FirstOrDefault(g => (g.Key * 10) == i);
                    if (group != null)
                    {
                        barCharts[i].Update(group.Count(), 0, (double)group.Count() / largestGroupSize);
                    }
                    else
                    {
                        barCharts[i].Update(0, 0, 0);
                    }
                }

                int positivePercentage = (sentiments.Where(s => s >= 0.5).Count() * 100) / sentiments.Count();
                this.overallPositiveTextBlock.Text = string.Format("{0}%", positivePercentage);
                this.overallNegativeTextBlock.Text = string.Format("{0}%", 100 - positivePercentage);
            }
            else
            {
                this.overallPositiveTextBlock.Text = this.overallNegativeTextBlock.Text = "";

                var barCharts = this.chartGrid.Children.Where(c => typeof(VerticalBarWithValueControl) == c.GetType()).Cast<VerticalBarWithValueControl>().ToArray();

                for (int i = 0; i < barCharts.Length; i++)
                {
                    barCharts[i].Update(0, 0, 0);
                }

                this.chartHostGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
