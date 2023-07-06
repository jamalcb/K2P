using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Xamarin.Forms;
using System.IO;

namespace K2p.Statics
{
  public static class TCP
  {
    //private static string _ipAddress = "10.14.100.25";
    private static bool _connected;
    public static Stream WiFiStream;
    public static TcpClient clientSocket = new TcpClient(AddressFamily.InterNetwork);
    private static readonly ManualResetEvent _connectDone = new ManualResetEvent(false);

    public static bool IsConnected => _connected;

    public static void Disconnect()
    {
      if (!_connected)
        return;
      try
      {
        _connected = false;

        clientSocket?.GetStream().Close();
        clientSocket?.Close();
        // WiFiStream = null;
        Settings.GetDirtyBusy = false;
        Utils.ConnectVm?.UpdateProperties();       
      }
      catch (Exception e)
      {
        Settings.GetDirtyBusy = false;
        Utils.ConnectVm?.UpdateProperties();
        Utils.MainPageVm?.UpdateConnectButton();
        Debug.WriteLine($"Disconnect error {e.Message}");
      }
      Utils.MainPageVm?.UpdateConnectButton();
    }

    public static void Reconnect()
    {
      Disconnect();
      // Connect((string)Application.Current.Properties["IP_Address"], 6666);
      Connect((string)Utils.GetApplicationProperty("IP_Address", "169.254.0.0"), 6666);
    }
    

    public static void Connect(string ip, int port)
    {
      try
      {
        
        clientSocket = new TcpClient(AddressFamily.InterNetwork);
        var remoteHost = IPAddress.Parse(ip);
        _connected = clientSocket.ConnectAsync(remoteHost, port).Wait(2000);
        if (_connected)
        {
          WiFiStream = clientSocket.GetStream();      
        }
      }
      catch
      {
        _connected = false; 
      }
      Utils.MainPageVm?.UpdateConnectButton();
      Application.Current.Properties["Last_Connected"] = _connected;
    }

    public static  bool TCP_Write(ref byte[] message, int command)
    {
      var temp = ParseMessage.Create_Message(ref message, command);
      try
      {
        if (!TCP.IsConnected) return false;

        //  myTcpClient.WiFiStream.Write(temp, 0, temp.Length);
        TCP.WiFiStream.WriteAsync(temp, 0, temp.Length);
        TCP.WiFiStream.FlushAsync();

        //  myTcpClient.WiFiStream.EndWrite();
        //  myTcpClient.WiFiStream.Flush();
        //   Thread.Sleep(1); //todo why??
        return true;
      }
      catch (Exception ee)
      {
        Debug.WriteLine($"Write Exception {ee.Message}");
        TCP.Disconnect();
        return false;
      }
    }

    public static bool Read(out byte[] buffer)
    {
      var lbuff = new byte[1400];
      buffer = null;
      if (!IsConnected)
      {
        return false;
      }

      try
      {
        var bytesRead = WiFiStream.Read(lbuff, 0, 1400); // 1400-4 bytes of the payload
        if (bytesRead != 1400) return false;
        var bytesToRead = BitConverter.ToInt32(lbuff, 0); // this is the size of the actual payload
        if (Math.Abs(bytesToRead) > 10000)
        {
          Debug.WriteLine($"Bytes to read: {bytesToRead}");
          return false;
        }

        buffer = new byte[bytesToRead];
        var bytesToCopy = bytesToRead < 1400 - 4 ? bytesToRead : 1400 - 4;
        Array.Copy(lbuff, 4, buffer, 0, bytesToCopy);

        bytesToRead -= 1400 - 4;
        if (bytesToRead <= 0)
        {
          return true;
        }

        var blocks = bytesToRead / 1400;
        var remainder = bytesToRead % 1400;
        var index = 1400 - 4;

        if (blocks == 0 && remainder <= 0) return true;


        int i;
        for (i = 0; i != blocks; i++)
        {
          Thread.Sleep(1);
          bytesRead = WiFiStream.Read(buffer, index, 1400);
          if (bytesRead == 0)
          {
            return false;
          }
          index += 1400;
        }

        if (remainder > 0)
        {
          bytesRead = WiFiStream.Read(buffer, index, remainder);
          if (bytesRead == 0)
          {
            return false;
          }
        }
      }
      catch (Exception ee)
      {
        Debug.WriteLine($"Exception {ee.Message}");
        Disconnect();
        return false;
      }
      return true;
    }
    //public static bool ConnectAsync(string ip, int port)
    //{
    //  if (!clientSocket.Connected)
    //  {
    //    try
    //    {
    //      //  clientSocket = new TcpClient(AddressFamily.InterNetwork);
    //      var remoteHost = IPAddress.Parse(ip);
    //      clientSocket = new TcpClient(AddressFamily.InterNetwork);
    //      clientSocket.BeginConnect(remoteHost, port, ConnectCallback, clientSocket);
    //    }
    //    catch (Exception e)
    //    {
    //      return false;
    //    }
    //    _connectDone.WaitOne();
    //    _connected = true;
    //  }
    //  else
    //  {
    //    clientSocket.GetStream().Close();
    //    clientSocket.Close();
    //    WiFiStream = null;
    //    _connected = false;
    //    Settings.GetDirtyBusy = false;
    //    return false;
    //  }

    //  WiFiStream = clientSocket.GetStream();
    //  return clientSocket.Connected;
    //}
    //private static void ConnectCallback(IAsyncResult ar)
    //{
    //  var client = (TcpClient)ar.AsyncState;

    //  // Complete the connection. 
    //  try
    //  {
    //    client.EndConnect(ar);
    //    _connectDone.Set();
    //  }
    //  catch
    //  {

    //  }
    //}

    private static bool CheckIpAddress(string address)
    {
      var spl = address.Split('.');
      return spl.Length == 4;
    }
  }
}
