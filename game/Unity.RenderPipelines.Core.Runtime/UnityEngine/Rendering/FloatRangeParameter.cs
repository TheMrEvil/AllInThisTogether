using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D1 RID: 209
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001DABF File Offset: 0x0001BCBF
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

		// Token: 0x06000695 RID: 1685 RVA: 0x0001DAF9 File Offset: 0x0001BCF9
		public FloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001DB14 File Offset: 0x0001BD14
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}

		// Token: 0x040003BB RID: 955
		[NonSerialized]
		public float min;

		// Token: 0x040003BC RID: 956
		[NonSerialized]
		public float max;
	}
}
