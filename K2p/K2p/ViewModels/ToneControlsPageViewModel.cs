using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using K2p.Statics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms.Xaml.Internals;

namespace K2p.ViewModels
{
  public class Peq
  {
    public double[] Frequency = new double[8];
    public double[] Gain = new double[8];
    public double[] Q = new double[8];
    public bool[] IsParametric = new bool[8];

    //public static double[] Freq = 
    //{
    //  20, 25, 31.5, 40, 50, 62.5, 80, 100, 125, 160,
    //  200, 250, 315, 400, 500, 625, 800, 1000, 1250, 1600,
    //  2000, 2500, 3150, 4000, 5000, 6250, 8000, 10000, 12500, 16000,
    //  20000
    //};
    public Peq(double freq, double q)
    {
      int i;
      for (i = 0; i != 8; i++)
      {
        Frequency[i] = freq;
        Q[i] = q;
        IsParametric[i] = true;
      }
    }
    public void UpdateTone(ref int index, ref byte[] array)
    {
      int i;
      for (i = 0; i != 8; i++)
      {
        Frequency[i] = BitConverter.ToSingle(array, index);
        Frequency[i] = Frequency[i] < 20 ? 20 : Frequency[i];
        Frequency[i] = Frequency[i] > 20000 ? 20000 : Frequency[i];
        index += 4;
      }
      for (i = 0; i != 8; i++)
      {
        Gain[i] = BitConverter.ToSingle(array, index);
        index += 4;
      }
      for (i = 0; i != 8; i++)
      {
        Q[i] = BitConverter.ToSingle(array, index);
        index += 4;
      }
    }
  }
  public class ToneControlsPageViewModel : ViewModelBase
  {
    public DelegateCommand MasterEntryCompletedCommand { get; }
    public DelegateCommand BassEntryCompletedCommand { get; }
    public DelegateCommand BassFreqEntryCompletedCommand { get; }
  
   
    public DelegateCommand BassSlopeEntryCompletedCommand { get; }
    public DelegateCommand MidrangeEntryCompletedCommand { get; }
    public DelegateCommand MidrangeQEntryCompletedCommand { get; }
    public DelegateCommand MidrangeFreqEntryCompletedCommand { get; }

    public DelegateCommand TrebleEntryCompletedCommand { get; }
    public DelegateCommand TrebleFreqEntryCompletedCommand { get; }
    public DelegateCommand TrebleSlopeEntryCompletedCommand { get; }

   
    private string _masterEntryValue = "0.0";
    private string _bassEntryValue = "0.0";
    private string _midrangeEntryValue = "0.0";
    private string _trebleEntryValue = "0.0";

    private string _bassFreqEntryValue = "50.0";
    private string _bassSlopeEntryValue = ".5"; 
    private string _midrangeQEntryValue = ".2";
  
    private string _midrangeFreqEntryValue = "2000.0";
    private string _trebleFreqEntryValue = "8000.0";  
    private string _trebleSlopeEntryValue = "5.000";

    public enum ToneControls { Bass, Midrange, Treble}

    public enum ToneFunctions
    {
      PEAKING_LOW_SHELF,
      PEAKING_PEAKING,
      PEAKING_HIGH_SHELF,
      PEAKING_ALL_PASS
    }
    private IPageDialogService _dialogService;

    public ToneControlsPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
    {
      Utils.ToneControlVm = this;
      _dialogService = dialogService;
      MasterEntryCompletedCommand = new DelegateCommand(ExecuteMasterEntryCompleted);
      BassEntryCompletedCommand = new DelegateCommand(ExecuteBassEntryCompleted);
      BassFreqEntryCompletedCommand = new DelegateCommand(ExecuteBassFreqEntryCompleted);
      MidrangeEntryCompletedCommand = new DelegateCommand(ExecuteMidrangeEntryCompleted);
      TrebleEntryCompletedCommand = new DelegateCommand(ExecuteTrebleEntryCompleted);
      TrebleFreqEntryCompletedCommand = new DelegateCommand(ExecuteTrebleFreqEntryCompleted);
      BassSlopeEntryCompletedCommand = new DelegateCommand(ExecuteBassSlopeCompleted);
      MidrangeQEntryCompletedCommand = new DelegateCommand(ExecuteMidrangeQCompleted);
      MidrangeFreqEntryCompletedCommand = new DelegateCommand(ExecuteMidrangeFreqCompleted);
      TrebleSlopeEntryCompletedCommand = new DelegateCommand(ExecuteTrebleSlopeCompleted);
      UpdateTone();
      //if (TCP.clientSocket == null)
      //{
      //  dialogService.DisplayAlertAsync("Notice", "You are not connected", "OK");
      //}
    }


