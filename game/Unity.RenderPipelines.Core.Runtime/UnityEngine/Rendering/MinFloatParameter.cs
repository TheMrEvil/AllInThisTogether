using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CB RID: 203
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinFloatParameter : FloatParameter
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001D98D File Offset: 0x0001BB8D
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001D995 File Offset: 0x0001BB95
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

		// Token: 0x06000683 RID: 1667 RVA: 0x0001D9A9 File Offset: 0x0001BBA9
		public MinFloatParameter(float value, float min, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003B3 RID: 947
		[NonSerialized]
		public float min;
	}
}
