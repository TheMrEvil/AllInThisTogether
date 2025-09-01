using System;

namespace Photon.Realtime
{
	// Token: 0x02000035 RID: 53
	public abstract class PhotonPing : IDisposable
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00008711 File Offset: 0x00006911
		public virtual bool StartPing(string ip)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008718 File Offset: 0x00006918
		public virtual bool Done()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000871F File Offset: 0x0000691F
		public virtual void Dispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008726 File Offset: 0x00006926
		protected internal void Init()
		{
			this.GotResult = false;
			this.Successful = false;
			this.PingId = (byte)PhotonPing.RandomIdProvider.Next(255);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000874C File Offset: 0x0000694C
		protected PhotonPing()
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000877F File Offset: 0x0000697F
		// Note: this type is marked as 'beforefieldinit'.
		static PhotonPing()
		{
		}

		// Token: 0x040001B9 RID: 441
		public string DebugString = "";

		// Token: 0x040001BA RID: 442
		public bool Successful;

		// Token: 0x040001BB RID: 443
		protected internal bool GotResult;

		// Token: 0x040001BC RID: 444
		protected internal int PingLength = 13;

		// Token: 0x040001BD RID: 445
		protected internal byte[] PingBytes = new byte[]
		{
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			125,
			0
		};

		// Token: 0x040001BE RID: 446
		protected internal byte PingId;

		// Token: 0x040001BF RID: 447
		private static readonly Random RandomIdProvider = new Random();
	}
}
