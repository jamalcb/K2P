using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using K2p.Statics;

namespace K2p.ViewModels
{
  public class ReadbackPageViewModel : BindableBase
  {
    private double _transformerTemperature;
    private double _heatsinkTemperature;
    private int _presetLines;
    private int _currentPreset;
    private int _turnonDelay;
    private int _auxTurnonDelay;
    private int _auxTurnoffDelay;


    public ReadbackPageViewModel()
    {
      Utils.ReadbackVm = this;
    }

    

    public double TransformerTemperature
    {
      get => _transformerTemperature;
      set
      {
        _transformerTemperature = value;
        RaisePropertyChanged();
      }
    }
    public double HeatsinkTemperature
    {
      get => _heatsinkTemperature;
      set
      {
        _heatsinkTemperature = value;
        RaisePropertyChanged();
      }
    }

    
    public int PresetLines
    {
      get => _presetLines;
      set => SetProperty(ref _presetLines, value);
    }
    public int CurrentPreset
    {
      get => _currentPreset;
      set => SetProperty(ref _currentPreset, value);
    }
    public int TurnonDelay
    {
      get => _turnonDelay;
      set => SetProperty(ref _turnonDelay, value);
    }
    public int AuxTurnonDelay
    {
      get => _auxTurnonDelay;
      set => SetProperty(ref _auxTurnonDelay, value);
    }
    public int AuxTurnoffDelay
    {
      get => _auxTurnoffDelay;
      set => SetProperty(ref _auxTurnoffDelay, value);
    }

  }
}
