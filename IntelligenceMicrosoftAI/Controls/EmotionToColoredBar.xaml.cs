

using Microsoft.ProjectOxford.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace IntelligenceMicrosoftAI.Controls
{
    public partial class EmotionToColoredBar: UserControl
    {
        private Dictionary<String, SolidColorBrush> emotionToColorMapping = new Dictionary<string, SolidColorBrush>();

        public static SolidColorBrush AngerColor = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x54, 0x2c));
        public static SolidColorBrush ContemptColor = new SolidColorBrush(Color.FromArgb(0xff, 0xce, 0x2d, 0x90));
        public static SolidColorBrush DisgustColor = new SolidColorBrush(Color.FromArgb(0xff, 0x8c, 0x43, 0xbd));
        public static SolidColorBrush FearColor = new SolidColorBrush(Color.FromArgb(0xff, 0xfe, 0xb5, 0x52));
        public static SolidColorBrush HappinessColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x4f, 0xc7, 0x45));
        public static SolidColorBrush NeutralColor = new SolidColorBrush(Color.FromArgb(0xff, 0xaf, 0xaf, 0xaf));
        public static SolidColorBrush SadnessColor = new SolidColorBrush(Color.FromArgb(0xff, 0x47, 0x8b, 0xcb));
        public static SolidColorBrush SurpriseColor = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xf6, 0xd6));

        public EmotionToColoredBar()
        {
            emotionToColorMapping.Add("Anger", AngerColor);
            emotionToColorMapping.Add("Contempt", ContemptColor);
            emotionToColorMapping.Add("Disgust", DisgustColor);
            emotionToColorMapping.Add("Fear", FearColor);
            emotionToColorMapping.Add("Happiness", HappinessColor);
            emotionToColorMapping.Add("Neutral", NeutralColor);
            emotionToColorMapping.Add("Sadness", SadnessColor);
            emotionToColorMapping.Add("Surprise", SurpriseColor);

            InitializeComponent();
        }

        public void UpdateEmotion(EmotionScores scores)
        {
            var topEmotion = scores.ToRankedList().First();

            this.filledBar.Background = this.emotionToColorMapping[topEmotion.Key];
            this.emptySpaceRowDefinition.Height = new GridLength(1 - topEmotion.Value, Windows.UI.Xaml.GridUnitType.Star);
            this.filledSpaceRowDefinition.Height = new GridLength(topEmotion.Value, Windows.UI.Xaml.GridUnitType.Star);
        }
    }
}