    private void UpdateTone()
    {

      _masterEntryValue = $"{Settings.MasterGain:N2}";
      _trebleSlopeEntryValue = $"{Settings.TrebleTone.Q[0]:N2}";
      _trebleFreqEntryValue = $"{Settings.TrebleTone.Frequency[0]:N1}";
      _trebleEntryValue = $"{Settings.TrebleTone.Gain[0]:N2}";

      _midrangeQEntryValue = $"{Settings.MidTone.Q[0]:N2}";
      _midrangeFreqEntryValue = $"{Settings.MidTone.Frequency[0]:N1}";
      _midrangeEntryValue = $"{Settings.MidTone.Gain[0]:N2}";

      _bassSlopeEntryValue = $"{Settings.BassTone.Q[0]:N2}";
      _bassFreqEntryValue = $"{Settings.BassTone.Frequency[0]:N1}";
      _bassEntryValue = $"{Settings.BassTone.Gain[0]:N2}";

      RaisePropertyChanged("MasterEntryValue");
      RaisePropertyChanged("MasterSliderValue");

      RaisePropertyChanged("TrebleSlopeEntryValue");
      RaisePropertyChanged("TrebleFreqEntryValue");
      RaisePropertyChanged("TrebleEntryValue");
      RaisePropertyChanged("TrebleSlopeSliderValue");
      RaisePropertyChanged("TrebleLogFreqSliderValue");
      RaisePropertyChanged("TrebleSliderValue");

      RaisePropertyChanged("MidrangeQEntryValue");
      RaisePropertyChanged("MidrangeFreqEntryValue");
      RaisePropertyChanged("MidrangeEntryValue");
      RaisePropertyChanged("MidrangeLogFreqSliderValue");
      RaisePropertyChanged("MidrangeSliderValue");
      RaisePropertyChanged("MidrangeQSliderValue");

      RaisePropertyChanged("BassEntryValue");
      RaisePropertyChanged("BassFreqEntryValue");
      RaisePropertyChanged("BassSlopeEntryValue");
      RaisePropertyChanged("BassSliderValue");
      RaisePropertyChanged("BassLogFreqSliderValue");
      RaisePropertyChanged("BassSlopeSliderValue");
    }

    public double BoostMax => Constants.Boost_Max;
    public double BoostMin => Constants.Boost_Min;
    #region treble
    public string TrebleSlopeEntryValue
    {
      get => _trebleSlopeEntryValue;
      set => SetProperty(ref _trebleSlopeEntryValue, value);
    }
    public string TrebleEntryValue
    {
      get => _trebleEntryValue;
      set => SetProperty(ref _trebleEntryValue, value);
    }
    public string TrebleFreqEntryValue
    {
      get => _trebleFreqEntryValue;
      set => SetProperty(ref _trebleFreqEntryValue, value);
    }
    private void ExecuteTrebleEntryCompleted()
    {
      if (!double.TryParse(_trebleEntryValue, out var result)) return;   
      Settings.TrebleTone.Gain[0] = result;
      RaisePropertyChanged("TrebleSliderValue"); 
      SendTreble();
    }
    private void ExecuteTrebleFreqEntryCompleted()
    {
      if (!double.TryParse(_trebleFreqEntryValue, out var result)) return;   
      Settings.TrebleTone.Frequency[0] = result;
      RaisePropertyChanged("TrebleLogFreqSliderValue");  
      SendTreble();
    }

    private void ExecuteTrebleSlopeCompleted()
    {
      if (!double.TryParse( _trebleSlopeEntryValue, out var result)) return;    
      Settings.TrebleTone.Q[0] = result;
      RaisePropertyChanged("TrebleSlopeSliderValue");    
      SendTreble();
    }

    public double TrebleSlopeSliderValue
    {
      get => Settings.TrebleTone.Q[0];
      set
      {      
        if (!SetProperty(ref Settings.TrebleTone.Q[0], value)) return;        
        TrebleSlopeEntryValue = $"{value:N2}";
        SendTreble();
      }
    }     
   
    public double TrebleLogFreqSliderValue
    {
      get => Math.Log10(Settings.TrebleTone.Frequency[0]);
      set
      {
        value = Math.Pow(10, value);
        if (!SetProperty(ref Settings.TrebleTone.Frequency[0], value)) return;     
        TrebleFreqEntryValue = $"{value:N1}";
        SendTreble();
      }
    }       

