using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ChartPerformance
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Chart.SuspendSeriesNotification();

            //Make the required changes in items source that is not necessary to be updated in UI immediately.

            Chart.ResumeSeriesNotification();
        }
    }
}