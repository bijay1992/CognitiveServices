

using System.Linq;
using Windows.UI.Xaml.Controls;

namespace IntelligenceMicrosoftAI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class DemoLauncherPage : Page
    {
        public DemoLauncherPage()
        {
            this.InitializeComponent();

            this.DataContext = KioskExperiences.Experiences;
        }

        private void OnDemoClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(((KioskExperience)e.ClickedItem).PageType);
        }
    }
}