    public double TrebleSliderValue
    {
      get => Settings.TrebleTone.Gain[0];
      set
      {
        value = Settings.GetEQGainIncrement(value);
        if (!SetProperty(ref Settings.TrebleTone.Gain[0], value)) return;
        TrebleEntryValue = $"{value:N2}";
        RaisePropertyChanged("TrebleEntryValue");
        SendTreble();
      }
    }

    #endregion

    #region midrange
    public string MidrangeQEntryValue
    {
      get => _midrangeQEntryValue;
      set { SetProperty(ref _midrangeQEntryValue, value); }
    }
    public string MidrangeFreqEntryValue
    {
      get => _midrangeFreqEntryValue;
      set { SetProperty(ref _midrangeFreqEntryValue, value); }
    }
    public string MidrangeEntryValue
    {
      get => _midrangeEntryValue;
      set { SetProperty(ref _midrangeEntryValue, value); }
    }
    private void ExecuteMidrangeEntryCompleted()
    {
      if (!double.TryParse(_midrangeEntryValue, out var result)) return;  
        Settings.MidTone.Gain[0] = result;
      SendMidrange();
      RaisePropertyChanged("MidrangeSliderValue");
    }
    private void ExecuteMidrangeQCompleted()
    {
      if (!double.TryParse(_midrangeQEntryValue, out var result)) return;
      if (Math.Abs(Settings.MidTone.Q[0] - result) < Tolerance) return;

      Settings.MidTone.Q[0] = result;
      RaisePropertyChanged("MidrangeQSliderValue");
      SendMidrange();
    }
    private void ExecuteMidrangeFreqCompleted()
    {
      if (!double.TryParse(_midrangeFreqEntryValue, out var result)) return; 
      Settings.MidTone.Frequency[0] = result;
      RaisePropertyChanged("MidrangeLogFreqSliderValue");
      SendMidrange();
    }

    public double MidrangeLogFreqSliderValue
    {
      get => Math.Log10(Settings.MidTone.Frequency[0]);
      set
      {
        value = Math.Pow(10, value);
        if(!SetProperty(ref Settings.MidTone.Frequency[0], value)) return;
        MidrangeFreqEntryValue = $"{ Settings.MidTone.Frequency[0]:N1}";
        SendMidrange();
      }
    }
   
    public double MidrangeSliderValue
    {
      get => Settings.MidTone.Gain[0];
      set
      {
        value = Settings.GetEQGainIncrement(value);
        if (!SetProperty(ref Settings.MidTone.Gain[0], value)) return;
        _midrangeEntryValue = $"{value:N2}";
        RaisePropertyChanged("MidrangeEntryValue");
        SendMidrange();
      }
    }
    public double MidrangeQSliderValue
    {
      get => Settings.MidTone.Q[0];
      set
      {
        if (!SetProperty(ref Settings.MidTone.Q[0], value)) return;
        MidrangeQEntryValue = $"{value:N2}";
        SendMidrange();
      }
    }



    #endregion
    #region bass
    public string BassEntryValue
    {
      get => _bassEntryValue;
      set { SetProperty(ref _bassEntryValue, value); }
    }
    public string BassFreqEntryValue
    {
      get => _bassFreqEntryValue;
      set { SetProperty(ref _bassFreqEntryValue, value); }
    }
    public string BassSlopeEntryValue
    {
      get => _bassSlopeEntryValue;
      set { SetProperty(ref _bassSlopeEntryValue, value); }
    }

    private void ExecuteBassEntryCompleted()
    {
      if (!double.TryParse(_bassEntryValue, out var result)) return;  
      Settings.BassTone.Gain[0] = result;
      RaisePropertyChanged("BassSliderValue");
      SendBass();
    }
    private void ExecuteBassFreqEntryCompleted()
    {
      if (!double.TryParse(_bassFreqEntryValue, out var result)) return; 
      Settings.BassTone.Frequency[0] = result;
      BassLogFreqSliderValue = Math.Log10(result);
      SendBass();
    }
    private void ExecuteBassSlopeCompleted()
    {     
      if (!double.TryParse(_bassSlopeEntryValue, out var result)) return;    
      Settings.BassTone.Q[0] = result;
      RaisePropertyChanged("BassSlopeSliderValue");
      SendBass();
    }      
    public double BassSliderValue
    {
      get => Settings.BassTone.Gain[0];
      set
      {
        value = Settings.GetEQGainIncrement(value);
        if (!SetProperty(ref Settings.BassTone.Gain[0], value)) return;       
        _bassEntryValue = $"{value:N2}";
        RaisePropertyChanged("BassEntryValue");
        SendBass();
      }
    }

