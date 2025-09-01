using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000013 RID: 19
	internal class NCommandPool
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000786C File Offset: 0x00005A6C
		public NCommand Acquire(EnetPeer peer, byte[] inBuff, ref int readingOffset)
		{
			Stack<NCommand> obj = this.pool;
			NCommand ncommand;
			lock (obj)
			{
				bool flag2 = this.pool.Count == 0;
				if (flag2)
				{
					ncommand = new NCommand(peer, inBuff, ref readingOffset);
					ncommand.returnPool = this;
				}
				else
				{
					ncommand = this.pool.Pop();
					ncommand.Initialize(peer, inBuff, ref readingOffset);
				}
			}
			return ncommand;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000078F0 File Offset: 0x00005AF0
		public NCommand Acquire(EnetPeer peer, byte commandType, StreamBuffer payload, byte channel)
		{
			Stack<NCommand> obj = this.pool;
			NCommand ncommand;
			lock (obj)
			{
				bool flag2 = this.pool.Count == 0;
				if (flag2)
				{
					ncommand = new NCommand(peer, commandType, payload, channel);
					ncommand.returnPool = this;
				}
				else
				{
					ncommand = this.pool.Pop();
					ncommand.Initialize(peer, commandType, payload, channel);
				}
			}
			return ncommand;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007978 File Offset: 0x00005B78
		public void Release(NCommand nCommand)
		{
			nCommand.Reset();
			Stack<NCommand> obj = this.pool;
			lock (obj)
			{
				this.pool.Push(nCommand);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000079CC File Offset: 0x00005BCC
		public NCommandPool()
		{
		}

		// Token: 0x04000079 RID: 121
		private readonly Stack<NCommand> pool = new Stack<NCommand>();
	}
}
