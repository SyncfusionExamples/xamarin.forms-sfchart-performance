using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChartPerformance
{
    class MainPageViewModel : INotifyPropertyChanged
    {

        public ICommand UpdateDataCommand { get; set; }

        private const int count = 5 * 100000; //500K data

        private const double startValue = 10;

        private bool isBusy;

        private bool isDataGenerated = true;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value) return;
                isBusy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBusy"));
            }
        }

        private ObservableCollection<Data> data;

        public ObservableCollection<Data> Data
        {
            get { return data; }
            set
            {
                if (data == value) return;
                data = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Data"));
                MeasureRenderingTime();
            }
        }

        private string renderingTime;

        public string RenderingTime
        {
            get { return renderingTime; }
            set
            {
                if (renderingTime == value) return;
                renderingTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RenderingTime"));
            }
        }

        //This is used to avoid the data points generation time.
        private ObservableCollection<Data> tempData;

        public MainPageViewModel()
        {
            Data = new ObservableCollection<ChartPerformance.Data>();

            GenerateData();

            UpdateDataCommand = new Command(e =>
            {
                if (isDataGenerated)
                {
                    Data = tempData;
                    GenerateData();
                }
                else
                {
                    IsBusy = true;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void GenerateData()
        {
            if (!isDataGenerated) return;

            isDataGenerated = false;

            await Task.Run(() =>
            {
                tempData = new ObservableCollection<Data>();

                Random rand = new Random();
                double value = startValue;

                for (int i = 0; i < count; i++)
                {

                    if (rand.NextDouble() > 0.5)
                        value += rand.NextDouble();
                    else
                        value -= rand.NextDouble();

                    tempData.Add(new Data { YValue = value, XValue = i });
                }
                isDataGenerated = true;

                if (IsBusy)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Data = tempData;

                    });
                }

                IsBusy = false;
            });
        }

        private void MeasureRenderingTime()
        {
            var start = DateTime.Now;

            Device.BeginInvokeOnMainThread(() =>
            {
                var end = DateTime.Now;

                RenderingTime = (end - start).TotalMilliseconds + "ms";
            });
        }
    }
}