using System;
using System.Collections.Generic;
using System.Text;

namespace K2p.Statics
{
  public static class USB_Commands
  {
    public enum GLOBAL_PARAMS
    {
      GLOBAL_PARAMS_AMP_TURN_ON_DELAY,
      GLOBAL_PARAMS_AUX_TURN_ON_DELAY,
      GLOBAL_PARAMS_AUX_TURN_OFF_DELAY,
      GLOBAL_PARAMS_AUTOSENSE_HOLD_TIME,
      GLOBAL_PARAMS_MCU_CLOCK_SOURCE,
      GLOBAL_PARAMS_MCU_CLOCK_DIVIDER,
      GLOBAL_PARAMS_DEVICE_NAME,
      GLOBAL_PARAMS_SERIAL_NUMBER,
      GLOBAL_PARAMS_OVER_VOLTAGE,
      GLOBAL_PARAMS_UNDER_VOLTAGE,
      GLOBAL_PARAMS_MAX_HEATSINK_TEMPERATURE,
      GLOBAL_PARAMS_MAX_TRANSFORMER_TEMPERATURE,
      GLOBAL_PARAMS_WEAR_COUNT,
      GLOBAL_PARAMS_SHORT_COUNT,
      GLOBAL_PARAMS_TRANSFORMER_PROTECT_COUNT,
      GLOBAL_PARAMS_HEATSINK_PROTECT_COUNT,
      GLOBAL_PARAMS_OVER_VOLTAGE_PROTECT_COUNT,
      GLOBAL_PARAMS_UNDER_VOLTAGE_PROTECT_COUNT,
      GLOBAL_PARAMS_VENDOR_ID,
      GLOBAL_PARAMS_PRODUCT_ID,
      GLOBAL_PARAMS_PROTECTION_DELAY
    };
      
    public const byte CMD_REPORT_ID = 0;

    public const byte CMD_GET_GLOBAL = 0x10;

    public const byte CMD_SET_FADER = 0x41;
    public const byte CMD_SET_PEQ_LINKING = 0x4a;
    public const byte CMD_SET_LINK_CROSSOVER = 0x54;
    public const byte CMD_GET_DIRTY = 0x63; //CMD_SEND_DIRTY = 0x59;
    public const byte CMD_GET_READBACK = 0x6b; //0x5c;
    public const byte CMD_GET_SETTINGS = 0x77;
   // public const byte USB_PACKET_SIZE = 65;
    public const byte CMD_GET_TONE = 0x61; //0x69;
    public const byte CMD_SET_TONE = 0x62;


    public const byte CMD_SET_OUTPUT_DELAY = 0x68;
    public const byte CMD_SET_HIGHPASS_CROSSOVER = 0x74;
    public const byte CMD_SET_LOWPASS_CROSSOVER = 0x76;
    public const byte CMD_GET_MASTER_GAIN = 0x8d; //0x73;
    public const byte CMD_SET_MASTER_GAIN = 0x8e;
  

    public const byte CMD_SET_FADER_ASSIGNMENTS = 0x70;

    public const byte CMD_SET_TRIMS = 0x72;
    public const byte CmdGetStatus = 0x75;

    public const byte CMD_OUTPUT_EQ = 0x76;
    //  public const byte CMD_OUTPUT_DELAYS = 0x77;
    // public const byte CMD_PRESETS = 0x78;


    public const byte CMD_FORCE_REMOTE_HANG = 0x79;
    //public const byte CMD_READ_GLOBAL = 0x7a;
    public const byte CMD_WRITE_GLOBAL = 0x7b;
    public const byte CMD_READ_WRITE_PAGE = 0x7c;
    public const byte CMD_MUTE_DAC = 0x7d;






    public const byte CMD_SPDIF_SELECT = 0x7f;
    public const byte CMD_REMOTE_PROFILE = 0x80; // only select remote profile
    public const byte CMD_HEADER = 0x81;
    public const byte CMD_FLASH_ROW = 0x82;
    public const byte CMD_BOOT_ACTION = 0x83;
    public const byte CMD_FLASH_JUMP_START = 0x84;
    public const byte CMD_AUX_COMMAND = 0x84;

    public const byte CMD_CLASS_D_FREQ = 0x85;
    public const byte CMD_FLASH_ERASE = 0x86;
    public const byte CMD_READ_SPI = 0x87;
    public const byte CMD_SET_SX1502 = 0x88;
    public const byte CMD_WRITE_SPI = 0x89;
    public const byte CMD_SET_PEQ = 0x8c;
    public const byte CMD_START = 0xa5;
    public const byte CMD_GET_METERS = 0xb8;
    public const byte CMD_SET_PEQ_FREQ = 0xbd;
    public const byte CMD_SET_PEQ_Q = 0xbf;
    public const byte CMD_SAFELOAD = 0xf9;
    public const byte CMD_ACK = 0xcc;

