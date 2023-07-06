using K2p.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class OptionsPage : ContentPage
  {
    private OptionsPageViewModel _vm;
    public OptionsPage()
    {
      InitializeComponent();
      if (BindingContext is OptionsPageViewModel vm)
      {
        _vm = vm;
        vm.AllowUpdates = true;
      }
    }
    protected override void OnDisappearing()
    {
      _vm.AllowUpdates = false;
      base.OnDisappearing();
    }

    
  }
}
