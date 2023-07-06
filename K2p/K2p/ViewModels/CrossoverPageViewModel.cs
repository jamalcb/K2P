using K2p.Statics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace K2p.ViewModels
{
  public class CrossoverPageViewModel : ViewModelBase
  {
    private IPageDialogService _dialogService;
    public bool AllowUpdates;
    private SKPath _xAxisPath;
    private ChannelNameData _channelNameData = new ChannelNameData();
    public IList<PickerName> ChannelNames => _channelNameData.NameSource;

    private DampingNameData _dampingNameData = new DampingNameData();
    public IList<DampingPickerName> DampingNames => _dampingNameData.NameSource;

    private SlopeNameData _HPslopeNameDatach0 = new SlopeNameData(Crossover.Damping_e.Butterworth);
    private SlopeNameData _HPslopeNameDatach1 = new SlopeNameData(Crossover.Damping_e.Butterworth);
    private SlopeNameData _LPslopeNameDatach0 = new SlopeNameData(Crossover.Damping_e.Butterworth);
    private SlopeNameData _LPslopeNameDatach1 = new SlopeNameData(Crossover.Damping_e.Butterworth);
    public IList<SlopePickerName> HPSlopeNamesCh0 => _HPslopeNameDatach0.NameSource;
    public IList<SlopePickerName> HPSlopeNamesCh1 => _HPslopeNameDatach1.NameSource;
    public IList<SlopePickerName> LPSlopeNamesCh0 => _LPslopeNameDatach0.NameSource;
    public IList<SlopePickerName> LPSlopeNamesCh1 => _LPslopeNameDatach1.NameSource;
    public ICommand PaintCommand { get; }
    public DelegateCommand HpFreqEntryCompletedCommandCh0 { get; }
    public DelegateCommand LpFreqEntryCompletedCommandCh0 { get; }
    public DelegateCommand HpFreqEntryCompletedCommandCh1 { get; }
    public DelegateCommand LpFreqEntryCompletedCommandCh1 { get; }
    public DelegateCommand HpQEntryCompletedCommandCh0 { get; }
    public DelegateCommand HpQEntryCompletedCommandCh1 { get; }
    public DelegateCommand LpQEntryCompletedCommandCh0 { get; }
    public DelegateCommand LpQEntryCompletedCommandCh1 { get; }

    private readonly SKPaint _xAxisPaint;
    private readonly SKPaint[] _strokePaints = new SKPaint[8];

    private string _sHpFreqCh0;
    private string _sHpFreqCh1;
    private string _sLpFreqCh0;
    private string _sLpFreqCh1;

    private string _sHpQCh0;
    private string _sHpQCh1;
    private string _sLpQCh0;
    private string _sLpQCh1;

    private float QMAX = 3.0f;
    private float QMIN = .1f;

   // private PickerName _selectedChanelPairName;

    private bool _showQ;
    private bool _showPlotPortrait = true;
    public byte LinkMask = 0;  // 4 least significant bits are channel pairs 1-2, 3-4, 5-6, 7-8
    

    public CrossoverPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
    {
      _dialogService = dialogService;
    //  _selectedChanelPairName = ChannelNames[0];

      PaintCommand = new Command<SKPaintSurfaceEventArgs>(OnPainting);

      HpFreqEntryCompletedCommandCh0 = new DelegateCommand(ExecuteHpFreqEntryCompletedCommandCh0);
      LpFreqEntryCompletedCommandCh0 = new DelegateCommand(ExecuteLpFreqEntryCompletedCommandCh0);
      HpFreqEntryCompletedCommandCh1 = new DelegateCommand(ExecuteHpFreqEntryCompletedCommandCh1);
      LpFreqEntryCompletedCommandCh1 = new DelegateCommand(ExecuteLpFreqEntryCompletedCommandCh1);

      HpQEntryCompletedCommandCh0 = new DelegateCommand(ExecuteHpQEntryCompletedCommandCh0);
      LpQEntryCompletedCommandCh0 = new DelegateCommand(ExecuteLpQEntryCompletedCommandCh0);
      HpQEntryCompletedCommandCh1 = new DelegateCommand(ExecuteHpQEntryCompletedCommandCh1);
      LpQEntryCompletedCommandCh1 = new DelegateCommand(ExecuteLpQEntryCompletedCommandCh1);

      _xAxisPaint = new SKPaint
      {
        Style = SKPaintStyle.Stroke,
        Color = new SKColor(98, 98, 98),
        StrokeWidth = 4,
        StrokeCap = SKStrokeCap.Butt,
        IsAntialias = true
      };
      int channel;
      try
      {
        for (channel = 0; channel != 8; channel++)
        {
          _strokePaints[channel] = new SKPaint
          {
            Style = SKPaintStyle.Stroke,
            Color = Settings.mixer_2_output_brush[channel].ToSKColor(),
            StrokeWidth = 4,
            StrokeCap = SKStrokeCap.Butt,
            IsAntialias = true
          };
        }

        for (channel = 0; channel != 8; channel++)
        {
          HPUpdatePlot(channel);
          LPUpdatePlot(channel);
        }
      }
      catch(Exception wtf)
      {
        Debug.WriteLine($"WTF {wtf.Message}");
      }

      _channelPair = 0;

      SelectChannel();
  
      Utils.CrossoverPageView?.Redraw();
    }
    

    public int PlotRow { get; set; } = 1;
    public int PlotColumn { get; set; } = 6;
    public int PlotRowSpan { get; set; } = 1;
    public int PlotColumnSpan { get; set; } = 1;
    public int PlotHeight { get; set; } = 250;
    public int PlotWidth { get; set; } = 100;

    //private void Redraw() => Utils.CrossoverPageView?.Redraw();
    private int _channelPair;

    private void UpdateProperties()
    {
      RaisePropertyChanged("HpLogControlFreqCh0");
      RaisePropertyChanged("HpSliderQCh0");
      RaisePropertyChanged("HpLogControlFreqCh0");
      RaisePropertyChanged("HpDampingIndexCh0");
      RaisePropertyChanged("HPSlopeIndexCh0");

      RaisePropertyChanged("HpLogControlFreqCh1");
      RaisePropertyChanged("HpSliderQCh1");
      RaisePropertyChanged("HpLogControlFreqCh1");
      RaisePropertyChanged("HpDampingIndexCh1");
      RaisePropertyChanged("HPSlopeIndexCh1");

      RaisePropertyChanged("LpLogControlFreqCh0");
      RaisePropertyChanged("LpSliderQCh0");
      RaisePropertyChanged("LpLogControlFreqCh0");
      RaisePropertyChanged("LpDampingIndexCh0");
      RaisePropertyChanged("LPSlopeIndexCh0");

      RaisePropertyChanged("LpLogControlFreqCh1");
      RaisePropertyChanged("LpSliderQCh1");
      RaisePropertyChanged("LpLogControlFreqCh1");
      RaisePropertyChanged("LpDampingIndexCh1");
      RaisePropertyChanged("LPSlopeIndexCh1");

      RaisePropertyChanged("HpFreqVisibleCh0");
      RaisePropertyChanged("HpFreqVisibleCh1");
      RaisePropertyChanged("LpFreqVisibleCh0");
      RaisePropertyChanged("LpFreqVisibleCh1");

      RaisePropertyChanged("TextColorCh0");
      RaisePropertyChanged("TextColorCh1");
    }
    private void SelectChannel()
    {
      var x = 1 << _channelPair;
      RaisePropertyChanged("IsLinked");

      _channel0 = _channelPair * 2;
      _channel1 = _channelPair * 2 + 1;
      UpdateProperties();



      //RaisePropertyChanged("LpLogControlFreqCh0");
      //RaisePropertyChanged("LpDampingIndexCh0");


      // LpSliderQCh0 = (float)Crossover.LpQ[_channel0];
      //LpDampingIndexCh0 = (int)Crossover.LpDamping[_channel0];
      //LpDampingIndexCh1 = (int)Crossover.LpDamping[_channel1];
      // HpDampingIndexCh0 = (int)Crossover.HpDamping[_channel0];
      // HpDampingIndexCh1 = (int)Crossover.HpDamping[_channel1];

      //HPSlopeIndexCh1 = HPSlopeIndexCh0; //SlopeToIndex
      // LPSlopeIndexCh1 = LPSlopeIndexCh0;
      //SelectedLpDampingNameCh0 = DampingNames[(int)Crossover.LpDamping[_channel0]];

      //RaisePropertyChanged("HpLogControlFreqCh1");
      //HpSliderQCh1 = (float)Crossover.HpQ[_channel1];

      //RaisePropertyChanged("LpLogControlFreqCh1");
      //LpSliderQCh1 = (float)Crossover.LpQ[_channel1];
      //SelectedLpDampingNameCh1 = DampingNames[(int)Crossover.LpDamping[_channel1]];

      //TextColorCh0 = Settings.mixer_2_output_brush[_channel0];
      //TextColorCh1 = Settings.mixer_2_output_brush[_channel1];

      //_sHpFreqCh0 = FormatFreq((float)Crossover.HpFrequency[_channel0]);
      _sHpFreqCh1 = FormatFreq((float)Crossover.HpFrequency[_channel1]);
      //_sLpFreqCh0 = FormatFreq((float)Crossover.LpFrequency[_channel0]);
      _sLpFreqCh1 = FormatFreq((float)Crossover.LpFrequency[_channel1]);

      _sHpQCh0 = $"{Crossover.HpQ[_channel0]:N1}";
      _sHpQCh1 = $"{Crossover.HpQ[_channel1]:N1}";
      _sLpQCh0 = $"{Crossover.LpQ[_channel0]:N1}";
      _sLpQCh1 = $"{Crossover.LpQ[_channel1]:N1}";

     // RaisePropertyChanged("TextColorCh0");
     // RaisePropertyChanged("TextColorCh1");

     // RaisePropertyChanged("HpDampingIndexCh1");
     // RaisePropertyChanged("LpDampingIndexCh1");

     //// RaisePropertyChanged("HPSlopeIndexCh0");
     // RaisePropertyChanged("HPSlopeIndexCh1");
     //// RaisePropertyChanged("LPSlopeIndexCh0");
     // RaisePropertyChanged("LPSlopeIndexCh1");
    }

    public bool HpSlopeVisibleIsVisibleCh0
    {
      get => _hpSlopeVisibleIsVisibleCh0;
      set => SetProperty(ref _hpSlopeVisibleIsVisibleCh0, value);
    }
    public bool HpSlopeVisibleIsVisibleCh1
    {
      get => _hpSlopeVisibleIsVisibleCh1;
      set => SetProperty(ref _hpSlopeVisibleIsVisibleCh1, value);
    }
    public bool LpSlopeVisibleIsVisibleCh0
    {
      get => _lpSlopeVisibleIsVisibleCh0;
      set => SetProperty(ref _lpSlopeVisibleIsVisibleCh0, value);
    }
    public bool LpSlopeVisibleIsVisibleCh1
    {
      get => _lpSlopeVisibleIsVisibleCh1;
      set => SetProperty(ref _lpSlopeVisibleIsVisibleCh1, value);
    }


    public bool ShowQ
    {
      get => _showQ;
      set { SetProperty(ref _showQ, value); RaisePropertyChanged("QGridLength"); }
    }

    public bool ShowPlotPortrait
    {
      get => _showPlotPortrait;
      set
      {
        SetProperty(ref _showPlotPortrait, value);
        if (value) // Portrait
        {
          PlotRow = 10;
          PlotColumn = 0;
          PlotColumnSpan = 7;
          PlotRowSpan = 1;
          PlotHeight = 180;
          PlotWidth = 320;
        }
        else
        {
          PlotRow = 3;
          PlotColumn = 6;
          PlotRowSpan = 4;
          PlotColumnSpan = 1;
          PlotHeight = 140;
          PlotWidth = 300;
        }

        RaisePropertyChanged("PlotRow");
        RaisePropertyChanged("PlotColumn");

        RaisePropertyChanged("PlotRowSpan");
        RaisePropertyChanged("PlotColumnSpan");
        RaisePropertyChanged("PlotHeight");
        RaisePropertyChanged("PlotWidth");
      }
    }

    public GridLength QGridLength => _showQ
      ? new GridLength(1, GridUnitType.Star)
      : new GridLength(0);

    public GridLength PlotGridLength => _showPlotPortrait
      ? new GridLength(150, GridUnitType.Absolute)
      : new GridLength(0);

    public bool HpQVisibleCh0 { get; set; }
    public bool LpQVisibleCh0 { get; set; }
    public bool HpQVisibleCh1 { get; set; }
    public bool LpQVisibleCh1 { get; set; }
    private string FormatFreq(float val)
    {
      // var n = (float)Math.Pow(10, val);
      val = (float)Math.Round(val, 1);
      if (val < 100)
      {
        return $"{val:N2}";
      }
      if (val < 1000)
      {
        return $"{val:N1}";
      }
      return $"{val:N0}";
    }

    #region HPCh0
    #region HPQCh0
    public float HpSliderQCh0
    {
      get => (float)Crossover.HpQ[_channel0];
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref Crossover.HpQ[_channel0], value);
        HpQCh0 = $"{value:N1}";
        RaisePropertyChanged("HpQCh0");
        HPUpdatePlot(_channel0);
        if (IsLinked)
        {
          Crossover.HpQ[_channel1] = Crossover.HpQ[_channel0];
          RaisePropertyChanged("HpSliderQCh1");
          RaisePropertyChanged("HpQCh1");
          HPUpdatePlot(_channel1);
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public string HpQCh0
    {
      get => _sHpQCh0;
      set => _sHpQCh0 = value;
    }
    public string HpFreqEntryCh0
    {
      get => _sHpFreqCh0;
      set => _sHpFreqCh0 = value;
    }
    private void ExecuteHpQEntryCompletedCommandCh0()
    {
      if (!float.TryParse(_sHpQCh0, out var result)) return;

      result = result > QMAX ? QMAX : result;
      result = result < QMIN ? QMIN : result;
      Crossover.HpQ[_channel0] = result;

      RaisePropertyChanged($"HpSliderQCh0");
      RaisePropertyChanged($"HpQCh0");
      HPUpdatePlot(_channel0);
      if (IsLinked)
      {
        Crossover.HpQ[_channel1] = Crossover.HpQ[_channel0];
        RaisePropertyChanged($"HpSliderQCh1");
        RaisePropertyChanged($"HpQCh1");
        HPUpdatePlot(_channel1);
      }
      Utils.CrossoverPageView?.Redraw();
    }
    #endregion HPQCh0

    public Color TextColorCh0
    {
      get => Settings.mixer_2_output_brush[_channel0];
      set => SetProperty(ref Settings.mixer_2_output_brush[_channel0], value);
    }
    public Color TextColorCh1
    {
      get => Settings.mixer_2_output_brush[_channel1];
      set => SetProperty(ref Settings.mixer_2_output_brush[_channel1], value);
    }
    
    #region HPFreqCh0
    private void ExecuteHpFreqEntryCompletedCommandCh0()
    {
      if (!float.TryParse(_sHpFreqCh0, out var result)) return;
      //   _hpFreqCh0 = result;
      Crossover.HpFrequency[_channel0] = result;

      RaisePropertyChanged($"HpLogControlFreqCh0");
      HPUpdatePlot(_channel0);
      Send(Crossover.FilterType.HIGH_PASS, _channel0);
      if (IsLinked)
      {
        Crossover.HpFrequency[_channel1] = Crossover.HpFrequency[_channel0];
        _sHpFreqCh1 = _sHpFreqCh0;
        RaisePropertyChanged($"HpLogControlFreqCh1");
        RaisePropertyChanged($"HpFreqEntryCh1");
        HPUpdatePlot(_channel1);
        Send(Crossover.FilterType.HIGH_PASS, _channel1);
      }
      Utils.CrossoverPageView?.Redraw();
    }

    private void HPUpdatePlot(int channel)
    {
      CrossoverResponse.Clear_HP(channel);
      CrossoverResponse.Crunch_HP(channel, Crossover.HpFrequency[channel], Crossover.HpSlope[channel], Crossover.HpDamping[channel], Crossover.HpQ[channel]);
    }
    private void LPUpdatePlot(int channel)
    {
      CrossoverResponse.Clear_LP(channel);
      CrossoverResponse.Crunch_LP(channel, Crossover.LpFrequency[channel], Crossover.LpSlope[channel], Crossover.LpDamping[channel], Crossover.LpQ[channel]);
    }

    public float HpLogControlFreqCh0
    {
      get => (float)Math.Log10(Crossover.HpFrequency[_channel0]);
      set
      {
        if (Utils.CrossoverPageView == null || !AllowUpdates)
        {
          return;
        }

        var freq = Math.Pow(10, value);
        _sHpFreqCh0 = FormatFreq((float)freq);

        Crossover.HpFrequency[_channel0] = freq;
        RaisePropertyChanged($"HpFreqEntryCh0");
        RaisePropertyChanged();
        HPUpdatePlot(_channel0);
        Send(Crossover.FilterType.HIGH_PASS, _channel0);
        if (Crossover.LpSlope[_channel0] != Crossover.Slope_e.Flat && freq > Crossover.LpFrequency[_channel0])
        {
          LpLogControlFreqCh0 = HpLogControlFreqCh0;
        }
        if (IsLinked)
        {
          Crossover.HpFrequency[_channel1] = Crossover.HpFrequency[_channel0];
          _sHpFreqCh1 = _sHpFreqCh0;
          RaisePropertyChanged($"HpFreqEntryCh1");
          RaisePropertyChanged($"HpLogControlFreqCh1");
          HPUpdatePlot(_channel1);
          Send(Crossover.FilterType.HIGH_PASS, _channel1);
          if (Crossover.LpSlope[_channel1] != Crossover.Slope_e.Flat && freq > Crossover.LpFrequency[_channel1])
          {
            LpLogControlFreqCh1 = HpLogControlFreqCh1;
          }
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    #endregion
    
    private int SlopeToIndex(Crossover.Damping_e damping, Crossover.Slope_e slope)
    {
      switch (damping)
      {
        case Crossover.Damping_e.Linkwitz_Riley:
        case Crossover.Damping_e.Bessel:
          return (int) slope / 2;
        case Crossover.Damping_e.Chebyshev_01:
        case Crossover.Damping_e.Chebyshev_10:
        case Crossover.Damping_e.Chebyshev_25:
          return (int) slope - 1;
        default:
          return (int) slope;
      }
    }
    private Crossover.Slope_e IndexToSlope(Crossover.Damping_e damping, int Index)
    {
      switch (damping)
      {
        case Crossover.Damping_e.Linkwitz_Riley:
        case Crossover.Damping_e.Bessel:
          return (Crossover.Slope_e) (Index * 2);
        case Crossover.Damping_e.Chebyshev_01:
        case Crossover.Damping_e.Chebyshev_10:
        case Crossover.Damping_e.Chebyshev_25:
          return (Crossover.Slope_e)(Index + 1); // just skip 6 db
        default:
          return (Crossover.Slope_e) Index;
        
      }
    }


    private bool CheckValidSlopes(int channel, Crossover.FilterType filterType, Crossover.Damping_e damping)
    {
      if (filterType == Crossover.FilterType.HIGH_PASS)
      {
        switch (damping)
        {
          case Crossover.Damping_e.Linkwitz_Riley:
          case Crossover.Damping_e.Bessel:
            switch (Crossover.HpSlope[channel])
            {
              case Crossover.Slope_e.dB12:
              case Crossover.Slope_e.dB24:
              case Crossover.Slope_e.dB36:
              case Crossover.Slope_e.dB48:
                RaisePropertyChanged("HPSlopeIndexCh0");
                return true;
              default:
                Crossover.HpSlope[channel] = Crossover.Slope_e.dB48;
                RaisePropertyChanged($"HPSlopeNamesCh{channel}");
                RaisePropertyChanged("HPSlopeIndexCh0");
                Send(Crossover.FilterType.HIGH_PASS, channel);
                return false;
            }
          case Crossover.Damping_e.Variable_Q:
            if (Crossover.HpSlope[channel] == Crossover.Slope_e.dB12)
            {
              return true;
            }
            Crossover.HpSlope[channel] = Crossover.Slope_e.dB12;
            RaisePropertyChanged($"HPSlopeNamesCh{channel}");
            Send(Crossover.FilterType.HIGH_PASS, channel);
            return false;
        }
      }
      if (filterType == Crossover.FilterType.LOW_PASS)
      {
        switch (damping)
        {
          case Crossover.Damping_e.Linkwitz_Riley:
          case Crossover.Damping_e.Bessel:
            switch (Crossover.LpSlope[channel])
            {
              case Crossover.Slope_e.dB12:
              case Crossover.Slope_e.dB24:
              case Crossover.Slope_e.dB36:
              case Crossover.Slope_e.dB48:
                return true;
              default:
                Crossover.LpSlope[channel] = Crossover.Slope_e.dB48;
                RaisePropertyChanged($"HPSlopeNamesCh{channel}");
                Send(Crossover.FilterType.LOW_PASS, channel);
                return false;
            }
          case Crossover.Damping_e.Variable_Q:
            if (Crossover.LpSlope[channel] == Crossover.Slope_e.dB12)
            {
              return true;
            }
            Crossover.LpSlope[channel] = Crossover.Slope_e.dB12;
            RaisePropertyChanged($"LPSlopeNamesCh{channel}");
            Send(Crossover.FilterType.LOW_PASS, channel);
            return false;
        }
      }
      return true;
    }

    public int HpDampingIndexCh0
    {
      get => (int) Crossover.HpDamping[_channel0];
      set
      {
        if (!AllowUpdates) return;
        if (value == (int)Crossover.HpDamping[_channel0] || !AllowUpdates) return;
        _HPslopeNameDatach0 = new SlopeNameData((Crossover.Damping_e)value);
        RaisePropertyChanged("HPSlopeNamesCh0");
        Crossover.HpDamping[_channel0] = (Crossover.Damping_e) value;
        Crossover.HpSlope[_channel0] = Crossover.Slope_e.dB12;
        HPUpdatePlot(_channel0);
        Send(Crossover.FilterType.HIGH_PASS, _channel0);
        if (IsLinked)
        {
          _HPslopeNameDatach1 = new SlopeNameData((Crossover.Damping_e)value);
          RaisePropertyChanged("HPSlopeNamesCh1");
          Crossover.HpSlope[_channel1] = Crossover.HpSlope[_channel0];
          Crossover.HpDamping[_channel1] = Crossover.HpDamping[_channel0];
          HpQVisibleCh1 = HpQVisibleCh0;
          HpSlopeVisibleIsVisibleCh1 = HpSlopeVisibleIsVisibleCh0;
          HPUpdatePlot(_channel1);
          Send(Crossover.FilterType.HIGH_PASS, _channel1);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public int HpDampingIndexCh1
    {
      get => (int)Crossover.HpDamping[_channel1];
      set
      {
        if (value == (int)Crossover.HpDamping[_channel1] || !AllowUpdates) return;
        _HPslopeNameDatach1 = new SlopeNameData((Crossover.Damping_e)value);
        RaisePropertyChanged("HPSlopeNamesCh1");
        Crossover.HpDamping[_channel1] = (Crossover.Damping_e)value;
        Crossover.HpSlope[_channel1] = Crossover.Slope_e.dB12;
        HPUpdatePlot(_channel1);
        Send(Crossover.FilterType.HIGH_PASS, _channel1);
        if (IsLinked)
        {
          _HPslopeNameDatach0 = new SlopeNameData((Crossover.Damping_e)value);
          RaisePropertyChanged("HPSlopeNamesCh1");
          Crossover.HpSlope[_channel0] = Crossover.HpSlope[_channel1];
          Crossover.HpDamping[_channel0] = Crossover.HpDamping[_channel1];
          HpQVisibleCh0 = HpQVisibleCh1;
          HpSlopeVisibleIsVisibleCh1 = HpSlopeVisibleIsVisibleCh0;
          HPUpdatePlot(_channel0);
          HpSlopeVisibleIsVisibleCh0 = HpSlopeVisibleIsVisibleCh1;
          Send(Crossover.FilterType.HIGH_PASS, _channel0);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public int LpDampingIndexCh0
    {
      get => (int)Crossover.LpDamping[_channel0];
      set
      {
        if (value == (int)Crossover.LpDamping[_channel0] || !AllowUpdates) return;
        _LPslopeNameDatach0 = new SlopeNameData((Crossover.Damping_e)value);
        RaisePropertyChanged("LPSlopeNamesCh0");
        Crossover.LpDamping[_channel0] = (Crossover.Damping_e)value;
        Crossover.LpSlope[_channel0] = Crossover.Slope_e.dB12;
        LPUpdatePlot(_channel0);
        Send(Crossover.FilterType.LOW_PASS, _channel0);
        if (IsLinked)
        {
          _LPslopeNameDatach1 = new SlopeNameData((Crossover.Damping_e)value);
          RaisePropertyChanged("LPSlopeNamesCh1");
          Crossover.LpSlope[_channel1] = Crossover.LpSlope[_channel0];
          Crossover.LpDamping[_channel1] = Crossover.LpDamping[_channel0];
          LpQVisibleCh1 = LpQVisibleCh0;
          LpSlopeVisibleIsVisibleCh1 = LpSlopeVisibleIsVisibleCh0;
          LPUpdatePlot(_channel1);
          LpSlopeVisibleIsVisibleCh1 = LpSlopeVisibleIsVisibleCh0;
          Send(Crossover.FilterType.LOW_PASS, _channel1);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public int LpDampingIndexCh1
    {
      get => (int)Crossover.LpDamping[_channel1];
      set
      {
        if (value == (int)Crossover.LpDamping[_channel1] || !AllowUpdates) return;
        _LPslopeNameDatach1 = new SlopeNameData((Crossover.Damping_e)value);
        RaisePropertyChanged("LPSlopeNamesCh1");
        Crossover.LpDamping[_channel1] = (Crossover.Damping_e)value;
        Crossover.LpSlope[_channel0] = Crossover.Slope_e.dB12;
        LPUpdatePlot(_channel1);
        Send(Crossover.FilterType.LOW_PASS, _channel1);
        if (IsLinked)
        {
          Crossover.LpSlope[_channel0] = Crossover.LpSlope[_channel1];
          Crossover.LpDamping[_channel0] = Crossover.LpDamping[_channel1];
          _LPslopeNameDatach0 = new SlopeNameData((Crossover.Damping_e)value);
          RaisePropertyChanged("LPSlopeNamesCh0");
          LpQVisibleCh0 = LpQVisibleCh1;
          LpSlopeVisibleIsVisibleCh0 = LpSlopeVisibleIsVisibleCh1;
          LPUpdatePlot(_channel0);
          LpSlopeVisibleIsVisibleCh0 = LpSlopeVisibleIsVisibleCh1;
          Send(Crossover.FilterType.LOW_PASS, _channel0);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }
    
    public bool HpFreqVisibleCh0 { get; set; }
    #endregion
    #region LPCh0
    public float LpLogControlFreqCh0
    {
      get => (float)Math.Log10(Crossover.LpFrequency[_channel0]);
      set
      {
        if (Utils.CrossoverPageView == null || !AllowUpdates)
        {
          return;
        }
        var freq = Math.Pow(10, value);
        _sLpFreqCh0 = FormatFreq((float)freq);
        Crossover.LpFrequency[_channel0] = freq;
        RaisePropertyChanged();
        RaisePropertyChanged("LpFreqEntryCh0");
        LPUpdatePlot(_channel0);
        Send(Crossover.FilterType.LOW_PASS, _channel0);
        if (Crossover.HpSlope[_channel0] != Crossover.Slope_e.Flat && freq < Crossover.HpFrequency[_channel0])
        {
          HpLogControlFreqCh0 = LpLogControlFreqCh0;
        }
        if (IsLinked)
        {
          _sLpFreqCh1 = _sLpFreqCh0;
          Crossover.LpFrequency[_channel1] = Crossover.LpFrequency[_channel0];
          RaisePropertyChanged($"LpLogControlFreqCh1");
          RaisePropertyChanged($"LpFreqEntryCh1");
          LPUpdatePlot(_channel1);
          Send(Crossover.FilterType.LOW_PASS, _channel1);
          if (Crossover.HpSlope[_channel1] != Crossover.Slope_e.Flat && freq < Crossover.HpFrequency[_channel1])
          {
            HpLogControlFreqCh1 = LpLogControlFreqCh1;
          }
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public string LpFreqEntryCh0
    {
      get => _sLpFreqCh0;
      set => _sLpFreqCh0 = value;
    }

    #region LPQCh0
    public float LpSliderQCh0
    {
      get => (float)Crossover.LpQ[_channel0];
      set
      {
        if (Utils.CrossoverPageView == null || !AllowUpdates)
        {
          return;
        }
        SetProperty(ref Crossover.LpQ[_channel0], value);
        LpQCh0 = $"{value:N1}";
        RaisePropertyChanged("LpQCh0");
        LPUpdatePlot(_channel0);
        Send(Crossover.FilterType.LOW_PASS, _channel0);
        if (IsLinked)
        {
          Crossover.LpQ[_channel1] = Crossover.LpQ[_channel0];
          RaisePropertyChanged("LpSliderQCh1");
          RaisePropertyChanged("LpQCh1");
          LPUpdatePlot(_channel1);
          Send(Crossover.FilterType.LOW_PASS, _channel1);
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public string LpQCh0
    {
      get => _sLpQCh0;
      set => _sLpQCh0 = value;
    }
    private void ExecuteLpQEntryCompletedCommandCh0()
    {
      if (!float.TryParse(_sLpQCh0, out var result)) return;

      result = result > QMAX ? QMAX : result;
      result = result < QMIN ? QMIN : result;
      Crossover.LpQ[_channel0] = result;

      RaisePropertyChanged($"LpSliderQCh0");
      RaisePropertyChanged($"LpQCh0");
      LPUpdatePlot(_channel0);
      Send(Crossover.FilterType.LOW_PASS, _channel0);
      if (IsLinked)
      {
        Crossover.LpQ[_channel1] = Crossover.LpQ[_channel0];
        _sLpQCh1 = _sLpQCh0;
        RaisePropertyChanged($"LpSliderQCh1");
        RaisePropertyChanged($"LpQCh1");
        LPUpdatePlot(_channel1);
        Send(Crossover.FilterType.LOW_PASS, _channel1);
      }
      Utils.CrossoverPageView?.Redraw();
    }
    #endregion

    public bool PlotVisibleCh0 => (HpFreqVisibleCh0 | LpFreqVisibleCh0 | HpFreqVisibleCh1 | LpFreqVisibleCh1);

    #endregion
    #region LPFreqCh0
    private void ExecuteLpFreqEntryCompletedCommandCh0()
    {
      if (!float.TryParse(_sLpFreqCh0, out var result)) return;
      Crossover.LpFrequency[_channel0] = result;
      RaisePropertyChanged($"LpLogControlFreqCh0");
      LPUpdatePlot(_channel0);
      Send(Crossover.FilterType.LOW_PASS, _channel0);
      if (IsLinked)
      {
        _sLpFreqCh1 = _sLpFreqCh0;
        Crossover.LpFrequency[_channel1] = Crossover.LpFrequency[_channel0];
        RaisePropertyChanged($"LpLogControlFreqCh1");
        RaisePropertyChanged($"LpFreqEntryCh1");
        LPUpdatePlot(_channel1);
        Send(Crossover.FilterType.LOW_PASS, _channel1);
      }
      Utils.CrossoverPageView?.Redraw();
    }
    public bool LpFreqVisibleCh0 { get; set; }

    //public DampingPickerName SelectedLpDampingNameCh0
    //{
    //  get => DampingNames[(int)Crossover.LpDamping[_channel0]];
    //  set
    //  {
    //    if (Utils.CrossoverPageView == null || !AllowUpdates)
    //    {
    //      return;
    //    }
    //    if (value.NameIndex == Crossover.LpDamping[_channel0]) return;
    //    LpQVisibleCh0 = value.NameIndex == Crossover.Damping_e.Variable_Q;
    //    RaisePropertyChanged($"LpQVisibleCh0");
    //    //SetProperty(ref _selectedLpDampingNameCh0, value);
    //    RaisePropertyChanged();
    //    LPSetAvailableSlopesCh0((Crossover.Damping_e)value.NameIndex);
    //    RaisePropertyChanged("LPSlopeNamesCh0");
    //    //  LPSlopeIndexCh0 = LPSlopeNamesCh0.Count - 2;
    //    Crossover.LpDamping[_channel0] = (Crossover.Damping_e)value.NameIndex;
    //    LPUpdatePlot(_channel0);
    //    LpSlopeVisibleIsVisibleCh0 = value.NameIndex != Crossover.Damping_e.Variable_Q;
    //    Send(Crossover.FilterType.LOW_PASS, _channel0);
    //    if (IsLinked)
    //    {
    //      LPSetAvailableSlopesCh1(value.NameIndex);
    //      Crossover.LpDamping[_channel1] = Crossover.LpDamping[_channel0];
    //      Crossover.LpSlope[_channel1] = Crossover.LpSlope[_channel0];
    //      // _selectedLpDampingNameCh1 = _selectedLpDampingNameCh0;
    //      RaisePropertyChanged($"SelectedLpDampingNameCh1");
    //      LpQVisibleCh1 = LpQVisibleCh0;
    //      RaisePropertyChanged($"LpQVisibleCh1");
    //      RaisePropertyChanged("LPSlopeNamesCh1");
    //      //LPSlopeIndexCh1 = LPSlopeNamesCh1.Count - 2;
    //      LPUpdatePlot(_channel1);
    //      LpSlopeVisibleIsVisibleCh1 = LpSlopeVisibleIsVisibleCh0;
    //      Send(Crossover.FilterType.LOW_PASS, _channel1);
    //    }
    //    Utils.CrossoverPageView?.Redraw();
    //  }
    //}
    #endregion
    // ************************* Channel 1 ****************************
    #region HPCh1
    #region HPQCh1
    public float HpSliderQCh1
    {
      get => (float)Crossover.HpQ[_channel1];
      set
      {
        if (Utils.CrossoverPageView == null || !AllowUpdates)
        {
          return;
        }
        SetProperty(ref Crossover.HpQ[_channel1], value);
        HpQCh1 = $"{value:N1}";
        RaisePropertyChanged($"HpQCh1");
        HPUpdatePlot(_channel1);
        Send(Crossover.FilterType.HIGH_PASS, _channel1);
        if (IsLinked)
        {
          Crossover.HpQ[_channel0] = Crossover.HpQ[_channel1];
          RaisePropertyChanged($"HpSliderQCh0");
          RaisePropertyChanged($"HpQCh0");
          HPUpdatePlot(_channel0);
          Send(Crossover.FilterType.HIGH_PASS, _channel0);
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public string HpQCh1
    {
      get => _sHpQCh1;
      set => _sHpQCh1 = value;
    }
    private void ExecuteHpQEntryCompletedCommandCh1()
    {
      if (!float.TryParse(_sHpQCh1, out var result)) return;

      result = result > QMAX ? QMAX : result;
      result = result < QMIN ? QMIN : result;
      Crossover.HpQ[_channel1] = result;
      RaisePropertyChanged($"HpSliderQCh1");
      RaisePropertyChanged($"HpQCh1");
      HPUpdatePlot(_channel1);
      Send(Crossover.FilterType.HIGH_PASS, _channel1);
      if (IsLinked)
      {
        Crossover.HpQ[_channel0] = Crossover.HpQ[_channel1];
        RaisePropertyChanged($"HpSliderQCh0");
        RaisePropertyChanged($"HpQCh0");
        HPUpdatePlot(_channel0);
        Send(Crossover.FilterType.HIGH_PASS, _channel0);
      }
      Utils.CrossoverPageView?.Redraw();
    }
    #endregion HPQCh1
    #region HPFreqCh1
    public bool HpFreqVisibleCh1 { get; set; }
    public string HpFreqEntryCh1
    {
      get => _sHpFreqCh1;
      set => _sHpFreqCh1 = value;
    }
    private void ExecuteHpFreqEntryCompletedCommandCh1()
    {
      if (!float.TryParse(_sHpFreqCh1, out var result)) return;
      Crossover.HpFrequency[_channel1] = result;
      //   _hpFreqCh1 = result;
      RaisePropertyChanged($"HpLogControlFreqCh1");
      HPUpdatePlot(_channel1);
      if (IsLinked)
      {
        Crossover.HpFrequency[_channel0] = Crossover.HpFrequency[_channel1];
        _sHpFreqCh0 = _sHpFreqCh1;
        RaisePropertyChanged($"HpLogControlFreqCh0");
        RaisePropertyChanged($"HpFreqEntryCh0");
        HPUpdatePlot(_channel0);
      }
      Utils.CrossoverPageView?.Redraw();
    }
    public float HpLogControlFreqCh1
    {
      get => (float)Math.Log10(Crossover.HpFrequency[_channel1]);
      set
      {
        if (Utils.CrossoverPageView == null || !AllowUpdates)
        {
          return;
        }
        var freq = Math.Pow(10, value);
        _sHpFreqCh1 = FormatFreq((float)freq);
        Crossover.HpFrequency[_channel1] = freq;
        RaisePropertyChanged("HpFreqEntryCh1");
        RaisePropertyChanged();
        HPUpdatePlot(_channel1);
        if (Crossover.LpSlope[_channel1] != Crossover.Slope_e.Flat && freq > Crossover.LpFrequency[_channel1])
        {
          LpLogControlFreqCh1 = HpLogControlFreqCh1;
        }
        if (IsLinked)
        {
          Crossover.HpFrequency[_channel0] = Crossover.HpFrequency[_channel1];
          _sHpFreqCh0 = _sHpFreqCh1;
          Crossover.HpSlope[_channel0] = Crossover.HpSlope[_channel1];
          RaisePropertyChanged("HpFreqEntryCh0");
          RaisePropertyChanged("HpLogControlFreqCh0");
          HPUpdatePlot(_channel0);
          if (Crossover.LpSlope[_channel0] != Crossover.Slope_e.Flat && freq > Crossover.LpFrequency[_channel0])
          {
            LpLogControlFreqCh0 = HpLogControlFreqCh0;
          }
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    #endregion

    //public DampingPickerName SelectedHpDampingNameCh1
    //{
    //  get => DampingNames[(int)Crossover.HpDamping[_channel1]]; //_selectedHpDampingNameCh1;
    //  set
    //  {
    //    if (Utils.CrossoverPageView == null || !AllowSend)
    //    {
    //      return;
    //    }
    //    if (value.NameIndex == Crossover.HpDamping[_channel1]) return;
    //    HpQVisibleCh1 = value.NameIndex == Crossover.Damping_e.Variable_Q;
    //    RaisePropertyChanged($"HpQVisibleCh1");
    //    // SetProperty(ref _selectedHpDampingNameCh1, value);
    //    RaisePropertyChanged();
    //    Crossover.HpDamping[_channel1] = value.NameIndex;
    //    HPSetAvailableSlopesCh1(value.NameIndex);
    //    // HPSlopeIndexCh1 = HPSlopeNamesCh1.Count - 2;
    //    HPUpdatePlot(_channel1);
    //    HpSlopeVisibleIsVisibleCh1 = value.NameIndex != Crossover.Damping_e.Variable_Q;
    //    if (IsLinked)
    //    {
    //      HPSetAvailableSlopesCh0(value.NameIndex);
    //      Crossover.HpDamping[_channel0] = Crossover.HpDamping[_channel1];
    //      Crossover.HpSlope[_channel0] = Crossover.HpSlope[_channel1];
    //      // _selectedHpDampingNameCh0 = _selectedHpDampingNameCh1;
    //      RaisePropertyChanged($"SelectedHpDampingNameCh0");
    //      HpQVisibleCh0 = HpQVisibleCh1;
    //      RaisePropertyChanged($"HpQVisibleCh0");
    //      // HPSlopeIndexCh0 = HPSlopeNamesCh0.Count - 2;
    //      HPUpdatePlot(_channel0);
    //      HpSlopeVisibleIsVisibleCh0 = HpSlopeVisibleIsVisibleCh1;
    //    }
    //    Utils.CrossoverPageView?.Redraw();
    //  }
    //}

    #endregion

    #region LPCh1
    #region LPFreqCh1
    public bool LpFreqVisibleCh1 { get; set; }
    public string LpFreqEntryCh1
    {
      get => _sLpFreqCh1;
      set => _sLpFreqCh1 = value;
    }
    private void ExecuteLpFreqEntryCompletedCommandCh1()
    {
      if (!float.TryParse(_sLpFreqCh1, out var result)) return;
      Crossover.LpFrequency[_channel1] = result;
      RaisePropertyChanged($"LpLogControlFreqCh1");
      LPUpdatePlot(_channel1);
      if (IsLinked)
      {
        Crossover.LpFrequency[_channel0] = Crossover.LpFrequency[_channel1];
        RaisePropertyChanged($"LpLogControlFreqCh0");
        _sLpFreqCh0 = _sLpFreqCh1;
        RaisePropertyChanged($"LpFreqEntryCh0");
        LPUpdatePlot(_channel0);
      }
      Utils.CrossoverPageView?.Redraw();
    }
    public float LpLogControlFreqCh1
    {
      get => (float)Math.Log10(Crossover.LpFrequency[_channel1]);
      set
      {
        if (Utils.CrossoverPageView == null || !AllowUpdates)
        {
          return;
        }
        var freq = Math.Pow(10, value);
        _sLpFreqCh1 = FormatFreq((float)freq);
        Crossover.LpFrequency[_channel1] = freq;
        RaisePropertyChanged();
        RaisePropertyChanged("LpFreqEntryCh1");
        LPUpdatePlot(_channel1);
        if (Crossover.HpSlope[_channel1] != Crossover.Slope_e.Flat && freq < Crossover.HpFrequency[_channel1])
        {
          HpLogControlFreqCh1 = LpLogControlFreqCh1;
        }
        if (IsLinked)
        {
          _sLpFreqCh0 = _sLpFreqCh1;     
          Crossover.LpFrequency[_channel0] = Crossover.LpFrequency[_channel1];
          RaisePropertyChanged($"LpLogControlFreqCh0");
          RaisePropertyChanged($"LpFreqEntryCh0");
          LPUpdatePlot(_channel0);
          if (Crossover.HpSlope[_channel0] != Crossover.Slope_e.Flat && freq < Crossover.HpFrequency[_channel0])
          {
            HpLogControlFreqCh0 = LpLogControlFreqCh0;
          }
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    #endregion

    //public DampingPickerName SelectedLpDampingNameCh1
    //{
    //  get => DampingNames[(int)Crossover.LpDamping[_channel1]];  //_selectedLpDampingNameCh1;
    //  set
    //  {
    //    if (Utils.CrossoverPageView == null || !AllowUpdates)
    //    {
    //      return;
    //    }
    //    if (value.NameIndex == Crossover.LpDamping[_channel1]) return;
    //    LpQVisibleCh1 = value.NameIndex == Crossover.Damping_e.Variable_Q;
    //    LpSlopeVisibleIsVisibleCh1 = value.NameIndex != Crossover.Damping_e.Variable_Q;
    //    RaisePropertyChanged($"LpQVisibleCh1");
    //    // SetProperty(ref _selectedLpDampingNameCh1, value);
    //    Crossover.LpDamping[_channel1] = value.NameIndex;
    //    RaisePropertyChanged();
    //    LPSetAvailableSlopesCh1(value.NameIndex);
    //    //LPSlopeIndexCh1 = LPSlopeNamesCh1.Count - 2;
    //    LPUpdatePlot(_channel1);

    //    if (IsLinked)
    //    {
    //      LPSetAvailableSlopesCh0(value.NameIndex);
    //      Crossover.LpDamping[_channel0] = Crossover.LpDamping[_channel1];
    //      Crossover.HpSlope[_channel0] = Crossover.HpSlope[_channel1];
    //      //_selectedLpDampingNameCh0 = _selectedLpDampingNameCh1;
    //      RaisePropertyChanged($"LpDampingIndexCh0");
    //      LpQVisibleCh0 = LpQVisibleCh1;
    //      RaisePropertyChanged($"LpQVisibleCh0");
    //      //LPSlopeIndexCh0 = LPSlopeNamesCh0.Count - 2;
    //      LPUpdatePlot(_channel0);
    //      LpSlopeVisibleIsVisibleCh0 = LpSlopeVisibleIsVisibleCh1;
    //    }
    //    Utils.CrossoverPageView?.Redraw();
    //  }
    //}
    #region LPQCh1
    public float LpSliderQCh1
    {
      get => (float)Crossover.LpQ[_channel1];
      set
      {
        if (Utils.CrossoverPageView == null || !AllowUpdates)
        {
          return;
        }
        SetProperty(ref Crossover.LpQ[_channel1], value);
        LpQCh1 = $"{value:N1}";
        RaisePropertyChanged("LpQCh1");
        LPUpdatePlot(_channel1);
        if (IsLinked)
        {
          Crossover.LpQ[_channel0] = Crossover.LpQ[_channel1];
          RaisePropertyChanged("LpQSliderCh0");
          RaisePropertyChanged("LpQCh0");
          LPUpdatePlot(_channel0);
        }
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public string LpQCh1
    {
      get => _sLpQCh1;
      set => _sLpQCh1 = value;
    }
    private void ExecuteLpQEntryCompletedCommandCh1()
    {
      if (!float.TryParse(_sLpQCh1, out var result)) return;

      result = result > QMAX ? QMAX : result;
      result = result < QMIN ? QMIN : result;
      Crossover.LpQ[_channel1] = result;

      RaisePropertyChanged($"LpSliderQCh1");
      RaisePropertyChanged($"LpQCh1");
      LPUpdatePlot(_channel1);
      if (IsLinked)
      {
        Crossover.LpQ[_channel0] = Crossover.LpQ[_channel1];
        RaisePropertyChanged($"LpSliderQCh0");
        RaisePropertyChanged($"LpQCh0");
        LPUpdatePlot(_channel0);
      }
      Utils.CrossoverPageView?.Redraw();
    }
    #endregion LPQCh1

    #endregion LPCh1

    private static int _channel0 = 0;
    private static int _channel1 = 1;

    public int ChannelPairIndex
    {
      get => _channelPair;
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref _channelPair, value);
        SelectChannel();
      }
    }
    
    public bool IsLinked
    {
      get
      {
        int mask = 1 << _channelPair;
        return (Crossover.LinkMask & mask) == mask;
      }
      set
      {
        if (!AllowUpdates) return;
        int mask = 1 << _channelPair;
        Crossover.LinkMask &= ~mask;
        if (value)
        {
          Crossover.LinkMask |= mask;
          HpLogControlFreqCh1 = HpLogControlFreqCh0;
          LpLogControlFreqCh1 = LpLogControlFreqCh0;
          Crossover.HpQ[_channel1] = Crossover.HpQ[_channel0];
          Crossover.LpQ[_channel1] = Crossover.LpQ[_channel0];
          Crossover.HpDamping[_channel1] = Crossover.HpDamping[_channel0];
          Crossover.LpDamping[_channel1] = Crossover.LpDamping[_channel0];
          Crossover.HpSlope[_channel1] = Crossover.HpSlope[_channel0];
          Crossover.LpSlope[_channel1] = Crossover.LpSlope[_channel0];
          Crossover.HpFrequency[_channel1] = Crossover.HpFrequency[_channel0];
          Crossover.LpFrequency[_channel1] = Crossover.LpFrequency[_channel0];

          _HPslopeNameDatach1 = new SlopeNameData(Crossover.HpDamping[_channel0]);
          _LPslopeNameDatach1 = new SlopeNameData(Crossover.LpDamping[_channel0]);

          RaisePropertyChanged("HPSlopeNamesCh1");
          RaisePropertyChanged("LPSlopeNamesCh1");

          HpSliderQCh1 = HpSliderQCh0;
          LpSliderQCh1 = LpSliderQCh0;
          //SelectedHpDampingNameCh0 = SelectedHpDampingNameCh1;
          //SelectedLpDampingNameCh1 = SelectedLpDampingNameCh0;

          HpSlopeVisibleIsVisibleCh1 = HpSlopeVisibleIsVisibleCh0;
          LpSlopeVisibleIsVisibleCh1 = LpSlopeVisibleIsVisibleCh0;
          HpQVisibleCh1 = HpQVisibleCh0;
          LpQVisibleCh1 = LpQVisibleCh0;
          HpFreqVisibleCh1 = HpFreqVisibleCh0;
          LpFreqVisibleCh1 = LpFreqVisibleCh0;
          UpdateProperties();
        }
        SendLinking();
      }
    }

    public int HPSlopeIndexCh0
    {
      get => SlopeToIndex(Crossover.HpDamping[_channel0], Crossover.HpSlope[_channel0]);
      set
      {
        if (value < 0 || !AllowUpdates) return;
        Crossover.HpSlope[_channel0] = IndexToSlope(Crossover.HpDamping[_channel0], value);
        var slope = HPSlopeNamesCh0[value].SlopeIndex;
        HpFreqVisibleCh0 = slope != Crossover.Slope_e.Flat;
        HPUpdatePlot(_channel0);
        Send(Crossover.FilterType.HIGH_PASS, _channel0);
        if (IsLinked)
        {
          Crossover.HpSlope[_channel1] = Crossover.HpSlope[_channel0];
          HpFreqVisibleCh1 = HpFreqVisibleCh0;
          HPUpdatePlot(_channel1);
          Send(Crossover.FilterType.HIGH_PASS, _channel1);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public int HPSlopeIndexCh1
    {
      get => SlopeToIndex(Crossover.HpDamping[_channel1], Crossover.HpSlope[_channel1]);
      set
      {
        if (value < 0 || !AllowUpdates) return;
        Crossover.HpSlope[_channel1] = IndexToSlope(Crossover.HpDamping[_channel1], value);
        var slope = HPSlopeNamesCh1[value].SlopeIndex;
        HpFreqVisibleCh1 = slope != Crossover.Slope_e.Flat;
        HPUpdatePlot(_channel1);
        Send(Crossover.FilterType.HIGH_PASS, _channel1);
        if (IsLinked)
        {
          Crossover.HpSlope[_channel0] = Crossover.HpSlope[_channel1];
          HPUpdatePlot(_channel0);
          Send(Crossover.FilterType.HIGH_PASS, _channel0);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public int LPSlopeIndexCh0
    {
      get => SlopeToIndex(Crossover.LpDamping[_channel0], Crossover.LpSlope[_channel0]);
      set
      {
        if (value < 0 || !AllowUpdates) return;
        Crossover.LpSlope[_channel0] = IndexToSlope(Crossover.LpDamping[_channel0], value);
        var slope = LPSlopeNamesCh0[value].SlopeIndex;
        LpFreqVisibleCh0 = slope != Crossover.Slope_e.Flat;
        LPUpdatePlot(_channel0);
        Send(Crossover.FilterType.LOW_PASS, _channel0);
        if (IsLinked)
        {
          Crossover.LpSlope[_channel1] = Crossover.LpSlope[_channel0];
          LPUpdatePlot(_channel1);
          Send(Crossover.FilterType.LOW_PASS, _channel1);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }
    public int LPSlopeIndexCh1
    {
      get => SlopeToIndex(Crossover.LpDamping[_channel1], Crossover.LpSlope[_channel1]);
      set
      {
        if (value < 0 || !AllowUpdates) return;
        Crossover.LpSlope[_channel1] = IndexToSlope(Crossover.LpDamping[_channel1], value);
        var slope = LPSlopeNamesCh1[value].SlopeIndex;
        LpFreqVisibleCh1 = slope != Crossover.Slope_e.Flat;
        LPUpdatePlot(_channel1);
        Send(Crossover.FilterType.LOW_PASS, _channel1);
        if (IsLinked)
        {
          Crossover.LpSlope[_channel0] = Crossover.LpSlope[_channel1];
          LPUpdatePlot(_channel0);
          Send(Crossover.FilterType.LOW_PASS, _channel0);
        }
        UpdateProperties();
        Utils.CrossoverPageView?.Redraw();
      }
    }

    private static void SendLinking()
    {
      var commandArray = new byte[4];
      commandArray[0] = 0;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_LINK_CROSSOVER;
      commandArray[3] = (byte)Crossover.LinkMask;
      Utils.Universal_Write(ref commandArray);
    }

    //private static float _chartMax = 20.0f;
    //private static float _chartMin = -24.0f;
    private static float _bottomMargin = 80;
    // private bool _activityIsLoading;
    private float _leftMargin = 40;
    private float _rightMargin = 8;


    private float TranslateYValue(float dB) // returns plot value given dB
    {
      dB += 24;
      return _height - _bottomMargin - dB * (_height - _bottomMargin) / 44.0f;  // +20 to -24 dB + 4 for controls at bottom
    }
    private double TranslateYValue(double dB) // returns plot value given dB
    {
      dB += 24;
      return (double)_height - (double)_bottomMargin - dB * ((double)_height - (double)_bottomMargin) / 44.0;  // +20 to -24 dB + 4 for controls at bottom
    }
    private float TranslateXValue(float x)
    {
      float y;
      if (x > 4.3f)
      {
        y = 0;
      }
      var xscale = (_width - _rightMargin - _leftMargin);
      y = _leftMargin + xscale * (x - 1.301029f) / 3.0f;
      return _leftMargin + xscale * (x - 1.301029f) / 3.0f;
    }

    private int FrequencyToX(float frequency)
    {
      var logf = (Math.Log10(frequency) - 1.301029) / 3.0;
      return (int)(_leftMargin + (_width - _leftMargin - _rightMargin) * logf);
    }

    private float _width = -1;
    private float _height = -1;

    private bool _hpSlopeVisibleIsVisibleCh0 = true;
    private bool _hpSlopeVisibleIsVisibleCh1 = true;
    private bool _lpSlopeVisibleIsVisibleCh0 = true;
    private bool _lpSlopeVisibleIsVisibleCh1 = true;

    private void OnPainting(SKPaintSurfaceEventArgs e)
    {
      var surface = e.Surface;
      var canvas = surface.Canvas;

      _width = e.Info.Width;
      _height = e.Info.Height;

      canvas.Clear(Constants.PlotBackgroundColor);
      _xAxisPath = new SKPath();
      canvas.DrawRect(_leftMargin, 0, _width - _leftMargin - _rightMargin, _height - _bottomMargin, _xAxisPaint);

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(15));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(15));

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(10));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(10));

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(5));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(5));

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(0));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(0));

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(-5));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(-5));

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(-10));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(-10));

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(-15));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(-15));

      _xAxisPath.MoveTo(_leftMargin, TranslateYValue(-20));
      _xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(-20));

      var bottom = _height - _bottomMargin;

      foreach (var freq in ThirdOctavePageViewModel.ThirdOctaveFrequencies)
      {
        _xAxisPath.MoveTo(FrequencyToX(freq), bottom);
        _xAxisPath.LineTo(FrequencyToX(freq), 0);
      }
      canvas.DrawPath(_xAxisPath, _xAxisPaint);

      SKPaint textPaint = new SKPaint
      {
        IsAntialias = true,
        Style = SKPaintStyle.Fill,
        Color = SKColors.White,
        TextSize = 20,
        IsVerticalText = false
      };

      float yOffset = 20;
      float leftText = 5;
      canvas.DrawText("20", leftText, TranslateYValue(20) + yOffset + 10, textPaint);
      canvas.DrawText("15", leftText, TranslateYValue(15) + yOffset, textPaint);
      canvas.DrawText("10", leftText, TranslateYValue(10) + yOffset, textPaint);
      canvas.DrawText("5", leftText, TranslateYValue(5) + yOffset, textPaint);
      canvas.DrawText("0", leftText, TranslateYValue(0) + yOffset, textPaint);
      canvas.DrawText("-5", leftText, TranslateYValue(-5) + yOffset, textPaint);
      canvas.DrawText("-10", leftText, TranslateYValue(-10) + yOffset, textPaint);
      canvas.DrawText("-15", leftText, TranslateYValue(-15) + yOffset, textPaint);
      canvas.DrawText("-20", leftText, TranslateYValue(-20) + yOffset, textPaint);
      canvas.DrawText("-24", leftText, TranslateYValue(-24) + yOffset, textPaint);

      yOffset = TranslateYValue(-24) + 44;

      var xOffset = -20;

      canvas.DrawText("20", FrequencyToX(20) + xOffset, yOffset, textPaint);
      canvas.DrawText("50", FrequencyToX(50) + xOffset, yOffset, textPaint);
      canvas.DrawText("100", FrequencyToX(100) + xOffset, yOffset, textPaint);
      canvas.DrawText("200", FrequencyToX(200) + xOffset, yOffset, textPaint);
      canvas.DrawText("500", FrequencyToX(500) + xOffset, yOffset, textPaint);
      canvas.DrawText("1K", FrequencyToX(1000) + xOffset, yOffset, textPaint);
      canvas.DrawText("2K", FrequencyToX(2000) + xOffset, yOffset, textPaint);
      canvas.DrawText("5K", FrequencyToX(5000) + xOffset, yOffset, textPaint);
      canvas.DrawText("10K", FrequencyToX(10000) + xOffset, yOffset, textPaint);
      canvas.DrawText("20K", FrequencyToX(20000) - 35, yOffset, textPaint);

      SKPath clipPath = new SKPath();
      clipPath.MoveTo(_leftMargin, 0); // top left
      clipPath.LineTo(_leftMargin, _height - _bottomMargin); // bottom left
      clipPath.LineTo(_width - _rightMargin, _height - _bottomMargin); // bottom right
      clipPath.LineTo(_width - _rightMargin, 0); // top right
      clipPath.LineTo(_leftMargin, 0);

      canvas.ClipPath(clipPath);

      var plotPath = new SKPath();
      int channel;
      int x;
      int startPoint = 0;
      double y = 0;
      for (channel = 0; channel != 8; channel++)
      {
        plotPath.Rewind();
        for (x = 0; x != Constants.PlotPoints; x++)
        {
          y = 20 * Math.Log10(CrossoverResponse.hp_gains[channel, x].Magnitude * CrossoverResponse.lp_gains[channel, x].Magnitude);
          y = TranslateYValue(y);
        }
        plotPath.MoveTo(TranslateXValue((float)CrossoverResponse.XyVals[startPoint].Frequency), (float)y);
        for (x = startPoint; x != Constants.PlotPoints; x++)
        {
          y = 20 * Math.Log10(CrossoverResponse.hp_gains[channel, x].Magnitude * CrossoverResponse.lp_gains[channel, x].Magnitude);
          y = TranslateYValue(y);
          plotPath.LineTo(TranslateXValue((float)CrossoverResponse.XyVals[x].Frequency), (float)y);
        }
        canvas.DrawPath(plotPath, _strokePaints[channel]);
      }

    }

    private static bool Send(Crossover.FilterType type, int channel)
    {
      if (type == Crossover.FilterType.HIGH_PASS)
      {
        return SendSection(Crossover.FilterType.HIGH_PASS, Crossover.HpSlope[channel], Crossover.HpDamping[channel],
                   channel, Crossover.HpFrequency[channel], Crossover.HpQ[channel]);
      }
      else
      {
        return SendSection(Crossover.FilterType.LOW_PASS, Crossover.LpSlope[channel], Crossover.LpDamping[channel],
                   channel, Crossover.LpFrequency[channel], Crossover.LpQ[channel]);
      }

    }
    //  public static bool BlockRemoteUpdate;
    private static bool SendSection(Crossover.FilterType type, Crossover.Slope_e slope, Crossover.Damping_e damping, int channel, double freq, double q)
    {
      // if (BlockRemoteUpdate) return true;
      var commandArray = new byte[14];
      var command = type == Crossover.FilterType.HIGH_PASS ? USB_Commands.CMD_SET_HIGHPASS_CROSSOVER : USB_Commands.CMD_SET_LOWPASS_CROSSOVER;
      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = command;
      commandArray[3] = (byte)channel;
      commandArray[4] = (byte)damping;
      commandArray[5] = (byte)slope;

      Array.Copy(BitConverter.GetBytes((float)freq), 0, commandArray, 6, 4);
      Array.Copy(BitConverter.GetBytes((float)q), 0, commandArray, 10, 4);
      return Utils.Universal_Write(ref commandArray);
    }
  }

  public class DampingPickerName
  {
    public string Name { get; set; } // Name binds with the Picker's ItemDisplayBinding
    public Crossover.Damping_e NameIndex { get; set; }
  }
  public class PickerName
  {
    public string Name { get; set; } // Name binds with the Picker's ItemDisplayBinding
    public int NameIndex { get; set; }
  }
  public class SlopePickerName
  {
    public string Name { get; set; } // Name binds with the Picker's ItemDisplayBinding
    public Crossover.Slope_e SlopeIndex { get; set; }
  }

  public class SlopeNameData
  {
    public IList<SlopePickerName> NameSource { get; set; }

    public SlopeNameData(Crossover.Damping_e damping)
    {
      switch (damping)
      {
        case Crossover.Damping_e.Butterworth:
        default:
          NameSource = new List<SlopePickerName>
          {
            new SlopePickerName
            {
              Name = "6 dB",
              SlopeIndex = Crossover.Slope_e.dB6
            },
           new SlopePickerName
            {
              Name = "12 dB",
              SlopeIndex = Crossover.Slope_e.dB12
            },
           new SlopePickerName
            {
              Name = "18 dB",
              SlopeIndex = Crossover.Slope_e.dB18
            },
           new SlopePickerName
            {
              Name = "24 dB",
              SlopeIndex = Crossover.Slope_e.dB24
            },
           new SlopePickerName
            {
              Name = "30 dB",
              SlopeIndex = Crossover.Slope_e.dB30
            },
           new SlopePickerName
            {
              Name = "36 dB",
              SlopeIndex = Crossover.Slope_e.dB36
            },
           new SlopePickerName
            {
              Name = "42 dB",
              SlopeIndex = Crossover.Slope_e.dB42
            },
           new SlopePickerName
            {
              Name = "48 dB",
              SlopeIndex = Crossover.Slope_e.dB48
            },
           new SlopePickerName
            {
              Name = "Flat",
              SlopeIndex = Crossover.Slope_e.Flat
            },
          };
          break;
        case Crossover.Damping_e.Linkwitz_Riley:
        case Crossover.Damping_e.Bessel:
          NameSource = new List<SlopePickerName>
          {
            new SlopePickerName
            {
              Name = "12 dB",
              SlopeIndex = Crossover.Slope_e.dB12
            },
            new SlopePickerName
            {
              Name = "24 dB",
              SlopeIndex = Crossover.Slope_e.dB24
            },
            new SlopePickerName
            {
              Name = "36 dB",
              SlopeIndex = Crossover.Slope_e.dB36
            },
            new SlopePickerName
            {
              Name = "48 dB",
              SlopeIndex = Crossover.Slope_e.dB48
            },
            new SlopePickerName
            {
              Name = "Flat",
              SlopeIndex = Crossover.Slope_e.Flat
            },
          };
          break;
        case Crossover.Damping_e.Chebyshev_01:
        case Crossover.Damping_e.Chebyshev_10:
        case Crossover.Damping_e.Chebyshev_25:
          NameSource = new List<SlopePickerName>
          {
            new SlopePickerName
            {
              Name = "12 dB",
              SlopeIndex = Crossover.Slope_e.dB12
            },
            new SlopePickerName
            {
              Name = "18 dB",
              SlopeIndex = Crossover.Slope_e.dB18
            },
            new SlopePickerName
            {
              Name = "24 dB",
              SlopeIndex = Crossover.Slope_e.dB24
            },
            new SlopePickerName
            {
              Name = "30 dB",
              SlopeIndex = Crossover.Slope_e.dB30
            },
            new SlopePickerName
            {
              Name = "36 dB",
              SlopeIndex = Crossover.Slope_e.dB36
            },
            new SlopePickerName
            {
              Name = "42 dB",
              SlopeIndex = Crossover.Slope_e.dB42
            },
            new SlopePickerName
            {
              Name = "48 dB",
              SlopeIndex = Crossover.Slope_e.dB48
            },
            new SlopePickerName
            {
              Name = "Flat",
              SlopeIndex = Crossover.Slope_e.Flat
            },
          };
          break;
      }
    }

  }
  public class DampingNameData
  {
    public IList<DampingPickerName> NameSource { get; }
    public DampingNameData()
    {
      NameSource = new List<DampingPickerName>
      {
        new DampingPickerName
        {
          Name = "Butter",
          NameIndex = Crossover.Damping_e.Butterworth
        },
        new DampingPickerName
        {
          Name = "Link-R",
          NameIndex = Crossover.Damping_e.Linkwitz_Riley
        },
        new DampingPickerName
        {
          Name = "Bessel",
          NameIndex = Crossover.Damping_e.Bessel
        },
        new DampingPickerName
        {
          Name = "Cheby .25",
          NameIndex = Crossover.Damping_e.Chebyshev_25
        },
        new DampingPickerName
        {
          Name = "Cheby .1",
          NameIndex = Crossover.Damping_e.Chebyshev_10
        },
        new DampingPickerName
        {
          Name = "Cheby .01",
          NameIndex = Crossover.Damping_e.Chebyshev_01
        },
        new DampingPickerName
        {
          Name = "Var-Q",
          NameIndex = Crossover.Damping_e.Variable_Q
        },
      };
    }
  }

  public class ChannelNameData
  {
    public IList<PickerName> NameSource { get; }

    public ChannelNameData()
    {
      NameSource = new List<PickerName>
      {
        new PickerName
        {
          Name = "Channel 1-2",
          NameIndex = 0
        },
        new PickerName
        {
          Name = "Channel 3-4",
          NameIndex = 1
        },
        new PickerName
        {
          Name = "Channel 5-6",
          NameIndex = 2
        },
        new PickerName
        {
          Name = "Channel 7-8",
          NameIndex = 3
        }
      };
    }
  }

  public static class Crossover
  {
    public static double[] HpFrequency;
    public static double[] LpFrequency;
    public static double[] HpQ;
    public static double[] LpQ;
    public static Crossover.Slope_e[] HpSlope;
    public static Crossover.Slope_e[] LpSlope;
    public static Crossover.Damping_e[] HpDamping;
    public static Crossover.Damping_e[] LpDamping;
    public static int LinkMask; // 4 least significant bits are channel pairs 1-2, 3-4, 5-6, 7-8

    public enum FilterType { ALL_PASS, LOW_PASS, HIGH_PASS, LOW_PASS_FIRST_ORDER, HIGH_PASS_FIRST_ORDER, UNUSED }   // never use ALL_PASS, It's in slope
    public enum Slope_e
    {
      dB6,
      dB12,
      dB18,
      dB24,
      dB30,
      dB36,
      dB42,
      dB48,
      Flat,
    };
    public enum Damping_e
    {
      Butterworth, Linkwitz_Riley, Bessel, Chebyshev_25, Chebyshev_10, Chebyshev_01, Variable_Q, Bypass, All_Pass
    };

    static Crossover()
    {
      int channel;
      HpFrequency = new double[8];
      LpFrequency = new double[8];
      HpQ = new double[8];
      LpQ = new double[8];
      HpSlope = new Slope_e[8];
      LpSlope = new Slope_e[8];
      HpDamping = new Damping_e[8];
      LpDamping = new Damping_e[8];
      for (channel = 0; channel != 8; channel++)
      {
        HpFrequency[channel] = 100;
        LpFrequency[channel] = 5000;
        HpQ[channel] = .7071;
        LpQ[channel] = .7071;
        HpSlope[channel] = Slope_e.dB12;
        LpSlope[channel] = Slope_e.dB12;
        HpDamping[channel] = Damping_e.Butterworth;
        LpDamping[channel] = Damping_e.Butterworth;
      }
    }
  }

}

