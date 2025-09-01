using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CF RID: 207
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedFloatParameter : FloatParameter
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001DA41 File Offset: 0x0001BC41
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001DA49 File Offset: 0x0001BC49
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

		// Token: 0x0600068F RID: 1679 RVA: 0x0001DA63 File Offset: 0x0001BC63
		public ClampedFloatParameter(float value, float min, float max, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003B7 RID: 951
		[NonSerialized]
		public float min;

		// Token: 0x040003B8 RID: 952
		[NonSerialized]
		public float max;
	}
}
