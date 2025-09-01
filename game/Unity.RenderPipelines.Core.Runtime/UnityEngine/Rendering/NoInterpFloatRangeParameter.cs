using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D2 RID: 210
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001DB63 File Offset: 0x0001BD63
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0001DB6B File Offset: 0x0001BD6B
		public override Vector2 value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value.x = Mathf.Max(value.x, this.min);
				this.m_Value.y = Mathf.Min(value.y, this.max);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001DBA5 File Offset: 0x0001BDA5
		public NoInterpFloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003BD RID: 957
		[NonSerialized]
		public float min;

		// Token: 0x040003BE RID: 958
		[NonSerialized]
		public float max;
	}
}
