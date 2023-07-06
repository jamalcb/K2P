using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace K2p.UserControls
{
  //[XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class PeqUserControl : Grid
  {
    public static readonly BindableProperty TextProperty =
      BindableProperty.Create(nameof(Text), typeof(string), typeof(PeqUserControl));

    public PeqUserControl()
    {
      InitializeComponent();
    }

    public string Text
    {
      get => (string) GetValue(TextProperty);
      set => SetValue(TextProperty, value);
    }
  }
}