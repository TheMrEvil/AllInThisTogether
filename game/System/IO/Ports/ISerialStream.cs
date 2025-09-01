using System;

namespace System.IO.Ports
{
	// Token: 0x0200052B RID: 1323
	internal interface ISerialStream : IDisposable
	{
		// Token: 0x06002A8F RID: 10895
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x06002A90 RID: 10896
		void Write(byte[] buffer, int offset, int count);

		// Token: 0x06002A91 RID: 10897
		void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs);

		// Token: 0x06002A92 RID: 10898
		void DiscardInBuffer();

		// Token: 0x06002A93 RID: 10899
		void DiscardOutBuffer();

		// Token: 0x06002A94 RID: 10900
		SerialSignal GetSignals();

		// Token: 0x06002A95 RID: 10901
		void SetSignal(SerialSignal signal, bool value);

		// Token: 0x06002A96 RID: 10902
		void SetBreakState(bool value);

		// Token: 0x06002A97 RID: 10903
		void Close();

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002A98 RID: 10904
		int BytesToRead { get; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002A99 RID: 10905
		int BytesToWrite { get; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002A9A RID: 10906
		// (set) Token: 0x06002A9B RID: 10907
		int ReadTimeout { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002A9C RID: 10908
		// (set) Token: 0x06002A9D RID: 10909
		int WriteTimeout { get; set; }
	}
}
