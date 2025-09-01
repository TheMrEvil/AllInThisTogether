using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C4 RID: 196
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinIntParameter : VolumeParameter<int>
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001D86D File Offset: 0x0001BA6D
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001D875 File Offset: 0x0001BA75
		public override int value
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

		// Token: 0x06000671 RID: 1649 RVA: 0x0001D889 File Offset: 0x0001BA89
		public NoInterpMinIntParameter(int value, int min, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003AC RID: 940
		[NonSerialized]
		public int min;
	}
}
