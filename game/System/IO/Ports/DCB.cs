using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x0200053B RID: 1339
	[StructLayout(LayoutKind.Sequential)]
	internal class DCB
	{
		// Token: 0x06002B5C RID: 11100 RVA: 0x00094508 File Offset: 0x00092708
		public void SetValues(int baud_rate, Parity parity, int byte_size, StopBits sb, Handshake hs)
		{
			switch (sb)
			{
			case StopBits.One:
				this.stop_bits = 0;
				break;
			case StopBits.Two:
				this.stop_bits = 2;
				break;
			case StopBits.OnePointFive:
				this.stop_bits = 1;
				break;
			}
			this.baud_rate = baud_rate;
			this.parity = (byte)parity;
			this.byte_size = (byte)byte_size;
			this.flags &= -8965;
			switch (hs)
			{
			case Handshake.None:
				break;
			case Handshake.XOnXOff:
				this.flags |= 768;
				return;
			case Handshake.RequestToSend:
				this.flags |= 8196;
				return;
			case Handshake.RequestToSendXOnXOff:
				this.flags |= 8964;
				break;
			default:
				return;
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0000219B File Offset: 0x0000039B
		public DCB()
		{
		}

		// Token: 0x0400176D RID: 5997
		public int dcb_length;

		// Token: 0x0400176E RID: 5998
		public int baud_rate;

		// Token: 0x0400176F RID: 5999
		public int flags;

		// Token: 0x04001770 RID: 6000
		public short w_reserved;

		// Token: 0x04001771 RID: 6001
		public short xon_lim;

		// Token: 0x04001772 RID: 6002
		public short xoff_lim;

		// Token: 0x04001773 RID: 6003
		public byte byte_size;

		// Token: 0x04001774 RID: 6004
		public byte parity;

		// Token: 0x04001775 RID: 6005
		public byte stop_bits;

		// Token: 0x04001776 RID: 6006
		public byte xon_char;

		// Token: 0x04001777 RID: 6007
		public byte xoff_char;

		// Token: 0x04001778 RID: 6008
		public byte error_char;

		// Token: 0x04001779 RID: 6009
		public byte eof_char;

		// Token: 0x0400177A RID: 6010
		public byte evt_char;

		// Token: 0x0400177B RID: 6011
		public short w_reserved1;

		// Token: 0x0400177C RID: 6012
		private const int fOutxCtsFlow = 4;

		// Token: 0x0400177D RID: 6013
		private const int fOutX = 256;

		// Token: 0x0400177E RID: 6014
		private const int fInX = 512;

		// Token: 0x0400177F RID: 6015
		private const int fRtsControl2 = 8192;
	}
}
