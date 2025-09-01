using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C8 RID: 200
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedIntParameter : VolumeParameter<int>
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001D92F File Offset: 0x0001BB2F
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001D937 File Offset: 0x0001BB37
		public override int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp(value, this.min, this.max);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001D951 File Offset: 0x0001BB51
		public NoInterpClampedIntParameter(int value, int min, int max, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003B1 RID: 945
		[NonSerialized]
		public int min;

		// Token: 0x040003B2 RID: 946
		[NonSerialized]
		public int max;
	}
}
