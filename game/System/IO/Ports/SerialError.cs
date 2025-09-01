using System;

namespace System.IO.Ports
{
	/// <summary>Specifies errors that occur on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200052E RID: 1326
	public enum SerialError
	{
		/// <summary>An input buffer overflow has occurred. There is either no room in the input buffer, or a character was received after the end-of-file (EOF) character.</summary>
		// Token: 0x04001713 RID: 5907
		RXOver = 1,
		/// <summary>A character-buffer overrun has occurred. The next character is lost.</summary>
		// Token: 0x04001714 RID: 5908
		Overrun,
		/// <summary>The hardware detected a parity error.</summary>
		// Token: 0x04001715 RID: 5909
		RXParity = 4,
		/// <summary>The hardware detected a framing error.</summary>
		// Token: 0x04001716 RID: 5910
		Frame = 8,
		/// <summary>The application tried to transmit a character, but the output buffer was full.</summary>
		// Token: 0x04001717 RID: 5911
		TXFull = 256
	}
}
