using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200022E RID: 558
	public class WaitForSecondsRealtime : CustomYieldInstruction
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00026F11 File Offset: 0x00025111
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x00026F19 File Offset: 0x00025119
		public float waitTime
		{
			[CompilerGenerated]
			get
			{
				return this.<waitTime>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<waitTime>k__BackingField = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00026F24 File Offset: 0x00025124
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_WaitUntilTime < 0f;
				if (flag)
				{
					this.m_WaitUntilTime = Time.realtimeSinceStartup + this.waitTime;
				}
				bool flag2 = Time.realtimeSinceStartup < this.m_WaitUntilTime;
				bool flag3 = !flag2;
				if (flag3)
				{
					this.Reset();
				}
				return flag2;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00026F7B File Offset: 0x0002517B
		public WaitForSecondsRealtime(float time)
		{
			this.waitTime = time;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00026F98 File Offset: 0x00025198
		public override void Reset()
		{
			this.m_WaitUntilTime = -1f;
		}

		// Token: 0x04000836 RID: 2102
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private float <waitTime>k__BackingField;

		// Token: 0x04000837 RID: 2103
		private float m_WaitUntilTime = -1f;
	}
}
