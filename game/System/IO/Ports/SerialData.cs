using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of character that was received on the serial port of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200052D RID: 1325
	public enum SerialData
	{
		/// <summary>A character was received and placed in the input buffer.</summary>
		// Token: 0x04001710 RID: 5904
		Chars = 1,
		/// <summary>The end of file character was received and placed in the input buffer.</summary>
		// Token: 0x04001711 RID: 5905
		Eof
	}
}
