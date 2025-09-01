using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C1 RID: 193
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class IntParameter : VolumeParameter<int>
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x0001D81A File Offset: 0x0001BA1A
		public IntParameter(int value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001D824 File Offset: 0x0001BA24
		public sealed override void Interp(int from, int to, float t)
		{
			this.m_Value = (int)((float)from + (float)(to - from) * t);
		}
	}
}
