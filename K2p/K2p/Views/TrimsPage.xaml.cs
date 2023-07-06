using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K2p.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrimsPage : ContentPage
	{
    private TrimsPageViewModel _vm;
    public TrimsPage ()
		{
			InitializeComponent ();
		}
    protected override void OnBindingContextChanged() // happens after FadersPage
    {
      base.OnBindingContextChanged();
      if (BindingContext is TrimsPageViewModel vm)
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