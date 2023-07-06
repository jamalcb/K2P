using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;
using K2p.Statics;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using static K2p.ViewModels.ConnectPageViewModel;

namespace K2p.ViewModels
{
  public class ConnectPageViewModel : ViewModelBase
  {
    //private readonly IDictionary<string, object> _properties;
    private string _ipAddress = "10.14.100.254"; // "64.146.180.131";

    private bool _activityIsLoading;
    private readonly IPageDialogService _dialogService; // { get; set; }


    private bool _isBusy = false;

    public DelegateCommand CancelCommand { get; }
    public DelegateCommand TestCommand { get; }
    //public DelegateCommand TrackEventCommand { get; }
    public DelegateCommand Discover { get; }
    private DelegateCommand _connectCommand;
    public DelegateCommand IPEntryCompletedCommand { get; }


    public DelegateCommand ConnectCommand
    {
      get
      {
        return _connectCommand ?? (_connectCommand =
                 new DelegateCommand(async () => await _executeConnectCommand(), () => !IsBusy));
      }
    }

    public ConnectPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(
      navigationService)
    {
      Utils.ConnectVm = this;
      _dialogService = dialogService;
      CancelCommand = new DelegateCommand(ExecuteCancelCommand).ObservesCanExecute(() => CancelButtonEnabled);
      //  TestCommand = new DelegateCommand(_executeTestCommand);
      //TrackEventCommand = new DelegateCommand(_executeTrackEventCommand);
      Discover = new DelegateCommand(_executeDiscoverCommand);
      IPEntryCompletedCommand = new DelegateCommand(ExecuteIPEntryCompletedCommand);

      if (Application.Current.Properties.ContainsKey("IP_Address"))
      {
        _ipAddress = (string)Application.Current.Properties["IP_Address"];
      }
      else
      {
        Application.Current.Properties["IP_Address"] = _ipAddress;
      }

      try
      {
        var ipAddress = IPAddress.Parse(_ipAddress);
      }
      catch (Exception e)
      {
        _dialogService?.DisplayAlertAsync("Exception", e.Message, "OK");
      }

      RaisePropertyChanged("ConnectButtonText");
      clients = new List<Ps8TcpClient>();
    }

    private readonly ManualResetEvent _connectDone = new ManualResetEvent(false);
    //private static bool _connected;

    public bool CancelButtonEnabled
    {
      get => _cancelButtonEnabled;
      set => SetProperty(ref _cancelButtonEnabled, value);
    }

    private void ExecuteIPEntryCompletedCommand()
    {
      Debug.WriteLine("Todo:  ExecuteIPEntryCompletedCommand()");
    }

    //  RaisePropertyChanged("Connected");
    //  RaisePropertyChanged("ConnectButtonText");
    //}

    public async void Connect()
    {
      await _executeConnectCommand();
    }

    private async Task _executeConnectCommand()
    {
      ActivityIsLoading = true;
      CancelButtonEnabled = true;

      Utils.TrackEvent("Connect Clicked");

      if (TCP.IsConnected)
      {
        Utils.MainPageVm.RequestDisconnect =
          true; // timer is awaitable and we don't want to disconnect in the middle of a communication.

        ActivityIsLoading = false;
        Application.Current.Properties["Last_Connected"] = false;
        return;
      }

      try
      {
        if (IsBusy)
          return;
        IsBusy = true;
        //await ConnectAsync(_ipAddress, 6666);
        TCP.Connect(_ipAddress, 6666);
        await Application.Current.SavePropertiesAsync();
        // Connected = !Connected;
        if (TCP.IsConnected)
        {
          Application.Current.Properties["IP_Address"] = _ipAddress;
          await Application.Current.SavePropertiesAsync();
          await Settings.GetSettings();
          Utils.MainPageVm.TimerEnabled = true;
          Utils.TrackEvent($"Connect Success {_ipAddress}");
        }
        else
        {
          await _dialogService?.DisplayAlertAsync("Notice", $"Could not connect to {_ipAddress}", "OK");
          Utils.TrackEvent($"Connect Failed {_ipAddress}");
        }
      }
      catch (Exception e)
      {
        Debug.WriteLine($"Canceled {e.Message}");
        IsBusy = false;
      }

      IsBusy = false;
      ActivityIsLoading = false;
      CancelButtonEnabled = false;

      UpdateProperties();
    }

