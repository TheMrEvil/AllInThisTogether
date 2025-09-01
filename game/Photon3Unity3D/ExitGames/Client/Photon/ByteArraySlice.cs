using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000002 RID: 2
	public class ByteArraySlice : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal ByteArraySlice(ByteArraySlicePool returnPool, int stackIndex)
		{
			this.Buffer = ((stackIndex == 0) ? null : new byte[1 << stackIndex]);
			this.returnPool = returnPool;
			this.stackIndex = stackIndex;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207F File Offset: 0x0000027F
		public ByteArraySlice(byte[] buffer, int offset = 0, int count = 0)
		{
			this.Buffer = buffer;
			this.Count = count;
			this.Offset = offset;
			this.returnPool = null;
			this.stackIndex = -1;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020AC File Offset: 0x000002AC
		public ByteArraySlice()
		{
			this.returnPool = null;
			this.stackIndex = -1;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C4 File Offset: 0x000002C4
		public void Dispose()
		{
			this.Release();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public bool Release()
		{
			bool flag = this.stackIndex < 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.Count = 0;
				this.Offset = 0;
				result = this.returnPool.Release(this, this.stackIndex);
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		public void Reset()
		{
			this.Count = 0;
			this.Offset = 0;
		}

		// Token: 0x04000001 RID: 1
		public byte[] Buffer;

		// Token: 0x04000002 RID: 2
		public int Offset;

		// Token: 0x04000003 RID: 3
		public int Count;

		// Token: 0x04000004 RID: 4
		private readonly ByteArraySlicePool returnPool;

		// Token: 0x04000005 RID: 5
		private readonly int stackIndex;
	}
}
