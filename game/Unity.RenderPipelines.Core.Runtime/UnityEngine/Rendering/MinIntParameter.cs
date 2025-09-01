using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C3 RID: 195
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinIntParameter : IntParameter
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001D840 File Offset: 0x0001BA40
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001D848 File Offset: 0x0001BA48
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

		// Token: 0x0600066E RID: 1646 RVA: 0x0001D85C File Offset: 0x0001BA5C
		public MinIntParameter(int value, int min, bool overrideState = false) : base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040003AB RID: 939
		[NonSerialized]
		public int min;
	}
}
