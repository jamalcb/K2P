using Foundation;
//using HockeyApp.iOS;
using UIKit;
//using Microsoft.Practices.Unity;
using Prism;
using Prism.Unity;
using Unity;
using Prism.Ioc;

namespace K2p.iOS
{
  // The UIApplicationDelegate for the application. This class is responsible for launching the 
  // User Interface of the application, as well as listening (and optionally responding) to 
  // application events from iOS.
  [Register("AppDelegate")]
  public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
  {
    //
    // This method is invoked when the application has loaded and is ready to run. In this 
    // method you should instantiate the window, load the UI into it and then make the window
    // visible.
    //
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    //

    private string _appId = "7fc81f5bd655485e88c8d2c6585ff0d8";
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {

      global::Xamarin.Forms.Forms.Init();
      LoadApplication(new App(new iOSInitializer()));

      return base.FinishedLaunching(app, options);
    }
  }

  public class iOSInitializer : IPlatformInitializer
  {
    //public void RegisterTypes(IUnityContainer container)
    //{

    //}

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {

    }
  }
}
