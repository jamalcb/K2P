using K2p.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FadersPage : ContentPage
	{
    private FadersPageViewModel _vm;
		public FadersPage ()
		{
			InitializeComponent ();
      //if (BindingContext is FadersPageViewModel vm)
      //{
      //  _vm = vm;
      //  vm.AllowUpdates = true;
      //}
    }

    protected override void OnBindingContextChanged() // happens after FadersPage
    {
      base.OnBindingContextChanged();
      if (BindingContext is FadersPageViewModel vm)
      {
        _vm = vm;
        vm.AllowUpdates = true;
      }
    }
    //protected override void OnAppearing()  // happens after OnBindingContextChanged
    //{   
    //  base.OnAppearing();
    //}

    protected override void OnDisappearing()
    {
      _vm.AllowUpdates = false;
      base.OnDisappearing();
    }
  }
}