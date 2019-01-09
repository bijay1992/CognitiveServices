

using Microsoft.ProjectOxford.Common.Contract;
using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IntelligenceMicrosoftAI.Controls
{
    public sealed partial class EmotionResponseStackBarControl : UserControl
    {
        public static SolidColorBrush PositiveResponseColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB8, 0xAA));
        public static SolidColorBrush NegativeResponseColor = new SolidColorBrush(Color.FromArgb(0xFF, 0xFD, 0x62, 0x5E));
        public static SolidColorBrush NeutralColor = new SolidColorBrush(Colors.Transparent);

        public EmotionResponseStackBarControl()
        {
            this.InitializeComponent();

            //Background = new SolidColorBrush(Colors.LightGray);
        }

        public void DrawEmotionData(EmotionScores emotionScores)
        {
            double positiveEmotionResponse = Math.Min(emotionScores.Happiness + emotionScores.Surprise, 1);
            double negativeEmotionResponse = Math.Min(emotionScores.Sadness + emotionScores.Fear + emotionScores.Disgust + emotionScores.Contempt, 1);
            double netResponse = positiveEmotionResponse - negativeEmotionResponse;

            if (netResponse > 0)
            {
                netResponse *= 0.5;
                topRowDefinition.Height = new GridLength(0.5 - netResponse, GridUnitType.Star);
                positiveResponseRowDefinition.Height = new GridLength(netResponse, GridUnitType.Star);
                negativeResponseRowDefinition.Height = new GridLength(0);
                bottomRowDefinition.Height = new GridLength(0.5, GridUnitType.Star);
            }
            else
            {
                netResponse *= -0.5;
                topRowDefinition.Height = new GridLength(0.5, GridUnitType.Star);
                positiveResponseRowDefinition.Height = new GridLength(0);
                negativeResponseRowDefinition.Height = new GridLength(netResponse, GridUnitType.Star);
                bottomRowDefinition.Height = new GridLength(0.5 - netResponse, GridUnitType.Star);
            }

            Border slice = new Border { Background = NeutralColor };
            Grid.SetRow(slice, 0);
            graph.Children.Add(slice);

            slice = new Border { Background = PositiveResponseColor };
            Grid.SetRow(slice, 1);
            graph.Children.Add(slice);

            slice = new Border { Background = NegativeResponseColor };
            Grid.SetRow(slice, 2);
            graph.Children.Add(slice);

            slice = new Border { Background = NeutralColor };
            Grid.SetRow(slice, 3);
            graph.Children.Add(slice);
        }
    }
}
