using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace K2p.Statics
{
  class Constants
  {
    public const double RThev = 5000.0;  // using a 10k pullup and 10k parallel resistor gives us 5000 Thevinin ohms / 1.65 volt source
    public const double SteinhartA = 0.0008377263034;
    public const double SteinhartB = 0.0002208815069;
    public const double SteinhartC = -5.761671235E-08;

    public const int GlobalSize = 240; // members of structure adds up to 196, but it's a union of 240 bytes
    public const int ReadbackSize = 64;
    public const int SettingsSize = 6744;  // to get this size, search C:\Users\Robert Zeff\Documents\VS 2015\Projects\Git\K2\Firmware\K2\Debug\K2.map for settings
    public const int DirtySize = 16;
    public const int PeqSize = 108;
    public const int FaderSize = 64;
    public const int MaxDelaySamples = 2880; 

    public const double VolMax = 6.0;
    public const double VolMin = -60.0;
    public const double TrimMax = 6.0;
    public const double TrimMin = -60.0;

    public const double FaderGainMax = 20;
    public const double FaderGainMin = -20;
    public const double FaderCenterMax = 0;
    public const double FaderCenterMin = -40;
    public const double Fader_Bass_Max = 0;
    public const double Fader_Bass_Min = -40;

    public const double Boost_Max = 15;
    public const double Boost_Min = -15;

    public const double ParaMetricBoostMax = 20;
    public const double ParaMetricBoostMin = -24;

   public const double ThirdOctaveQ = 4.31;

    public const double ParaQMax = 9;
    public const double ParaQMin = .2;
    public const double ParaFreqMax = 20000;
    public const double ParaFreqMin = 10;

    public static int PlotPoints = 256;
    //public static int CrossoverPlotPoints = 128;
    public const double Sample_Rate = 96000.0;

    public const double EqRoundTo = .25;
    public const int EqGainDelay = 2;

    public static readonly SKColor PlotBackgroundColor = new SKColor(51, 51, 51);
    public static float LOG20K = 4.30103f;
    
  }
}
