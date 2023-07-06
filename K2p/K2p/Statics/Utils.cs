using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using K2p.Statics;
using K2p.ViewModels;
using K2p.Views;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

//using Microsoft.Practices.ObjectBuilder2;

namespace K2p.Statics
{
  public static class Utils
  {
    enum MessageState { Start, Start_Found, Size_Found, Sequence_Found, Command_Found, Command_Float }
    public enum DataType { None, Float, Ack, Time, Byte_Array, End_Byte_Array }
    public static bool BlockSetters;
    public static bool MainPageIsLoading = true;
    //public static MainPageViewModel vm;
    public static ThirdOctavePage ThirdOctavePageMainView;
 //   public static ThirdOctavePageViewModel ThirdOctaveVM;
    public static TouchParametric TouchParametricMainView;
  // public static TouchParametricViewModel TouchParametricVM;
    public static CrossoverPage CrossoverPageView;
    public static VuMetersPage VuMetersPageView;

    public static object Parse(ref byte[] buffer, out int index, out int size, out UInt16 command, out int sequence)
    {
      size = 0;
      index = -1;
      sequence = -1;
      command = 0;     

      MessageState state = MessageState.Start;

      for (var i = 0; i != buffer.Length; i++)
      {
        switch (state)
        {
          case MessageState.Start:
            state = buffer[i] == USB_Commands.CMD_START ? MessageState.Start_Found : state;
            command = BitConverter.ToUInt16(buffer, 5);
            break;
          case MessageState.Start_Found:
            size = BitConverter.ToUInt16(buffer, i);
            sequence = BitConverter.ToUInt16(buffer, i + 2);
            i++;
            state = MessageState.Size_Found;

            switch ((DataType)buffer[7])
            {
              case DataType.None:
                return null;
              case DataType.Byte_Array:
                byte[] payload = new byte[size];
                Array.Copy(buffer, 8, payload, 0, size);
                return payload;
              case DataType.End_Byte_Array:
                return null;
              case DataType.Ack:
                ushort retval = BitConverter.ToUInt16(buffer, 8);
                return null;
              case DataType.Float:
                float temp2 = BitConverter.ToSingle(buffer, 8);
                return temp2;

            }

            break;

          case MessageState.Size_Found:
            sequence = BitConverter.ToUInt16(buffer, i);
            i++;
            state = MessageState.Sequence_Found;
            break;
          case MessageState.Sequence_Found:
            state = MessageState.Command_Found;
            break;
        }
      }
      return null;
    }
    //public static uint Sequence { get; set; }
   
    public static ConnectPageViewModel ConnectVm;
    public static ReadbackPageViewModel ReadbackVm;
    public static ToneControlsPageViewModel ToneControlVm;
    public static MainPageViewModel MainPageVm;
    public static TrimsPageViewModel TrimPageVm;

    public static async void SaveProperties()
    {
      await Application.Current.SavePropertiesAsync();
    }

    public static void SendData(int command, ref byte[] data)
    {
      try
      {
        if (!TCP.IsConnected)
          return;

        var temp = ParseMessage.Create_Message(ref data, command);
        if (TCP.WiFiStream == null) return;

        IAsyncResult asyncSend = TCP.WiFiStream.WriteAsync(temp, 0, temp.Length);
        Task.Delay(2).Wait();
        if (!asyncSend.IsCompleted)
          WriteDot(asyncSend);
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
      }
    }
    public static bool Universal_Write(ref byte[] message)
    {
      bool result;     

      if (!TCP.IsConnected)
      {
        return false;
      }       
      result = TCP.TCP_Write(ref message, message[2]);
      return result;
    }
   
    public static object GetApplicationProperty(string prop, object defaultVal)
    {
      if (!Application.Current.Properties.ContainsKey(prop))
      {
        Application.Current.Properties[prop] = defaultVal;
        return defaultVal;
      }
      return Application.Current.Properties[prop];
    }
    
    public static void TrackEvent(string myEvent)
    {
      if (Debugger.IsAttached)
      {
        myEvent = $"D:{myEvent}";
      }
      Analytics.TrackEvent(myEvent);
    }
    public static bool AllowSend = true;
    public static bool SendFloat(int command, double val)
    {
      try
      {
        if ( !TCP.IsConnected || !AllowSend)
          return false;
        Debug.WriteLine($"SendFloat({val})");

        var commandArray = new byte[7];
        commandArray[0] = 0;
        commandArray[1] = USB_Commands.CMD_START;
        commandArray[2] = (byte)command;
   
        Array.Copy(BitConverter.GetBytes((float)val), 0, commandArray, 3, 4);

        var result = Universal_Write(ref commandArray);
        return result;
        //var temp = ParseMessage.Create_Message(ref commandArray, command); //todo

        //if (TCP.WiFiStream == null) return;

        //IAsyncResult asyncSend = TCP.WiFiStream.WriteAsync(temp, 0, temp.Length);
        //Task.Delay(2).Wait();
        //if (!asyncSend.IsCompleted)
        //  WriteDot(asyncSend);
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
        return false;
      }
    }
    public static bool WriteDot(IAsyncResult ar)
    {
      var i = 0;
      while (ar.IsCompleted == false)
      {
        if (i++ > 200)
        {
          return false;
        }
        Task.Delay(5).Wait();
      }
      return true;
    }

    public static double GetSteinhartTemperature(double Vth)
    {
      var rTh = Constants.RThev / (1.65 / Vth - 1);
      var logRth = Math.Log(rTh);
      var T = 1.0 / (Constants.SteinhartA + Constants.SteinhartB * logRth + Constants.SteinhartC * Math.Pow(logRth, 3.0));
      T -= 273.15;
      return T;
    }
    public static double Convert_8_24_to_double(uint input)
    {
      var sign = 1.0;
      if ((input & 0x80000000) != 0)
      {
        input = 0xffffffff - input + 1;
        sign = -1.0;
      }

      const double div = 128.0 / (double)0x80000000;
      return (double)input * div * sign;
    }
  }
}
