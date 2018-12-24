using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Syncfusion.SfChart.XForms;

namespace ChartPerformance.Droid
{
    [Activity(Label = "ChartPerformance", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public object MainPage { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            RequestedOrientation = ScreenOrientation.Landscape;

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var app = new App();

            LoadApplication(app);

            MainPage = app.MainPage;
        }
    }
}