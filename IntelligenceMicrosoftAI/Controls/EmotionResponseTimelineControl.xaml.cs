

using Microsoft.ProjectOxford.Common.Contract;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IntelligenceMicrosoftAI.Controls
{
    public sealed partial class EmotionResponseTimelineControl : UserControl
    {
        public EmotionResponseTimelineControl()
        {
            this.InitializeComponent();
        }

        private double leftMargin;
        public void DrawEmotionData(EmotionScores emotionScores)
        {
            if (leftMargin >= graph.ActualWidth)
            {
                leftMargin = 0;
                graph.Children.Clear();
            }

            EmotionResponseStackBarControl stackBar = new EmotionResponseStackBarControl
            {
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            stackBar.Margin = new Thickness(leftMargin += (stackBar.Width * 1.5), 0, 0, 0);
            stackBar.DrawEmotionData(emotionScores);

            graph.Children.Add(stackBar);
        }
    }
}
