using K2p.ViewModels;
using Xamarin.Forms;

namespace K2p.Views
{
  public partial class ParaPage : ContentPage
  {
    public ParaPage()
    {
      InitializeComponent();
      if (BindingContext is ParaPageViewModel vm)
      {
        vm.AllowSend = true;
      }
    }
   


  }
}
