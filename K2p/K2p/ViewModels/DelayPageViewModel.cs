using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using K2p.Statics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace K2p.ViewModels
{class DelayPageViewModel : ViewModelBase
  {
    private IPageDialogService _dialogService;
    public bool AllowUpdates;
    private string[] _entryValue = new string[8];
  
    public DelegateCommand EntryCompletedCommandCh0 { get; }
    public DelegateCommand EntryCompletedCommandCh1 { get; }
    public DelegateCommand EntryCompletedCommandCh2 { get; }
    public DelegateCommand EntryCompletedCommandCh3 { get; }
    public DelegateCommand EntryCompletedCommandCh4 { get; }
    public DelegateCommand EntryCompletedCommandCh5 { get; }
    public DelegateCommand EntryCompletedCommandCh6 { get; }
    public DelegateCommand EntryCompletedCommandCh7 { get; }

    public DelegateCommand ButtonDefaultGroups { get; }

  //  private int[] _group_index = new int[8];
    private int[] _delay_samples = new int[8];

    public IList<Group> Groups => GroupData.GroupSource;

    //private Group _selectedGroupCh0;
    //private Group _selectedGroupCh1;
    //private Group _selectedGroupCh2;
    //private Group _selectedGroupCh3;
    //private Group _selectedGroupCh4;
    //private Group _selectedGroupCh5;
    //private Group _selectedGroupCh6;
    //private Group _selectedGroupCh7;

    public DelayPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
    {
      _dialogService = dialogService;
      EntryCompletedCommandCh0 = new DelegateCommand(ExecuteEntryCompletedCompletedCh0);
      EntryCompletedCommandCh1 = new DelegateCommand(ExecuteEntryCompletedCompletedCh1);
      EntryCompletedCommandCh2 = new DelegateCommand(ExecuteEntryCompletedCompletedCh2);
      EntryCompletedCommandCh3 = new DelegateCommand(ExecuteEntryCompletedCompletedCh3);
      EntryCompletedCommandCh4 = new DelegateCommand(ExecuteEntryCompletedCompletedCh4);
      EntryCompletedCommandCh5 = new DelegateCommand(ExecuteEntryCompletedCompletedCh5);
      EntryCompletedCommandCh6 = new DelegateCommand(ExecuteEntryCompletedCompletedCh6);
      EntryCompletedCommandCh7 = new DelegateCommand(ExecuteEntryCompletedCompletedCh7);

      ButtonDefaultGroups = new DelegateCommand(ExecuteButtonDefaultGroups);

      for (var i = 0; i != 8; i++)
      {
        //SetGroup(i, Settings.OutputDelayGroup[i]);
        _delay_samples[i] = Settings.OutputDelay[i];
        _entryValue[i] = $"{Convert_from_Samples(_delay_samples[i]):N1}";
      }

    }

    private void ExecuteButtonDefaultGroups()
    {
      for (var channel = 0; channel != 8; channel++)
      {
        Settings.OutputDelayGroup[channel] = (ushort)channel;
        RaisePropertyChanged($"SelectedIndexCh{channel}");
      }
    }
    public int SelectedIndexCh0
    {
      get => Settings.OutputDelayGroup[0];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[0], value);
      }
    }
    public int SelectedIndexCh1
    {
      get => Settings.OutputDelayGroup[1];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[1], value);
      }
    }
    public int SelectedIndexCh2
    {
      get => Settings.OutputDelayGroup[2];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[2], value);
      }
    }
    public int SelectedIndexCh3
    {
      get => Settings.OutputDelayGroup[3];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[3], value);
      }
    }
    public int SelectedIndexCh4
    {
      get => Settings.OutputDelayGroup[4];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[4], value);
      }
    }
    public int SelectedIndexCh5
    {
      get => Settings.OutputDelayGroup[5];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[5], value);
      }
    }
    public int SelectedIndexCh6
    {
      get => Settings.OutputDelayGroup[6];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[6], value);
      }
    }
    public int SelectedIndexCh7
    {
      get => Settings.OutputDelayGroup[7];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Settings.OutputDelayGroup[7], value);
      }
    }

    //public Group SelectedGroupCh0
    //{
    //  get => _selectedGroupCh0;
    //  set
    //  {
    //    if (value == _selectedGroupCh0) return;
    //    SetProperty(ref _selectedGroupCh0, value);
    //    _group_index[0] = _selectedGroupCh0.GroupIndex;
    //    SliderValueCh0 = SliderValueCh0;
    //  }
    //}
    //public Group SelectedGroupCh1
    //{
    //  get => _selectedGroupCh1;
    //  set
    //  {
    //    if (value == _selectedGroupCh1) return;
    //    SetProperty(ref _selectedGroupCh1, value);
    //    _group_index[1] = _selectedGroupCh1.GroupIndex;
    //    SliderValueCh1 = SliderValueCh1;
    //  }
    //}
    //public Group SelectedGroupCh2
    //{
    //  get => _selectedGroupCh2;
    //  set
    //  {
    //    SetProperty(ref _selectedGroupCh2, value);
    //    _group_index[2] = _selectedGroupCh2.GroupIndex;
    //    SliderValueCh1 = SliderValueCh1;
    //  }
    //}
    //public Group SelectedGroupCh3
    //{
    //  get => _selectedGroupCh3;
    //  set
    //  {
    //    SetProperty(ref _selectedGroupCh3, value);
    //    _group_index[3] = _selectedGroupCh3.GroupIndex;
    //    SliderValueCh3 = SliderValueCh3;
    //  }
    //}
    //public Group SelectedGroupCh4
    //{
    //  get => _selectedGroupCh4;
    //  set
    //  {
    //    SetProperty(ref _selectedGroupCh4, value);
    //    _group_index[4] = _selectedGroupCh4.GroupIndex;
    //    SliderValueCh4 = SliderValueCh4;
    //  }
    //}
    //public Group SelectedGroupCh5
    //{
    //  get => _selectedGroupCh5;
    //  set
    //  {
    //    SetProperty(ref _selectedGroupCh5, value);
    //    _group_index[5] = _selectedGroupCh5.GroupIndex;
    //    SliderValueCh5 = SliderValueCh5;
    //  }
    //}
    //public Group SelectedGroupCh6
    //{
    //  get => _selectedGroupCh6;
    //  set
    //  {
    //    SetProperty(ref _selectedGroupCh6, value);
    //    _group_index[6] = _selectedGroupCh6.GroupIndex;
    //    SliderValueCh6 = SliderValueCh6;
    //  }
    //}
    //public Group SelectedGroupCh7
    //{
    //  get => _selectedGroupCh7;
    //  set
    //  {
    //    SetProperty(ref _selectedGroupCh7, value);
    //    _group_index[7] = _selectedGroupCh7.GroupIndex;
    //    SliderValueCh7 = SliderValueCh7;
    //  }
    //}

    //private void SetGroup(int channel, int group)
    //{
    //  _group_index[channel] = group;
    //  switch (channel)
    //  {
    //    case 0:
    //      SelectedGroupCh0 = Groups[group];
    //      break;
    //    case 1:
    //      SelectedGroupCh1 = Groups[group];
    //      break;
    //    case 2:
    //      SelectedGroupCh2 = Groups[group];
    //      break;
    //    case 3:
    //      SelectedGroupCh3 = Groups[group];
    //      break;
    //    case 4:
    //      SelectedGroupCh4 = Groups[group];
    //      break;
    //    case 5:
    //      SelectedGroupCh5 = Groups[group];
    //      break;
    //    case 6:
    //      SelectedGroupCh6 = Groups[group];
    //      break;
    //    case 7:
    //      SelectedGroupCh7 = Groups[group];
    //      break;
    //  }
    //}


    #region CH 0
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

    private void ExecuteEntryCompletedCompletedCh0()
    {
      if (!double.TryParse(_entryValue[0], out var result)) return;

      _delay_samples[0] = Convert_to_Samples(result);
      Send(0, _delay_samples[0]);
      RaisePropertyChanged("SliderValueCh0");
      Debug.WriteLine($"Entry complete { _delay_samples[0]}");
    }
    
    public string EntryValueCh0
    {
      set => SetProperty(ref _entryValue[0], value);
      get => _entryValue[0];
    }
    public double SliderValueCh0
    {
      get => _delay_samples[0];
      set
      {
        if ((int)value == _delay_samples[0])
        {
          return;
        }

       // _entryValue[0] = $"{Convert_from_Samples((int) value):N1}";
        Send(0, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh0");
      }
    }
    #endregion
    #region CH 1
    private void ExecuteEntryCompletedCompletedCh1()
    {
      if (!double.TryParse(_entryValue[1], out var result)) return;
      _delay_samples[1] = Convert_to_Samples(result);
      Send(0, _delay_samples[1]);
      RaisePropertyChanged("SliderValueCh1");
      // RaisePropertyChanged("EntryValueCh1");
      Debug.WriteLine($"Entry complete { _delay_samples[1]}");
    }

    public string EntryValueCh1
    {
      set => SetProperty(ref _entryValue[1], value);
      get => _entryValue[1];
    }
    public double SliderValueCh1
    {
      get => _delay_samples[1];
      set
      {
        if ((int)value == _delay_samples[1])
        {
          return;
        }

       // _entryValue[1] = $"{Convert_from_Samples((int)value):N1}";
        Send(1, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh1");
      }
    }
    #endregion
    #region CH 2
    private void ExecuteEntryCompletedCompletedCh2()
    {
      if (!double.TryParse(_entryValue[2], out var result)) return;
      _delay_samples[2] = Convert_to_Samples(result);
      Send(2, _delay_samples[2]);
      RaisePropertyChanged("SliderValueCh2");
      Debug.WriteLine($"Entry complete { _delay_samples[2]}");
    }
    public string EntryValueCh2
    {
      set => SetProperty(ref _entryValue[2], value);
      get => _entryValue[2];
    }
    public double SliderValueCh2
    {
      get => _delay_samples[2];
      set
      {
        if ((int)value == _delay_samples[2])
        {
          return;
        }

     //   _entryValue[2] = $"{Convert_from_Samples((int)value):N1}";
        Send(2, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh2");
      }
    }
    #endregion
    #region CH 3
    private void ExecuteEntryCompletedCompletedCh3()
    {
      if (!double.TryParse(_entryValue[3], out var result)) return;
      _delay_samples[3] = Convert_to_Samples(result);
      Send(3, _delay_samples[3]);
      RaisePropertyChanged("SliderValueCh3");
      Debug.WriteLine($"Entry complete { _delay_samples[3]}");
    }
    public string EntryValueCh3
    {
      set => SetProperty(ref _entryValue[3], value);
      get => _entryValue[3];
    }
    public double SliderValueCh3
    {
      get => _delay_samples[3];
      set
      {
        if ((int)value == _delay_samples[3])
        {
          return;
        }

      //  _entryValue[3] = $"{Convert_from_Samples((int)value):N1}";
        Send(3, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh3");
      }
    }
    #endregion
    #region CH 4
    private void ExecuteEntryCompletedCompletedCh4()
    {
      if (!double.TryParse(_entryValue[4], out var result)) return;
      _delay_samples[4] = Convert_to_Samples(result);
      Send(4, _delay_samples[4]);
      RaisePropertyChanged("SliderValueCh4");
      Debug.WriteLine($"Entry complete { _delay_samples[4]}");
    }
    public string EntryValueCh4
    {
      set => SetProperty(ref _entryValue[4], value);
      get => _entryValue[4];
    }
    public double SliderValueCh4
    {
      get => _delay_samples[4];
      set
      {
        if ((int)value == _delay_samples[4])
        {
          return;
        }

      //  _entryValue[4] = $"{Convert_from_Samples((int)value):N1}";
        Send(4, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh4");
      }
    }
    #endregion
    #region CH 5
    private void ExecuteEntryCompletedCompletedCh5()
    {
      if (!double.TryParse(_entryValue[5], out var result)) return;
      _delay_samples[5] = Convert_to_Samples(result);
      Send(5, _delay_samples[5]);
      RaisePropertyChanged("SliderValueCh5");
      Debug.WriteLine($"Entry complete { _delay_samples[5]}");
    }
    public string EntryValueCh5
    {
      set => SetProperty(ref _entryValue[5], value);
      get => _entryValue[5];
    }
    public double SliderValueCh5
    {
      get => _delay_samples[5];
      set
      {
        if ((int)value == _delay_samples[5])
        {
          return;
        }

       // _entryValue[5] = $"{Convert_from_Samples((int)value):N1}";
        Send(5, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh5");
      }
    }
    #endregion
    #region CH 6
    private void ExecuteEntryCompletedCompletedCh6()
    {
      if (!double.TryParse(_entryValue[6], out var result)) return;
      _delay_samples[6] = Convert_to_Samples(result);
      Send(6, _delay_samples[6]);
      RaisePropertyChanged("SliderValueCh6");
      Debug.WriteLine($"Entry complete { _delay_samples[6]}");
    }
    public string EntryValueCh6
    {
      set => SetProperty(ref _entryValue[6], value);
      get => _entryValue[6];
    }
    public double SliderValueCh6
    {
      get => _delay_samples[6];
      set
      {
        if ((int)value == _delay_samples[6])
        {
          return;
        }

   //     _entryValue[6] = $"{Convert_from_Samples((int)value):N1}";
        Send(6, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh6");
      }
    }
    #endregion
    #region CH 7
    private void ExecuteEntryCompletedCompletedCh7()
    {
      if (!double.TryParse(_entryValue[7], out var result)) return;
      _delay_samples[7] = Convert_to_Samples(result);
      Send(6, _delay_samples[7]);
      RaisePropertyChanged("SliderValueCh7");
      Debug.WriteLine($"Entry complete { _delay_samples[7]}");
    }
    public string EntryValueCh7
    {
      set => SetProperty(ref _entryValue[7], value);
      get => _entryValue[7];
    }
    public double SliderValueCh7
    {
      get => _delay_samples[7];
      set
      {
        if ((int)value == _delay_samples[7])
        {
          return;
        }

     //   _entryValue[7] = $"{Convert_from_Samples((int)value):N1}";
        Send(7, (int)value);
        RaisePropertyChanged();
        RaisePropertyChanged("EntryValueCh7");
      }
    }
    #endregion

    public enum Units { Inches, Feet, Millimeters, Centimeters, Milliseconds, Samples }

    private int  units_index = (int) Units.Milliseconds;
    private const double SpeedOfSoundFPS = 1125.33;
    private double Convert_from_Samples(int delay)
    {
      double k = 1.0;
      switch (units_index)
      {
        case (int)Units.Inches:
          k = SpeedOfSoundFPS * 12 / 96000;  //.140666;
          break;
        case (int)Units.Feet:
          k = SpeedOfSoundFPS / 96000; // .01172217;
          break;
        case (int)Units.Millimeters:
          k = SpeedOfSoundFPS * 304.8 / 96000; // 3.572916;
          break;
        case (int)Units.Centimeters:
          k = SpeedOfSoundFPS * 30.48 / 96000;  //.3572916;
          break;
        case (int)Units.Milliseconds:
          k = 1.0 / 96.0;
          break;
        case (int)Units.Samples:
          k = 1.0;
          break;
      }
      return delay * k;
    }

    private int Convert_to_Samples(double delay)
    {
      double k = 1;
      switch (units_index)
      {
        case (int)Units.Inches:
          k = SpeedOfSoundFPS * 12 / 96000; //   .140666;
          break;
        case (int)Units.Feet:
          k = SpeedOfSoundFPS / 96000;
          break;
        case (int)Units.Millimeters:
          k = SpeedOfSoundFPS * 304.8 / 96000;    //3.572916;  
          break;
        case (int)Units.Centimeters:
          k = SpeedOfSoundFPS * 30.48 / 96000;  //  .3572916;
          break;
        case (int)Units.Milliseconds:
          k = 1.0 / 96.0;
          break;
        case (int)Units.Samples:
          k = 1.0;
          break;
      }
      var temp = delay / k;
      temp += .5;
      return (int)(temp);
    }

    public int SliderMax { get; set; } = Constants.MaxDelaySamples;
    internal void Send(int channel, int newValue)
    {
      var commandArray = new byte[7];

      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_OUTPUT_DELAY;
     

      newValue = newValue < 0 ? 0 : newValue;
      var group = Settings.OutputDelayGroup[channel];
      var max = 0;
      var min = 10000;
      int i;
      var desiredDelta = newValue - _delay_samples[channel];
      for (i = 0; i != 8; i++) // find min & max for all of the group members
      {
        if (Settings.OutputDelayGroup[i] != group) continue;
        max = _delay_samples[i] > max ? _delay_samples[i] : max;
        min = _delay_samples[i] < min ? _delay_samples[i] : min;
      }
      desiredDelta = max + desiredDelta >= SliderMax ? SliderMax - max : desiredDelta;
      desiredDelta = min + desiredDelta <= 0 ? -min : desiredDelta;

      /*
       * commandArray[3] : channel
       * commandArray[4-5] : data
       * commandArray[6] : group_index[channel]
       * 
       */
      commandArray[6] = (byte)Settings.OutputDelayGroup[channel];
      for (i = 0; i != 8; i++)
      {
        commandArray[3] = (byte)i;
        if (Settings.OutputDelayGroup[i] != group)
        {
          continue;
        }

        _delay_samples[i] += desiredDelta;
        _entryValue[i] = $"{Convert_from_Samples(_delay_samples[i]):N1}";
        RaisePropertyChanged("SliderValueCh" + i);
        RaisePropertyChanged("EntryValueCh" + i);
        var bytes = BitConverter.GetBytes((ushort)_delay_samples[i]);
        Debug.WriteLine($"Delay samples[{i}] = {_delay_samples[i]}");
        Array.Copy(bytes, 0, commandArray, 4, 2);

        if (!Utils.Universal_Write(ref commandArray))
        {
          Debug.WriteLine("Delay() failed");
        }

        newValue = newValue > SliderMax ? SliderMax : newValue;
      }

    }

  }
}
