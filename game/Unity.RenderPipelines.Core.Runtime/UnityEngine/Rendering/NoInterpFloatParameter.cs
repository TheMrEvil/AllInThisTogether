using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CA RID: 202
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatParameter : VolumeParameter<float>
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0001D983 File Offset: 0x0001BB83
		public NoInterpFloatParameter(float value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
