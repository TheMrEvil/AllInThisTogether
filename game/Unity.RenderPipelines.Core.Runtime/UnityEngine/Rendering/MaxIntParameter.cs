using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C5 RID: 197
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxIntParameter : IntParameter
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001D89A File Offset: 0x0001BA9A
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001D8A2 File Offset: 0x0001BAA2
		public override int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Min(value, this.max);
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001D8B6 File Offset: 0x0001BAB6
		public MaxIntParameter(int value, int max, bool overrideState = false) : base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003AD RID: 941
		[NonSerialized]
		public int max;
	}
}
