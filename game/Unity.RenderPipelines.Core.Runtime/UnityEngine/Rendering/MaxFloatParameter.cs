using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CD RID: 205
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxFloatParameter : FloatParameter
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001D9E7 File Offset: 0x0001BBE7
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001D9EF File Offset: 0x0001BBEF
		public override float value
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

		// Token: 0x06000689 RID: 1673 RVA: 0x0001DA03 File Offset: 0x0001BC03
		public MaxFloatParameter(float value, float max, bool overrideState = false) : base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003B5 RID: 949
		[NonSerialized]
		public float max;
	}
}
