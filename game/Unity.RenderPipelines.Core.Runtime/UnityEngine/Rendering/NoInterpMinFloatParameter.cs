using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CC RID: 204
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001D9BA File Offset: 0x0001BBBA
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001D9C2 File Offset: 0x0001BBC2
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Max(value, this.min);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001D9D6 File Offset: 0x0001BBD6
		public NoInterpMinFloatParameter(float value, float min, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003B4 RID: 948
		[NonSerialized]
		public float min;
	}
}