    public void UpdateProperties()
    {
      RaisePropertyChanged("ConnectButtonText");
    }

    public string IpAddress
    {
      get => _ipAddress;
      set
      {
        if (_ipAddress == value) return;

        _ipAddress = value;
        RaisePropertyChanged();
      }
    }

    private string GetModel(Model_e model)
    {
      switch (model)
      {
        case Model_e.arc10002d:
          return "10002D";
        case Model_e.arc10004d:
          return "10004D";
        case Model_e.arc10006d:
          return "10006D";
        case Model_e.dsp8_50:
          return "PS8 50";
        case Model_e.ps8pro:
          return "PS8 Pro";
        case Model_e.ips8_8:
          return "IPS 8.8";
        default:
          return "Unknown";
      }
    }

    private bool _discoverMode;
   
    private async void _executeDiscoverCommand()
    {
     
      //  _foundIP = false;
      ActivityIsLoading = true;
      // Utils.TrackEvent("Discover Clicked", new Dictionary<string, string> {
      //  { "Category", "Discovery" },
      //  { "option 1", "1"}
      //});
      Utils.TrackEvent("Discover Clicked");
      var result = await GetBroadcast();

      if (result.result == DiscoveryResult.NoAdapterFound)
      {
        Debug.WriteLine("No adapter found");  // await _dialogService.DisplayAlertAsync("No adapter found");
      }
      ActivityIsLoading = false;
      if (result.result == DiscoveryResult.OK)
      {
        if(ButtonFindText == "Find")
        {
          clients.Clear();
        }
        Ps8TcpClient client = new Ps8TcpClient
        {
          IP = result.IP,
          MAC = result.Mac,
          Name = result.Name,
          Model = GetModel(result.Model),
        };
        clients.Add(client);
        if (clients.Count != 0)
        {
          await _dialogService.DisplayAlertAsync("", $"Device found, {clients[0].IP}\r\nName: {clients[0].Name}\r\nModel: {clients[0].Model}", "OK");
        }
      }
      else
      {
        await _dialogService.DisplayAlertAsync("Notice", "No Devices found", "OK");
      }
      if (_discoverMode)
      {
        _discoverMode = false;
      }
      else
      {
        _discoverMode = true;
      }
    }

    // private bool _receivedUDP;

    private DiscoveryResult2 _discoveryResult = new DiscoveryResult2();

    public enum DiscoveryResult
    {
      OK, NoAdapterFound, NoBroadcastFound, Duplicate, Waiting, WrongMac
    }

    public async Task<DiscoveryResult2> GetBroadcast()
    {
      var localAdapterFound = false;
      DiscoveryResult2 result = new DiscoveryResult2();

      foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
      {
        if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
        {
          Console.WriteLine(ni.Name);
          // var ips = ni.GetIPProperties().UnicastAddresses;
          foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
          {
            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
            {
              Console.WriteLine(ip.Address);
              localAdapterFound = true;
            }
          }
        }
      }
      if (!localAdapterFound)
      {
        result.result = DiscoveryResult.NoAdapterFound;
        return result;
        //return DiscoveryResult.NoAdapterFound;
      }

      udpClient = new UdpClient();
      var hostEndPoint = new IPEndPoint(IPAddress.Any, 6667);
      _discoveryResult.result = DiscoveryResult.Waiting;
      // _receivedUDP = false;

      udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
      udpClient.Client.Bind(hostEndPoint);
      udpClient.BeginReceive(ReceiveCallback, null);
      /*
       * Packets are being transmitted every 2.52 seconds
       * PS8 returns to discovery mode in 30 seconds.
       */

      // see https://stackoverflow.com/questions/26798845/await-task-delay-vs-task-delay-wait
      for (var i = 0; i != 6; i++) // 7.5 seconds  
      {
        if (_discoveryResult.result == DiscoveryResult.OK)
        {
          udpClient.Close();
          ButtonFindText = "Find next";
          RaisePropertyChanged("ButtonFindText");
          result = _discoveryResult;
          //result.result = DiscoveryResult.OK;
          //result.IP = IpAddress;
          foreach (var client in clients)
          {
            if (client.IP == IpAddress)
            {
              result.result = DiscoveryResult.Duplicate;
              return result;
            }
          }

          return result; // DiscoveryResult.OK;
        }
        await Task.Delay(1250); // for activity indicator to work
       // Task.Delay(1250).Wait();


      }
      udpClient.Close();
      result.result = DiscoveryResult.NoBroadcastFound;
      //return DiscoveryResult.NoBroadcastFound;
      return result;

    }

