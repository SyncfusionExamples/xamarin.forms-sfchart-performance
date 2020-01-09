using AppKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace ChartPerformance.macOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override void DidFinishLaunching(NSNotification notification)
        {
            Forms.Init();
            Syncfusion.SfChart.XForms.MacOS.SfChartRenderer.Init();
            LoadApplication(new App());
            base.DidFinishLaunching(notification);
            // Insert code here to initialize your application
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
        NSWindow _window;

        public override NSWindow MainWindow
        {
            get { return _window; }
        }


        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled;

            var rect = new CoreGraphics.CGRect(200, 200, 800, 600);
            _window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            _window.Title = "Chart Performance";
        }
    }

}
