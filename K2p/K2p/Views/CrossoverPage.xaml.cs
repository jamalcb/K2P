using K2p.Statics;
using K2p.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class CrossoverPage : ContentPage
  {
    private CrossoverPageViewModel _vm;
    public CrossoverPage()
    {
      InitializeComponent();
      //if (BindingContext is CrossoverPageViewModel vm)
      //{
      //  vm.AllowSend = true;
      //}
      Utils.CrossoverPageView = this;
    }
    protected override void OnBindingContextChanged() // happens after FadersPage
    {
      base.OnBindingContextChanged();
      if (BindingContext is CrossoverPageViewModel vm)
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
    protected override void OnSizeAllocated(double width, double height)
    {
      base.OnSizeAllocated(width, height); //must be called

      var vm = (CrossoverPageViewModel)BindingContext;
      vm.ShowPlotPortrait = height > width;

    }
    public void Redraw()
    {
      CanvasView?.InvalidateSurface();
    }
    //public void RedrawCh1()
    //{
    //  CanvasViewCh1?.InvalidateSurface();
    //}


    //private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    //{
    //  var surface = e.Surface;
    //  var canvas = surface.Canvas;
    //  canvas.Clear(SKColors.DarkOliveGreen);
    //}

  }
}
