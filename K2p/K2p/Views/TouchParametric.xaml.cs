using K2p.Statics;
using K2p.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TouchParametric : ContentPage
  {
    public TouchParametric()
    {
      InitializeComponent();
      Utils.TouchParametricMainView = this;
      if (BindingContext is TouchParametricViewModel vm)
      {
        vm.AllowSend = true;
      }
    
    }
    public void Redraw() => CanvasView?.InvalidateSurface();   
  }
}
