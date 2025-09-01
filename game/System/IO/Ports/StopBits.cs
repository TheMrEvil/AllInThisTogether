using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the number of stop bits used on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000539 RID: 1337
	public enum StopBits
	{
		/// <summary>No stop bits are used. This value is not supported by the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> property.</summary>
		// Token: 0x04001748 RID: 5960
		None,
		/// <summary>One stop bit is used.</summary>
		// Token: 0x04001749 RID: 5961
		One,
		/// <summary>Two stop bits are used.</summary>
		// Token: 0x0400174A RID: 5962
		Two,
		/// <summary>1.5 stop bits are used.</summary>
		// Token: 0x0400174B RID: 5963
		OnePointFive
	}
}
