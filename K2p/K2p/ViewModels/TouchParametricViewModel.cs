using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using K2p.Statics;
using Prism.Navigation;
using Prism.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using System.Linq;

namespace K2p.ViewModels
{
  public class TouchParametricViewModel : ViewModelBase
  {
    //private Timer _touchTimer;
    public ICommand PaintCommand { get; }
    public ICommand TouchCommand { get; }
    private readonly SKPaint[] _strokePaints = new SKPaint[8];
    private SKPath _xAxisPath;
    private readonly SKPaint _xAxisPaint;
    private bool[] _isEnabled = { true, true, true, true, true, true, true, true };
    private int _SelectedBand;
    private IPageDialogService _dialogService;
    public bool AllowSend;
    public bool BlockProperties;

    public DelegateCommand SelectedBandEntryCompletedCommand { get; }
    public DelegateCommand SelectedGainEntryCompletedCommand { get; }
    public DelegateCommand FrequencyEntryCompletedCommand { get; }
    public DelegateCommand QEntryCompletedCommand { get; }
    public DelegateCommand ButtonBandUp { get; }
    public DelegateCommand ButtonBandDown { get; }
    public DelegateCommand ButtonFlat { get; }
    public DelegateCommand ButtonSelectedFlat { get; }
    public DelegateCommand ButtonLinkAll { get; }
    public DelegateCommand ButtonShowLinks { get; }
               
    private readonly SKPaint _qPaint;
    private readonly SKPaint _circlePaint;
    private float q_Bar_length = 400;

    public TouchParametricViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(
      navigationService)
    {
      _dialogService = dialogService;
     // _selectedCirclePaint = new SKPaint
      //{
      //  Style = SKPaintStyle.Stroke,
      //  Color = SKColors.Red,
      //  StrokeWidth = 6
      //};
      _circlePaint = new SKPaint
      {
        Style = SKPaintStyle.Fill,
        Color = SKColors.LightGreen,
        StrokeWidth = 1
      };
      _qPaint = new SKPaint
      {
        Style = SKPaintStyle.Stroke,
        Color = SKColors.Green,
        StrokeWidth = 8
      };

      PaintCommand = new Command<SKPaintSurfaceEventArgs>(OnPainting);
      TouchCommand = new Command<SKTouchEventArgs>(OnTouch);
      // Utils.TouchParametricVM = this;
      // _touchTimer = new Timer(OnTouchTimerTick, null, 500, 500);
      Utils.GetApplicationProperty("SelectedBand", SelectedBand);

      //if (Application.Current.Properties.ContainsKey("SelectedBand"))
      //{
      //  SelectedBand = (int)Application.Current.Properties["SelectedBand"];
      //}
      //else
      //{
      //  Application.Current.Properties["SelectedBand"] = SelectedBand;
      //}
      _bandEntryValue = $"{SelectedBand + 1:N0}";
      _qEntryValue = $"{Settings.Eq[SelectedBand].Q[0]:N1}";
      SelectedBandEntryCompletedCommand = new DelegateCommand(ExecuteSelectedBandEntryCompletedCommand);
      SelectedGainEntryCompletedCommand = new DelegateCommand(ExecuteSelectedGainEntryCompletedCommand);
      FrequencyEntryCompletedCommand = new DelegateCommand(ExecuteFrequencyEntryCompletedCommand);
      QEntryCompletedCommand = new DelegateCommand(executeQEntryCompletedCommand);
      ButtonBandUp = new DelegateCommand(ExecuteButtonBandUp);
      ButtonBandDown = new DelegateCommand(ExecuteButtonBandDown);
      ButtonFlat = new DelegateCommand(ExecuteButtonFlat);
      ButtonSelectedFlat = new DelegateCommand(ExecuteButtonSelectedFlat);
      ButtonLinkAll = new DelegateCommand(ExecuteButtonLinkAll);
      ButtonShowLinks = new DelegateCommand(ExecuteButtonShowLinks);
      _xAxisPaint = new SKPaint
      {
        Style = SKPaintStyle.Stroke,
        Color = new SKColor(98, 98, 98),
        StrokeWidth = 4,
        StrokeCap = SKStrokeCap.Butt,
        IsAntialias = true
      };

      for (var channel = 0; channel != 8; channel++)
      {
        _strokePaints[channel] = new SKPaint
        {
          Style = SKPaintStyle.Stroke,
          Color = GetSKColor(channel),//    Settings.mixer_2_output_brush[channel].ToSKColor(),
          StrokeWidth = 4,
          StrokeCap = SKStrokeCap.Butt,
          IsAntialias = true
        };
      }

      PeqResponse.ComputePeqGains();
      // InitXAxisPath();
      Debug.WriteLine("Finished with ctor");
    }


