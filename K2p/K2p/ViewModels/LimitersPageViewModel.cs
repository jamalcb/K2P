using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

using Prism.Navigation;
using Prism.Services;

namespace K2p.ViewModels
{
  public class LimitersPageViewModel : ViewModelBase
  {
    #region local_var
    private string _ch1AttackEntryValue = "50.0";
    private double _ch1AttackSliderValue;
    private string _ch1DecayEntryValue = "5.0";
    private double _ch1DecaySliderValue;
    private string _ch1ThresholdEntryValue = "12";
    private double _ch1ThresholdSliderValue;
    private string _ch1ClippingEntryValue = "100";
    private double _ch1ClippingSliderValue;

    private string _Ch2AttackEntryValue = "50.0";
    private double _Ch2AttackSliderValue;
    private string _Ch2DecayEntryValue = "5.0";
    private double _Ch2DecaySliderValue;
    private string _Ch2ThresholdEntryValue = "12";
    private double _Ch2ThresholdSliderValue;
    private string _Ch2ClippingEntryValue = "100";
    private double _Ch2ClippingSliderValue;

    private string _Ch3AttackEntryValue = "50.0";
    private double _Ch3AttackSliderValue;
    private string _Ch3DecayEntryValue = "5.0";
    private double _Ch3DecaySliderValue;
    private string _Ch3ThresholdEntryValue = "12";
    private double _Ch3ThresholdSliderValue;
    private string _Ch3ClippingEntryValue = "100";
    private double _Ch3ClippingSliderValue;

    private string _Ch4AttackEntryValue = "50.0";
    private double _Ch4AttackSliderValue;
    private string _Ch4DecayEntryValue = "5.0";
    private double _Ch4DecaySliderValue;
    private string _Ch4ThresholdEntryValue = "12";
    private double _Ch4ThresholdSliderValue;
    private string _Ch4ClippingEntryValue = "100";
    private double _Ch4ClippingSliderValue;

    private string _Ch5AttackEntryValue = "50.0";
    private double _Ch5AttackSliderValue;
    private string _Ch5DecayEntryValue = "5.0";
    private double _Ch5DecaySliderValue;
    private string _Ch5ThresholdEntryValue = "12";
    private double _Ch5ThresholdSliderValue;
    private string _Ch5ClippingEntryValue = "100";
    private double _Ch5ClippingSliderValue;

    private string _Ch6AttackEntryValue = "50.0";
    private double _Ch6AttackSliderValue;
    private string _Ch6DecayEntryValue = "5.0";
    private double _Ch6DecaySliderValue;
    private string _Ch6ThresholdEntryValue = "12";
    private double _Ch6ThresholdSliderValue;
    private string _Ch6ClippingEntryValue = "100";
    private double _Ch6ClippingSliderValue;

    private string _Ch7AttackEntryValue = "50.0";
    private double _Ch7AttackSliderValue;
    private string _Ch7DecayEntryValue = "5.0";
    private double _Ch7DecaySliderValue;
    private string _Ch7ThresholdEntryValue = "12";
    private double _Ch7ThresholdSliderValue;
    private string _Ch7ClippingEntryValue = "100";
    private double _Ch7ClippingSliderValue;

    private string _Ch8AttackEntryValue = "50.0";
    private double _Ch8AttackSliderValue;
    private string _Ch8DecayEntryValue = "5.0";
    private double _Ch8DecaySliderValue;
    private string _Ch8ThresholdEntryValue = "12";
    private double _Ch8ThresholdSliderValue;
    private string _Ch8ClippingEntryValue = "100";
    private double _Ch8ClippingSliderValue;
    #endregion
    #region command_delegates

