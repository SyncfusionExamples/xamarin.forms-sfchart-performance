using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChartPerformance
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel.BeginDataUpdate = () => Chart.SuspendSeriesNotification();
            ViewModel.EndDataUpdate = () => Chart.ResumeSeriesNotification();
        }

        private void Chart_SeriesRendered(object sender, System.EventArgs e)
        {
            ViewModel.MeasureRenderingTime();
        }
    }
}