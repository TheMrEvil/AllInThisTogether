using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C2 RID: 194
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpIntParameter : VolumeParameter<int>
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0001D836 File Offset: 0x0001BA36
		public NoInterpIntParameter(int value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
