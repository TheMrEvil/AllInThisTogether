using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D0 RID: 208
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0001DA84 File Offset: 0x0001BC84
		public override float value
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

		// Token: 0x06000692 RID: 1682 RVA: 0x0001DA9E File Offset: 0x0001BC9E
		public NoInterpClampedFloatParameter(float value, float min, float max, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003B9 RID: 953
		[NonSerialized]
		public float min;

		// Token: 0x040003BA RID: 954
		[NonSerialized]
		public float max;
	}
}
