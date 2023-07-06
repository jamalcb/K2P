using System;
using System.Collections.Generic;
using System.Text;

namespace K2p.Statics
{
    public class TcpResult
    {
      public TcpResult(int length = 1400)
      {
        Buffer = new byte[length];
      }
      public int Length;
      public byte[] Buffer;
    }
}