    private const double Tolerance = .01;

    public double BassLogFreqSliderValue
    {
      get => Math.Log10(Settings.BassTone.Frequency[0]);
      set
      {
        value = Math.Pow(10, value);
        if (!SetProperty(ref Settings.BassTone.Frequency[0], value)) return;    
        BassFreqEntryValue = $"{value:N1}";   
        SendBass();
      }
    }
       
    public double BassSlopeSliderValue
    {
      get => Settings.BassTone.Q[0];
      set
      {      
        if (!SetProperty(ref Settings.BassTone.Q[0], value)) return;    
        BassSlopeEntryValue = $"{value:N2}";
        SendBass();
      }
    }
   
    #endregion
    private enum Filter_Type_e { LOW_SHELF, PEAKING, HIGH_SHELF };  
    private static bool Send(double frequency, double gain, double q, Filter_Type_e type)
    {
     var commandArray = new byte[17];

      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_TONE;
      commandArray[3] = 0xff; // all channels
      commandArray[4] = (byte)type;
      Array.Copy(BitConverter.GetBytes((float)frequency), 0, commandArray, 5, 4);
      Array.Copy(BitConverter.GetBytes((float)gain), 0, commandArray, 9, 4);
      Array.Copy(BitConverter.GetBytes((float)q), 0, commandArray, 13, 4);

      return Utils.Universal_Write(ref commandArray);
    }
    public static void SendTreble()
    {
      if (Utils.BlockSetters) return;
      Send(Settings.TrebleTone.Frequency[0], Settings.TrebleTone.Gain[0], Settings.TrebleTone.Q[0],
        Filter_Type_e.HIGH_SHELF);
      Debug.WriteLine("SendTreble F: {0:f1}, G: {1:f2}, Q: {2:f2}", Settings.TrebleTone.Frequency[0], Settings.TrebleTone.Gain[0], Settings.TrebleTone.Q[0]);
    }
    public static void SendMidrange()
    {
      if (Utils.BlockSetters) return;
      Send(Settings.MidTone.Frequency[0], Settings.MidTone.Gain[0], Settings.MidTone.Q[0],
        Filter_Type_e.PEAKING);
      Debug.WriteLine("SendMidrange F: {0:f1}, G: {1:f2}, Q: {2:f2}", Settings.MidTone.Frequency[0], Settings.MidTone.Gain[0], Settings.MidTone.Q[0]);
    }
    public static void SendBass()
    {
      if (Utils.BlockSetters) return;
      Send(Settings.BassTone.Frequency[0], Settings.BassTone.Gain[0], Settings.BassTone.Q[0],
        Filter_Type_e.LOW_SHELF);
      Debug.WriteLine("SendBass F: {0:f1}, G: {1:f2}, Q: {2:f2}", Settings.BassTone.Frequency[0], Settings.BassTone.Gain[0], Settings.BassTone.Q[0]);
    }
    #region master
    private void ExecuteMasterEntryCompleted()
    {
      if (!double.TryParse(_masterEntryValue, out Settings.MasterGain)) return;   
      Utils.SendFloat(USB_Commands.CMD_GET_MASTER_GAIN, Settings.MasterGain);
      RaisePropertyChanged("MasterSliderValue");
    }
    public string MasterEntryValue
    {
      get => _masterEntryValue;
      set
      {
        if (!SetProperty(ref _masterEntryValue, value)) return; // todo why is this being called on ctor?
        Debug.WriteLine("MasterEntryValue " + value);
        if (!double.TryParse(_masterEntryValue, out Settings.MasterGain)) return;
        Utils.SendFloat(USB_Commands.CMD_GET_MASTER_GAIN, Settings.MasterGain);
      }
    }
    public double MasterSliderValue
    {
      get => Settings.MasterGain;
      set
      {
        value = Settings.GetMasterIncrement(value);
        if (!SetProperty(ref Settings.MasterGain, value)) return;
     
        _masterEntryValue = $"{value:N2}";
        RaisePropertyChanged("MasterEntryValue");
        Utils.SendFloat(USB_Commands.CMD_GET_MASTER_GAIN, value);
      }
    }
    #endregion
  }
}
