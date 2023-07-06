using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using K2p.Statics;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using System.Diagnostics;

namespace K2p.ViewModels
{
  public class OptionsPageViewModel : ViewModelBase
  {
    public DelegateCommand PasswordEntryCompletedCommand { get; }
    private DBIncrementName _selectedMasterIncrement;// = DBIncrementNameData.NameSource[0];
    private DBIncrementName _selectedEqIncrement;// = DBIncrementNameData.NameSource[0];
    private DBIncrementName _selectedTrimIncrement;// = DBIncrementNameData.NameSource[0];
    private DBIncrementNameData _nameData = new DBIncrementNameData();
    public List<DBIncrementName> DBIncrementsNames => _nameData.NameSource;
    private string _passwordEntryValue;
    private int _password;
    public bool AllowUpdates = false;
    
    public OptionsPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
    {
      PasswordEntryCompletedCommand = new DelegateCommand(ExecutePasswordEntryCompletedCommand);

      var index = (int)Application.Current.Properties["MasterIncrement"];

      _selectedMasterIncrement = new DBIncrementName();
      _selectedMasterIncrement.Index = index;

      index = (int)Application.Current.Properties["EQGainIncrement"];
      _selectedEqIncrement = new DBIncrementName();
      _selectedEqIncrement.Index = index;

      index = (int)Application.Current.Properties["TrimIncrement"];

      _selectedTrimIncrement = new DBIncrementName();
      _selectedTrimIncrement.Index = index;
    }
    private void ExecutePasswordEntryCompletedCommand()
    {
      _passwordEntryValue = _passwordEntryValue.Replace(",", string.Empty);
      if (!int.TryParse(_passwordEntryValue, out _password)) return;
      Debug.WriteLine($"ExecutePasswordEntryCompletedCommand() {_password}");
      Application.Current.Properties["Password"] = _password;
      Application.Current.SavePropertiesAsync();
      //PasswordLabel = _password == Settings.Password ? "Match" : "Error";
      RaisePropertyChanged("PasswordLabel");
      RaisePropertyChanged("PasswordOKColor");      
    }
    public string PasswordLabel => PasswordMatch ? "Password match" : "Password mismatch";

    public Color PasswordOKColor => PasswordMatch ? Color.LightGreen : Color.Red;

    public bool PasswordMatch => (int)Application.Current.Properties["Password"] == Settings.Password;

    public string PasswordEntryValue
    {
      get => _passwordEntryValue;
      set
      {
        if (!AllowUpdates) return;
        SetProperty(ref _passwordEntryValue, value);
      }
    }
  //  private int _masterIncrementIndex;
    public int SelectedMasterIndex
    {
      get => _selectedMasterIncrement.Index;
      set
      {
        if (!AllowUpdates) return;
        if (value < 0)
        {
          Debug.WriteLine("Whoops");
        }
        _selectedMasterIncrement.Index = value;
        Application.Current.Properties["MasterIncrement"] = value;
        Settings.MasterIncrement = Settings.GetIncrement(value);// _selectedMasterIncrement.Increment;
        RaisePropertyChanged();
        Utils.SaveProperties();
      }
    }

   // private int _EqIncrementIndex;
    public int SelectedEqIndex
    {
      get =>  _selectedEqIncrement.Index;
      set
      {
        if (!AllowUpdates) return;
        _selectedEqIncrement.Index = value;
        Application.Current.Properties["EQGainIncrement"] = value;
        Settings.EQGainIncrement = Settings.GetIncrement(value); //_selectedEqIncrement.Increment;
        RaisePropertyChanged();
        Utils.SaveProperties();
      }
    }


   
    public int SelectedTrimIndex
    {
      get => _selectedTrimIncrement.Index;
      set
      {
        if (!AllowUpdates) return;
        _selectedTrimIncrement.Index = value;
        Application.Current.Properties["TrimIncrement"] = value;
        Settings.TrimIncrement = Settings.GetIncrement(value); ;// _selectedTrimIncrement.Increment;
        RaisePropertyChanged();
        Utils.SaveProperties();
      }
    }

   
    //public DBIncrementName SelectedTrimIncrement
    //{
    //  get => _selectedTrimIncrement;
    //  set
    //  {
    //    SetProperty(ref _selectedTrimIncrement, value);
    //    Application.Current.Properties["TrimIncrement"] = _selectedTrimIncrement.Index;
    //    Settings.TrimIncrement = value.Increment;
    //    Utils.SaveProperties();
    //  }
    //}

    public string Version => "Version 1.0.26";

    public bool EqWarningIsEnabled
    {
      get => (bool)Utils.GetApplicationProperty("EqWarnings", true);
      set
      {
        Application.Current.Properties["EqWarnings"] = value;
        RaisePropertyChanged();
        Utils.SaveProperties();
      }
    }
  }

  public class DBIncrementNameData
  {
    public List<DBIncrementName> NameSource { get; }
    public DBIncrementNameData()
    {
      NameSource = new List<DBIncrementName>
      {
        new DBIncrementName
        {
          Name = "1 dB",
          Increment = 1,
          Index = 0
        },
        new DBIncrementName
        {
          Name = ".5 dB",
          Increment = .5,
          Index = 1
        },
        new DBIncrementName
        {
          Name = ".25 dB",
          Increment = .25,
          Index = 2
        },
        new DBIncrementName
        {
          Name = ".1 dB",
          Increment = .1,
          Index = 3
        },

      };
    }
  }
  public class DBIncrementName
  {
    public string Name { get; set; } // Name binds with the Picker's ItemDisplayBinding
    public double Increment { get; set; }
    public int Index { get; set; }
  }
}
