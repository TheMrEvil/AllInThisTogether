using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CE RID: 206
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001DA14 File Offset: 0x0001BC14
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0001DA1C File Offset: 0x0001BC1C
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

		// Token: 0x0600068C RID: 1676 RVA: 0x0001DA30 File Offset: 0x0001BC30
		public NoInterpMaxFloatParameter(float value, float max, bool overrideState = false) : base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003B6 RID: 950
		[NonSerialized]
		public float max;
	}
}
