using System.Diagnostics;
using K2p.Statics;
using K2p.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class VuMetersPage : ContentPage
  {
    VuMetersPageViewModel vm;
    public VuMetersPage()
    {
      InitializeComponent();
      vm = (VuMetersPageViewModel) BindingContext;
    }
    //protected override bool OnBackButtonPressed()
    //{
    //  vm._timer.Dispose();
    //  if (vm.PreventBackButton) return true;
    //  return base.OnBackButtonPressed();
    //}


    public static bool IsActive;
    protected override void OnSizeAllocated(double width, double height)
    {
      Settings.GetDirtyEnabled = false;

      var vm = (VuMetersPageViewModel)BindingContext;
      vm.ShowPlotPortrait = height > width;
      Utils.VuMetersPageView = this;
      VuMetersPageViewModel.TimerIsEnabled = true;
      IsActive = true;
      base.OnSizeAllocated(width, height); //must be called
    }

    protected override void OnDisappearing()
    {
      if(TCP.IsConnected) TCP.WiFiStream.Flush();
    //  Debug.WriteLine("OnDisappearing()");
      VuMetersPageViewModel.TimerIsEnabled = false;
      Settings.GetDirtyEnabled = true;
      IsActive = false;
      base.OnDisappearing();
    }

   
    public void Redraw0()
    {
      Device.BeginInvokeOnMainThread(() =>
      {
        CanvasView0?.InvalidateSurface();
      });

   
    }
    public void Redraw1()
    {
      Device.BeginInvokeOnMainThread(() =>
      {
        CanvasView1?.InvalidateSurface();
      });
    }
    public void Redraw2()
    {
      Device.BeginInvokeOnMainThread(() =>
      {
        CanvasView2?.InvalidateSurface();
      });
    }
    public void Redraw3()
    {
      Device.BeginInvokeOnMainThread(() =>
      {
        CanvasView3?.InvalidateSurface();
      });
    }


    //private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    //{
    //  var surface = e.Surface;
    //  var canvas = surface.Canvas;
    //  canvas.Clear(SKColors.DarkOliveGreen);

    //  //float width = e.Info.Width;
    //  //float height = e.Info.Height;
    //}
  }
}
