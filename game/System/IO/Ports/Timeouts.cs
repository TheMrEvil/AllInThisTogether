using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x0200053C RID: 1340
	[StructLayout(LayoutKind.Sequential)]
	internal class Timeouts
	{
		// Token: 0x06002B5E RID: 11102 RVA: 0x000945BE File Offset: 0x000927BE
		public Timeouts(int read_timeout, int write_timeout)
		{
			this.SetValues(read_timeout, write_timeout);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000945CE File Offset: 0x000927CE
		public void SetValues(int read_timeout, int write_timeout)
		{
			this.ReadIntervalTimeout = uint.MaxValue;
			this.ReadTotalTimeoutMultiplier = uint.MaxValue;
			this.ReadTotalTimeoutConstant = (uint)((read_timeout == -1) ? -2 : read_timeout);
			this.WriteTotalTimeoutMultiplier = 0U;
			this.WriteTotalTimeoutConstant = (uint)((write_timeout == -1) ? -1 : write_timeout);
		}

		// Token: 0x04001780 RID: 6016
		public uint ReadIntervalTimeout;

		// Token: 0x04001781 RID: 6017
		public uint ReadTotalTimeoutMultiplier;

		// Token: 0x04001782 RID: 6018
		public uint ReadTotalTimeoutConstant;

		// Token: 0x04001783 RID: 6019
		public uint WriteTotalTimeoutMultiplier;

		// Token: 0x04001784 RID: 6020
		public uint WriteTotalTimeoutConstant;

		// Token: 0x04001785 RID: 6021
		public const uint MaxDWord = 4294967295U;
	}
}
