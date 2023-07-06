using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K2p.Statics;
using K2p.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ThirdOctavePage : ContentPage
  {
    //private Dictionary<long, SKPath> temporaryPaths = new Dictionary<long, SKPath>();
    //private List<SKPath> paths = new List<SKPath>();

    public ThirdOctavePage()
    {
      InitializeComponent();
      if (BindingContext is ThirdOctavePageViewModel vm)
      {
        vm.AllowSend = true;
      }
      Utils.ThirdOctavePageMainView = this;
    }

    public string TestVal = "25.0";

    public void Redraw() => CanvasView?.InvalidateSurface();    
  }
}