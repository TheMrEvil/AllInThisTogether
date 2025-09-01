using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x0200026E RID: 622
	internal sealed class WritePacketCache : IDisposable
	{
		// Token: 0x06001CFC RID: 7420 RVA: 0x00089B57 File Offset: 0x00087D57
		public WritePacketCache()
		{
			this._disposed = false;
			this._packets = new Stack<SNIPacket>();
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00089B74 File Offset: 0x00087D74
		public SNIPacket Take(SNIHandle sniHandle)
		{
			SNIPacket snipacket;
			if (this._packets.Count > 0)
			{
				snipacket = this._packets.Pop();
				SNINativeMethodWrapper.SNIPacketReset(sniHandle, SNINativeMethodWrapper.IOType.WRITE, snipacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
			}
			else
			{
				snipacket = new SNIPacket(sniHandle);
			}
			return snipacket;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00089BAE File Offset: 0x00087DAE
		public void Add(SNIPacket packet)
		{
			if (!this._disposed)
			{
				this._packets.Push(packet);
				return;
			}
			packet.Dispose();
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00089BCB File Offset: 0x00087DCB
		public void Clear()
		{
			while (this._packets.Count > 0)
			{
				this._packets.Pop().Dispose();
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00089BED File Offset: 0x00087DED
		public void Dispose()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				this.Clear();
			}
		}

		// Token: 0x04001427 RID: 5159
		private bool _disposed;

		// Token: 0x04001428 RID: 5160
		private Stack<SNIPacket> _packets;
	}
}
