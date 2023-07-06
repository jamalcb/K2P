using System.Diagnostics;
using K2p.ViewModels;
using K2p.Views;
//using Microsoft.Practices.Unity;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using K2p.Statics;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace K2p
{
  public partial class App : PrismApplication
  {
    /* 
     * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
     * This imposes a limitation in which the App class must have a default constructor. 
     * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
     */
    public App() : this(null) { }

    public App(IPlatformInitializer initializer) : base(initializer) { }

    protected override async void OnInitialized()
    {
      InitializeComponent();
      Utils.MainPageIsLoading = true; // hack! // todo
      await NavigationService.NavigateAsync("NavigationPage/MainPage");
      Utils.MainPageIsLoading = false;
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry) // protected override void RegisterTypes()
    {
      containerRegistry.RegisterForNavigation<NavigationPage>();
      containerRegistry.RegisterForNavigation<MainPage>();
      // in this case,   prism:ViewModelLocator.AutowireViewModel="True" in xxxPage.xaml isn't needed.  faster
      //     containerRegistry.RegisterForNavigation<ConnectPage, ConnectPageViewModel>();
      containerRegistry.RegisterForNavigation<ConnectPage>();
      containerRegistry.RegisterForNavigation<ReadbackPage>();
      containerRegistry.RegisterForNavigation<FadersPage>();
      containerRegistry.RegisterForNavigation<DelayPage>();
      containerRegistry.RegisterForNavigation<ThirdOctavePage>();
      containerRegistry.RegisterForNavigation<SettingsPage>();
      containerRegistry.RegisterForNavigation<ToneControlsPage>();
      containerRegistry.RegisterForNavigation<LimitersPage>();
      containerRegistry.RegisterForNavigation<ParaPage>();
      containerRegistry.RegisterForNavigation<TrimsPage>();
      containerRegistry.RegisterForNavigation<CrossoverPage, CrossoverPageViewModel>();
      containerRegistry.RegisterForNavigation<VuMetersPage, VuMetersPageViewModel>();
      containerRegistry.RegisterForNavigation<OptionsPage, OptionsPageViewModel>();
      containerRegistry.RegisterForNavigation<TouchParametric, TouchParametricViewModel>();
    }
    protected override void OnStart()
    {
      AppCenter.Start("android=2511fdeb-c759-4b85-994b-ad1a0d2034b7;",
        typeof(Analytics), typeof(Crashes), typeof(Push));
    }
    protected override void OnSleep()
    {

    }
    protected override void OnResume()
    {
      Debug.WriteLine("OnResume");
     // Settings.GetSettings();
    }




  }
}
