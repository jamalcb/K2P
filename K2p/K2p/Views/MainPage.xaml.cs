using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Xamarin.Forms;

//https://jimblizzard.wordpress.com/2016/08/09/building-cross-platform-xamarin-forms-apps-in-vsts/ 

namespace K2p.Views
{
  public partial class MainPage
  {
    
    public MainPage ()
    {
      InitializeComponent ();
      if (Device.RuntimePlatform == Device.iOS)
      {
        Padding = new Thickness(0, 20, 0, 0);
      }
    }
 
  }
}