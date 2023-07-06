﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using K2p.Statics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace K2p.ViewModels
{
  public class ThirdOctavePageViewModel : ViewModelBase
  {
    private Timer _touchTimer;
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

    public DelegateCommand SelectedGainEntryCompletedCommand { get; }
    public DelegateCommand ButtonBandUp { get; }
    public DelegateCommand ButtonBandDown { get; }
    public DelegateCommand ButtonFlat { get; }
    public DelegateCommand ButtonSelectedFlat { get; }
    public DelegateCommand ButtonLinkAll { get; }
    public DelegateCommand ButtonShowLinks { get; }

    public static float[] ThirdOctaveFrequencies =
    {
      20f,   25f,   31.5f, 40f,   50f,   63f,   80f,   100f,   125f,   160f,
      200f,  250f,  315f,  400f,  500f,  630f,  800f,  1000f,  1250f,  1600f,
      2000f, 2500f, 3150f, 4000f, 5000f, 6300f, 8000f, 10000f, 12500f, 16000f, 20000f
    };
    private readonly SKPaint _selectedCirclePaint;
    private readonly SKPaint _circlePaint;
  
    public ThirdOctavePageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(
      navigationService)
    {
      _dialogService = dialogService;
      _selectedCirclePaint = new SKPaint
      {
        Style = SKPaintStyle.Fill,
        Color = SKColors.Red,
        StrokeWidth = 1
      };
      _circlePaint = new SKPaint
      {
        Style = SKPaintStyle.Fill,
        Color = SKColors.LightGreen,
        StrokeWidth = 1
      };

      PaintCommand = new Command<SKPaintSurfaceEventArgs>(OnPainting);
      TouchCommand = new Command<SKTouchEventArgs>(OnTouch);
    //  Utils.ThirdOctaveVM = this;
      _touchTimer = new Timer(OnTouchTimerTick, null, 500, 500);

      if (Application.Current.Properties.ContainsKey("SelectedBand"))
      {
        SelectedBand = (int)Application.Current.Properties["SelectedBand"];
      }
      else
      {
        Application.Current.Properties["SelectedBand"] = SelectedBand;
      }

      SelectedGainEntryCompletedCommand = new DelegateCommand(ExecuteSelectedGainEntryCompletedCommand);
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
          Color = Settings.mixer_2_output_brush[channel].ToSKColor(),
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
      Utils.ThirdOctavePageMainView.Redraw();
      _gainSliderVal = 0;
      RaisePropertyChanged("GainSliderVal");
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
        }
      }
      PeqResponse.ComputePeqGains();
      Utils.ThirdOctavePageMainView.Redraw();
      _gainSliderVal = 0;
      RaisePropertyChanged("GainSliderVal");
      _gainEntryValue = $"0.00";
      RaisePropertyChanged("GainEntryValue");
      ActivityIsLoading = false;
      Utils.MainPageVm.TimerEnabled = true;
    }

    private void SetSliderCurrentSelection()
    {
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          _gainSliderVal = Settings.Eq[SelectedBand].Gain[channel];
          RaisePropertyChanged("GainSliderVal");
          _gainEntryValue = $"{_gainSliderVal:N2}";
          RaisePropertyChanged("GainEntryValue");
          return;
        }
      }
    }
    public bool LinkButtonsVisible { get; set; }

    private void ExecuteButtonShowLinks()
    {
      LinkButtonsVisible = !LinkButtonsVisible;
      RaisePropertyChanged("LinkButtonsVisible");
    }


    private void ExecuteButtonBandUp()
    {
      BlockProperties = true;

      Debug.WriteLine("Setting SelectedBand");
      SelectedBand = _SelectedBand < 30 ? _SelectedBand + 1 : _SelectedBand;
      Debug.WriteLine("Calling SetSliderCurrentSelection()");
      SetSliderCurrentSelection();
      Debug.WriteLine("Calling Redraw()");

      Utils.ThirdOctavePageMainView.Redraw();
      Debug.WriteLine("ExecuteButtonBandUp() completed");
    }
    private void ExecuteButtonBandDown()
    {
      BlockProperties = true;
      SelectedBand = _SelectedBand > 0 ? _SelectedBand - 1 : _SelectedBand;
      SetSliderCurrentSelection();
      Utils.ThirdOctavePageMainView.Redraw();
      Debug.WriteLine("ExecuteButtonBandDown() completed");
    }

    private Dictionary<long, SKPath> temporaryPaths = new Dictionary<long, SKPath>();
    private List<SKPath> paths = new List<SKPath>();

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
        //    SaveProperties();
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
    private double _gainSliderVal;
    public double GainSliderVal
    {
      get => _gainSliderVal;
      set
      {
        value = Settings.GetMasterIncrement(value);
        SetProperty(ref _gainSliderVal, value);
        GainEntryValue = $"{value:N2}";
        // Debug.WriteLine("GainSliderVal Setter");
        /*
         * Weird hack.  If we call ExecuteButtonBandDown(),  GainSlierVal's setter gets called after ExecuteButtonBandDown() completes.
         */
        if (BlockProperties)
        {
          BlockProperties = false;
          Debug.WriteLine("Blocking GainSliderVal's Setter");
          return;
        }
        for (var channel = 0; channel != 8; channel++)
        {
          if (_isEnabled[channel])
          {
            Settings.Eq[SelectedBand].Gain[channel] = _gainSliderVal;
          }
        }
        PeqResponse.ComputePeqGains();

        Utils.ThirdOctavePageMainView.Redraw();
        SendGainNonAsync(SelectedBand);
      }
    }
    private string _gainEntryValue = "0.0";
    public string GainEntryValue
    {
      get => _gainEntryValue;
      set => SetProperty(ref _gainEntryValue, value);
    }

    private void ExecuteSelectedGainEntryCompletedCommand()
    {     
      if (!double.TryParse(_gainEntryValue, out var result)) return; 
      for (var channel = 0; channel != 8; channel++)
      {
        if (_isEnabled[channel])
        {
          Settings.Eq[SelectedBand].Gain[channel] = result;
        }
      }
      _gainSliderVal = (float)result;
      RaisePropertyChanged("GainSliderVal");
      Utils.ThirdOctavePageMainView.Redraw();
      SendGainNonAsync(SelectedBand);
    }
    #endregion

    #region Enables
    public bool IsEnabledCh0
    {
      get => _isEnabled[0];
      set => SetProperty(ref _isEnabled[0], value);
    }

    public bool IsEnabledCh1
    {
      get => _isEnabled[1];
      set => SetProperty(ref _isEnabled[1], value);
    }
    public bool IsEnabledCh2
    {
      get => _isEnabled[2];
      set => SetProperty(ref _isEnabled[2], value);
    }
    public bool IsEnabledCh3
    {
      get => _isEnabled[3];
      set => SetProperty(ref _isEnabled[3], value);
    }
    public bool IsEnabledCh4
    {
      get => _isEnabled[4];
      set => SetProperty(ref _isEnabled[4], value);
    }
    public bool IsEnabledCh5
    {
      get => _isEnabled[5];
      set => SetProperty(ref _isEnabled[5], value);
    }
    public bool IsEnabledCh6
    {
      get => _isEnabled[6];
      set => SetProperty(ref _isEnabled[6], value);
    }
    public bool IsEnabledCh7
    {
      get => _isEnabled[7];
      set => SetProperty(ref _isEnabled[7], value);
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
      var xscale = (_width - _rightMargin - _leftMargin);    // 4.301029f;
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
      return _height - _bottomMargin - dB * (_height - _bottomMargin) / 44.0f;  // +20 to -24 dB + 4 for controls at bottom
    }
    private double TranslateYValue(double dB) // returns plot value given dB
    {
      dB += 24;
      return (double)_height - (double)_bottomMargin - dB * ((double)_height - (double)_bottomMargin) / 44.0;  // +20 to -24 dB + 4 for controls at bottom
    }

    private int FrequencyToX(float frequency)
    {
      var logf = (Math.Log10(frequency) - 1.301029) / 3.0;
      return (int)(_leftMargin + (_width - _leftMargin - _rightMargin) * logf);
      //var logf = (Math.Log10(frequency) - 1.0) / 3.302029;
      //return (int)(_leftMargin + (_width - _leftMargin - _rightMargin) * logf);
    }

    private int _touchtimerCount = 0;
    private void OnTouchTimerTick(object state)
    {
      _touchtimerCount++;
    }

    private void OnTouch(SKTouchEventArgs e)
    {
      if (_width < 0)
      {
        return;
      }
      // Need to get the index into the Settings data.
      // Will just try to select the band.

      var PlotWidth = _width - _leftMargin;
      var scale = 3.0f; //Math.Log10(20000.0);
      double dB;
      int band;
      switch (e.ActionType)
      {

        case SKTouchAction.Pressed:
          _touchtimerCount = 0;
          var position = e.Location.X - _leftMargin;
          var logVal = 1.301029f + scale * position / (_width - _leftMargin - _rightMargin);
          var freq = Math.Pow(10, logVal);
          Debug.WriteLine($"logVal {logVal} Freq = {freq}");
          for (band = 0; band != 31; band++)
          {
            if (Settings.Eq[band].Frequency[0] >= freq)
            {
              Debug.WriteLine($"Band {band}, freq {freq}");
              band = band < 0 ? 0 : band;
              SelectedBand = band < 31 ? band : 30;

              break;
            }
          }
          break;
        case SKTouchAction.Released:

        case SKTouchAction.Moved:
          dB = (double)PlotValueTodB(e.Location.Y);
          dB = dB > 20 ? 20 : dB;
          dB = dB < -24 ? -24 : dB;
          //   Debug.WriteLine($"dB {dB}");
          int round = (int)(1 / Constants.EqRoundTo);
          dB = Math.Round(dB * round, MidpointRounding.ToEven) / round;
          var changed = false;
          for (var channel = 0; channel != 8; channel++)
          {
            if (_isEnabled[channel])
            {
              if (Math.Abs(Settings.Eq[SelectedBand].Gain[channel] - dB) > .05)
              {
                Settings.Eq[SelectedBand].Gain[channel] = dB;
                changed = true;
              }
            }
          }

          _gainSliderVal = dB;
          RaisePropertyChanged("GainSliderVal");
          Utils.ThirdOctavePageMainView.Redraw();
          if (changed)
          {
            SendGainNonAsync(SelectedBand);
          }

          break;

      }
      e.Handled = true;

    }

    private float _width = -1;
    private float _height = -1;

    private void OnPainting(SKPaintSurfaceEventArgs e)
    {
      //Debug.WriteLine("Entering OnPaint");
      //   ActivityIsLoading = true;

      var surface = e.Surface;
      var canvas = surface.Canvas;
      canvas.Clear(Constants.PlotBackgroundColor);

      Response.NewPeqCurve();

      int channel;
      var plotPath = new SKPath();

      _width = e.Info.Width;
      _height = e.Info.Height;

      var axisTopY = TranslateYValue(20);
      //var axisBottomY = TranslateYValue(-24);

      _xAxisPath = new SKPath();

      canvas.DrawRect(_leftMargin, 0, _width - _leftMargin - _rightMargin, _height - _bottomMargin, _xAxisPaint);

      //_xAxisPath.MoveTo(_leftMargin, TranslateYValue(20));
      //_xAxisPath.LineTo(_width - _rightMargin, TranslateYValue(20));

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

      //for (var index = 0; index != 31; index++)
      //{
      //  var freq = CircleFrequencies[index];
      //  _xAxisPath.MoveTo(FrequencyToX(20), bottom);
      //  _xAxisPath.LineTo(FrequencyToX(20), 0);
      //}

      foreach (var freq in ThirdOctaveFrequencies)
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
      // var xOffset = -20;
      var xOffset = -20;
      //canvas.DrawText("10", FrequencyToX(10) + xOffset, yOffset, textPaint);
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

      for (channel = 0; channel != 8; channel++)
      {
        var band = 0;
        if (!_isEnabled[channel]) continue;
        foreach (var freq in ThirdOctaveFrequencies)
        {
          // var plotPoint = Response.FrequencyToPlotpoint(freq);
          //var y = TranslateYValue(Response.XYVals[plotPoint].YVal[channel]);
          var y = TranslateYValue(Settings.Eq[band].Gain[channel]);
          var lastx = FrequencyToX(freq);
          canvas.DrawCircle(lastx, (float)y, 10, SelectedBand == band ? _circlePaint : _selectedCirclePaint);

          band++;
        }
      }

      for (channel = 0; channel != 8; channel++)
      {
        plotPath.Rewind();
        plotPath.MoveTo(TranslateXValue((float)Response.XyVals[0].Frequency), TranslateYValue(Response.XyVals[0].YVal[channel]));
        for (var x = 1; x != Constants.PlotPoints; x++)
        {
          plotPath.LineTo(TranslateXValue((float)Response.XyVals[x].Frequency), TranslateYValue(Response.XyVals[x].YVal[channel]));
        }
        canvas.DrawPath(plotPath, _strokePaints[channel]);
      }

      // create the paint for the touch path
      var touchPathStroke = new SKPaint
      {
        IsAntialias = true,
        Style = SKPaintStyle.Stroke,
        Color = SKColors.Purple,
        StrokeWidth = 1
      };

      //  draw the paths
      //foreach (var touchPath in temporaryPaths)
      //{
      //  canvas.DrawPath(touchPath.Value, touchPathStroke);
      //}
      //foreach (var touchPath in paths)
      //{
      //  canvas.DrawPath(touchPath, touchPathStroke);
      //}
      // ActivityIsLoading = false;
    //  Debug.WriteLine($"Exit On paint: Width {_width}, Height {_height}");
    }

    public static bool Send(int mask, int band)
    {
      var commandArray = new byte[37];

      commandArray[0] = USB_Commands.CMD_REPORT_ID;
      commandArray[1] = USB_Commands.CMD_START;
      commandArray[2] = USB_Commands.CMD_SET_PEQ;
      commandArray[3] = (byte)mask;
      commandArray[4] = (byte)band;

      for (var channel = 0; channel != 8; channel++)
      {
        var bytes = BitConverter.GetBytes((float)Settings.Eq[band].Gain[channel]);
        Array.Copy(bytes, 0, commandArray, 5 + channel * 4, 4);
      }
      return  Utils.Universal_Write(ref commandArray);
    }

    public static void SetAllStandard()
    {
      Settings.SetStandardThirdOctave();
      for (var band = 0; band != 31; band++)
      {
        Send(0xff, band);
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

      return Send(mask, band);

      //var commandArray = new byte[65];

      //commandArray[0] = USB_Commands.CMD_REPORT_ID;
      //commandArray[1] = USB_Commands.CMD_START;
      //commandArray[2] = USB_Commands.CMD_SET_PEQ;
      //commandArray[3] = (byte)mask;
      //commandArray[4] = (byte)band;

      //// sending all gains, but the remote end will use only those specified in the mask
      //for (var channel = 0; channel != 8; channel++)
      //{
      //  var bytes = BitConverter.GetBytes((float)Settings.Eq[band].Gain[channel]);
      //  Array.Copy(bytes, 0, commandArray, 5 + channel * 4, 4);
      //}
      //bool result = Utils.Universal_Write(ref commandArray);
     
     // return result;
    }
  }
}