    public const byte CMD_GET_VERSION = 0xde;
    public const byte CMD_SEND_AUX = 0xdf;
    public const byte CMD_READ_METERS = 0xe3;

    public const byte NACK_CHECKSUM = 0xfb;
    public const byte NACK_TIMEOUT = 0xfc;
    public const byte NACK_START_BYTE = 0xfd;
    public const byte NACK_UNKNOWN_CMD = 0xfe;




    // Fixe do a recall, then twiddle psc gain, watch faders


    //  public const int NVMCTRL_PAGE_SIZE = 64;

    #region serial_flash

    public const int CMD_ERASE_EEPROM = 0x02;
    public const int CMD_READ_EEPROM = 0x03;
    public const int CMD_WRITE_EEPROM = 0x04;
    public const int CMD_WRITE_PRESET = 0x05;
    public const int CMD_LOAD_PRESET = 0x06;
    public const int CMD_READ_EEPROM_CONTINUE = 0x07;
    public const int CMD_TEST = 0x08;

    #endregion

    public const int CMD_READ_I2C = 0x09;

    public const int CMD_WRITE_I2C = 0x0A;
    // public const int CMD_WRITE_I2C = 0x0A;

    #region sub_commands

    public const int CMD_SUB_COMMAND_BOOT_DSP = 0x01;
    public const int CMD_SUB_COMMAND_TRIGGER_AUX_READ = 0x02;
    public const int CMD_SUB_COMMAND_INIT_AKM = 0x03;
    public const int CMD_SUB_COMMAND_ENTER_BOOTLOADER = 0x04;
    public const int CMD_SUB_COMMAND_REBOOT = 0x05;

    #endregion

    #region bootloader

    public const int ATMEL_START_ADDRESS = 0x8000;



    #endregion
  }

  public static class ParseMessage
  {

    enum MessageState { Start, Start_Found, Size_Found, Sequence_Found, Command_Found, Command_Float }
    internal enum DataType { None, Float, Ack, Time, Byte_Array, End_Byte_Array }
    /// <summary>
    /// Scan for valid message
    /// Message has already been checksumed
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="index">will point to beginning of message</param>
    /// <param name="size"></param>
    /// <returns></returns>
    internal static object Parse(ref byte[] buffer, out int index, out int size, out UInt16 command, out int sequence)
    {
      size = 0;
      index = -1;
      sequence = -1;
      command = 0;
      //   data = null;

      MessageState state = MessageState.Start;

      for (int i = 0; i != buffer.Length; i++)
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

    static UInt16 Calc_Checksum(ref byte[] buffer, int Length)
    {
      ushort checksum = 0;
      for (int k = 0; k != Length; k++)
      {
        checksum += buffer[k];
      }
      return (ushort)~checksum;
    }

    //internal static void StripMessage(ref byte[] buffer, ref Stack<byte[]> message_list)
    //{
    //  var index = 0;
    //  var state = MessageState.Start;
    //  for (int i = 0; i != buffer.Length; i++)
    //  {
    //    switch (state)
    //    {
    //      case MessageState.Start:
    //        //  checksum = 0;
    //        if (buffer[index + i] == USB_Commands.CMD_START)
    //        {
    //          state = MessageState.Start_Found;
    //          break;
    //        }
    //        return;
    //      case MessageState.Start_Found:
    //        var size = BitConverter.ToUInt16(buffer, index + i);
    //        //    int start_index = i;
    //        i++;
    //        state = MessageState.Size_Found;
    //        var l_sequence = BitConverter.ToUInt16(buffer, index + i + 1);
          
    //        try
    //        {
    //          byte[] dest_array = new byte[size + 8];
    //          Array.Copy(buffer, index, dest_array, 0, size + 8);
    //          message_list.Push(dest_array);
    //          index += size + 8;
    //          if (index >= buffer.Length)
    //            return;

    //          state = MessageState.Start;
    //          i = -1;
    //        }
    //        catch
    //        {
             
    //        }
    //        break;


    //    }
    //  }
    //}
    public static ushort sequence;
    public  static byte[] Create_Message(ref byte[] message, int command, bool resetSequence = false)
    {
      int length = message.Length + 8;
      byte[] result = new byte[length];

      sequence = resetSequence ? (ushort)0 : sequence;

      result[0] = USB_Commands.CMD_START;
      result[1] = (byte)(length);
      result[2] = (byte)(length >> 8);
      result[3] = (byte)(sequence);
      result[4] = (byte)(sequence >> 8);
      result[5] = (byte)(command);
      result[6] = (byte)(command >> 8);
      result[7] = 0; // data type
      Array.Copy(message, 0, result, 8, message.Length);
      sequence++;
      return result;

    }

    
  }
}