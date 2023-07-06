using System;
using System.Diagnostics;
using System.Text;
using K2p.Statics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace K2p.ViewModels
{
  public class ParaPageViewModel : ViewModelBase
  {
    //private static bool _isLoading;
    private double[] _freq;
    private double[] _gain;
    private double[] _q;

    private string[] _sFreq;
    private string[] _sGain;
    private string[] _sQ;        
    public DelegateCommand B1FreqEntryCompletedCommand { get; }
    public DelegateCommand B1GainEntryCompletedCommand { get; }
    public DelegateCommand B1QEntryCompletedCommand { get; }
    public DelegateCommand B2FreqEntryCompletedCommand { get; }
    public DelegateCommand B2GainEntryCompletedCommand { get; }
    public DelegateCommand B2QEntryCompletedCommand { get; }
    public DelegateCommand B3FreqEntryCompletedCommand { get; }
    public DelegateCommand B3GainEntryCompletedCommand { get; }
    public DelegateCommand B3QEntryCompletedCommand { get; }
    public DelegateCommand B4FreqEntryCompletedCommand { get; }
    public DelegateCommand B4GainEntryCompletedCommand { get; }
    public DelegateCommand B4QEntryCompletedCommand { get; }
    public DelegateCommand B5FreqEntryCompletedCommand { get; }
    public DelegateCommand B5GainEntryCompletedCommand { get; }
    public DelegateCommand B5QEntryCompletedCommand { get; }
    public DelegateCommand B6FreqEntryCompletedCommand { get; }
    public DelegateCommand B6GainEntryCompletedCommand { get; }
    public DelegateCommand B6QEntryCompletedCommand { get; }
    public DelegateCommand B7FreqEntryCompletedCommand { get; }
    public DelegateCommand B7GainEntryCompletedCommand { get; }
    public DelegateCommand B7QEntryCompletedCommand { get; }
    public DelegateCommand B8FreqEntryCompletedCommand { get; }
    public DelegateCommand B8GainEntryCompletedCommand { get; }
    public DelegateCommand B8QEntryCompletedCommand { get; }
    public DelegateCommand B9FreqEntryCompletedCommand { get; }
    public DelegateCommand B9GainEntryCompletedCommand { get; }
    public DelegateCommand B9QEntryCompletedCommand { get; }
    public DelegateCommand B10FreqEntryCompletedCommand { get; }
    public DelegateCommand B10GainEntryCompletedCommand { get; }
    public DelegateCommand B10QEntryCompletedCommand { get; }
    public DelegateCommand B11FreqEntryCompletedCommand { get; }
    public DelegateCommand B11GainEntryCompletedCommand { get; }
    public DelegateCommand B11QEntryCompletedCommand { get; }
    public DelegateCommand B12FreqEntryCompletedCommand { get; }
    public DelegateCommand B12GainEntryCompletedCommand { get; }
    public DelegateCommand B12QEntryCompletedCommand { get; }
    public DelegateCommand B13FreqEntryCompletedCommand { get; }
    public DelegateCommand B13GainEntryCompletedCommand { get; }
    public DelegateCommand B13QEntryCompletedCommand { get; }
    public DelegateCommand B14FreqEntryCompletedCommand { get; }
    public DelegateCommand B14GainEntryCompletedCommand { get; }
    public DelegateCommand B14QEntryCompletedCommand { get; }
    public DelegateCommand B15FreqEntryCompletedCommand { get; }
    public DelegateCommand B15GainEntryCompletedCommand { get; }
    public DelegateCommand B15QEntryCompletedCommand { get; }
    public DelegateCommand B16FreqEntryCompletedCommand { get; }
    public DelegateCommand B16GainEntryCompletedCommand { get; }
    public DelegateCommand B16QEntryCompletedCommand { get; }
    public DelegateCommand B17FreqEntryCompletedCommand { get; }
    public DelegateCommand B17GainEntryCompletedCommand { get; }
    public DelegateCommand B17QEntryCompletedCommand { get; }
    public DelegateCommand B18FreqEntryCompletedCommand { get; }
    public DelegateCommand B18GainEntryCompletedCommand { get; }
    public DelegateCommand B18QEntryCompletedCommand { get; }
    public DelegateCommand B19FreqEntryCompletedCommand { get; }
    public DelegateCommand B19GainEntryCompletedCommand { get; }
    public DelegateCommand B19QEntryCompletedCommand { get; }
    public DelegateCommand B20FreqEntryCompletedCommand { get; }
    public DelegateCommand B20GainEntryCompletedCommand { get; }
    public DelegateCommand B20QEntryCompletedCommand { get; }
    public DelegateCommand B21FreqEntryCompletedCommand { get; }
    public DelegateCommand B21GainEntryCompletedCommand { get; }
    public DelegateCommand B21QEntryCompletedCommand { get; }
    public DelegateCommand B22FreqEntryCompletedCommand { get; }
    public DelegateCommand B22GainEntryCompletedCommand { get; }
    public DelegateCommand B22QEntryCompletedCommand { get; }
    public DelegateCommand B23FreqEntryCompletedCommand { get; }
    public DelegateCommand B23GainEntryCompletedCommand { get; }
    public DelegateCommand B23QEntryCompletedCommand { get; }
    public DelegateCommand B24FreqEntryCompletedCommand { get; }
    public DelegateCommand B24GainEntryCompletedCommand { get; }
    public DelegateCommand B24QEntryCompletedCommand { get; }
    public DelegateCommand B25FreqEntryCompletedCommand { get; }
    public DelegateCommand B25GainEntryCompletedCommand { get; }
    public DelegateCommand B25QEntryCompletedCommand { get; }
    public DelegateCommand B26FreqEntryCompletedCommand { get; }
    public DelegateCommand B26GainEntryCompletedCommand { get; }
    public DelegateCommand B26QEntryCompletedCommand { get; }
    public DelegateCommand B27FreqEntryCompletedCommand { get; }
    public DelegateCommand B27GainEntryCompletedCommand { get; }
    public DelegateCommand B27QEntryCompletedCommand { get; }
    public DelegateCommand B28FreqEntryCompletedCommand { get; }
    public DelegateCommand B28GainEntryCompletedCommand { get; }
    public DelegateCommand B28QEntryCompletedCommand { get; }
    public DelegateCommand B29FreqEntryCompletedCommand { get; }
    public DelegateCommand B29GainEntryCompletedCommand { get; }
    public DelegateCommand B29QEntryCompletedCommand { get; }
    public DelegateCommand B30FreqEntryCompletedCommand { get; }
    public DelegateCommand B30GainEntryCompletedCommand { get; }
    public DelegateCommand B30QEntryCompletedCommand { get; }
    public DelegateCommand B31FreqEntryCompletedCommand { get; }
    public DelegateCommand B31GainEntryCompletedCommand { get; }
    public DelegateCommand B31QEntryCompletedCommand { get; }


    public ParaPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
    {
      _freq = new double[31];
      //{
      //  20, 25, 31.5, 40, 50, 62.5, 80, 100, 125, 160,
      //  200, 250, 315, 400, 500, 625, 800, 1000, 1250, 1600,
      //  2000, 2500, 3150, 4000, 5000, 6250, 8000, 10000, 12500, 16000,
      //  20000
      //};

      _sGain = new string[31];
      _sQ = new string[31];
      _gain = new double[31];
      _q = new double[31];
      _sFreq = new string[31];
      for (var band = 0; band != 31; band++)
      {
        _gain[band] = Settings.Eq[band].Gain[0];
        _sGain[band] = $"{_gain[band]:N1}";

        _q[band] = Settings.Eq[band].Q[0];
        _sQ[band] = $"{_q[band]:N2}";

        _freq[band] = Settings.Eq[band].Frequency[0];
        _sFreq[band] = _freq[band] < 1000 ? $"{_freq[band]:N1}" : $"{_freq[band]:N0}";
      }

      for (var channel = 0; channel != 8; channel++)
      {
        _linked[channel] = true;
      }
      #region Band1
      B1FreqEntryCompletedCommand = new DelegateCommand(ExecuteB1FreqEntryCompleted);
      B1GainEntryCompletedCommand = new DelegateCommand(ExecuteB1GainCompleted);
      B1QEntryCompletedCommand = new DelegateCommand(ExecuteB1QCompleted);
      #endregion
      #region Band2
      B2FreqEntryCompletedCommand = new DelegateCommand(ExecuteB2FreqEntryCompleted);
      B2GainEntryCompletedCommand = new DelegateCommand(ExecuteB2GainCompleted);
      B2QEntryCompletedCommand = new DelegateCommand(ExecuteB2QCompleted);
      #endregion
      #region Band3
      B3FreqEntryCompletedCommand = new DelegateCommand(ExecuteB3FreqEntryCompleted);
      B3GainEntryCompletedCommand = new DelegateCommand(ExecuteB3GainCompleted);
      B3QEntryCompletedCommand = new DelegateCommand(ExecuteB3QCompleted);
      #endregion
      #region Band4
      B4FreqEntryCompletedCommand = new DelegateCommand(ExecuteB4FreqEntryCompleted);
      B4GainEntryCompletedCommand = new DelegateCommand(ExecuteB4GainCompleted);
      B4QEntryCompletedCommand = new DelegateCommand(ExecuteB4QCompleted);
      #endregion
      #region Band5
      B5FreqEntryCompletedCommand = new DelegateCommand(ExecuteB5FreqEntryCompleted);
      B5GainEntryCompletedCommand = new DelegateCommand(ExecuteB5GainCompleted);
      B5QEntryCompletedCommand = new DelegateCommand(ExecuteB5QCompleted);
      #endregion
      #region Band6
      B6FreqEntryCompletedCommand = new DelegateCommand(ExecuteB6FreqEntryCompleted);
      B6GainEntryCompletedCommand = new DelegateCommand(ExecuteB6GainCompleted);
      B6QEntryCompletedCommand = new DelegateCommand(ExecuteB6QCompleted);
      #endregion
      #region Band7
      B7FreqEntryCompletedCommand = new DelegateCommand(ExecuteB7FreqEntryCompleted);
      B7GainEntryCompletedCommand = new DelegateCommand(ExecuteB7GainCompleted);
      B7QEntryCompletedCommand = new DelegateCommand(ExecuteB7QCompleted);
      #endregion
      #region Band8
      B8FreqEntryCompletedCommand = new DelegateCommand(ExecuteB8FreqEntryCompleted);
      B8GainEntryCompletedCommand = new DelegateCommand(ExecuteB8GainCompleted);
      B8QEntryCompletedCommand = new DelegateCommand(ExecuteB8QCompleted);
      #endregion
      #region Band9
      B9FreqEntryCompletedCommand = new DelegateCommand(ExecuteB9FreqEntryCompleted);
      B9GainEntryCompletedCommand = new DelegateCommand(ExecuteB9GainCompleted);
      B9QEntryCompletedCommand = new DelegateCommand(ExecuteB9QCompleted);
      #endregion
      #region Band10
      B10FreqEntryCompletedCommand = new DelegateCommand(ExecuteB10FreqEntryCompleted);
      B10GainEntryCompletedCommand = new DelegateCommand(ExecuteB10GainCompleted);
      B10QEntryCompletedCommand = new DelegateCommand(ExecuteB10QCompleted);
      #endregion
      #region Band11
      B11FreqEntryCompletedCommand = new DelegateCommand(ExecuteB11FreqEntryCompleted);
      B11GainEntryCompletedCommand = new DelegateCommand(ExecuteB11GainCompleted);
      B11QEntryCompletedCommand = new DelegateCommand(ExecuteB11QCompleted);
      #endregion
      #region Band12
      B12FreqEntryCompletedCommand = new DelegateCommand(ExecuteB12FreqEntryCompleted);
      B12GainEntryCompletedCommand = new DelegateCommand(ExecuteB12GainCompleted);
      B12QEntryCompletedCommand = new DelegateCommand(ExecuteB12QCompleted);
      #endregion
      #region Band13
      B13FreqEntryCompletedCommand = new DelegateCommand(ExecuteB13FreqEntryCompleted);
      B13GainEntryCompletedCommand = new DelegateCommand(ExecuteB13GainCompleted);
      B13QEntryCompletedCommand = new DelegateCommand(ExecuteB13QCompleted);
      #endregion
      #region Band14
      B14FreqEntryCompletedCommand = new DelegateCommand(ExecuteB14FreqEntryCompleted);
      B14GainEntryCompletedCommand = new DelegateCommand(ExecuteB14GainCompleted);
      B14QEntryCompletedCommand = new DelegateCommand(ExecuteB14QCompleted);
      #endregion
      #region Band15
      B15FreqEntryCompletedCommand = new DelegateCommand(ExecuteB15FreqEntryCompleted);
      B15GainEntryCompletedCommand = new DelegateCommand(ExecuteB15GainCompleted);
      B15QEntryCompletedCommand = new DelegateCommand(ExecuteB15QCompleted);
      #endregion
      #region Band16
      B16FreqEntryCompletedCommand = new DelegateCommand(ExecuteB16FreqEntryCompleted);
      B16GainEntryCompletedCommand = new DelegateCommand(ExecuteB16GainCompleted);
      B16QEntryCompletedCommand = new DelegateCommand(ExecuteB16QCompleted);
      #endregion
      #region Band17
      B17FreqEntryCompletedCommand = new DelegateCommand(ExecuteB17FreqEntryCompleted);
      B17GainEntryCompletedCommand = new DelegateCommand(ExecuteB17GainCompleted);
      B17QEntryCompletedCommand = new DelegateCommand(ExecuteB17QCompleted);
      #endregion
      #region Band18
      B18FreqEntryCompletedCommand = new DelegateCommand(ExecuteB18FreqEntryCompleted);
      B18GainEntryCompletedCommand = new DelegateCommand(ExecuteB18GainCompleted);
      B18QEntryCompletedCommand = new DelegateCommand(ExecuteB18QCompleted);
      #endregion
      #region Band19
      B19FreqEntryCompletedCommand = new DelegateCommand(ExecuteB19FreqEntryCompleted);
      B19GainEntryCompletedCommand = new DelegateCommand(ExecuteB19GainCompleted);
      B19QEntryCompletedCommand = new DelegateCommand(ExecuteB19QCompleted);
      #endregion
      #region Band20
      B20FreqEntryCompletedCommand = new DelegateCommand(ExecuteB20FreqEntryCompleted);
      B20GainEntryCompletedCommand = new DelegateCommand(ExecuteB20GainCompleted);
      B20QEntryCompletedCommand = new DelegateCommand(ExecuteB20QCompleted);
      #endregion
      #region Band21
      B21FreqEntryCompletedCommand = new DelegateCommand(ExecuteB21FreqEntryCompleted);
      B21GainEntryCompletedCommand = new DelegateCommand(ExecuteB21GainCompleted);
      B21QEntryCompletedCommand = new DelegateCommand(ExecuteB21QCompleted);
      #endregion
      #region Band22
      B22FreqEntryCompletedCommand = new DelegateCommand(ExecuteB22FreqEntryCompleted);
      B22GainEntryCompletedCommand = new DelegateCommand(ExecuteB22GainCompleted);
      B22QEntryCompletedCommand = new DelegateCommand(ExecuteB22QCompleted);
      #endregion
      #region Band23
      B23FreqEntryCompletedCommand = new DelegateCommand(ExecuteB23FreqEntryCompleted);
      B23GainEntryCompletedCommand = new DelegateCommand(ExecuteB23GainCompleted);
      B23QEntryCompletedCommand = new DelegateCommand(ExecuteB23QCompleted);
      #endregion
      #region Band24
      B24FreqEntryCompletedCommand = new DelegateCommand(ExecuteB24FreqEntryCompleted);
      B24GainEntryCompletedCommand = new DelegateCommand(ExecuteB24GainCompleted);
      B24QEntryCompletedCommand = new DelegateCommand(ExecuteB24QCompleted);
      #endregion
      #region Band25
      B25FreqEntryCompletedCommand = new DelegateCommand(ExecuteB25FreqEntryCompleted);
      B25GainEntryCompletedCommand = new DelegateCommand(ExecuteB25GainCompleted);
      B25QEntryCompletedCommand = new DelegateCommand(ExecuteB25QCompleted);
      #endregion
      #region Band26
      B26FreqEntryCompletedCommand = new DelegateCommand(ExecuteB26FreqEntryCompleted);
      B26GainEntryCompletedCommand = new DelegateCommand(ExecuteB26GainCompleted);
      B26QEntryCompletedCommand = new DelegateCommand(ExecuteB26QCompleted);
      #endregion
      #region Band27
      B27FreqEntryCompletedCommand = new DelegateCommand(ExecuteB27FreqEntryCompleted);
      B27GainEntryCompletedCommand = new DelegateCommand(ExecuteB27GainCompleted);
      B27QEntryCompletedCommand = new DelegateCommand(ExecuteB27QCompleted);
      #endregion
      #region Band28
      B28FreqEntryCompletedCommand = new DelegateCommand(ExecuteB28FreqEntryCompleted);
      B28GainEntryCompletedCommand = new DelegateCommand(ExecuteB28GainCompleted);
      B28QEntryCompletedCommand = new DelegateCommand(ExecuteB28QCompleted);
      #endregion
      #region Band29
      B29FreqEntryCompletedCommand = new DelegateCommand(ExecuteB29FreqEntryCompleted);
      B29GainEntryCompletedCommand = new DelegateCommand(ExecuteB29GainCompleted);
      B29QEntryCompletedCommand = new DelegateCommand(ExecuteB29QCompleted);
      #endregion
      #region Band30
      B30FreqEntryCompletedCommand = new DelegateCommand(ExecuteB30FreqEntryCompleted);
      B30GainEntryCompletedCommand = new DelegateCommand(ExecuteB30GainCompleted);
      B30QEntryCompletedCommand = new DelegateCommand(ExecuteB30QCompleted);
      #endregion
      #region Band31
      B31FreqEntryCompletedCommand = new DelegateCommand(ExecuteB31FreqEntryCompleted);
      B31GainEntryCompletedCommand = new DelegateCommand(ExecuteB31GainCompleted);
      B31QEntryCompletedCommand = new DelegateCommand(ExecuteB31QCompleted);
      #endregion

      SendLinking();
     
      Debug.WriteLine("*********************************Page Loaded*******************");
     
    }
    #region Channel_Selection
    public bool IsEnabledCh1
    {
      get => (mask & 0x01) == 0x01;
      set
      {
        if (value) mask |= 0x01;
        else mask &= ~0x01;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }

    public bool IsEnabledCh2
    {
      get => (mask & 0x02) == 0x02;
      set
      {
        if (value) mask |= 0x02;
        else mask &= ~0x02;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }
    public bool IsEnabledCh3
    {
      get => (mask & 0x04) == 0x04;
      set
      {
        if (value) mask |= 0x04;
        else mask &= ~0x04;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }
    public bool IsEnabledCh4
    {
      get => (mask & 0x08) == 0x08;
      set
      {
        if (value) mask |= 0x08;
        else mask &= ~0x08;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }
    public bool IsEnabledCh5
    {
      get => (mask & 0x10) == 0x10;
      set
      {
        if (value) mask |= 0x10;
        else mask &= ~0x10;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }
    public bool IsEnabledCh6
    {
      get => (mask & 0x20) == 0x20;
      set
      {
        if (value) mask |= 0x20;
        else mask &= ~0x20;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }
    public bool IsEnabledCh7
    {
      get => (mask & 0x40) == 0x40;
      set
      {
        if (value) mask |= 0x40;
        else mask &= ~0x40;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }
    public bool IsEnabledCh8
    {
      get => (mask & 0x80) == 0x80;
      set
      {
        if (value) mask |= 0x80;
        else mask &= ~0x80;
        RaisePropertyChanged();
        Debug.WriteLine($"0x{mask:X2}");
      }
    }
#endregion
    public double FreqSliderMin => Math.Log10(20.0);
    public double FreqSliderMax => Math.Log10(20e3);

    public double GainSliderMin => Constants.ParaMetricBoostMin;
    public double GainSliderMax => Constants.ParaMetricBoostMax;
    public double QSliderMin => 1.5;
    public double QSliderMax => 10;

    public bool AllowSend = false;
    private int mask = 0xff;
    private bool[] _linked = new bool[8];
    private bool _activityIsLoading;

    public bool ActivityIsLoading
    {
      get => _activityIsLoading;
      set => SetProperty(ref _activityIsLoading, value);
    }

    private bool SendLinking()
    {

      if (!AllowSend)
        return true;
      var commandArray = new byte[12];

      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_PEQ_LINKING;
      commandArray[3] = 0;

      for (var i = 0; i != 8; i++)
      {
        if (_linked[i]) commandArray[3] |= (byte)(1 << i);
      }
      return Utils.Universal_Write(ref commandArray);
    }
    

    #region Band1
    public double B1FreqSliderValue
    {
      get => Math.Log10(_freq[0]);
      set
      {
        _sFreq[0] = FormatFreq(ref value);
        _freq[0] = value;
        SendFreq(0);
        RaisePropertyChanged("B1FreqEntryValue");
      }
    }
    public double B1GainSliderValue
    {
      get => _gain[0];
      set
      {
        _sGain[0] = $"{Math.Round(value + .05, 1):N1}";
        _gain[0] = value;
        Send_Gain(0);
        RaisePropertyChanged("B1GainEntryValue");
      }
    }
    public double B1QSliderValue
    {
      get => _q[0];
      set
      {
        _q[0] = value;
        Send_Q(0);
        _sQ[0] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B1QEntryValue");
      }
    }
    private void ExecuteB1FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[0], out var result)) return;
      _freq[0] = result;
      RaisePropertyChanged($"B1FreqSliderValue");
    }
    private void ExecuteB1GainCompleted()
    {
      if (!double.TryParse(_sGain[0], out var result)) return;
      _gain[0] = result;
      Send_Gain(0);
      RaisePropertyChanged($"B1GainSliderValue");
    }
    private void ExecuteB1QCompleted()
    {
      if (!double.TryParse(_sQ[0], out var result)) return;
      _q[0] = result;
      Send_Q(0);
      RaisePropertyChanged($"B1QSliderValue");
    }

    public string B1FreqEntryValue
    {
      get => _sFreq[0];
      set => _sFreq[0] = value;
    }
    public string B1GainEntryValue
    {
      get => _sGain[0];
      set => _sGain[0] = value;
    }
    public string B1QEntryValue
    {
      get => _sQ[0];
      set => _sQ[0] = value;
    }
    #endregion Band1
    #region Band2
    public double B2FreqSliderValue
    {
      get => Math.Log10(_freq[1]);
      set
      {
        _sFreq[1] = FormatFreq(ref value);
        _freq[1] = value;
        SendFreq(1);
        RaisePropertyChanged("B2FreqEntryValue");
      }
    }
    public double B2GainSliderValue
    {
      get => _gain[1];
      set
      {
        _sGain[1] = $"{Math.Round(value + .05, 1):N1}";
        _gain[1] = value;
        Send_Gain(1);
        RaisePropertyChanged("B2GainEntryValue");
      }
    }
    public double B2QSliderValue
    {
      get => _q[1];
      set
      {
        _q[1] = value;
        Send_Q(1);
        _sQ[1] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B2QEntryValue");
      }
    }
    private void ExecuteB2FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[1], out var result)) return;
      _freq[1] = result;
      RaisePropertyChanged($"B2FreqSliderValue");
    }
    private void ExecuteB2GainCompleted()
    {
      if (!double.TryParse(_sGain[1], out var result)) return;
      _gain[1] = result;
      Send_Gain(1);
      RaisePropertyChanged($"B2GainSliderValue");
    }
    private void ExecuteB2QCompleted()
    {
      if (!double.TryParse(_sQ[1], out var result)) return;
      _q[1] = result;
      Send_Q(1);
      RaisePropertyChanged($"B2QSliderValue");
    }

    public string B2FreqEntryValue
    {
      get => _sFreq[1];
      set => _sFreq[1] = value;
    }
    public string B2GainEntryValue
    {
      get => _sGain[1];
      set => _sGain[1] = value;
    }
    public string B2QEntryValue
    {
      get => _sQ[1];
      set => _sQ[1] = value;
    }
    #endregion Band2
    #region Band3
    public double B3FreqSliderValue
    {
      get => Math.Log10(_freq[2]);
      set
      {
        _sFreq[2] = FormatFreq(ref value);
        _freq[2] = value;
        SendFreq(2);
        RaisePropertyChanged("B3FreqEntryValue");
      }
    }
    public double B3GainSliderValue
    {
      get => _gain[2];
      set
      {
        _gain[2] = value;
        Send_Gain(2);
        _sGain[2] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B3GainEntryValue");
      }
    }
    public double B3QSliderValue
    {
      get => _q[2];
      set
      {
        _q[2] = value;
        Send_Q(2);
        _sQ[2] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B3QEntryValue");
      }
    }
    private void ExecuteB3FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[2], out var result)) return;
      _freq[2] = result;
      RaisePropertyChanged($"B3FreqSliderValue");
    }
    private void ExecuteB3GainCompleted()
    {
      if (!double.TryParse(_sGain[2], out var result)) return;
      _gain[2] = result;
      Send_Gain(2);
      RaisePropertyChanged($"B3GainSliderValue");
    }
    private void ExecuteB3QCompleted()
    {
      if (!double.TryParse(_sQ[2], out var result)) return;
      _q[2] = result;
      Send_Q(2);
      RaisePropertyChanged($"B3QSliderValue");
    }

    public string B3FreqEntryValue
    {
      get => _sFreq[2];
      set => _sFreq[2] = value;
    }
    public string B3GainEntryValue
    {
      get => _sGain[2];
      set => _sGain[2] = value;
    }
    public string B3QEntryValue
    {
      get => _sQ[2];
      set => _sQ[2] = value;
    }
    #endregion Band3
    #region Band4
    public double B4FreqSliderValue
    {
      get => Math.Log10(_freq[3]);
      set
      {
        _sFreq[3] = FormatFreq(ref value);
        _freq[3] = value;
        SendFreq(3);
        RaisePropertyChanged("B4FreqEntryValue");
      }
    }
    public double B4GainSliderValue
    {
      get => _gain[3];
      set
      {
        _gain[3] = value;
        Send_Gain(3);
        _sGain[3] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B4GainEntryValue");
      }
    }
    public double B4QSliderValue
    {
      get => _q[3];
      set
      {
        _q[3] = value;
        Send_Q(3);
        _sQ[3] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B4QEntryValue");
      }
    }
    private void ExecuteB4FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[3], out var result)) return;
      _freq[3] = result;
      RaisePropertyChanged($"B4FreqSliderValue");
    }
    private void ExecuteB4GainCompleted()
    {
      if (!double.TryParse(_sGain[3], out var result)) return;
      _gain[3] = result;
      Send_Gain(3);
      RaisePropertyChanged($"B4GainSliderValue");
    }
    private void ExecuteB4QCompleted()
    {
      if (!double.TryParse(_sQ[3], out var result)) return;
      _q[3] = result;
      Send_Q(3);
      RaisePropertyChanged($"B4QSliderValue");
    }

    public string B4FreqEntryValue
    {
      get => _sFreq[3];
      set => _sFreq[3] = value;
    }
    public string B4GainEntryValue
    {
      get => _sGain[3];
      set => _sGain[3] = value;
    }
    public string B4QEntryValue
    {
      get => _sQ[3];
      set => _sQ[3] = value;
    }
    #endregion Band4
    #region Band5
    public double B5FreqSliderValue
    {
      get => Math.Log10(_freq[4]);
      set
      {
        _sFreq[4] = FormatFreq(ref value);
        _freq[4] = value;
        SendFreq(4);
        RaisePropertyChanged("B5FreqEntryValue");
      }
    }
    public double B5GainSliderValue
    {
      get => _gain[4];
      set
      {
        _gain[4] = value;
        Send_Gain(4);
        _sGain[4] = $"{Math.Round(value + .05, 1):N1}";
         RaisePropertyChanged("B5GainEntryValue");
      }
    }
    public double B5QSliderValue
    {
      get => _q[4];
      set
      {
        _q[4] = value;
        Send_Q(4);
        _sQ[4] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B5QEntryValue");
      }
    }
    private void ExecuteB5FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[4], out var result)) return;
      _freq[4] = result;
      RaisePropertyChanged($"B5FreqSliderValue");
    }
    private void ExecuteB5GainCompleted()
    {
      if (!double.TryParse(_sGain[4], out var result)) return;
      _gain[4] = result;
      Send_Gain(4);
      RaisePropertyChanged($"B5GainSliderValue");
    }
    private void ExecuteB5QCompleted()
    {
      if (!double.TryParse(_sQ[4], out var result)) return;
      _q[4] = result;
      Send_Q(4);
      RaisePropertyChanged($"B5QSliderValue");
    }

    public string B5FreqEntryValue
    {
      get => _sFreq[4];
      set => _sFreq[4] = value;
    }
    public string B5GainEntryValue
    {
      get => _sGain[4];
      set => _sGain[4] = value;
    }
    public string B5QEntryValue
    {
      get => _sQ[4];
      set => _sQ[4] = value;
    }
    #endregion Band5
    #region Band6
    public double B6FreqSliderValue
    {
      get => Math.Log10(_freq[5]);
      set
      {
        _sFreq[5] = FormatFreq(ref value);
        _freq[5] = value;
        SendFreq(5);
        RaisePropertyChanged("B6FreqEntryValue");
      }
    }
    public double B6GainSliderValue
    {
      get => _gain[5];
      set
      {
        _gain[5] = value;
        Send_Gain(5);
        _sGain[5] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B6GainEntryValue");
      }
    }
    public double B6QSliderValue
    {
      get => _q[5];
      set
      {
        _q[5] = value;
        Send_Q(5);
        _sQ[5] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B6QEntryValue");
      }
    }
    private void ExecuteB6FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[5], out var result)) return;
      _freq[5] = result;
      RaisePropertyChanged($"B6FreqSliderValue");
    }
    private void ExecuteB6GainCompleted()
    {
      if (!double.TryParse(_sGain[5], out var result)) return;
      _gain[5] = result;
      Send_Gain(5);
      RaisePropertyChanged($"B6GainSliderValue");
    }
    private void ExecuteB6QCompleted()
    {
      if (!double.TryParse(_sQ[5], out var result)) return;
      _q[5] = result;
      Send_Q(5);
      RaisePropertyChanged($"B6QSliderValue");
    }

    public string B6FreqEntryValue
    {
      get => _sFreq[5];
      set => _sFreq[5] = value;
    }
    public string B6GainEntryValue
    {
      get => _sGain[5];
      set => _sGain[5] = value;
    }
    public string B6QEntryValue
    {
      get => _sQ[5];
      set => _sQ[5] = value;
    }
    #endregion Band6
    #region Band7
    public double B7FreqSliderValue
    {
      get => Math.Log10(_freq[6]);
      set
      {
        _sFreq[6] = FormatFreq(ref value);
        _freq[6] = value;
        SendFreq(6);
        RaisePropertyChanged("B7FreqEntryValue");
      }
    }
    public double B7GainSliderValue
    {
      get => _gain[6];
      set
      {
        _gain[6] = value;
        Send_Gain(6);
        _sGain[6] = $"{Math.Round(value + .05, 1):N1}";
         RaisePropertyChanged("B7GainEntryValue");
      }
    }
    public double B7QSliderValue
    {
      get => _q[6];
      set
      {
        _q[6] = value;
        Send_Q(6);
        _sQ[6] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B7QEntryValue");
      }
    }
    private void ExecuteB7FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[6], out var result)) return;
      _freq[6] = result;
      RaisePropertyChanged($"B7FreqSliderValue");
    }
    private void ExecuteB7GainCompleted()
    {
      if (!double.TryParse(_sGain[6], out var result)) return;
      _gain[6] = result;
      Send_Gain(6);
      RaisePropertyChanged($"B7GainSliderValue");
    }
    private void ExecuteB7QCompleted()
    {
      if (!double.TryParse(_sQ[6], out var result)) return;
      _q[6] = result;
      Send_Q(6);
      RaisePropertyChanged($"B7QSliderValue");
    }

    public string B7FreqEntryValue
    {
      get => _sFreq[6];
      set => _sFreq[6] = value;
    }
    public string B7GainEntryValue
    {
      get => _sGain[6];
      set => _sGain[6] = value;
    }
    public string B7QEntryValue
    {
      get => _sQ[6];
      set => _sQ[6] = value;
    }
    #endregion Band7
    #region Band8
    public double B8FreqSliderValue
    {
      get => Math.Log10(_freq[7]);
      set
      {
        _sFreq[7] = FormatFreq(ref value);
        _freq[7] = value;
        SendFreq(7);
        RaisePropertyChanged("B8FreqEntryValue");
      }
    }
    public double B8GainSliderValue
    {
      get => _gain[7];
      set
      {
        _gain[7] = value;
        Send_Gain(7);
        _sGain[7] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B8GainEntryValue");
      }
    }
    public double B8QSliderValue
    {
      get => _q[7];
      set
      {
        _q[7] = value;
        Send_Q(7);
        _sQ[7] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B8QEntryValue");
      }
    }
    private void ExecuteB8FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[7], out var result)) return;
      _freq[7] = result;
      RaisePropertyChanged($"B8FreqSliderValue");
    }
    private void ExecuteB8GainCompleted()
    {
      if (!double.TryParse(_sGain[7], out var result)) return;
      _gain[7] = result;
      Send_Gain(7);
      RaisePropertyChanged($"B8GainSliderValue");
    }
    private void ExecuteB8QCompleted()
    {
      if (!double.TryParse(_sQ[7], out var result)) return;
      _q[7] = result;
      Send_Q(7);
      RaisePropertyChanged($"B8QSliderValue");
    }

    public string B8FreqEntryValue
    {
      get => _sFreq[7];
      set => _sFreq[7] = value;
    }
    public string B8GainEntryValue
    {
      get => _sGain[7];
      set => _sGain[7] = value;
    }
    public string B8QEntryValue
    {
      get => _sQ[7];
      set => _sQ[7] = value;
    }
    #endregion Band8
    #region Band9
    public double B9FreqSliderValue
    {
      get => Math.Log10(_freq[8]);
      set
      {
        _sFreq[8] = FormatFreq(ref value);
        _freq[8] = value;
        SendFreq(8);
        RaisePropertyChanged("B9FreqEntryValue");
      }
    }
    public double B9GainSliderValue
    {
      get => _gain[8];
      set
      {
        _gain[8] = value;
        Send_Gain(8);
        _sGain[8] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B9GainEntryValue");
      }
    }
    public double B9QSliderValue
    {
      get => _q[8];
      set
      {
        _q[8] = value;
        Send_Q(8);
        _sQ[8] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B9QEntryValue");
      }
    }
    private void ExecuteB9FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[8], out var result)) return;
      _freq[8] = result;
      RaisePropertyChanged($"B9FreqSliderValue");
    }
    private void ExecuteB9GainCompleted()
    {
      if (!double.TryParse(_sGain[8], out var result)) return;
      _gain[8] = result;
      Send_Gain(8);
      RaisePropertyChanged($"B9GainSliderValue");
    }
    private void ExecuteB9QCompleted()
    {
      if (!double.TryParse(_sQ[8], out var result)) return;
      _q[8] = result;
      RaisePropertyChanged($"B9QSliderValue");
    }

    public string B9FreqEntryValue
    {
      get => _sFreq[8];
      set => _sFreq[8] = value;
    }
    public string B9GainEntryValue
    {
      get => _sGain[8];
      set => _sGain[8] = value;
    }
    public string B9QEntryValue
    {
      get => _sQ[8];
      set => _sQ[8] = value;
    }
    #endregion Band9
    #region Band10
    public double B10FreqSliderValue
    {
      get => Math.Log10(_freq[9]);
      set
      {
        _sFreq[9] = FormatFreq(ref value);
        _freq[9] = value;
        SendFreq(9);
        RaisePropertyChanged("B10FreqEntryValue");
      }
    }
    public double B10GainSliderValue
    {
      get => _gain[9];
      set
      {
        _gain[9] = value;
        Send_Gain(9);
        _sGain[9] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B10GainEntryValue");
      }
    }
    public double B10QSliderValue
    {
      get => _q[9];
      set
      {
        _q[9] = value;
        Send_Q(9);
        _sQ[9] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B10QEntryValue");
      }
    }
    private void ExecuteB10FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[9], out var result)) return;
      _freq[9] = result;
      RaisePropertyChanged($"B10FreqSliderValue");
    }
    private void ExecuteB10GainCompleted()
    {
      if (!double.TryParse(_sGain[9], out var result)) return;
      _gain[9] = result;
      Send_Gain(9);
      RaisePropertyChanged($"B10GainSliderValue");
    }
    private void ExecuteB10QCompleted()
    {
      if (!double.TryParse(_sQ[9], out var result)) return;
      _q[9] = result;
      Send_Q(9);
      RaisePropertyChanged($"B10QSliderValue");
    }

    public string B10FreqEntryValue
    {
      get => _sFreq[9];
      set => _sFreq[9] = value;
    }
    public string B10GainEntryValue
    {
      get => _sGain[9];
      set => _sGain[9] = value;
    }
    public string B10QEntryValue
    {
      get => _sQ[9];
      set => _sQ[9] = value;
    }
    #endregion Band10
    #region Band11
    public double B11FreqSliderValue
    {
      get => Math.Log10(_freq[10]);
      set
      {
        _sFreq[10] = FormatFreq(ref value);
        _freq[10] = value;
        SendFreq(10);
        RaisePropertyChanged("B11FreqEntryValue");
      }
    }
    public double B11GainSliderValue
    {
      get => _gain[10];
      set
      {
        _gain[10] = value;
        Send_Gain(10);
        _sGain[10] = $"{Math.Round(value + .05, 1):N1}";
         RaisePropertyChanged("B11GainEntryValue");
      }
    }
    public double B11QSliderValue
    {
      get => _q[10];
      set
      {
        _q[10] = value;
        Send_Q(10);
        _sQ[10] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B11QEntryValue");
      }
    }
    private void ExecuteB11FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[10], out var result)) return;
      _freq[10] = result;
      RaisePropertyChanged($"B11FreqSliderValue");
    }
    private void ExecuteB11GainCompleted()
    {
      if (!double.TryParse(_sGain[10], out var result)) return;
      _gain[10] = result;
      Send_Gain(10);
      RaisePropertyChanged($"B11GainSliderValue");
    }
    private void ExecuteB11QCompleted()
    {
      if (!double.TryParse(_sQ[10], out var result)) return;
      _q[10] = result;
      Send_Q(10);
      RaisePropertyChanged($"B11QSliderValue");
    }

    public string B11FreqEntryValue
    {
      get => _sFreq[10];
      set => _sFreq[10] = value;
    }
    public string B11GainEntryValue
    {
      get => _sGain[10];
      set => _sGain[10] = value;
    }
    public string B11QEntryValue
    {
      get => _sQ[10];
      set => _sQ[10] = value;
    }
    #endregion Band11
    #region Band12
    public double B12FreqSliderValue
    {
      get => Math.Log10(_freq[11]);
      set
      {
        _sFreq[11] = FormatFreq(ref value);
        _freq[11] = value;
        SendFreq(11);
        RaisePropertyChanged("B12FreqEntryValue");
      }
    }
    public double B12GainSliderValue
    {
      get => _gain[11];
      set
      {
        _gain[11] = value;
        Send_Gain(11);
        _sGain[11] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B12GainEntryValue");
      }
    }
    public double B12QSliderValue
    {
      get => _q[11];
      set
      {
        _q[11] = value;
        Send_Q(11);
        _sQ[11] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B12QEntryValue");
      }
    }
    private void ExecuteB12FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[11], out var result)) return;
      _freq[11] = result;
      RaisePropertyChanged($"B12FreqSliderValue");
    }
    private void ExecuteB12GainCompleted()
    {
      if (!double.TryParse(_sGain[11], out var result)) return;
      _gain[11] = result;
      Send_Gain(11);
      RaisePropertyChanged($"B12GainSliderValue");
    }
    private void ExecuteB12QCompleted()
    {
      if (!double.TryParse(_sQ[11], out var result)) return;
      _q[11] = result;
      Send_Q(11);
      RaisePropertyChanged($"B12QSliderValue");
    }

    public string B12FreqEntryValue
    {
      get => _sFreq[11];
      set => _sFreq[11] = value;
    }
    public string B12GainEntryValue
    {
      get => _sGain[11];
      set => _sGain[11] = value;
    }
    public string B12QEntryValue
    {
      get => _sQ[11];
      set => _sQ[11] = value;
    }
    #endregion Band12
    #region Band13
    public double B13FreqSliderValue
    {
      get => Math.Log10(_freq[12]); set
      {
        _sFreq[12] = FormatFreq(ref value);
        _freq[12] = value;
        SendFreq(12);
        RaisePropertyChanged("B13FreqEntryValue");
      }
    }
    public double B13GainSliderValue
    {
      get => _gain[12];
      set
      {
        _gain[12] = value;
        Send_Gain(12);
        _sGain[12] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B13GainEntryValue");
      }
    }
    public double B13QSliderValue
    {
      get => _q[12];
      set
      {
        _q[12] = value;
        Send_Q(12);
        _sQ[12] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B13QEntryValue");
      }
    }
    private void ExecuteB13FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[12], out var result)) return;
      _freq[12] = result;
      RaisePropertyChanged($"B13FreqSliderValue");
    }
    private void ExecuteB13GainCompleted()
    {
      if (!double.TryParse(_sGain[12], out var result)) return;
      _gain[12] = result;
      Send_Gain(12);
      RaisePropertyChanged($"B13GainSliderValue");
    }
    private void ExecuteB13QCompleted()
    {
      if (!double.TryParse(_sQ[12], out var result)) return;
      _q[12] = result;
      Send_Q(12);
      RaisePropertyChanged($"B13QSliderValue");
    }

    public string B13FreqEntryValue
    {
      get => _sFreq[12];
      set => _sFreq[12] = value;
    }
    public string B13GainEntryValue
    {
      get => _sGain[12];
      set => _sGain[12] = value;
    }
    public string B13QEntryValue
    {
      get => _sQ[12];
      set => _sQ[12] = value;
    }
    #endregion Band13
    #region Band14
    public double B14FreqSliderValue
    {
      get => Math.Log10(_freq[13]); set
      {
        _sFreq[13] = FormatFreq(ref value);
        _freq[13] = value;
        SendFreq(13);
        RaisePropertyChanged("B14FreqEntryValue");
      }
    }
    public double B14GainSliderValue
    {
      get => _gain[13];
      set
      {
        _gain[13] = value;
        Send_Gain(13);
        _sGain[13] = $"{Math.Round(value + .05, 1):N1}";
           RaisePropertyChanged("B14GainEntryValue");
      }
    }
    public double B14QSliderValue
    {
      get => _q[13];
      set
      {
        _q[13] = value;
        Send_Q(13);
        _sQ[13] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B14QEntryValue");
      }
    }
    private void ExecuteB14FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[13], out var result)) return;
      _freq[13] = result;
      RaisePropertyChanged($"B14FreqSliderValue");
    }
    private void ExecuteB14GainCompleted()
    {
      if (!double.TryParse(_sGain[13], out var result)) return;
      _gain[13] = result;
      Send_Gain(13);
      RaisePropertyChanged($"B14GainSliderValue");
    }
    private void ExecuteB14QCompleted()
    {
      if (!double.TryParse(_sQ[13], out var result)) return;
      _q[13] = result;
      Send_Q(13);
      RaisePropertyChanged($"B14QSliderValue");
    }

    public string B14FreqEntryValue
    {
      get => _sFreq[13];
      set => _sFreq[13] = value;
    }
    public string B14GainEntryValue
    {
      get => _sGain[13];
      set => _sGain[13] = value;
    }
    public string B14QEntryValue
    {
      get => _sQ[13];
      set => _sQ[13] = value;
    }
    #endregion Band14
    #region Band15
    public double B15FreqSliderValue
    {
      get => Math.Log10(_freq[14]);
      set
      {
        _sFreq[14] = FormatFreq(ref value);
        _freq[14] = value;
        SendFreq(14);
        RaisePropertyChanged("B15FreqEntryValue");
      }
    }
    public double B15GainSliderValue
    {
      get => _gain[14];
      set
      {
        _gain[14] = value;
        Send_Gain(14);
        _sGain[14] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B15GainEntryValue");
      }
    }
    public double B15QSliderValue
    {
      get => _q[14];
      set
      {
        _q[14] = value;
        Send_Q(14);
        _sQ[14] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B15QEntryValue");
      }
    }
    private void ExecuteB15FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[14], out var result)) return;
      _freq[14] = result;
      RaisePropertyChanged($"B15FreqSliderValue");
    }
    private void ExecuteB15GainCompleted()
    {
      if (!double.TryParse(_sGain[14], out var result)) return;
      _gain[14] = result;
      Send_Gain(14);
      RaisePropertyChanged($"B15GainSliderValue");
    }
    private void ExecuteB15QCompleted()
    {
      if (!double.TryParse(_sQ[14], out var result)) return;
      _q[14] = result;
      Send_Q(14);
      RaisePropertyChanged($"B15QSliderValue");
    }

    public string B15FreqEntryValue
    {
      get => _sFreq[14];
      set => _sFreq[14] = value;
    }
    public string B15GainEntryValue
    {
      get => _sGain[14];
      set => _sGain[14] = value;
    }
    public string B15QEntryValue
    {
      get => _sQ[14];
      set => _sQ[14] = value;
    }
    #endregion Band15
    #region Band16
    public double B16FreqSliderValue
    {
      get => Math.Log10(_freq[15]); set
      {
        _sFreq[15] = FormatFreq(ref value);
        _freq[15] = value;
        SendFreq(15);
        RaisePropertyChanged("B16FreqEntryValue");
      }
    }
    public double B16GainSliderValue
    {
      get => _gain[15];
      set
      {
        _gain[15] = value;
        Send_Gain(15);
        _sGain[15] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B16GainEntryValue");
      }
    }
    public double B16QSliderValue
    {
      get => _q[15];
      set
      {
        _q[15] = value;
        Send_Q(15);
        _sQ[15] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B16QEntryValue");
      }
    }
    private void ExecuteB16FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[15], out var result)) return;
      _freq[15] = result;
      RaisePropertyChanged($"B16FreqSliderValue");
    }
    private void ExecuteB16GainCompleted()
    {
      if (!double.TryParse(_sGain[15], out var result)) return;
      _gain[15] = result;
      Send_Gain(15);
      RaisePropertyChanged($"B16GainSliderValue");
    }
    private void ExecuteB16QCompleted()
    {
      if (!double.TryParse(_sQ[15], out var result)) return;
      _q[15] = result;
      Send_Q(15);
      RaisePropertyChanged($"B16QSliderValue");
    }

    public string B16FreqEntryValue
    {
      get => _sFreq[15];
      set => _sFreq[15] = value;
    }
    public string B16GainEntryValue
    {
      get => _sGain[15];
      set => _sGain[15] = value;
    }
    public string B16QEntryValue
    {
      get => _sQ[15];
      set => _sQ[15] = value;
    }
    #endregion Band16
    #region Band17
    public double B17FreqSliderValue
    {
      get => Math.Log10(_freq[16]); set
      {
        _sFreq[16] = FormatFreq(ref value);
        _freq[16] = value;
        SendFreq(16);
        RaisePropertyChanged("B17FreqEntryValue");
      }
    }
    public double B17GainSliderValue
    {
      get => _gain[16];
      set
      {
        _gain[16] = value;
        Send_Gain(16);
        _sGain[16] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B17GainEntryValue");
      }
    }
    public double B17QSliderValue
    {
      get => _q[16];
      set
      {
        _q[16] = value;
        Send_Q(16);
        _sQ[16] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B17QEntryValue");
      }
    }
    private void ExecuteB17FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[16], out var result)) return;
      _freq[16] = result;
      RaisePropertyChanged($"B17FreqSliderValue");
    }
    private void ExecuteB17GainCompleted()
    {
      if (!double.TryParse(_sGain[16], out var result)) return;
      _gain[16] = result;
      Send_Gain(16);
      RaisePropertyChanged($"B17GainSliderValue");
    }
    private void ExecuteB17QCompleted()
    {
      if (!double.TryParse(_sQ[16], out var result)) return;
      _q[16] = result;
      Send_Q(16);
      RaisePropertyChanged($"B17QSliderValue");
    }
    public string B17FreqEntryValue
    {
      get => _sFreq[16];
      set => _sFreq[16] = value;
    }
    public string B17GainEntryValue
    {
      get => _sGain[16];
      set => _sGain[16] = value;
    }
    public string B17QEntryValue
    {
      get => _sQ[16];
      set => _sQ[16] = value;
    }
    #endregion Band17
    #region Band18
    public double B18FreqSliderValue
    {
      get => Math.Log10(_freq[17]); set
      {
        _sFreq[17] = FormatFreq(ref value);
        _freq[17] = value;
        SendFreq(17);
        RaisePropertyChanged("B18FreqEntryValue");
      }
    }
    public double B18GainSliderValue
    {
      get => _gain[17];
      set
      {
        _gain[17] = value;
        Send_Gain(17);
        _sGain[17] = $"{Math.Round(value + .05, 1):N1}";
           RaisePropertyChanged("B18GainEntryValue");
      }
    }
    public double B18QSliderValue
    {
      get => _q[17];
      set
      {
        _q[17] = value;
        Send_Q(17);
        _sQ[17] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B18QEntryValue");
      }
    }
    private void ExecuteB18FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[17], out var result)) return;
      _freq[17] = result;
      RaisePropertyChanged($"B18FreqSliderValue");
    }
    private void ExecuteB18GainCompleted()
    {
      if (!double.TryParse(_sGain[17], out var result)) return;
      _gain[17] = result;
      Send_Gain(17);
      RaisePropertyChanged($"B18GainSliderValue");
    }
    private void ExecuteB18QCompleted()
    {
      if (!double.TryParse(_sQ[17], out var result)) return;
      _q[17] = result;
      Send_Q(17);
      RaisePropertyChanged($"B18QSliderValue");
    }
    public string B18FreqEntryValue
    {
      get => _sFreq[17];
      set => _sFreq[17] = value;
    }
    public string B18GainEntryValue
    {
      get => _sGain[17];
      set => _sGain[17] = value;
    }
    public string B18QEntryValue
    {
      get => _sQ[17];
      set => _sQ[17] = value;
    }
    #endregion Band17
    #region Band19
    public double B19FreqSliderValue
    {
      get => Math.Log10(_freq[18]); set
      {
        _sFreq[18] = FormatFreq(ref value);
        _freq[18] = value;
        SendFreq(18);
        RaisePropertyChanged("B19FreqEntryValue");
      }
    }
    public double B19GainSliderValue
    {
      get => _gain[18];
      set
      {
        _gain[18] = value;
        Send_Gain(18);
        _sGain[18] = $"{Math.Round(value + .05, 1):N1}";
         RaisePropertyChanged("B19GainEntryValue");
      }
    }
    public double B19QSliderValue
    {
      get => _q[18];
      set
      {
        _q[18] = value;
        Send_Q(18);
        _sQ[18] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B19QEntryValue");
      }
    }
    private void ExecuteB19FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[18], out var result)) return;
      _freq[18] = result;
      RaisePropertyChanged($"B19FreqSliderValue");
    }
    private void ExecuteB19GainCompleted()
    {
      if (!double.TryParse(_sGain[18], out var result)) return;
      _gain[18] = result;
      Send_Gain(18);
      RaisePropertyChanged($"B19GainSliderValue");
    }
    private void ExecuteB19QCompleted()
    {
      if (!double.TryParse(_sQ[18], out var result)) return;
      _q[18] = result;
      Send_Q(18);
      RaisePropertyChanged($"B19QSliderValue");
    }

    public string B19FreqEntryValue
    {
      get => _sFreq[18];
      set => _sFreq[18] = value;
    }
    public string B19GainEntryValue
    {
      get => _sGain[18];
      set => _sGain[18] = value;
    }
    public string B19QEntryValue
    {
      get => _sQ[18];
      set => _sQ[18] = value;
    }
    #endregion Band19
    #region Band20
    public double B20FreqSliderValue
    {
      get => Math.Log10(_freq[19]);
      set
      {
        _sFreq[19] = FormatFreq(ref value);
        _freq[19] = value;
        SendFreq(19);
        RaisePropertyChanged("B20FreqEntryValue");
      }
    }
    public double B20GainSliderValue
    {
      get => _gain[19];
      set
      {
        _gain[19] = value;
        Send_Gain(19);
        _sGain[19] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B20GainEntryValue");
      }
    }
    public double B20QSliderValue
    {
      get => _q[19];
      set
      {
        _q[19] = value;
        Send_Q(19);
        _sQ[19] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B20QEntryValue");
      }
    }
    private void ExecuteB20FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[19], out var result)) return;
      _freq[19] = result;
      RaisePropertyChanged($"B20FreqSliderValue");
    }
    private void ExecuteB20GainCompleted()
    {
      if (!double.TryParse(_sGain[19], out var result)) return;
      _gain[19] = result;
      Send_Gain(19);
      RaisePropertyChanged($"B20GainSliderValue");
    }
    private void ExecuteB20QCompleted()
    {
      if (!double.TryParse(_sQ[19], out var result)) return;
      _q[19] = result;
      Send_Q(19);
      RaisePropertyChanged($"B20QSliderValue");
    }

    public string B20FreqEntryValue
    {
      get => _sFreq[19];
      set => _sFreq[19] = value;
    }
    public string B20GainEntryValue
    {
      get => _sGain[19];
      set => _sGain[19] = value;
    }
    public string B20QEntryValue
    {
      get => _sQ[19];
      set => _sQ[19] = value;
    }
    #endregion Band20
    #region Band21
    public double B21FreqSliderValue
    {
      get => Math.Log10(_freq[20]); set
      {
        _sFreq[20] = FormatFreq(ref value);
        RaisePropertyChanged("B21FreqEntryValue");
      }
    }
    public double B21GainSliderValue
    {
      get => _gain[20];
      set
      {
        _gain[20] = value;
        Send_Gain(20);
        _sGain[20] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B21GainEntryValue");
      }
    }
    public double B21QSliderValue
    {
      get => _q[20];
      set
      {
        _q[20] = value;
        Send_Q(20);
        _sQ[20] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B21QEntryValue");
      }
    }
    private void ExecuteB21FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[20], out var result)) return;
      _freq[20] = result;
      RaisePropertyChanged($"B21FreqSliderValue");
    }
    private void ExecuteB21GainCompleted()
    {
      if (!double.TryParse(_sGain[20], out var result)) return;
      _gain[20] = result;
      Send_Gain(20);
      RaisePropertyChanged($"B21GainSliderValue");
    }
    private void ExecuteB21QCompleted()
    {
      if (!double.TryParse(_sQ[20], out var result)) return;
      _q[20] = result;
      Send_Q(20);
      RaisePropertyChanged($"B21QSliderValue");
    }
    public string B21FreqEntryValue
    {
      get => _sFreq[20];
      set => _sFreq[20] = value;
    }
    public string B21GainEntryValue
    {
      get => _sGain[20];
      set => _sGain[20] = value;
    }
    public string B21QEntryValue
    {
      get => _sQ[20];
      set => _sQ[20] = value;
    }
    #endregion Band21
    #region Band22
    public double B22FreqSliderValue
    {
      get => Math.Log10(_freq[21]); set
      {
        _sFreq[21] = FormatFreq(ref value);
        _freq[21] = value;
        SendFreq(21);
        RaisePropertyChanged("B22FreqEntryValue");
      }
    }
    public double B22GainSliderValue
    {
      get => _gain[21];
      set
      {
        _gain[21] = value;
        Send_Gain(21);
        _sGain[21] = $"{Math.Round(value + .05, 1):N1}";
           RaisePropertyChanged("B22GainEntryValue");
      }
    }
    public double B22QSliderValue
    {
      get => _q[21];
      set
      {
        _q[21] = value;
        Send_Q(21);
        _sQ[21] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B22QEntryValue");
      }
    }
    private void ExecuteB22FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[21], out var result)) return;
      _freq[21] = result;
      RaisePropertyChanged($"B22FreqSliderValue");
    }
    private void ExecuteB22GainCompleted()
    {
      if (!double.TryParse(_sGain[21], out var result)) return;
      _gain[21] = result;
      Send_Gain(21);
      RaisePropertyChanged($"B22GainSliderValue");
    }
    private void ExecuteB22QCompleted()
    {
      if (!double.TryParse(_sQ[21], out var result)) return;
      _q[21] = result;
      Send_Q(21);
      RaisePropertyChanged($"B22QSliderValue");
    }
    public string B22FreqEntryValue
    {
      get => _sFreq[21];
      set => _sFreq[21] = value;
    }
    public string B22GainEntryValue
    {
      get => _sGain[21];
      set => _sGain[21] = value;
    }
    public string B22QEntryValue
    {
      get => _sQ[21];
      set => _sQ[21] = value;
    }
    #endregion Band22
    #region Band23
    public double B23FreqSliderValue
    {
      get => Math.Log10(_freq[22]); set
      {
        _sFreq[22] = FormatFreq(ref value);
        _freq[22] = value;
        SendFreq(22);
        RaisePropertyChanged("B23FreqEntryValue");
      }
    }
    public double B23GainSliderValue
    {
      get => _gain[22];
      set
      {
        _gain[22] = value;
        Send_Gain(22);
        _sGain[22] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B23GainEntryValue");
      }
    }
    public double B23QSliderValue
    {
      get => _q[22];
      set
      {
        _q[22] = value;
        Send_Q(22);
        _sQ[22] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B23QEntryValue");
      }
    }
    private void ExecuteB23FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[22], out var result)) return;
      _freq[22] = result;
      RaisePropertyChanged($"B23FreqSliderValue");
    }
    private void ExecuteB23GainCompleted()
    {
      if (!double.TryParse(_sGain[22], out var result)) return;
      _gain[22] = result;
      Send_Gain(22);
      RaisePropertyChanged($"B23GainSliderValue");
    }
    private void ExecuteB23QCompleted()
    {
      if (!double.TryParse(_sQ[22], out var result)) return;
      _q[22] = result;
      Send_Q(22);
      RaisePropertyChanged($"B23QSliderValue");
    }
    public string B23FreqEntryValue
    {
      get => _sFreq[22];
      set => _sFreq[22] = value;
    }
    public string B23GainEntryValue
    {
      get => _sGain[22];
      set => _sGain[22] = value;
    }
    public string B23QEntryValue
    {
      get => _sQ[22];
      set => _sQ[22] = value;
    }
    #endregion Band23
    #region Band24
    public double B24FreqSliderValue
    {
      get => Math.Log10(_freq[23]); set
      {
        _sFreq[23] = FormatFreq(ref value);
        _freq[23] = value;
        SendFreq(23);
        RaisePropertyChanged("B24FreqEntryValue");
      }
    }
    public double B24GainSliderValue
    {
      get => _gain[23];
      set
      {
        _gain[23] = value;
        Send_Gain(23);
        _sGain[23] = $"{Math.Round(value + .05, 1):N1}";
            RaisePropertyChanged("B24GainEntryValue");
      }
    }
    public double B24QSliderValue
    {
      get => _q[23];
      set
      {
        _q[23] = value;
        Send_Q(23);
        _sQ[23] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B24QEntryValue");
      }
    }
    private void ExecuteB24FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[23], out var result)) return;
      _freq[23] = result;
      RaisePropertyChanged($"B24FreqSliderValue");
    }
    private void ExecuteB24GainCompleted()
    {
      if (!double.TryParse(_sGain[23], out var result)) return;
      _gain[23] = result;
      Send_Gain(23);
      RaisePropertyChanged($"B24GainSliderValue");
    }
    private void ExecuteB24QCompleted()
    {
      if (!double.TryParse(_sQ[23], out var result)) return;
      _q[23] = result;
      Send_Q(23);
      RaisePropertyChanged($"B24QSliderValue");
    }
    public string B24FreqEntryValue
    {
      get => _sFreq[23];
      set => _sFreq[23] = value;
    }
    public string B24GainEntryValue
    {
      get => _sGain[23];
      set => _sGain[23] = value;
    }
    public string B24QEntryValue
    {
      get => _sQ[23];
      set => _sQ[23] = value;
    }
    #endregion Band24
    #region Band25
    public double B25FreqSliderValue
    {
      get => Math.Log10(_freq[24]);
      set
      {
       _sFreq[24] = FormatFreq(ref value);
        _freq[24] = value;
        SendFreq(24);
        RaisePropertyChanged("B25FreqEntryValue");
      }
    }
    public double B25GainSliderValue
    {
      get => _gain[24];
      set
      {
        _gain[42] = value;
        Send_Gain(24);
        _sGain[24] = $"{Math.Round(value + .05, 1):N1}";
            RaisePropertyChanged("B25GainEntryValue");
      }
    }
    public double B25QSliderValue
    {
      get => _q[24];
      set
      {
        _q[24] = value;
        Send_Q(24);
        _sQ[24] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B25QEntryValue");
      }
    }
    private void ExecuteB25FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[24], out var result)) return;
      _freq[24] = result;
      RaisePropertyChanged($"B25FreqSliderValue");
    }
    private void ExecuteB25GainCompleted()
    {
      if (!double.TryParse(_sGain[24], out var result)) return;
      _gain[24] = result;
      Send_Gain(24);
      RaisePropertyChanged($"B25GainSliderValue");
    }
    private void ExecuteB25QCompleted()
    {
      if (!double.TryParse(_sQ[24], out var result)) return;
      _q[24] = result;
      Send_Q(24);
      RaisePropertyChanged($"B25QSliderValue");
    }
    public string B25FreqEntryValue
    {
      get => _sFreq[24];
      set => _sFreq[24] = value;
    }
    public string B25GainEntryValue
    {
      get => _sGain[24];
      set => _sGain[24] = value;
    }
    public string B25QEntryValue
    {
      get => _sQ[24];
      set => _sQ[24] = value;
    }
    #endregion Band25
    #region Band26
    public double B26FreqSliderValue
    {
      get => Math.Log10(_freq[25]);
      set
      {
        _sFreq[25] = FormatFreq(ref value);
        _freq[25] = value;
        SendFreq(25);
        RaisePropertyChanged("B26FreqEntryValue");
      }
    }
    public double B26GainSliderValue
    {
      get => _gain[25];
      set
      {
        _gain[26] = value;
        Send_Gain(26);
        _sGain[25] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B26GainEntryValue");
      }
    }
    public double B26QSliderValue
    {
      get => _q[25];
      set
      {
        _q[25] = value;
        Send_Q(25);
        _sQ[25] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B26QEntryValue");
      }
    }
    private void ExecuteB26FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[25], out var result)) return;
      _freq[25] = result;
      RaisePropertyChanged($"B26FreqSliderValue");
    }
    private void ExecuteB26GainCompleted()
    {
      if (!double.TryParse(_sGain[25], out var result)) return;
      _gain[25] = result;
      Send_Gain(25);
      RaisePropertyChanged($"B26GainSliderValue");
    }
    private void ExecuteB26QCompleted()
    {
      if (!double.TryParse(_sQ[25], out var result)) return;
      _q[25] = result;
      Send_Q(25);
      RaisePropertyChanged($"B26QSliderValue");
    }
    public string B26FreqEntryValue
    {
      get => _sFreq[25];
      set => _sFreq[25] = value;
    }
    public string B26GainEntryValue
    {
      get => _sGain[25];
      set => _sGain[25] = value;
    }
    public string B26QEntryValue
    {
      get => _sQ[25];
      set => _sQ[25] = value;
    }
    #endregion Band26
    #region Band27
    public double B27FreqSliderValue
    {
      get => Math.Log10(_freq[26]);
      set
      {
        _sFreq[26] = FormatFreq(ref value);
        _freq[26] = value;
        SendFreq(26);
        RaisePropertyChanged("B27FreqEntryValue");
      }
    }
    public double B27GainSliderValue
    {
      get => _gain[26];
      set
      {
        _gain[26] = value;
        Send_Gain(26);
        _sGain[26] = $"{Math.Round(value + .05, 1):N1}";
           RaisePropertyChanged("B27GainEntryValue");
      }
    }
    public double B27QSliderValue
    {
      get => _q[26];
      set
      {
        _q[26] = value;
        Send_Q(26);
        _sQ[26] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B27QEntryValue");
      }
    }
    private void ExecuteB27FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[26], out var result)) return;
      _freq[26] = result;
      RaisePropertyChanged($"B27FreqSliderValue");
    }
    private void ExecuteB27GainCompleted()
    {
      if (!double.TryParse(_sGain[26], out var result)) return;
      _gain[26] = result;
      Send_Gain(26);
      RaisePropertyChanged($"B27GainSliderValue");
    }
    private void ExecuteB27QCompleted()
    {
      if (!double.TryParse(_sQ[26], out var result)) return;
      _q[26] = result;
      Send_Q(26);
      RaisePropertyChanged($"B27QSliderValue");
    }
    public string B27FreqEntryValue
    {
      get => _sFreq[26];
      set => _sFreq[26] = value;
    }
    public string B27GainEntryValue
    {
      get => _sGain[26];
      set => _sGain[26] = value;
    }
    public string B27QEntryValue
    {
      get => _sQ[26];
      set => _sQ[26] = value;
    }
    #endregion Band27
    #region Band28
    public double B28FreqSliderValue
    {
      get => Math.Log10(_freq[27]);
      set
      {
        _sFreq[27] = FormatFreq(ref value);
        _freq[27] = value;
        SendFreq(27);
        RaisePropertyChanged("B28FreqEntryValue");
      }
    }
    public double B28GainSliderValue
    {
      get => _gain[27];
      set
      {
        _gain[27] = value;
        Send_Gain(27);
        _sGain[27] = $"{Math.Round(value + .05, 1):N1}";
          RaisePropertyChanged("B28GainEntryValue");
      }
    }
    public double B28QSliderValue
    {
      get => _q[27];
      set
      {
        _q[27] = value;
        Send_Q(27);
        _sQ[27] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B28QEntryValue");
      }
    }
    private void ExecuteB28FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[27], out var result)) return;
      _freq[27] = result;
      RaisePropertyChanged($"B28FreqSliderValue");
    }
    private void ExecuteB28GainCompleted()
    {
      if (!double.TryParse(_sGain[27], out var result)) return;
      _gain[27] = result;
      Send_Gain(27);
      RaisePropertyChanged($"B28GainSliderValue");
    }
    private void ExecuteB28QCompleted()
    {
      if (!double.TryParse(_sQ[27], out var result)) return;
      _q[27] = result;
      Send_Q(27);
      RaisePropertyChanged($"B28QSliderValue");
    }
    public string B28FreqEntryValue
    {
      get => _sFreq[27];
      set => _sFreq[27] = value;
    }
    public string B28GainEntryValue
    {
      get => _sGain[27];
      set => _sGain[27] = value;
    }
    public string B28QEntryValue
    {
      get => _sQ[27];
      set => _sQ[27] = value;
    }
    #endregion Band28
    #region Band29
    public double B29FreqSliderValue
    {
      get => Math.Log10(_freq[28]);
      set
      {
        _sFreq[28] = FormatFreq(ref value);
        _freq[28] = value;
        SendFreq(28);
        RaisePropertyChanged("B29FreqEntryValue");
      }
    }
    public double B29GainSliderValue
    {
      get => _gain[28];
      set
      {
        _gain[28] = value;
        Send_Gain(28);
        _sGain[28] = $"{Math.Round(value + .05, 1):N1}";
         RaisePropertyChanged("B29GainEntryValue");
      }
    }
    public double B29QSliderValue
    {
      get => _q[28];
      set
      {
        _q[28] = value;
        Send_Q(28);
        _sQ[28] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B29QEntryValue");
      }
    }
    private void ExecuteB29FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[28], out var result)) return;
      _freq[28] = result;
      RaisePropertyChanged($"B29FreqSliderValue");
    }
    private void ExecuteB29GainCompleted()
    {
      if (!double.TryParse(_sGain[28], out var result)) return;
      _gain[28] = result;
      Send_Gain(28);
      RaisePropertyChanged($"B29GainSliderValue");
    }
    private void ExecuteB29QCompleted()
    {
      if (!double.TryParse(_sQ[28], out var result)) return;
      _q[28] = result;
      Send_Q(28);
      RaisePropertyChanged($"B29QSliderValue");
    }
    public string B29FreqEntryValue
    {
      get => _sFreq[28];
      set => _sFreq[28] = value;
    }
    public string B29GainEntryValue
    {
      get => _sGain[28];
      set => _sGain[28] = value;
    }
    public string B29QEntryValue
    {
      get => _sQ[28];
      set => _sQ[28] = value;
    }
    #endregion Band29
    #region Band30
    public double B30FreqSliderValue
    {
      get => Math.Log10(_freq[29]);
      set
      {
        _sFreq[29] = FormatFreq(ref value);
        _freq[29] = value;
        SendFreq(29);
        RaisePropertyChanged("B30FreqEntryValue");
      }
    }
    public double B30GainSliderValue
    {
      get => _gain[29];
      set
      {
        _gain[29] = value;
        Send_Gain(29);
        _sGain[29] = $"{Math.Round(value + .05, 1):N1}";
         RaisePropertyChanged("B30GainEntryValue");
      }
    }
    public double B30QSliderValue
    {
      get => _q[29];
      set
      {
        _q[29] = value;
        Send_Q(29);
        _sQ[29] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B30QEntryValue");
      }
    }
    private void ExecuteB30FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[29], out var result)) return;
      _freq[29] = result;
      RaisePropertyChanged($"B30FreqSliderValue");
    }
    private void ExecuteB30GainCompleted()
    {
      if (!double.TryParse(_sGain[29], out var result)) return;
      _gain[29] = result;
      Send_Gain(29);
      RaisePropertyChanged($"B30GainSliderValue");
    }
    private void ExecuteB30QCompleted()
    {
      if (!double.TryParse(_sQ[29], out var result)) return;
      _q[29] = result;
      Send_Q(29);
      RaisePropertyChanged($"B30QSliderValue");
    }
    public string B30FreqEntryValue
    {
      get => _sFreq[29];
      set => _sFreq[29] = value;
    }
    public string B30GainEntryValue
    {
      get => _sGain[29];
      set => _sGain[29] = value;
    }
    public string B30QEntryValue
    {
      get => _sQ[29];
      set => _sQ[29] = value;
    }
    #endregion Band30
    #region Band31
    public double B31FreqSliderValue
    {
      get => Math.Log10(_freq[30]);
      set
      {
        _sFreq[30] = FormatFreq(ref value);
        _freq[30] = value;
        SendFreq(30);
        RaisePropertyChanged("B31FreqEntryValue");
      }
    }
    public double B31GainSliderValue
    {
      get => _gain[30];
      set
      {
        _gain[30] = value;
        Send_Gain(30);
        _sGain[30] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B31GainEntryValue");
      }
    }
    public double B31QSliderValue
    {
      get => _q[30];
      set
      {
        _q[30] = value;
        Send_Q(30);
        _sQ[30] = $"{Math.Round(value + .05, 1):N1}";
        RaisePropertyChanged("B31QEntryValue");
      }
    }
    private void ExecuteB31FreqEntryCompleted()
    {
      if (!double.TryParse(_sFreq[30], out var result)) return;
      _freq[30] = result;
      RaisePropertyChanged($"B31FreqSliderValue");
    }
    private void ExecuteB31GainCompleted()
    {
      if (!double.TryParse(_sGain[30], out var result)) return;
      _gain[30] = result;
      Send_Gain(30);
      RaisePropertyChanged($"B31GainSliderValue");
    }
    private void ExecuteB31QCompleted()
    {
      if (!double.TryParse(_sQ[30], out var result)) return;
      _q[30] = result;
      Send_Q(30);
      RaisePropertyChanged($"B31QSliderValue");
    }
    public string B31FreqEntryValue
    {
      get => _sFreq[30];
      set => _sFreq[30] = value;
    }
    public string B31GainEntryValue
    {
      get => _sGain[30];
      set => _sGain[30] = value;
    }
    public string B31QEntryValue
    {
      get => _sQ[30];
      set => _sQ[30] = value;
    }
    #endregion Band31

    public bool Send_Gain(int band)
    {
      if (mask == 0)
        return false;

      if (!AllowSend)
        return false;
      if (band > 30)
      {
        throw new Exception($"EQ Gain band {band} greater than 30.");
      }
      var commandArray = new byte[37];

      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_PEQ;
      commandArray[3] = (byte)mask;
      commandArray[4] = (byte)band;

      // sending all gains, but the remote end will use only those specified in the mask
      for (var ch = 0; ch != 8; ch++)
      {
        var bytes = BitConverter.GetBytes((float)_gain[band]);
        Array.Copy(bytes, 0, commandArray, 5 + ch * 4, 4);

      }
      Debug.WriteLine($"Band {band}, freq {_freq[band]}, gain {_gain[band]}, mask {(byte)mask:X2}");
      return Utils.Universal_Write(ref commandArray);
    }
    public bool Send_Q(int band)
    {
     
      if (mask == 0)
        return false;

      if (!AllowSend)
        return false;


      if (band > 30)
      {
        throw new Exception($"EQ Q band {band} greater than 30.");
      }

      var commandArray = new byte[37];

      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_PEQ_Q;
      commandArray[3] = (byte)mask;
      commandArray[4] = (byte)band;

      Debug.WriteLine($"Send Mask 0x{mask:x}, Band {band}, Q {_q[band]}");
      // sending all freqs, but the remote end will use only those specified in the mask
      for (var ch = 0; ch != 8; ch++)
      {
        var bytes = BitConverter.GetBytes((float)_q[band]);
        Array.Copy(bytes, 0, commandArray, 5 + ch * 4, 4);
      }
      return Utils.Universal_Write(ref commandArray);

    }


    private bool SendFreq(int band)
    {
      if (!AllowSend)
        return false;

      if (band > 30)
      {
        throw new Exception($"EQ Frequency band {band} greater than 30.");
      }

      var commandArray = new byte[37];

      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_PEQ_FREQ;
      commandArray[3] = (byte)mask;
      commandArray[4] = (byte)band;

    
      // sending all freqs, but the remote end will use only those specified in the mask
      for (var ch = 0; ch != 8; ch++)
      {
        var bytes = BitConverter.GetBytes((float) _freq[band]);
        Array.Copy(bytes, 0, commandArray, 5 + ch * 4, 4);
      }
      return Utils.Universal_Write(ref commandArray);
    }
















    private string FormatFreq(ref double val)
    {
      var n = Math.Pow(10, val);
      // val = Math.Round(n + .05, 1);
      val = Math.Round(n, 1);
      return !(val < 1000) ? $"{val:N0}" : $"{val:N1}";
    }



  }
}
