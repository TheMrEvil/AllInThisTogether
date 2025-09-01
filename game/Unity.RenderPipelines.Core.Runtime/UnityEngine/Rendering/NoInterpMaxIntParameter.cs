using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C6 RID: 198
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxIntParameter : VolumeParameter<int>
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001D8C7 File Offset: 0x0001BAC7
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001D8CF File Offset: 0x0001BACF
		public override int value
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

		// Token: 0x06000677 RID: 1655 RVA: 0x0001D8E3 File Offset: 0x0001BAE3
		public NoInterpMaxIntParameter(int value, int max, bool overrideState = false) : base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040003AE RID: 942
		[NonSerialized]
		public int max;
	}
}
