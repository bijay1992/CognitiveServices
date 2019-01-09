 using IntelligenceMicrosoftAI.Views;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IntelligenceMicrosoftAI.Controls
{
    public sealed partial class AgeGenderDistributionControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
            "HeaderText",
            typeof(string),
            typeof(AgeGenderDistributionControl),
            new PropertyMetadata("Demographics")
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
            typeof(AgeGenderDistributionControl),
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
            typeof(AgeGenderDistributionControl),
            new PropertyMetadata(Visibility.Collapsed)
            );

        public Visibility SubHeaderVisibility
        {
            get { return (Visibility)GetValue(SubHeaderVisibilityProperty); }
            set { SetValue(SubHeaderVisibilityProperty, (Visibility)value); }
        }

        public AgeGenderDistributionControl()
        {
            this.InitializeComponent();
        }

        public void UpdateData(DemographicsData data)
        {
            int totalPeople = data.OverallFemaleCount + data.OverallMaleCount;

            this.group0to15Bar.Update(data.AgeGenderDistribution.FemaleDistribution.Age0To15,
                                      data.AgeGenderDistribution.MaleDistribution.Age0To15,
                                      (double)(data.AgeGenderDistribution.FemaleDistribution.Age0To15 + data.AgeGenderDistribution.MaleDistribution.Age0To15) / totalPeople);

            this.group16to19Bar.Update(data.AgeGenderDistribution.FemaleDistribution.Age16To19,
                                      data.AgeGenderDistribution.MaleDistribution.Age16To19,
                                      (double)(data.AgeGenderDistribution.FemaleDistribution.Age16To19 + data.AgeGenderDistribution.MaleDistribution.Age16To19) / totalPeople);

            this.group20sBar.Update(data.AgeGenderDistribution.FemaleDistribution.Age20s,
                                      data.AgeGenderDistribution.MaleDistribution.Age20s,
                                      (double)(data.AgeGenderDistribution.FemaleDistribution.Age20s + data.AgeGenderDistribution.MaleDistribution.Age20s) / totalPeople);

            this.group30sBar.Update(data.AgeGenderDistribution.FemaleDistribution.Age30s,
                                      data.AgeGenderDistribution.MaleDistribution.Age30s,
                                      (double)(data.AgeGenderDistribution.FemaleDistribution.Age30s + data.AgeGenderDistribution.MaleDistribution.Age30s) / totalPeople);

            this.group40sBar.Update(data.AgeGenderDistribution.FemaleDistribution.Age40s,
                          data.AgeGenderDistribution.MaleDistribution.Age40s,
                          (double)(data.AgeGenderDistribution.FemaleDistribution.Age40s + data.AgeGenderDistribution.MaleDistribution.Age40s) / totalPeople);

            this.group50sAndOlderBar.Update(data.AgeGenderDistribution.FemaleDistribution.Age50sAndOlder,
                          data.AgeGenderDistribution.MaleDistribution.Age50sAndOlder,
                          (double)(data.AgeGenderDistribution.FemaleDistribution.Age50sAndOlder + data.AgeGenderDistribution.MaleDistribution.Age50sAndOlder) / totalPeople);

            this.overallFemaleTextBlock.Text = data.OverallFemaleCount.ToString();
            this.overallMaleTextBlock.Text = data.OverallMaleCount.ToString();
        }
    }

    [XmlType]
    [XmlRoot]
    public class DemographicsData
    {
        public DateTime StartTime { get; set; }

        public AgeGenderDistribution AgeGenderDistribution { get; set; }

        public int OverallMaleCount { get; set; }

        public int OverallFemaleCount { get; set; }

        [XmlArrayItem]
        public List<Visitor> Visitors { get; set; }
    }

    [XmlType]
    public class Visitor
    {
        [XmlAttribute]
        public Guid UniqueId { get; set; }

        [XmlAttribute]
        public int Count { get; set; }
    }

    [XmlType]
    public class AgeDistribution
    {
        public int Age0To15 { get; set; }
        public int Age16To19 { get; set; }
        public int Age20s { get; set; }
        public int Age30s { get; set; }
        public int Age40s { get; set; }
        public int Age50sAndOlder { get; set; }
    }

    [XmlType]
    public class AgeGenderDistribution
    {
        public AgeDistribution MaleDistribution { get; set; }
        public AgeDistribution FemaleDistribution { get; set; }
    }
}