    public string ButtonFindText { get; set; } = "Find";

    private int _clientCount;
    UdpClient udpClient;

    List<Ps8TcpClient> clients;

    private void ReceiveCallback(IAsyncResult ar)
    {
      /*
       * Packet contains:
       * Ip address,  4 bytes
       * Mac address, 6 bytes
       * Device namem, 16 bytes
       * Model, 1 byte
       */

      try
      {
        if (udpClient.Client == null)
        {
          return;
        }
        var hostEndPoint = new IPEndPoint(IPAddress.Any, 6667);
        var bytes = udpClient.EndReceive(ar, ref hostEndPoint);

        var ip = $"{bytes[0]}.{bytes[1]}.{bytes[2]}.{bytes[3]}";

        string viewer = $"Discovery {++_clientCount}\r\n";

        viewer += $"IP Address: ip\r\n";
        var mac = $"{bytes[4]:X2}-{bytes[5]:X2}-{bytes[6]:X2}-{bytes[7]:X2}-{bytes[8]:X2}-{bytes[9]:X2}";
        viewer += $"MAC {mac}\r\n";

        Debug.WriteLine($"Callback, address {ip}, MAC {mac}");
        var name = Encoding.UTF8.GetString(bytes, 10, 16);
        viewer += $"Name: {name}\r\n";
        //Debug.WriteLine($"Name {name}\r\n");
        //Debug.WriteLine($"Model {(Model_e)bytes[26]}\r\n");
        viewer += $"Model {(Model_e)bytes[26]}\r\n";
        //     _receivedUDP = true;      

        if (mac.StartsWith("f8-f0-05", StringComparison.CurrentCultureIgnoreCase | StringComparison.Ordinal))
        {
          _discoveryResult.result = DiscoveryResult.OK;
          _discoveryResult.IP = ip;
          _discoveryResult.Mac = mac;
          _discoveryResult.Name = name;
          if (bytes.Length > 26) // older firmware may not send the model enumeration
          {
            _discoveryResult.Model = (Model_e)bytes[26];
          }
        }
        else
        {
          _discoveryResult.result = DiscoveryResult.WrongMac;
        }
        ActivityIsLoading = false;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }

    private void ExecuteCancelCommand()
    {

      TCP.clientSocket?.Close();
      //TCP.WiFiStream = null;
      CancelButtonEnabled = false;
    }
    public string ConnectButtonText => TCP.IsConnected ? "Disconnect" : "Connect";

    // public static TcpClient clientSocket;// = new TcpClient(AddressFamily.InterNetwork);
    private bool _cancelButtonEnabled;

    public bool IsBusy
    {
      get => _isBusy;
      set => SetProperty(ref _isBusy, value);
    }
    public bool ActivityIsLoading
    {
      get => _activityIsLoading;
      set => SetProperty(ref _activityIsLoading, value);
    }
    // private static bool CheckIpAddress(string address)
    //{
    //  var spl = address.Split('.');
    //   return spl.Length == 4;
    //}
    // public enum Model_e { Arc10002D, Arc10004d, Arc10006d, Arc10002a, Arc10004a, Arc10006a, PS8Pro, DSP850, ips8_8, model_unknown };
    public enum Model_e { arc10002d, arc10004d, arc10006d, arc10002a, arc10004a, arc10006a, ps8pro, dsp8_50, ips8_8, model_unknown };
  }

  public class Ps8TcpClient
  {
    public string IP;
    public string MAC;
    public string Name;
    public string Model;
  }

  public class DiscoveryResult2
  {
    public DiscoveryResult result;
    public string IP;
    public string Mac;
    public string Name;
    public Model_e Model;

  }




}
