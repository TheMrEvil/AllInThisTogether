using System;
using System.Runtime.CompilerServices;

namespace Febucci.UI
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public struct TimeData
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public float timeSinceStart
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<timeSinceStart>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<timeSinceStart>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public float deltaTime
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<deltaTime>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<deltaTime>k__BackingField = value;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public void RestartTime()
		{
			this.timeSinceStart = 0f;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000207F File Offset: 0x0000027F
		internal void IncreaseTime()
		{
			this.timeSinceStart += this.deltaTime;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002094 File Offset: 0x00000294
		internal void UpdateDeltaTime(float deltaTime)
		{
			this.deltaTime = deltaTime;
			if (deltaTime < 0f)
			{
				deltaTime = 0f;
			}
		}

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		private float <timeSinceStart>k__BackingField;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private float <deltaTime>k__BackingField;
	}
}