    public DelegateCommand Ch1AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch1DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch1ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch1ClippingEntryCompletedCommand { get; }
    public DelegateCommand Ch2AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch2DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch2ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch2ClippingEntryCompletedCommand { get; }
    public DelegateCommand Ch3AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch3DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch3ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch3ClippingEntryCompletedCommand { get; }
    public DelegateCommand Ch4AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch4DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch4ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch4ClippingEntryCompletedCommand { get; }
    public DelegateCommand Ch5AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch5DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch5ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch5ClippingEntryCompletedCommand { get; }
    public DelegateCommand Ch6AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch6DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch6ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch6ClippingEntryCompletedCommand { get; }
    public DelegateCommand Ch7AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch7DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch7ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch7ClippingEntryCompletedCommand { get; }
    public DelegateCommand Ch8AttackEntryCompletedCommand { get; }
    public DelegateCommand Ch8DecayEntryCompletedCommand { get; }
    public DelegateCommand Ch8ThresholdEntryCompletedCommand { get; }
    public DelegateCommand Ch8ClippingEntryCompletedCommand { get; }
    #endregion command_delegates

    public LimitersPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
    {
      #region command_init
      Ch1AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh1AttackEntryCompleted);
      Ch1DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh1DecayEntryCompleted);
      Ch1ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh1ThresholdEntryCompleted);
      Ch1ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh1ClippingEntryCompleted);

      Ch2AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh2AttackEntryCompleted);
      Ch2DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh2DecayEntryCompleted);
      Ch2ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh2ThresholdEntryCompleted);
      Ch2ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh2ClippingEntryCompleted);

      Ch3AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh3AttackEntryCompleted);
      Ch3DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh3DecayEntryCompleted);
      Ch3ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh3ThresholdEntryCompleted);
      Ch3ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh3ClippingEntryCompleted);

      Ch4AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh4AttackEntryCompleted);
      Ch4DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh4DecayEntryCompleted);
      Ch4ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh4ThresholdEntryCompleted);
      Ch4ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh4ClippingEntryCompleted);

      Ch5AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh5AttackEntryCompleted);
      Ch5DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh5DecayEntryCompleted);
      Ch5ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh5ThresholdEntryCompleted);
      Ch5ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh5ClippingEntryCompleted);

      Ch6AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh6AttackEntryCompleted);
      Ch6DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh6DecayEntryCompleted);
      Ch6ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh6ThresholdEntryCompleted);
      Ch6ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh6ClippingEntryCompleted);

      Ch7AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh7AttackEntryCompleted);
      Ch7DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh7DecayEntryCompleted);
      Ch7ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh7ThresholdEntryCompleted);
      Ch7ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh7ClippingEntryCompleted);

      Ch8AttackEntryCompletedCommand = new DelegateCommand(ExecuteCh8AttackEntryCompleted);
      Ch8DecayEntryCompletedCommand = new DelegateCommand(ExecuteCh8DecayEntryCompleted);
      Ch8ThresholdEntryCompletedCommand = new DelegateCommand(ExecuteCh8ThresholdEntryCompleted);
      Ch8ClippingEntryCompletedCommand = new DelegateCommand(ExecuteCh8ClippingEntryCompleted);
      #endregion
    }

    public double ThresholdMax => 3;
    public double ThresholdMin => -20;

    #region Channel_1
    #region Attack
    private void ExecuteCh1AttackEntryCompleted()
    {
      if (!double.TryParse(_ch1AttackEntryValue, out var result)) return;
      _ch1AttackSliderValue = result;
      RaisePropertyChanged($"Ch1AttackSliderValue");
    }
    public string Ch1AttackEntryValue
    {
      get => _ch1AttackEntryValue;
      set
      {
        _ch1AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch1AttackSliderValue
    {
      get => _ch1AttackSliderValue;
      set
      {
        _ch1AttackSliderValue = value;

        var entry = Math.Round(_ch1AttackSliderValue + .05, 1);
        _ch1AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch1AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh1DecayEntryCompleted()
    {
      if (!double.TryParse(_ch1DecayEntryValue, out var result)) return;
      _ch1DecaySliderValue = result;
      RaisePropertyChanged($"Ch1DecaySliderValue");
    }
    public string Ch1DecayEntryValue
    {
      get => _ch1DecayEntryValue;
      set
      {
        _ch1DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch1DecaySliderValue
    {
      get => _ch1DecaySliderValue;
      set
      {
        _ch1DecaySliderValue = value;

        var entry = Math.Round(_ch1DecaySliderValue + .05, 1);
        _ch1DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch1DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh1ThresholdEntryCompleted()
    {
      if (!double.TryParse(_ch1ThresholdEntryValue, out var result)) return;
      _ch1ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch1ThresholdSliderValue");
    }
    public string Ch1ThresholdEntryValue
    {
      get => _ch1ThresholdEntryValue;
      set
      {
        _ch1ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch1ThresholdSliderValue
    {
      get => _ch1ThresholdSliderValue;
      set
      {
        _ch1ThresholdSliderValue = value;

        var entry = Math.Round(_ch1ThresholdSliderValue + .05, 1);
        _ch1ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch1ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh1ClippingEntryCompleted()
    {
      if (!double.TryParse(_ch1ClippingEntryValue, out var result)) return;
      _ch1ClippingSliderValue = result;
      RaisePropertyChanged($"Ch1ClippingSliderValue");
    }
    public string Ch1ClippingEntryValue
    {
      get => _ch1ClippingEntryValue;
      set
      {
        _ch1ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch1ClippingSliderValue
    {
      get => _ch1ClippingSliderValue;
      set
      {
        _ch1ClippingSliderValue = value;

        var entry = Math.Round(_ch1ClippingSliderValue + .05, 1);
        _ch1ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch1ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_1
    #region Channel_2
    #region Attack
    private void ExecuteCh2AttackEntryCompleted()
    {
      if (!double.TryParse(_Ch2AttackEntryValue, out var result)) return;
      _Ch2AttackSliderValue = result;
      RaisePropertyChanged($"Ch2AttackSliderValue");
    }
    public string Ch2AttackEntryValue
    {
      get => _Ch2AttackEntryValue;
      set
      {
        _Ch2AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch2AttackSliderValue
    {
      get => _Ch2AttackSliderValue;
      set
      {
        _Ch2AttackSliderValue = value;

        var entry = Math.Round(_Ch2AttackSliderValue + .05, 1);
        _Ch2AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch2AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh2DecayEntryCompleted()
    {
      if (!double.TryParse(_Ch2DecayEntryValue, out var result)) return;
      _Ch2DecaySliderValue = result;
      RaisePropertyChanged($"Ch2DecaySliderValue");
    }
    public string Ch2DecayEntryValue
    {
      get => _Ch2DecayEntryValue;
      set
      {
        _Ch2DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch2DecaySliderValue
    {
      get => _Ch2DecaySliderValue;
      set
      {
        _Ch2DecaySliderValue = value;

        var entry = Math.Round(_Ch2DecaySliderValue + .05, 1);
        _Ch2DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch2DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh2ThresholdEntryCompleted()
    {
      if (!double.TryParse(_Ch2ThresholdEntryValue, out var result)) return;
      _Ch2ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch2ThresholdSliderValue");
    }
    public string Ch2ThresholdEntryValue
    {
      get => _Ch2ThresholdEntryValue;
      set
      {
        _Ch2ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch2ThresholdSliderValue
    {
      get => _Ch2ThresholdSliderValue;
      set
      {
        _Ch2ThresholdSliderValue = value;

        var entry = Math.Round(_Ch2ThresholdSliderValue + .05, 1);
        _Ch2ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch2ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh2ClippingEntryCompleted()
    {
      if (!double.TryParse(_Ch2ClippingEntryValue, out var result)) return;
      _Ch2ClippingSliderValue = result;
      RaisePropertyChanged($"Ch2ClippingSliderValue");
    }
    public string Ch2ClippingEntryValue
    {
      get => _Ch2ClippingEntryValue;
      set
      {
        _Ch2ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch2ClippingSliderValue
    {
      get => _Ch2ClippingSliderValue;
      set
      {
        _Ch2ClippingSliderValue = value;

        var entry = Math.Round(_Ch2ClippingSliderValue + .05, 1);
        _Ch2ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch2ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_2
    #region Channel_3
    #region Attack
    private void ExecuteCh3AttackEntryCompleted()
    {
      if (!double.TryParse(_Ch3AttackEntryValue, out var result)) return;
      _Ch3AttackSliderValue = result;
      RaisePropertyChanged($"Ch3AttackSliderValue");
    }
    public string Ch3AttackEntryValue
    {
      get => _Ch3AttackEntryValue;
      set
      {
        _Ch3AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch3AttackSliderValue
    {
      get => _Ch3AttackSliderValue;
      set
      {
        _Ch3AttackSliderValue = value;

        var entry = Math.Round(_Ch3AttackSliderValue + .05, 1);
        _Ch3AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch3AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh3DecayEntryCompleted()
    {
      if (!double.TryParse(_Ch3DecayEntryValue, out var result)) return;
      _Ch3DecaySliderValue = result;
      RaisePropertyChanged($"Ch3DecaySliderValue");
    }
    public string Ch3DecayEntryValue
    {
      get => _Ch3DecayEntryValue;
      set
      {
        _Ch3DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch3DecaySliderValue
    {
      get => _Ch3DecaySliderValue;
      set
      {
        _Ch3DecaySliderValue = value;

        var entry = Math.Round(_Ch3DecaySliderValue + .05, 1);
        _Ch3DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch3DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh3ThresholdEntryCompleted()
    {
      if (!double.TryParse(_Ch3ThresholdEntryValue, out var result)) return;
      _Ch3ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch3ThresholdSliderValue");
    }
    public string Ch3ThresholdEntryValue
    {
      get => _Ch3ThresholdEntryValue;
      set
      {
        _Ch3ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch3ThresholdSliderValue
    {
      get => _Ch3ThresholdSliderValue;
      set
      {
        _Ch3ThresholdSliderValue = value;

        var entry = Math.Round(_Ch3ThresholdSliderValue + .05, 1);
        _Ch3ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch3ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh3ClippingEntryCompleted()
    {
      if (!double.TryParse(_Ch3ClippingEntryValue, out var result)) return;
      _Ch3ClippingSliderValue = result;
      RaisePropertyChanged($"Ch3ClippingSliderValue");
    }
    public string Ch3ClippingEntryValue
    {
      get => _Ch3ClippingEntryValue;
      set
      {
        _Ch3ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch3ClippingSliderValue
    {
      get => _Ch3ClippingSliderValue;
      set
      {
        _Ch3ClippingSliderValue = value;

        var entry = Math.Round(_Ch3ClippingSliderValue + .05, 1);
        _Ch3ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch3ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_3
    #region Channel_4
    #region Attack
    private void ExecuteCh4AttackEntryCompleted()
    {
      if (!double.TryParse(_Ch4AttackEntryValue, out var result)) return;
      _Ch4AttackSliderValue = result;
      RaisePropertyChanged($"Ch4AttackSliderValue");
    }
    public string Ch4AttackEntryValue
    {
      get => _Ch4AttackEntryValue;
      set
      {
        _Ch4AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch4AttackSliderValue
    {
      get => _Ch4AttackSliderValue;
      set
      {
        _Ch4AttackSliderValue = value;

        var entry = Math.Round(_Ch4AttackSliderValue + .05, 1);
        _Ch4AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch4AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh4DecayEntryCompleted()
    {
      if (!double.TryParse(_Ch4DecayEntryValue, out var result)) return;
      _Ch4DecaySliderValue = result;
      RaisePropertyChanged($"Ch4DecaySliderValue");
    }
    public string Ch4DecayEntryValue
    {
      get => _Ch4DecayEntryValue;
      set
      {
        _Ch4DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch4DecaySliderValue
    {
      get => _Ch4DecaySliderValue;
      set
      {
        _Ch4DecaySliderValue = value;

        var entry = Math.Round(_Ch4DecaySliderValue + .05, 1);
        _Ch4DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch4DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh4ThresholdEntryCompleted()
    {
      if (!double.TryParse(_Ch4ThresholdEntryValue, out var result)) return;
      _Ch4ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch4ThresholdSliderValue");
    }
    public string Ch4ThresholdEntryValue
    {
      get => _Ch4ThresholdEntryValue;
      set
      {
        _Ch4ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch4ThresholdSliderValue
    {
      get => _Ch4ThresholdSliderValue;
      set
      {
        _Ch4ThresholdSliderValue = value;

        var entry = Math.Round(_Ch4ThresholdSliderValue + .05, 1);
        _Ch4ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch4ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh4ClippingEntryCompleted()
    {
      if (!double.TryParse(_Ch4ClippingEntryValue, out var result)) return;
      _Ch4ClippingSliderValue = result;
      RaisePropertyChanged($"Ch4ClippingSliderValue");
    }
    public string Ch4ClippingEntryValue
    {
      get => _Ch4ClippingEntryValue;
      set
      {
        _Ch4ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch4ClippingSliderValue
    {
      get => _Ch4ClippingSliderValue;
      set
      {
        _Ch4ClippingSliderValue = value;

        var entry = Math.Round(_Ch4ClippingSliderValue + .05, 1);
        _Ch4ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch4ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_4
    #region Channel_5
    #region Attack
    private void ExecuteCh5AttackEntryCompleted()
    {
      if (!double.TryParse(_Ch5AttackEntryValue, out var result)) return;
      _Ch5AttackSliderValue = result;
      RaisePropertyChanged($"Ch5AttackSliderValue");
    }
    public string Ch5AttackEntryValue
    {
      get => _Ch5AttackEntryValue;
      set
      {
        _Ch5AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch5AttackSliderValue
    {
      get => _Ch5AttackSliderValue;
      set
      {
        _Ch5AttackSliderValue = value;

        var entry = Math.Round(_Ch5AttackSliderValue + .05, 1);
        _Ch5AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch5AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh5DecayEntryCompleted()
    {
      if (!double.TryParse(_Ch5DecayEntryValue, out var result)) return;
      _Ch5DecaySliderValue = result;
      RaisePropertyChanged($"Ch5DecaySliderValue");
    }
    public string Ch5DecayEntryValue
    {
      get => _Ch5DecayEntryValue;
      set
      {
        _Ch5DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch5DecaySliderValue
    {
      get => _Ch5DecaySliderValue;
      set
      {
        _Ch5DecaySliderValue = value;

        var entry = Math.Round(_Ch5DecaySliderValue + .05, 1);
        _Ch5DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch5DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh5ThresholdEntryCompleted()
    {
      if (!double.TryParse(_Ch5ThresholdEntryValue, out var result)) return;
      _Ch5ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch5ThresholdSliderValue");
    }
    public string Ch5ThresholdEntryValue
    {
      get => _Ch5ThresholdEntryValue;
      set
      {
        _Ch5ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch5ThresholdSliderValue
    {
      get => _Ch5ThresholdSliderValue;
      set
      {
        _Ch5ThresholdSliderValue = value;

        var entry = Math.Round(_Ch5ThresholdSliderValue + .05, 1);
        _Ch5ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch5ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh5ClippingEntryCompleted()
    {
      if (!double.TryParse(_Ch5ClippingEntryValue, out var result)) return;
      _Ch5ClippingSliderValue = result;
      RaisePropertyChanged($"Ch5ClippingSliderValue");
    }
    public string Ch5ClippingEntryValue
    {
      get => _Ch5ClippingEntryValue;
      set
      {
        _Ch5ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch5ClippingSliderValue
    {
      get => _Ch5ClippingSliderValue;
      set
      {
        _Ch5ClippingSliderValue = value;

        var entry = Math.Round(_Ch5ClippingSliderValue + .05, 1);
        _Ch5ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch5ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_5
    #region Channel_6
    #region Attack
    private void ExecuteCh6AttackEntryCompleted()
    {
      if (!double.TryParse(_Ch6AttackEntryValue, out var result)) return;
      _Ch6AttackSliderValue = result;
      RaisePropertyChanged($"Ch6AttackSliderValue");
    }
    public string Ch6AttackEntryValue
    {
      get => _Ch6AttackEntryValue;
      set
      {
        _Ch6AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch6AttackSliderValue
    {
      get => _Ch6AttackSliderValue;
      set
      {
        _Ch6AttackSliderValue = value;

        var entry = Math.Round(_Ch6AttackSliderValue + .05, 1);
        _Ch6AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch6AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh6DecayEntryCompleted()
    {
      if (!double.TryParse(_Ch6DecayEntryValue, out var result)) return;
      _Ch6DecaySliderValue = result;
      RaisePropertyChanged($"Ch6DecaySliderValue");
    }
    public string Ch6DecayEntryValue
    {
      get => _Ch6DecayEntryValue;
      set
      {
        _Ch6DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch6DecaySliderValue
    {
      get => _Ch6DecaySliderValue;
      set
      {
        _Ch6DecaySliderValue = value;

        var entry = Math.Round(_Ch6DecaySliderValue + .05, 1);
        _Ch6DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch6DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh6ThresholdEntryCompleted()
    {
      if (!double.TryParse(_Ch6ThresholdEntryValue, out var result)) return;
      _Ch6ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch6ThresholdSliderValue");
    }
    public string Ch6ThresholdEntryValue
    {
      get => _Ch6ThresholdEntryValue;
      set
      {
        _Ch6ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch6ThresholdSliderValue
    {
      get => _Ch6ThresholdSliderValue;
      set
      {
        _Ch6ThresholdSliderValue = value;

        var entry = Math.Round(_Ch6ThresholdSliderValue + .05, 1);
        _Ch6ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch6ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh6ClippingEntryCompleted()
    {
      if (!double.TryParse(_Ch6ClippingEntryValue, out var result)) return;
      _Ch6ClippingSliderValue = result;
      RaisePropertyChanged($"Ch6ClippingSliderValue");
    }
    public string Ch6ClippingEntryValue
    {
      get => _Ch6ClippingEntryValue;
      set
      {
        _Ch6ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch6ClippingSliderValue
    {
      get => _Ch6ClippingSliderValue;
      set
      {
        _Ch6ClippingSliderValue = value;

        var entry = Math.Round(_Ch6ClippingSliderValue + .05, 1);
        _Ch6ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch6ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_5
    #region Channel_7
    #region Attack
    private void ExecuteCh7AttackEntryCompleted()
    {
      if (!double.TryParse(_Ch7AttackEntryValue, out var result)) return;
      _Ch7AttackSliderValue = result;
      RaisePropertyChanged($"Ch7AttackSliderValue");
    }
    public string Ch7AttackEntryValue
    {
      get => _Ch7AttackEntryValue;
      set
      {
        _Ch7AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch7AttackSliderValue
    {
      get => _Ch7AttackSliderValue;
      set
      {
        _Ch7AttackSliderValue = value;

        var entry = Math.Round(_Ch7AttackSliderValue + .05, 1);
        _Ch7AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch7AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh7DecayEntryCompleted()
    {
      if (!double.TryParse(_Ch7DecayEntryValue, out var result)) return;
      _Ch7DecaySliderValue = result;
      RaisePropertyChanged($"Ch7DecaySliderValue");
    }
    public string Ch7DecayEntryValue
    {
      get => _Ch7DecayEntryValue;
      set
      {
        _Ch7DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch7DecaySliderValue
    {
      get => _Ch7DecaySliderValue;
      set
      {
        _Ch7DecaySliderValue = value;

        var entry = Math.Round(_Ch7DecaySliderValue + .05, 1);
        _Ch7DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch7DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh7ThresholdEntryCompleted()
    {
      if (!double.TryParse(_Ch7ThresholdEntryValue, out var result)) return;
      _Ch7ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch7ThresholdSliderValue");
    }
    public string Ch7ThresholdEntryValue
    {
      get => _Ch7ThresholdEntryValue;
      set
      {
        _Ch7ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch7ThresholdSliderValue
    {
      get => _Ch7ThresholdSliderValue;
      set
      {
        _Ch7ThresholdSliderValue = value;

        var entry = Math.Round(_Ch7ThresholdSliderValue + .05, 1);
        _Ch7ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch7ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh7ClippingEntryCompleted()
    {
      if (!double.TryParse(_Ch7ClippingEntryValue, out var result)) return;
      _Ch7ClippingSliderValue = result;
      RaisePropertyChanged($"Ch7ClippingSliderValue");
    }
    public string Ch7ClippingEntryValue
    {
      get => _Ch7ClippingEntryValue;
      set
      {
        _Ch7ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch7ClippingSliderValue
    {
      get => _Ch7ClippingSliderValue;
      set
      {
        _Ch7ClippingSliderValue = value;

        var entry = Math.Round(_Ch7ClippingSliderValue + .05, 1);
        _Ch7ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch7ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_1
    #region Channel_8
    #region Attack
    private void ExecuteCh8AttackEntryCompleted()
    {
      if (!double.TryParse(_Ch8AttackEntryValue, out var result)) return;
      _Ch8AttackSliderValue = result;
      RaisePropertyChanged($"Ch8AttackSliderValue");
    }
    public string Ch8AttackEntryValue
    {
      get => _Ch8AttackEntryValue;
      set
      {
        _Ch8AttackEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch8AttackSliderValue
    {
      get => _Ch8AttackSliderValue;
      set
      {
        _Ch8AttackSliderValue = value;

        var entry = Math.Round(_Ch8AttackSliderValue + .05, 1);
        _Ch8AttackEntryValue = $"{entry:N0}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch8AttackEntryValue");
      }
    }
    #endregion Attack
    #region Decay
    private void ExecuteCh8DecayEntryCompleted()
    {
      if (!double.TryParse(_Ch8DecayEntryValue, out var result)) return;
      _Ch8DecaySliderValue = result;
      RaisePropertyChanged($"Ch8DecaySliderValue");
    }
    public string Ch8DecayEntryValue
    {
      get => _Ch8DecayEntryValue;
      set
      {
        _Ch8DecayEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch8DecaySliderValue
    {
      get => _Ch8DecaySliderValue;
      set
      {
        _Ch8DecaySliderValue = value;

        var entry = Math.Round(_Ch8DecaySliderValue + .05, 1);
        _Ch8DecayEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch8DecayEntryValue");
      }
    }
    #endregion Decay
    #region Threshold
    private void ExecuteCh8ThresholdEntryCompleted()
    {
      if (!double.TryParse(_Ch8ThresholdEntryValue, out var result)) return;
      _Ch8ThresholdSliderValue = result;
      RaisePropertyChanged($"Ch8ThresholdSliderValue");
    }
    public string Ch8ThresholdEntryValue
    {
      get => _Ch8ThresholdEntryValue;
      set
      {
        _Ch8ThresholdEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch8ThresholdSliderValue
    {
      get => _Ch8ThresholdSliderValue;
      set
      {
        _Ch8ThresholdSliderValue = value;

        var entry = Math.Round(_Ch8ThresholdSliderValue + .05, 1);
        _Ch8ThresholdEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch8ThresholdEntryValue");
      }
    }
    #endregion Threshold
    #region Clipping
    private void ExecuteCh8ClippingEntryCompleted()
    {
      if (!double.TryParse(_Ch8ClippingEntryValue, out var result)) return;
      _Ch8ClippingSliderValue = result;
      RaisePropertyChanged($"Ch8ClippingSliderValue");
    }
    public string Ch8ClippingEntryValue
    {
      get => _Ch8ClippingEntryValue;
      set
      {
        _Ch8ClippingEntryValue = value;
        RaisePropertyChanged();
      }
    }
    public double Ch8ClippingSliderValue
    {
      get => _Ch8ClippingSliderValue;
      set
      {
        _Ch8ClippingSliderValue = value;

        var entry = Math.Round(_Ch8ClippingSliderValue + .05, 1);
        _Ch8ClippingEntryValue = $"{entry:N1}";
        RaisePropertyChanged();
        RaisePropertyChanged("Ch8ClippingEntryValue");
      }
    }
    #endregion Clipping
    #endregion Channel_1
  }
}
