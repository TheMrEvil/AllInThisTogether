using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020005DF RID: 1503
	internal struct InterlockedGate
	{
		// Token: 0x06003048 RID: 12360 RVA: 0x000A680C File Offset: 0x000A4A0C
		internal void Reset()
		{
			this.m_State = 0;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000A6818 File Offset: 0x000A4A18
		internal bool Trigger(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 2, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000A6848 File Offset: 0x000A4A48
		internal bool StartTriggering(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 1, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000A6878 File Offset: 0x000A4A78
		internal void FinishTriggering()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 2, 1) != 1)
			{
				throw new InternalException();
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000A6890 File Offset: 0x000A4A90
		internal bool StartSignaling(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 3, 2);
			if (exclusive && (num == 3 || num == 4))
			{
				throw new InternalException();
			}
			return num == 2;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000A68C0 File Offset: 0x000A4AC0
		internal void FinishSignaling()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 4, 3) != 3)
			{
				throw new InternalException();
			}
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000A68D8 File Offset: 0x000A4AD8
		internal bool Complete()
		{
			return Interlocked.CompareExchange(ref this.m_State, 5, 4) == 4;
		}

		// Token: 0x04001B07 RID: 6919
		private int m_State;

		// Token: 0x04001B08 RID: 6920
		internal const int Open = 0;

		// Token: 0x04001B09 RID: 6921
		internal const int Triggering = 1;

		// Token: 0x04001B0A RID: 6922
		internal const int Triggered = 2;

		// Token: 0x04001B0B RID: 6923
		internal const int Signaling = 3;

		// Token: 0x04001B0C RID: 6924
		internal const int Signaled = 4;

		// Token: 0x04001B0D RID: 6925
		internal const int Completed = 5;
	}
}
