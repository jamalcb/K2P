using Android.App;
using Android.Content.PM;
using Android.OS;
//using HockeyApp.Android;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Prism;
using Unity;
using Prism.Ioc;

namespace K2p.Droid
{
  [Activity(Label = "K2p", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    //private string _appId = "7fc81f5bd655485e88c8d2c6585ff0d8";
    protected override void OnResume()
    {
      base.OnResume();
      //     CrashManager.Register(this,_appId);
    }
    protected override void OnCreate(Bundle bundle)
    {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(bundle);

      global::Xamarin.Forms.Forms.Init(this, bundle);
      LoadApplication(new App(new AndroidInitializer()));
      //CheckForUpdates();

    //  AppCenter.Start("{Your App Secret}", typeof(Analytics), typeof(Crashes));

    }
    protected override void OnPause()
    {
      base.OnPause();
     // UnregisterManagers();
    }
    protected override void OnDestroy()
    {
      base.OnDestroy();
     // UnregisterManagers();
    }



    //private void CheckForUpdates()
    //{
    //  // Remove this for store builds!
    //  UpdateManager.Register(this, _appId);
    //}
    //private void UnregisterManagers()
    //{
    //  UpdateManager.Unregister();
    //}


  }


  public class AndroidInitializer : IPlatformInitializer
  {

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {

    }
  }
}