    private bool SendGainNonAsync(int band)
    {
      var result = SendGain(band);
      Task.Delay(Constants.EqGainDelay).Wait();
      return result;
    }
    private bool SendFreqNonAsync(int band)
    {
      var result = SendFreq(band);
      Task.Delay(Constants.EqGainDelay).Wait();
      return result;
    }
    private bool SendQNonAsync(int band)
    {
      var result = SendQ(band);
      Task.Delay(Constants.EqGainDelay).Wait();
      return result;
    }

    private async Task<bool> SendGainAsync(int band)
    {
      bool result;

      result = SendGain(band);
      await Task.Delay(Constants.EqGainDelay);

      return result;
    }

    private async void ExecuteButtonSelectedFlat()
    {
      if ((bool)Application.Current.Properties["EqWarnings"])
      {
        var answer =
          await _dialogService.DisplayAlertAsync("Warning",
            $"This action will set all selected channels of the selected band to flat", "OK", "Cancel");
        if (!answer)
        {

          return;
        }
      }

      var changed = false;
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          if (Math.Abs(Settings.Eq[SelectedBand].Gain[channel]) > .01)
          {
            changed = true;
          }

          Settings.Eq[SelectedBand].Gain[channel] = 0;
        }

        if (changed)
        {
          await SendGainAsync(SelectedBand);

        }

      }

