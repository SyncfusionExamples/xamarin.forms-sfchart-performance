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

        #region private fields

        private const int count = 1 * 500000; //500K data

        private const double startValue = 10;

        private bool isBusy = false;

        private ObservableCollection<Data> data;

        private DateTime start = DateTime.Now;

        private string renderingTime;

        //This is used to avoid the data points generation time.
        private ObservableCollection<Data> tempData;

        #endregion

        #region public properties

        public event PropertyChangedEventHandler PropertyChanged;

        public Action BeginDataUpdate;
        public Action EndDataUpdate;

        public ICommand UpdateDataCommand { get; set; }
        public ICommand AddDataCommand { get; set; }

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

        public ObservableCollection<Data> Data
        {
            get { return data; }
            set
            {
                if (data == value) return;
                data = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Data"));

            }
        }

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
        #endregion

        public MainPageViewModel()
        {
            Data = new ObservableCollection<ChartPerformance.Data>();

            GenerateData();

            UpdateDataCommand = new Command(e =>
            {
                GenerateData();

            });

            AddDataCommand = new Command(e =>
            {
                AddData(50);

            });
        }


        private async void GenerateData()
        {
            if (Device.RuntimePlatform != Device.UWP)
            {
                await Task.Run(() =>
                {
                    LoadData();
                });
            }
            else
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            IsBusy = true;

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

            start = DateTime.Now;

            Data = tempData;
        }

        void AddData(int dataCount)
        {
            BeginDataUpdate();
            Random rand = new Random();
            double value = startValue;

            for (int i = 0; i < dataCount; i++)
            {
                if (rand.NextDouble() > 0.5)
                    value += rand.NextDouble();
                else
                    value -= rand.NextDouble();

                Data.Add(new Data { YValue = value, XValue = i });
            }

            EndDataUpdate();
        }

        public void MeasureRenderingTime()
        {
            var end = DateTime.Now;

            RenderingTime = (end - start).TotalMilliseconds + "ms";

            IsBusy = false;
        }
    }
}
