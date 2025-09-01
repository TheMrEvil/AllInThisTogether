using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BF RID: 191
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class BoolParameter : VolumeParameter<bool>
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x0001D806 File Offset: 0x0001BA06
		public BoolParameter(bool value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