      PeqResponse.ComputePeqGains();
      Redraw();
      _gainEntryValue = $"0.00";
      RaisePropertyChanged("GainEntryValue");
    }

    private bool _allLinked;

    public string LinkAllText => _allLinked ? "Unlink" : "Link";

    private void ExecuteButtonLinkAll()
    {
      LinkButtonsVisible = true;
      RaisePropertyChanged("LinkButtonsVisible");
      _allLinked = !_allLinked;

      IsEnabledCh0 = _allLinked;
      IsEnabledCh1 = _allLinked;
      IsEnabledCh2 = _allLinked;
      IsEnabledCh3 = _allLinked;
      IsEnabledCh4 = _allLinked;
      IsEnabledCh5 = _allLinked;
      IsEnabledCh6 = _allLinked;
      IsEnabledCh7 = _allLinked;
      RaisePropertyChanged("LinkAllText");
    }

    private async void ExecuteButtonFlat()
    {
      if ((bool)Application.Current.Properties["EqWarnings"])
      {
        var answer =
          await _dialogService.DisplayAlertAsync("Warning", $"This action will set all selected channels to flat", "OK",
            "Cancel");
        if (!answer)
        {
          return;
        }
      }

      ActivityIsLoading = true;
      Utils.MainPageVm.TimerEnabled = false;    
      for (var band = 0; band != 31; band++)
      {
        var changed = false;
        for (var channel = 0; channel != 8; channel++)
        {
          if (_isEnabled[channel])
          {
            if (Math.Abs(Settings.Eq[band].Gain[channel]) > .01)
            {
              changed = true;
            }

            Settings.Eq[band].Gain[channel] = 0;
          }
        }

        if (changed)
        {
          await SendGainAsync(band);

          //changed = false;
        }
      }
      PeqResponse.ComputePeqGains();
      Redraw();

      _gainEntryValue = $"0.00";
      RaisePropertyChanged("GainEntryValue");
      ActivityIsLoading = false;
      Utils.MainPageVm.TimerEnabled = true;
    }

    public bool LinkButtonsVisible { get; set; }

    private void ExecuteButtonShowLinks()
    {
      LinkButtonsVisible = !LinkButtonsVisible;
      RaisePropertyChanged("LinkButtonsVisible");
    }

    private bool GetActiveChannel(out int ActiveChannel)
    {
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          ActiveChannel = channel;
          return true;
        }
      }
      ActiveChannel = 0;
      return false;
    }

    private void UpdateEntrys()
    {
      if (GetActiveChannel(out int channel))
      {
        _bandEntryValue = $"{SelectedBand + 1:N0}";
        _qEntryValue = $"{Settings.Eq[SelectedBand].Q[channel]:N1}";
        _frequencyEntryValue = $"{Settings.Eq[SelectedBand].Frequency[channel]:N1}";
        _gainEntryValue = $"{Settings.Eq[SelectedBand].Gain[channel]:N2}";
        RaisePropertyChanged("BandEntryValue");
        RaisePropertyChanged("FrequencyEntryValue");
        RaisePropertyChanged("GainEntryValue");
      }
    }

    private void ExecuteButtonBandUp()
    {
      BlockProperties = true;
      SelectedBand = _SelectedBand < 30 ? _SelectedBand + 1 : _SelectedBand;
      UpdateEntrys();
      Redraw();
    }

    private void ExecuteButtonBandDown()
    {
      BlockProperties = true;
      SelectedBand = _SelectedBand > 0 ? _SelectedBand - 1 : _SelectedBand;
      UpdateEntrys();
      Redraw();
    }


    public int SelectedBand
    {
      get => _SelectedBand;
      set
      {
        SetProperty(ref _SelectedBand, value);
        var freq = (float)Settings.Eq[_SelectedBand].Frequency[0];
        SelectedBandFrequency = freq < 1000 ? $"{freq:N1} Hz" : $"{freq:N0} Hz";
        RaisePropertyChanged("SelectedBandFrequency");
        Application.Current.Properties["SelectedBand"] = SelectedBand;
      }
    }

    public async void SaveProperties()
    {
      await Application.Current.SavePropertiesAsync();
    }

    public string SelectedBandFrequency { get; set; } = "20.0 Hz";

    public bool ActivityIsLoading
    {
      get => _activityIsLoading;
      set => SetProperty(ref _activityIsLoading, value);
    }


    #region SelectedGain

    //  private double _gainSliderVal;

    //public double GainSliderVal
    //{
    //  get => _gainSliderVal;
    //  set
    //  {
    //    value = Settings.GetMasterIncrement(value);
    //    SetProperty(ref _gainSliderVal, value);
    //    GainEntryValue = $"{value:N2}";
    //    //Debug.WriteLine("GainSliderVal Setter");
    //    /*
    //     * Weird hack.  If we call ExecuteButtonBandDown(),  GainSlierVal's setter gets called after ExecuteButtonBandDown() completes.
    //     */
    //    if (BlockProperties)
    //    {
    //      BlockProperties = false;
    //      Debug.WriteLine("Blocking GainSliderVal's Setter");
    //      return;
    //    }

    //    for (var channel = 0; channel != 8; channel++)
    //    {
    //      if (_isEnabled[channel])
    //      {
    //        Settings.Eq[SelectedBand].Gain[channel] = _gainSliderVal;
    //      }
    //    }

    //    PeqResponse.ComputePeqGains();

    //    Utils.TouchParametricMainView.Redraw();
    //    SendGainNonAsync(SelectedBand);
    //  }
    //}


    private string _bandEntryValue = "1";
    private string _gainEntryValue = "0.0";
    private string _frequencyEntryValue = "1000.0";
    private string _qEntryValue = "4.4";

    public string BandEntryValue
    {
      get => _bandEntryValue;
      set => SetProperty(ref _bandEntryValue, value);
    }

    public string GainEntryValue
    {
      get => _gainEntryValue;
      set => SetProperty(ref _gainEntryValue, value);
    }
    public string FrequencyEntryValue
    {
      get => _frequencyEntryValue;
      set => SetProperty(ref _frequencyEntryValue, value);
    }
    public string QEntryValue
    {
      get => _qEntryValue;
      set => SetProperty(ref _qEntryValue, value);
    }

    private void ExecuteSelectedBandEntryCompletedCommand()
    {
      if (!int.TryParse(_bandEntryValue, out var result)) return;
      SelectedBand = result - 1;
      UpdateEntrys();
    }

    private void ExecuteSelectedGainEntryCompletedCommand()
    {

      if (!double.TryParse(_gainEntryValue, out var result))
      {
        return;
      }
      Debug.WriteLine($"ExecuteSelectedGainEntryCompletedCommand() {result}");
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          Settings.Eq[SelectedBand].Gain[channel] = result;
        }
      }
      PeqResponse.ComputePeqGains();
      Redraw();
      SendGainNonAsync(SelectedBand);
    }

    private void ExecuteFrequencyEntryCompletedCommand()
    {
      if (!double.TryParse(_frequencyEntryValue, out var result)) return;
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          Settings.Eq[SelectedBand].Frequency[channel] = result;
        }
      }
      PeqResponse.ComputePeqGains();
      Redraw();
      SendFreqNonAsync(SelectedBand);
    }
    private void executeQEntryCompletedCommand()
    {
      Debug.WriteLine("executeQEntryCompletedCommand()");
      if (!double.TryParse(_qEntryValue, out var result)) return;
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          Settings.Eq[SelectedBand].Q[channel] = result;
        }
      }
      PeqResponse.ComputePeqGains();
      Redraw();
      SendQNonAsync(SelectedBand);
    }
    private void Redraw() => Utils.TouchParametricMainView?.Redraw();
    #endregion

    #region Enables

    public bool IsEnabledCh0
    {
      get => _isEnabled[0];
      set
      {
        if (value == _isEnabled[0] || !AllowSend) return;
        SetProperty(ref _isEnabled[0], value);
        Redraw();
      }
    }

    public bool IsEnabledCh1
    {
      get => _isEnabled[1];
      set
      {
        if (value == _isEnabled[1] || !AllowSend) return;
        SetProperty(ref _isEnabled[1], value);
        Redraw();
      }
    }

    public bool IsEnabledCh2
    {
      get => _isEnabled[2];
      set
      {
        if (value == _isEnabled[2] || !AllowSend) return;
        SetProperty(ref _isEnabled[2], value);
        Redraw();
      }
    }

    public bool IsEnabledCh3
    {
      get => _isEnabled[3];
      set
      {
        if (value == _isEnabled[3] || !AllowSend) return;
        SetProperty(ref _isEnabled[3], value);
        Redraw();
      }
    }

    public bool IsEnabledCh4
    {
      get => _isEnabled[4];
      set
      {
        if (value == _isEnabled[4] || !AllowSend) return;
        SetProperty(ref _isEnabled[4], value);
        Redraw();
      }
    }

    public bool IsEnabledCh5
    {
      get => _isEnabled[5];
      set
      {
        if (value == _isEnabled[5] || !AllowSend) return;
        SetProperty(ref _isEnabled[5], value);
        Redraw();
      }
    }

    public bool IsEnabledCh6
    {
      get => _isEnabled[6];
      set
      {
        if (value == _isEnabled[6] || !AllowSend) return;
        SetProperty(ref _isEnabled[6], value);
        Redraw();
      }
    }

    public bool IsEnabledCh7
    {
      get => _isEnabled[7];
      set
      {
        if (value == _isEnabled[7] || !AllowSend) return;
        SetProperty(ref _isEnabled[7], value);
        Redraw();
      }
    }

    public string ChannelLabel0 { get; set; } = Settings.output_labels[0];
    public string ChannelLabel1 { get; set; } = Settings.output_labels[1];
    public string ChannelLabel2 { get; set; } = Settings.output_labels[2];
    public string ChannelLabel3 { get; set; } = Settings.output_labels[3];
    public string ChannelLabel4 { get; set; } = Settings.output_labels[4];
    public string ChannelLabel5 { get; set; } = Settings.output_labels[5];
    public string ChannelLabel6 { get; set; } = Settings.output_labels[6];
    public string ChannelLabel7 { get; set; } = Settings.output_labels[7];

    public Color TextColorCh0 { get; set; } = Settings.mixer_2_output_brush[0];
    public Color TextColorCh1 { get; set; } = Settings.mixer_2_output_brush[1];
    public Color TextColorCh2 { get; set; } = Settings.mixer_2_output_brush[2];
    public Color TextColorCh3 { get; set; } = Settings.mixer_2_output_brush[3];
    public Color TextColorCh4 { get; set; } = Settings.mixer_2_output_brush[4];
    public Color TextColorCh5 { get; set; } = Settings.mixer_2_output_brush[5];
    public Color TextColorCh6 { get; set; } = Settings.mixer_2_output_brush[6];
    public Color TextColorCh7 { get; set; } = Settings.mixer_2_output_brush[7];

    #endregion



    //   private static float _chartMax = 20.0f;
    //  private static float _chartMin = -24.0f;
    private static float _bottomMargin = 100f;
    private bool _activityIsLoading;
    private float _leftMargin = 100f;
    private float _rightMargin = 80f;

    private float TranslateXValue(float x)
    {
      // float y;
      //if (x > 4.3f)
      //{
      //  y = 0;
      //}
      var xscale = (_width - _rightMargin - _leftMargin); // 4.301029f;
      //  y = _leftMargin + xscale * (x - 1.301029f) / 3.0f;
      return _leftMargin + xscale * (x - 1.301029f) / 3.0f;
    }

    private float PlotValueTodB(float yValue)
    {
      var yPos = yValue / (_height - _bottomMargin); // gives us 0 to 1
      return 20 - yPos * 44.0f;
    }


    private float TranslateYValue(float dB) // returns plot value given dB
    {
      dB += 24;
      return _height - _bottomMargin -
             dB * (_height - _bottomMargin) / 44.0f; // +20 to -24 dB + 4 for controls at bottom
    }

    private double TranslateYValue(double dB) // returns plot value given dB
    {
      dB += 24;
      return (double)_height - (double)_bottomMargin -
             dB * ((double)_height - (double)_bottomMargin) / 44.0; // +20 to -24 dB + 4 for controls at bottom
    }

    private int FrequencyToX(float frequency)
    {
      var logf = (Math.Log10(frequency) - 1.301029) / 3.0;
      return (int)(_leftMargin + (_width - _leftMargin - _rightMargin) * logf);
    }

    //   private int _touchtimerCount = 0;

    //private void OnTouchTimerTick(object state)
    //{
    //  // _touchtimerCount++;
    //}
    private bool _leftQTouched;
    private bool _rightQTouched;
    // private bool _isParaMode;
    private bool _isSelected;

    private bool IsClose(SKPoint a, SKPoint b)
    {
      if (Math.Abs(a.X - b.X) < 150 && Math.Abs(a.Y - b.Y) < 150)
        return true;
      return false;
    }

    private bool GetSelectedChannel(out int selected)
    {
      selected = 0;
      for (var channel = 0; channel != 8; channel++)
      {
        if (!_isEnabled[channel]) continue;
        selected = channel;
        return true;
      }
      return false;
    }


    private void OnTouch(SKTouchEventArgs e)
    {
      int channel;
      if (_width < 0)
      {
        return;
      }
      // Need to get the index into the Settings data.
      // Will just try to select the band.

      //var PlotWidth = _width - _leftMargin;
      var scale = 3.0f; //Math.Log10(20000.0);
      double dB;
      //  int band;
      float position;
      float logVal;
      double freq;

      if (!GetSelectedChannel(out int selectedQChannel))
      {
        return;
      }
      switch (e.ActionType)
      {
        case SKTouchAction.Pressed:
          // _touchtimerCount = 0;
          position = e.Location.X - _leftMargin;
          logVal = 1.301029f + scale * position / (_width - _leftMargin - _rightMargin);
          //   freq = Math.Pow(10, logVal);
          // Debug.WriteLine($"logVal {logVal} Freq = {freq}");
          _leftQTouched = IsClose(e.Location, _leftQLocation);
          _rightQTouched = IsClose(e.Location, _rightQLocation);
          _isSelected = false;
          //if(GetSelectedChannel(out selectedQChannel))
          //{
          _isSelected = IsClose(e.Location, GetLocation(SelectedBand, selectedQChannel));
          //}
          //for (channel = 0; channel != 8; channel++)
          //{
          //  if (!_isEnabled[channel]) continue;
          //  selectedQChannel = channel;
          //  _isSelected = IsClose(e.Location, GetLocation(SelectedBand, channel));
          //  if (_isSelected)
          //  {
          //    Debug.WriteLine("Selected");             
          //    break;
          //  }
          //}

          break;
        //  case SKTouchAction.Released:
        case SKTouchAction.Moved:

          var filterLoc = GetLocation(SelectedBand, selectedQChannel);
          if (e.Location.X < 0)
            return;
         // todo //q bar length will be expresses as a percentage of the total with, 0 - 1.0
          if (_leftQTouched)
          {
            var length = (filterLoc.X - e.Location.X) * 2f;

            if (length < (_width *  .1f) || length > (_width * .9f))
              return;            
            q_Bar_length = length;

            Debug.WriteLine("Left Q");
            _leftQLocation.X = e.Location.X;
            // map q from .1 to 11
           // var qq = q_Bar_length / _width;
            var q = PlotWidthToQ(q_Bar_length / _width);
           // Debug.WriteLine($"qq {qq}, q {q}");
            
       //     var q = 2000f / q_Bar_length;
            for (channel = 0; channel != 8; channel++)
            {
              if (!_isEnabled[channel]) continue;
              Settings.Eq[SelectedBand].Q[channel] = q;
            }
            _qEntryValue = $"{q:N1}";
            RaisePropertyChanged("QEntryValue");
            SendQNonAsync(SelectedBand);
            PeqResponse.ComputePeqGains();
            Redraw();
            return;
          }
          if (_rightQTouched)
          {
            var length = (e.Location.X - filterLoc.X) * 2f;
            if (length < (_width * .1f) || length > (_width * .9f))
              return;
            q_Bar_length = length;

            Debug.WriteLine("Right Q");
            _rightQLocation.X = e.Location.X;
            var q = PlotWidthToQ(q_Bar_length / _width);

            for (channel = 0; channel != 8; channel++)
            {
              if (!_isEnabled[channel]) continue;
              Settings.Eq[SelectedBand].Q[channel] = q;
            }
            _qEntryValue = $"{q:N1}";
            RaisePropertyChanged("QEntryValue");
            SendQNonAsync(SelectedBand);
            PeqResponse.ComputePeqGains();
            Redraw();
            return;
          }
          if (!_isSelected)
          {
            return;
          }

          var x = e.Location.X;
          x = x > _width - _rightMargin ? _width - _rightMargin : x;
          x = x < _leftMargin ? _leftMargin : x;
          position = x - _leftMargin;

          logVal = 1.301029f + scale * position / (_width - _leftMargin - _rightMargin);
          freq = Math.Pow(10, logVal);

          dB = PlotValueTodB(e.Location.Y);
          dB = Settings.GetEQGainIncrement(dB, -24.0, 20.0);         

          var changed = false;
          for (channel = 0; channel != 8; channel++)
          {
            if (!_isEnabled[channel]) continue;
            if (!_leftQTouched && !_rightQTouched)
            {
              if (freq < 100)
                SelectedBandFrequency = $"{freq:N2}";
              else if (freq < 1000)
                SelectedBandFrequency = $"{freq:N1}";
              else
              {
                SelectedBandFrequency = $"{freq:N0}";
              }
              RaisePropertyChanged("SelectedBandFrequency");
            }

            if (!(Math.Abs(Settings.Eq[SelectedBand].Gain[channel] - dB) > .05)) continue;
            Settings.Eq[SelectedBand].Gain[channel] = dB;
            Settings.Eq[SelectedBand].Frequency[channel] = freq;
            changed = true;
          }
          GainEntryValue = $"{dB:N2}";
          if (freq < 100)
          {
            FrequencyEntryValue = $"{freq:N2}";
          }
          if (freq < 1000)
          {
            FrequencyEntryValue = $"{freq:N1}";
          }
          else
          {
            FrequencyEntryValue = $"{freq:N0}";
          }
          PeqResponse.ComputePeqGains();
          Redraw();
          if (changed)
          {
            SendGainNonAsync(SelectedBand);
            SendFreqNonAsync(SelectedBand);
          }
          break;
      }
      e.Handled = true;
    }
    private float PlotWidthToQ(float width)
    {
      var val = -16.5f * width +13.3f;
      val = val < .1f ? .1f : val;
      val = val > 14f ? 14f : val;
      Debug.WriteLine($"PlotWidthToQ({width}) = {val}");
      return val;
    }

    private float _width = -1;
    private float _height = -1;
    private SKPoint _leftQLocation;
    private SKPoint _rightQLocation;
   
    SKPoint GetLocation(int band, int channel)
    {
      var point = new SKPoint
      {
        Y = (float)TranslateYValue(Settings.Eq[band].Gain[channel]),
        X = FrequencyToX((float)Settings.Eq[band].Frequency[channel])
      };
      return point;
    }

    private SKColor GetSKColor(int channel) => Settings.mixer_2_output_brush[channel].ToSKColor();

    private void OnPainting(SKPaintSurfaceEventArgs e)
    {  
      var surface = e.Surface;
      var canvas = surface.Canvas;
      canvas.Clear(Constants.PlotBackgroundColor);

      Response.NewPeqCurve();

      int channel;
      var plotPath = new SKPath();

      _width = e.Info.Width;
      _height = e.Info.Height;

      #region Axis
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

      foreach (var freq in Response.ThirdOctaveFrequencies)
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
        TextSize = 40,
        IsVerticalText = false
      };

      float yOffset = 20;
      canvas.DrawText("20", _leftMargin - 80, TranslateYValue(20) + yOffset + 10, textPaint);
      canvas.DrawText("15", _leftMargin - 80, TranslateYValue(15) + yOffset, textPaint);
      canvas.DrawText("10", _leftMargin - 80, TranslateYValue(10) + yOffset, textPaint);
      canvas.DrawText("5", _leftMargin - 80, TranslateYValue(5) + yOffset, textPaint);
      canvas.DrawText("0", _leftMargin - 80, TranslateYValue(0) + yOffset, textPaint);
      canvas.DrawText("-5", _leftMargin - 80, TranslateYValue(-5) + yOffset, textPaint);
      canvas.DrawText("-10", _leftMargin - 80, TranslateYValue(-10) + yOffset, textPaint);
      canvas.DrawText("-15", _leftMargin - 80, TranslateYValue(-15) + yOffset, textPaint);
      canvas.DrawText("-20", _leftMargin - 80, TranslateYValue(-20) + yOffset, textPaint);
      canvas.DrawText("-24", _leftMargin - 80, TranslateYValue(-24) + yOffset, textPaint);

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
      canvas.DrawText("20K", FrequencyToX(20000) + xOffset, yOffset, textPaint);
      #endregion

      SKPoint pos = new SKPoint();
      using (var paint = new SKPaint())
      {
        paint.Style = SKPaintStyle.Stroke;
        paint.Color = SKColors.Red;
        paint.StrokeWidth = 6;
        for (channel = 0; channel != 8; channel++)
        {
          if (!_isEnabled[channel]) continue;
          pos = GetLocation(SelectedBand, channel);
          canvas.DrawCircle(pos, 10, paint);
        }
      }

      for (channel = 0; channel != 8; channel++)
      {
        if (!_isEnabled[channel]) continue;
        pos = GetLocation(SelectedBand, channel);
        _qPaint.Color = GetSKColor(channel); 
        break;
      }

      _leftQLocation = new SKPoint(pos.X - q_Bar_length / 2f, pos.Y);
      _rightQLocation = new SKPoint(pos.X + q_Bar_length / 2f, pos.Y);

      canvas.DrawLine(_leftQLocation, _rightQLocation, _qPaint);

      canvas.DrawCircle(_leftQLocation, 20, _circlePaint);
      canvas.DrawCircle(_rightQLocation, 20, _circlePaint);

      for (channel = 0; channel != 8; channel++)
      {
        plotPath.Rewind();
        plotPath.MoveTo(TranslateXValue((float)Response.XyVals[0].Frequency),
          TranslateYValue(Response.XyVals[0].YVal[channel]));
        for (var x = 1; x != Constants.PlotPoints; x++)
        {
          plotPath.LineTo(TranslateXValue((float)Response.XyVals[x].Frequency),
            TranslateYValue(Response.XyVals[x].YVal[channel]));
        }
        canvas.DrawPath(plotPath, _strokePaints[channel]);
      }
    }

    public bool SendGain(int band)
    {
      var mask = 0;
      if (!AllowSend)
        return false;
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          mask |= 1 << channel;
        }
      }
      if (mask == 0)
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
      for (var channel = 0; channel != 8; channel++)
      {
        var bytes = BitConverter.GetBytes((float)Settings.Eq[band].Gain[channel]);
        Array.Copy(bytes, 0, commandArray, 5 + channel * 4, 4);
      }

      bool result = Utils.Universal_Write(ref commandArray);
      if (!result && TCP.IsConnected)
      {
        Debug.WriteLine("Universal_Write() failed");
      }

      return result;
    }
    public bool SendFreq(int band)
    {
      var mask = 0;
      if (!AllowSend)
        return false;

      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          mask |= 1 << channel;
        }
      }

      if (mask == 0)
        return false;

      if (band > 30)
      {
        throw new Exception($"EQ Gain band {band} greater than 30.");
      }

      var commandArray = new byte[37];
      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_PEQ_FREQ;
      commandArray[3] = (byte)mask;
      commandArray[4] = (byte)band;

      // sending all gains, but the remote end will use only those specified in the mask
      for (var channel = 0; channel != 8; channel++)
      {
        var bytes = BitConverter.GetBytes((float)Settings.Eq[band].Frequency[channel]);
        Array.Copy(bytes, 0, commandArray, 5 + channel * 4, 4);
      }

      bool result = Utils.Universal_Write(ref commandArray);
      if (!result && TCP.IsConnected)
      {
        Debug.WriteLine("Universal_Write() failed");
      }

      return result;
    }
    public bool SendQ(int band)
    {
      var mask = 0;
      if (!AllowSend)
        return false;

      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          mask |= 1 << channel;
        }
      }

      if (mask == 0)
        return false;

      if (band > 30)
      {
        throw new Exception($"EQ Gain band {band} greater than 30.");
      }

      var commandArray = new byte[37];
      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_PEQ_Q;
      commandArray[3] = (byte)mask;
      commandArray[4] = (byte)band;

      // sending all gains, but the remote end will use only those specified in the mask
      for (var channel = 0; channel != 8; channel++)
      {
        var bytes = BitConverter.GetBytes((float)Settings.Eq[band].Q[channel]);
        Array.Copy(bytes, 0, commandArray, 5 + channel * 4, 4);
      }

      bool result = Utils.Universal_Write(ref commandArray);
      if (!result && TCP.IsConnected)
      {
        Debug.WriteLine("Universal_Write() failed");
      }
      return result;
    }
  }
}
