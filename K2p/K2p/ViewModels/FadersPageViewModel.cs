using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using K2p.Statics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace K2p.ViewModels
{
  public class FadersPageViewModel : ViewModelBase
  {
    private IPageDialogService _dialogService;
    public bool AllowUpdates;

    private string _masterEntryValue = "0.0";

    private string _left_Entry;
    private string _right_Entry;
    private string _front_Entry;
    private string _rear_Entry;

    private string _center_Entry;

    private string _bass_Entry;

    private AssignmentData _assignmentData = new AssignmentData();
    public IList<Assignment> Assignments => _assignmentData.AssignmentSource;
    private Assignment _selectedAssignmentCh0;
    private Assignment _selectedAssignmentCh1;
    private Assignment _selectedAssignmentCh2;
    private Assignment _selectedAssignmentCh3;
    private Assignment _selectedAssignmentCh4;
    private Assignment _selectedAssignmentCh5;
    private Assignment _selectedAssignmentCh6;
    private Assignment _selectedAssignmentCh7;


    public DelegateCommand MasterEntryCompletedCommand { get; }


    public DelegateCommand LeftEntryCompletedCommand { get; }
    public DelegateCommand RightEntryCompletedCommand { get; }
    public DelegateCommand FrontEntryCompletedCommand { get; }
    public DelegateCommand RearEntryCompletedCommand { get; }
    public DelegateCommand CenterEntryCompletedCommand { get; }
    public DelegateCommand BassEntryCompletedCommand { get; }
    public DelegateCommand ButtonDefaultAssignments { get; }
    private Color[] _brushes = new Color[8];
    private string[] _labels = new string[8];

    public FadersPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(
      navigationService)
    {
      _dialogService = dialogService;

      MasterEntryCompletedCommand = new DelegateCommand(ExecuteMasterEntryCompleted);
      LeftEntryCompletedCommand = new DelegateCommand(ExecuteLeftEntryCompletedCommand);
      RightEntryCompletedCommand = new DelegateCommand(ExecuteRightEntryCompletedCommand);
      FrontEntryCompletedCommand = new DelegateCommand(ExecuteFrontEntryCompletedCommand);
      RearEntryCompletedCommand = new DelegateCommand(ExecuteRearEntryCompletedCommand);
      CenterEntryCompletedCommand = new DelegateCommand(ExecuteCenterEntryCompletedCommand);
      BassEntryCompletedCommand = new DelegateCommand(ExecuteBassEntryCompletedCommand);
      ButtonDefaultAssignments = new DelegateCommand(ExecuteButtonDefaultAssignments);
      _masterEntryValue = $"{Settings.MasterGain:N2}";
      RaisePropertyChanged("MasterEntryValue");
      RaisePropertyChanged("MasterSliderValue");

      for (var channel = 0; channel != 8; channel++)
      {
        SetAssignment(channel, Settings.FaderAssignment[channel]);
        _brushes[channel] = Settings.mixer_2_output_brush[channel];
        _labels[channel] = Settings.output_labels[channel];
      }

      UpdateLeftRightEntrys();
      UpdateFrontRearEntrys();
      UpdateCenterEntry();
      UpdateBassEntry();
    }

    public int SelectedIndexCh0
    {
      get => (int)Settings.FaderAssignment[0];
      set { Settings.FaderAssignment[0] = (AssignmentE)value; RaisePropertyChanged(); }
    }
    public int SelectedIndexCh1
    {
      get => (int)Settings.FaderAssignment[1];
      set { Settings.FaderAssignment[1] = (AssignmentE)value; RaisePropertyChanged(); }
    }
    public int SelectedIndexCh2
    {
      get => (int)Settings.FaderAssignment[2];
      set { Settings.FaderAssignment[2] = (AssignmentE)value; RaisePropertyChanged(); }
    }
    public int SelectedIndexCh3
    {
      get => (int)Settings.FaderAssignment[3];
      set { Settings.FaderAssignment[3] = (AssignmentE)value; RaisePropertyChanged(); }
    }
    public int SelectedIndexCh4
    {
      get => (int)Settings.FaderAssignment[4];
      set { Settings.FaderAssignment[4] = (AssignmentE)value; RaisePropertyChanged(); }
    }
    public int SelectedIndexCh5
    {
      get => (int)Settings.FaderAssignment[5];
      set { Settings.FaderAssignment[5] = (AssignmentE)value; RaisePropertyChanged(); }
    }
    public int SelectedIndexCh6
    {
      get => (int)Settings.FaderAssignment[6];
      set { Settings.FaderAssignment[6] = (AssignmentE)value; RaisePropertyChanged(); }
    }
    public int SelectedIndexCh7
    {
      get => (int)Settings.FaderAssignment[7];
      set { Settings.FaderAssignment[7] = (AssignmentE)value; RaisePropertyChanged(); }
    }


    private void ExecuteButtonDefaultAssignments()
    {
      SelectedIndexCh0 = (int)AssignmentE.FrontLeft;
      SelectedIndexCh1 = (int)AssignmentE.FrontRight;
      SelectedIndexCh2 = (int)AssignmentE.RearLeft;
      SelectedIndexCh3 = (int)AssignmentE.RearRight;
      SelectedIndexCh4 = (int)AssignmentE.FrontLeft;
      SelectedIndexCh5 = (int)AssignmentE.Center;
      SelectedIndexCh6 = (int)AssignmentE.Sub;
      SelectedIndexCh7 = (int)AssignmentE.Sub;
    }


    public Color TextColorCh0 => _brushes[0];
    public Color TextColorCh1 => _brushes[1];
    public Color TextColorCh2 => _brushes[2];
    public Color TextColorCh3 => _brushes[3];
    public Color TextColorCh4 => _brushes[4];
    public Color TextColorCh5 => _brushes[5];
    public Color TextColorCh6 => _brushes[6];
    public Color TextColorCh7 => _brushes[7];

    public string LabelCh0 => _labels[0];
    public string LabelCh1 => _labels[1];
    public string LabelCh2 => _labels[2];
    public string LabelCh3 => _labels[3];
    public string LabelCh4 => _labels[4];
    public string LabelCh5 => _labels[5];
    public string LabelCh6 => _labels[6];
    public string LabelCh7 => _labels[7];

    public string PickerNameCh0 => $"{_labels[0]} Assignment";
    public string PickerNameCh1 => $"{_labels[1]} Assignment";
    public string PickerNameCh2 => $"{_labels[2]} Assignment";
    public string PickerNameCh3 => $"{_labels[3]} Assignment";
    public string PickerNameCh4 => $"{_labels[4]} Assignment";
    public string PickerNameCh5 => $"{_labels[5]} Assignment";
    public string PickerNameCh6 => $"{_labels[6]} Assignment";
    public string PickerNameCh7 => $"{_labels[7]} Assignment";

    #region Assignments
    public enum AssignmentE { FrontLeft, FrontRight, RearLeft, RearRight, Center, Sub }


    private void SetAssignment(int channel, AssignmentE assignment)
    {
      if (!AllowUpdates) return;
      int assignmentIndex = (int)assignment;
      Settings.FaderAssignment[channel] = assignment;
      switch (channel)
      {
        case 0:
          SelectedAssignmentCh0 = Assignments[assignmentIndex];
          break;
        case 1:
          SelectedAssignmentCh1 = Assignments[assignmentIndex];
          break;
        case 2:
          SelectedAssignmentCh2 = Assignments[assignmentIndex];
          break;
        case 3:
          SelectedAssignmentCh3 = Assignments[assignmentIndex];
          break;
        case 4:
          SelectedAssignmentCh4 = Assignments[assignmentIndex];
          break;
        case 5:
          SelectedAssignmentCh5 = Assignments[assignmentIndex];
          break;
        case 6:
          SelectedAssignmentCh6 = Assignments[assignmentIndex];
          break;
        case 7:
          SelectedAssignmentCh7 = Assignments[assignmentIndex];
          break;
      }
    }
    public Assignment SelectedAssignmentCh0
    {
      get => _selectedAssignmentCh0;
      set
      {
        if (value == _selectedAssignmentCh0 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh0, value);       
        SendAssignment();
        Settings.FaderAssignment[0] = (AssignmentE)_selectedAssignmentCh0.AssignmentIndex;
      }
    }
    public Assignment SelectedAssignmentCh1
    {
      get => _selectedAssignmentCh1;
      set
      {
        if (value == _selectedAssignmentCh1 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh1, value);
        Settings.FaderAssignment[1] = (AssignmentE)_selectedAssignmentCh1.AssignmentIndex;
        SendAssignment();
      }
    }
    public Assignment SelectedAssignmentCh2
    {
      get => _selectedAssignmentCh2;
      set
      {
        if (value == _selectedAssignmentCh2 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh2, value);
        Settings.FaderAssignment[2] = (AssignmentE)_selectedAssignmentCh2.AssignmentIndex;
        SendAssignment();
      }
    }
    public Assignment SelectedAssignmentCh3
    {
      get => _selectedAssignmentCh3;
      set
      {
        if (value == _selectedAssignmentCh3 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh3, value);
        Settings.FaderAssignment[3] = (AssignmentE)_selectedAssignmentCh3.AssignmentIndex;
        SendAssignment();
      }
    }
    public Assignment SelectedAssignmentCh4
    {
      get => _selectedAssignmentCh4;
      set
      {
        if (value == _selectedAssignmentCh4 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh4, value);
        Settings.FaderAssignment[4] = (AssignmentE)_selectedAssignmentCh4.AssignmentIndex;
        SendAssignment();
      }
    }
    public Assignment SelectedAssignmentCh5
    {
      get => _selectedAssignmentCh5;
      set
      {
        if (value == _selectedAssignmentCh5 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh5, value);
        Settings.FaderAssignment[5] = (AssignmentE)_selectedAssignmentCh5.AssignmentIndex;
        SendAssignment();
      }
    }
    public Assignment SelectedAssignmentCh6
    {
      get => _selectedAssignmentCh6;
      set
      {
        if (value == _selectedAssignmentCh6 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh6, value);
        Settings.FaderAssignment[6] = (AssignmentE)_selectedAssignmentCh6.AssignmentIndex;
        SendAssignment();
      }
    }
    public Assignment SelectedAssignmentCh7
    {
      get => _selectedAssignmentCh7;
      set
      {
        if (value == _selectedAssignmentCh7 || !AllowUpdates) return;
        SetProperty(ref _selectedAssignmentCh7, value);
        Settings.FaderAssignment[7] = (AssignmentE)_selectedAssignmentCh7.AssignmentIndex;
        SendAssignment();
      }
    }
    private bool SendAssignment()
    {
      var commandArray = new byte[35];
      var index = 3;
      commandArray[0] = 0;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_FADER_ASSIGNMENTS;
      for (var channel = 0; channel != 8; channel++)
      {
        commandArray[index++] = (byte)Settings.FaderAssignment[channel];
        Debug.WriteLine($"Assignment CH{channel} : {Settings.FaderAssignment[channel]}");
      }

      return Utils.Universal_Write(ref commandArray);

    }
    #endregion Assignments
   
    public double VolMax { get; } = Constants.VolMax;
    public double VolMin { get; } = Constants.VolMin;

    public double FaderSliderMax { get; } = Constants.FaderGainMax;
    // public double FaderSliderMax => Constants.Fader_Gain_Max;
    public double FaderSliderMin { get; } = Constants.FaderGainMin;

    public double FaderCenterMax { get; } = Constants.FaderCenterMax;
    public double FaderCenterMin { get; } = Constants.FaderCenterMin;

    public double FaderBassMax { get; } = Constants.Fader_Bass_Max;
    public double FaderBassMin { get; } = Constants.Fader_Bass_Min;



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
        Utils.MainPageVm?.UpdateMainPage();
        RaisePropertyChanged();
        RaisePropertyChanged("MasterEntryValue");
        Utils.SendFloat(USB_Commands.CMD_SET_MASTER_GAIN, value);
      }
    }

    #endregion master


    #region balance
    #region LeftRight

    private void ExecuteLeftEntryCompletedCommand()
    {
      double.TryParse(LeftEntryValue, out var _temp);
      Settings.LeftRightFader = -_temp;
      RaisePropertyChanged("LeftRightSliderValue");
      Send_Faders();
    }

    private void ExecuteRightEntryCompletedCommand()
    {
      double.TryParse(RightEntryValue, out var _temp);
      Settings.LeftRightFader = _temp;
      RaisePropertyChanged("LeftRightSliderValue");
      Send_Faders();
    }

    public string LeftEntryValue
    {
      get => _left_Entry;
      set
      {
        _left_Entry = value;
        Debug.WriteLine($"LeftEntryValue {value}");
        RaisePropertyChanged();
      }
    }
    public string RightEntryValue
    {
      get => _right_Entry;
      set
      {
        _right_Entry = value;
        Debug.WriteLine($"RightEntryValue {value}");
        RaisePropertyChanged();
      }
    }

    public double LeftRightSliderValue
    {
      get => Settings.LeftRightFader;
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.LeftRightFader) return;
        Settings.LeftRightFader = val;
        UpdateLeftRightEntrys();
        RaisePropertyChanged();
        Send_Faders();
      }
    }

    private void UpdateLeftRightEntrys()
    {
      var val = Settings.LeftRightFader <= 0 ? 0 : -Settings.LeftRightFader;
      _left_Entry = $"{val:N2}";

      val = Settings.LeftRightFader <= 0 ? Settings.LeftRightFader : 0;
      _right_Entry = $"{val:N2}";
      RaisePropertyChanged("LeftEntryValue");
      RaisePropertyChanged("RightEntryValue");
    }
    #endregion LeftRight

    #region FrontRear

    private void ExecuteFrontEntryCompletedCommand()
    {
      double.TryParse(FrontEntryValue, out var _temp);
      Settings.FrontRearFader = -_temp;
      RaisePropertyChanged("FrontRearSliderValue");
      Send_Faders();
    }
    private void ExecuteRearEntryCompletedCommand()
    {
      double.TryParse(RearEntryValue, out var _temp);
      Settings.FrontRearFader = _temp;
      RaisePropertyChanged("FrontRearSliderValue");
      Send_Faders();
    }
    public string FrontntryValue
    {
      get => _front_Entry;
      set
      {
        _front_Entry = value;
        Debug.WriteLine($"FrontEntryValue {value}");
        RaisePropertyChanged();
      }
    }

    public string FrontEntryValue
    {
      get => _front_Entry;
      set
      {
        _front_Entry = value;
        Debug.WriteLine($"FrontEntryValue {value}");
        RaisePropertyChanged();
      }
    }
    public string RearEntryValue
    {
      get => _rear_Entry;
      set
      {
        _rear_Entry = value;
        Debug.WriteLine($"RearEntryValue {value}");
        RaisePropertyChanged();
      }
    }
    public double FrontRearSliderValue
    {
      get => Settings.FrontRearFader;
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.FrontRearFader) return;
        Settings.FrontRearFader = val;
        RaisePropertyChanged();
        Send_Faders();
      }
    }

    private void UpdateFrontRearEntrys()
    {
      var val = Settings.FrontRearFader <= 0 ? 0 : -Settings.FrontRearFader;
      _front_Entry = $"{val:N2}";

      val = Settings.FrontRearFader <= 0 ? Settings.FrontRearFader : 0;
      _rear_Entry = $"{val:N2}";
      RaisePropertyChanged("FrontEntryValue");
      RaisePropertyChanged("RearEntryValue");
    }

    #endregion
    #region Center
    private void ExecuteCenterEntryCompletedCommand()
    {
      double.TryParse(CenterEntryValue, out var _temp);
      Settings.CenterFader = _temp;
      RaisePropertyChanged("CenterSliderValue");
      Send_Faders();
    }
    public string CenterEntryValue
    {
      get => _center_Entry;
      set
      {
        if (!AllowUpdates) return;
        _center_Entry = value;
        Debug.WriteLine($"CenterEntryValue {value}");
        RaisePropertyChanged();
      }
    }

    public double CenterSliderValue
    {
      get => Settings.CenterFader;
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.CenterFader) return;
        Settings.CenterFader = val;
        UpdateCenterEntry();
        RaisePropertyChanged();
        Send_Faders();
      }
    }

    private void UpdateCenterEntry()
    {
      _center_Entry = $"{ Settings.CenterFader:N2}";
      RaisePropertyChanged("CenterEntryValue");
    }
    #endregion

    #region  bass
    private void ExecuteBassEntryCompletedCommand()
    {
      double.TryParse(BassEntryValue, out var _temp);
      Settings.BassFader = _temp;
      RaisePropertyChanged("BassSliderValue");
      Send_Faders();
    }
    public string BassEntryValue
    {
      get => _bass_Entry;
      set
      {
        _bass_Entry = value;
        Debug.WriteLine($"BassEntryValue {value}");
        RaisePropertyChanged();
      }
    }
    public double BassSliderValue
    {
      get => Settings.BassFader;
      set
      {
        if (!AllowUpdates) return;
        var val = Settings.GetTrimIncrement(value);
        if (val == Settings.BassFader) return;
        Settings.BassFader = val;
        UpdateBassEntry();
        RaisePropertyChanged();
        Send_Faders();
      }
    }

    private void UpdateBassEntry()
    {
      _bass_Entry = $"{Settings.BassFader:N2}";
      RaisePropertyChanged("BassEntryValue");
    }
    #endregion


    private bool Send_Faders()
    {
      if (!AllowUpdates) return true;
      Debug.WriteLine($"Send_Faders() _left_right_slider: {Settings.LeftRightFader}, _front_rear_slider: { Settings.FrontRearFader}, _center_slider: { Settings.CenterFader },  _bass_slider: {Settings.BassFader }");
      var commandArray = new byte[35];
      var index = 3;
      commandArray[0] = 0;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_FADER;
      Array.Copy(BitConverter.GetBytes((float)-Settings.LeftRightFader), 0, commandArray, index, 4);
      index += 4;
      Array.Copy(BitConverter.GetBytes((float)Settings.FrontRearFader), 0, commandArray, index, 4);
      index += 4;
      Array.Copy(BitConverter.GetBytes((float)Settings.CenterFader), 0, commandArray, index, 4);
      index += 4;
      Array.Copy(BitConverter.GetBytes((float)Settings.BassFader), 0, commandArray, index, 4);
      index += 4;
      var result = Utils.Universal_Write(ref commandArray);
      if (!result)
        Debug.WriteLine("Write Error");
      return result;

      #endregion


    }
  }



}

