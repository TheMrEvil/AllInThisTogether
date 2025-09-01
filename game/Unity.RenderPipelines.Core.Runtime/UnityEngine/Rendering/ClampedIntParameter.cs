using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C7 RID: 199
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedIntParameter : IntParameter
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001D8FC File Offset: 0x0001BAFC
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

		// Token: 0x0600067A RID: 1658 RVA: 0x0001D916 File Offset: 0x0001BB16
		public ClampedIntParameter(int value, int min, int max, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003AF RID: 943
		[NonSerialized]
		public int min;

		// Token: 0x040003B0 RID: 944
		[NonSerialized]
		public int max;
	}
}
