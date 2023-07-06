using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using K2p.Statics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace K2p.ViewModels
{
  public class TrimsPageViewModel : ViewModelBase
  {
    // private IPageDialogService _dialogService;

    public bool AllowUpdates;
    private string _masterEntryValue = "0.0";

    private string[] _trimEntry = new string[8];
  //  private double[] _trimSlider = new double[8];
    //private int[] _group_index = new int[8];
    public IList<Group> Groups => GroupData.GroupSource;
    //private Group _selectedGroupCh0;
    //private Group _selectedGroupCh1;
    //private Group _selectedGroupCh2;
    //private Group _selectedGroupCh3;
    //private Group _selectedGroupCh4;
    //private Group _selectedGroupCh5;
    //private Group _selectedGroupCh6;
    //private Group _selectedGroupCh7;

    public DelegateCommand MasterEntryCompletedCommand { get; }
    public DelegateCommand TrimEntryCompletedCommandCh0 { get; }
    public DelegateCommand TrimEntryCompletedCommandCh1 { get; }
    public DelegateCommand TrimEntryCompletedCommandCh2 { get; }
    public DelegateCommand TrimEntryCompletedCommandCh3 { get; }
    public DelegateCommand TrimEntryCompletedCommandCh4 { get; }
    public DelegateCommand TrimEntryCompletedCommandCh5 { get; }
    public DelegateCommand TrimEntryCompletedCommandCh6 { get; }
    public DelegateCommand TrimEntryCompletedCommandCh7 { get; }

    public DelegateCommand ButtonDefaultGroups { get; }

    public TrimsPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
    {
      // _dialogService = dialogService;

      Utils.TrimPageVm = this;

      MasterEntryCompletedCommand = new DelegateCommand(ExecuteMasterEntryCompleted);
      TrimEntryCompletedCommandCh0 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh0);
      TrimEntryCompletedCommandCh1 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh1);
      TrimEntryCompletedCommandCh2 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh2);
      TrimEntryCompletedCommandCh3 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh3);
      TrimEntryCompletedCommandCh4 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh4);
      TrimEntryCompletedCommandCh5 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh5);
      TrimEntryCompletedCommandCh6 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh6);
      TrimEntryCompletedCommandCh7 = new DelegateCommand(ExecuteTrimEntryCompletedCommandCh7);

      ButtonDefaultGroups = new DelegateCommand(ExecuteButtonDefaultGroups);
      _masterEntryValue = $"{Settings.MasterGain:N2}";
      // _masterSliderVal = Settings.MasterGain;
      RaisePropertyChanged("MasterEntryValue");
      RaisePropertyChanged("MasterSliderValue");
      for (var channel = 0; channel != 8; channel++)
      {
        _trimEntry[channel] = $"{Settings.Trims[channel]:N2}";      
      }
    }
    private void ExecuteButtonDefaultGroups()
    {
      for (var channel = 0; channel != 8; channel++)
      {
        Settings.TrimGroup[channel] = channel;
        RaisePropertyChanged($"SelectedIndexCh{channel}");
      }
    }

    public Color TextColorCh0 { get; set; } = Settings.mixer_2_output_brush[0];
    public Color TextColorCh1 { get; set; } = Settings.mixer_2_output_brush[1];
    public Color TextColorCh2 { get; set; } = Settings.mixer_2_output_brush[2];
    public Color TextColorCh3 { get; set; } = Settings.mixer_2_output_brush[3];
    public Color TextColorCh4 { get; set; } = Settings.mixer_2_output_brush[4];
    public Color TextColorCh5 { get; set; } = Settings.mixer_2_output_brush[5];
    public Color TextColorCh6 { get; set; } = Settings.mixer_2_output_brush[6];
    public Color TextColorCh7 { get; set; } = Settings.mixer_2_output_brush[7];

    public string LabelCh0 { get; set; } = Settings.output_labels[0];
    public string LabelCh1 { get; set; } = Settings.output_labels[1];
    public string LabelCh2 { get; set; } = Settings.output_labels[2];
    public string LabelCh3 { get; set; } = Settings.output_labels[3];
    public string LabelCh4 { get; set; } = Settings.output_labels[4];
    public string LabelCh5 { get; set; } = Settings.output_labels[5];
    public string LabelCh6 { get; set; } = Settings.output_labels[6];
    public string LabelCh7 { get; set; } = Settings.output_labels[7];

    public int SelectedIndexCh0
    {
      get => Settings.TrimGroup[0];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[0], value);
      }
    }
    public int SelectedIndexCh1
    {
      get => Settings.TrimGroup[1];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[1], value);
      }
    }
    public int SelectedIndexCh2
    {
      get => Settings.TrimGroup[2];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[2], value);
      }
    }
    public int SelectedIndexCh3
    {
      get => Settings.TrimGroup[3];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[3], value);
      }
    }
    public int SelectedIndexCh4
    {
      get => Settings.TrimGroup[4];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[4], value);
      }
    }
    public int SelectedIndexCh5
    {
      get => Settings.TrimGroup[5];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[5], value);
      }
    }
    public int SelectedIndexCh6
    {
      get => Settings.TrimGroup[6];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[6], value);
      }
    }
    public int SelectedIndexCh7
    {
      get => Settings.TrimGroup[7];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.TrimGroup[7], value);
      }
    }
     
    public double TrimMax { get; set; } = Constants.TrimMax;
    public double TrimMin { get; set; } = Constants.TrimMin;
    public double VolMax { get; set; } = Constants.VolMax;
    public double VolMin { get; set; } = Constants.VolMin;

    #region master

    private void ExecuteMasterEntryCompleted()
    {
      double.TryParse(MasterEntryValue, out var _temp);
      MasterSliderValue = _temp;
      RaisePropertyChanged("MasterEntryValue");
    }

    public string MasterEntryValue
    {
      get => _masterEntryValue;
      set
      {
        _masterEntryValue = value;
        Debug.WriteLine("MasterEntryValue " + value);
        RaisePropertyChanged();
      }
    }

    public void UpdatePage()
    {
      RaisePropertyChanged("MasterEntryValue");
      RaisePropertyChanged("MasterSliderValue");
    }

    public double MasterSliderValue
    {
      get => Settings.MasterGain;
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetMasterIncrement(value);
        if (val == Settings.MasterGain) return;
        Settings.MasterGain = val;      
        _masterEntryValue = $"{Settings.MasterGain:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("MasterEntryValue");
        Utils.MainPageVm?.UpdateMainPage();       
        Utils.SendFloat(USB_Commands.CMD_SET_MASTER_GAIN, Settings.MasterGain);
      }
    }

    #endregion master

    #region trims

    #region completeCommands

    private void ExecuteTrimEntryCompletedCommandCh0()
    {
      double.TryParse(_trimEntry[0], out var _temp);
      TrimSliderValueCh0 = _temp;
      RaisePropertyChanged("TrimEntryValueCh0");
    }

    private void ExecuteTrimEntryCompletedCommandCh1()
    {
      double.TryParse(_trimEntry[1], out var _temp);
      TrimSliderValueCh1 = _temp;
      RaisePropertyChanged("TrimEntryValueCh1");
    }

    private void ExecuteTrimEntryCompletedCommandCh2()
    {
      double.TryParse(_trimEntry[2], out var _temp);
      TrimSliderValueCh2 = _temp;
      RaisePropertyChanged("TrimEntryValueCh2");
    }

    private void ExecuteTrimEntryCompletedCommandCh3()
    {
      double.TryParse(_trimEntry[3], out var _temp);
      TrimSliderValueCh3 = _temp;
      RaisePropertyChanged("TrimEntryValueCh3");
    }

    private void ExecuteTrimEntryCompletedCommandCh4()
    {
      double.TryParse(_trimEntry[4], out var _temp);
      TrimSliderValueCh4 = _temp;
      RaisePropertyChanged("TrimEntryValueCh4");
    }

    private void ExecuteTrimEntryCompletedCommandCh5()
    {
      double.TryParse(_trimEntry[5], out var _temp);
      TrimSliderValueCh5 = _temp;
      RaisePropertyChanged("TrimEntryValueCh5");
    }

    private void ExecuteTrimEntryCompletedCommandCh6()
    {
      double.TryParse(_trimEntry[6], out var _temp);
      TrimSliderValueCh6 = _temp;
      RaisePropertyChanged("TrimEntryValueCh6");
    }

    private void ExecuteTrimEntryCompletedCommandCh7()
    {
      double.TryParse(_trimEntry[7], out var _temp);
      TrimSliderValueCh7 = _temp;
      RaisePropertyChanged("TrimEntryValueCh7");
    }

    #endregion

    #region trimEntrys

    public string TrimEntryValueCh0
    {
      get => _trimEntry[0];
      set
      {
        _trimEntry[0] = value;
        Debug.WriteLine($"TrimEntryValueCh0 {value}");
        RaisePropertyChanged();
      }
    }

    public string TrimEntryValueCh1
    {
      get => _trimEntry[1];
      set
      {
        _trimEntry[1] = value;
        Debug.WriteLine($"TrimEntryValueCh1 {value}");
        RaisePropertyChanged();
      }
    }

    public string TrimEntryValueCh2
    {
      get => _trimEntry[2];
      set
      {
        _trimEntry[2] = value;
        Debug.WriteLine($"TrimEntryValueCh2 {value}");
        RaisePropertyChanged();
      }
    }

    public string TrimEntryValueCh3
    {
      get => _trimEntry[3];
      set
      {
        _trimEntry[3] = value;
        Debug.WriteLine($"TrimEntryValueCh3 {value}");
        RaisePropertyChanged();
      }
    }

    public string TrimEntryValueCh4
    {
      get => _trimEntry[4];
      set
      {
        _trimEntry[4] = value;
        Debug.WriteLine($"TrimEntryValueCh4 {value}");
        RaisePropertyChanged();
      }
    }

    public string TrimEntryValueCh5
    {
      get => _trimEntry[5];
      set
      {
        _trimEntry[5] = value;
        Debug.WriteLine($"TrimEntryValueCh5 {value}");
        RaisePropertyChanged();
      }
    }

    public string TrimEntryValueCh6
    {
      get => _trimEntry[6];
      set
      {
        _trimEntry[6] = value;
        Debug.WriteLine($"TrimEntryValueCh6 {value}");
        RaisePropertyChanged();
      }
    }

    public string TrimEntryValueCh7
    {
      get => _trimEntry[7];
      set
      {
        _trimEntry[7] = value;
        Debug.WriteLine($"TrimEntryValueCh7 {value}");
        RaisePropertyChanged();
      }
    }

    #endregion

    #region sliders

    public double TrimSliderValueCh0
    {
      get => Settings.Trims[0]; //_trimSlider [0];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[0]) return;
        Settings.Trims[0] = val;

        _trimEntry[0] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh0");
        Send_Trims(0, val);
      }
    }

    public double TrimSliderValueCh1
    {
      get => Settings.Trims[1];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[1]) return;
        Settings.Trims[1] = val;

        _trimEntry[1] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh1");
        Send_Trims(1, val);
      }
    }

    public double TrimSliderValueCh2
    {
      get => Settings.Trims[2];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[2]) return;
        Settings.Trims[2] = val;

        _trimEntry[2] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh2");
        Send_Trims(2, val);
      }
    }

    public double TrimSliderValueCh3
    {
      get => Settings.Trims[3];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[3]) return;
        Settings.Trims[3] = val;

        _trimEntry[3] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh3");
        Send_Trims(3, val);
      }
    }

    public double TrimSliderValueCh4
    {
      get => Settings.Trims[4];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[4]) return;
        Settings.Trims[4] = val;

        _trimEntry[4] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh4");
        Send_Trims(4, val);
      }
    }

    public double TrimSliderValueCh5
    {
      get => Settings.Trims[5];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[5]) return;
        Settings.Trims[5] = val;

        _trimEntry[5] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh5");
        Send_Trims(5, val);
      }
    }

    public double TrimSliderValueCh6
    {
      get => Settings.Trims[6];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[6]) return;
        Settings.Trims[6] = val;

        _trimEntry[6] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh6");
        Send_Trims(6, val);
      }
    }

    public double TrimSliderValueCh7
    {
      get => Settings.Trims[7];
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.Trims[7]) return;
        Settings.Trims[7] = val;

        _trimEntry[7] = $"{val:N2}";
        RaisePropertyChanged();
        RaisePropertyChanged("TrimEntryValueCh7");
        Send_Trims(7, val);
      }
    }

    #endregion sliders

    internal bool Send_Trims(int ch, double value)
    {     
      var group = Settings.TrimGroup[ch];
      var max = -1000.0;
      var min = 1000.0;

      var desiredDelta = value - Settings.Trims[ch];
      int i;
      for (i = 0; i != 8; i++) // find min & max for all of the group members
      {
        if (Settings.TrimGroup[i] != group) continue;
        max = Settings.Trims[i] > max ? Settings.Trims[i] : max;
        min = Settings.Trims[i] < min ? Settings.Trims[i] : min;
      }

      desiredDelta = max + desiredDelta >= TrimMax ? TrimMax - max : desiredDelta;
      desiredDelta = min + desiredDelta <= TrimMin ? TrimMin - min : desiredDelta;
      //if (desiredDelta == 0)
      //  return true;

      for (i = 0; i != 8; i++)
      {
        if (Settings.TrimGroup[i] == group)
        {
          Settings.Trims[i] += desiredDelta;
          _trimEntry[i] = $"{Settings.Trims[i]:N2}";
          RaisePropertyChanged("TrimSliderValueCh" + i);
          RaisePropertyChanged("TrimEntryValueCh" + i);
        }
      }

      // _gain_trim[ch] = value;

      var CommandArray = new byte[43];
      var index = 3;
      CommandArray[0] = 0;
      CommandArray[1] = USB_Commands.CMD_START;
      CommandArray[2] = USB_Commands.CMD_SET_TRIMS;
      int channel;
      for (channel = 0; channel != 8; channel++)
      {
        Array.Copy(BitConverter.GetBytes((float)Settings.Trims[channel]), 0, CommandArray, index, 4);
        index += 4;
      }

      for (channel = 0; channel != 8; channel++)
      {
        CommandArray[index++] = (byte)Settings.TrimGroup[channel];
      }

      var result = Utils.Universal_Write(ref CommandArray);

      if (!result)
        Debug.WriteLine("Write Error");
      return result;
    }
    #endregion trims

   
  }
}
