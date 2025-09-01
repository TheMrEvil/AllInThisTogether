using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C9 RID: 201
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatParameter : VolumeParameter<float>
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x0001D96A File Offset: 0x0001BB6A
		public FloatParameter(float value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001D974 File Offset: 0x0001BB74
		public sealed override void Interp(float from, float to, float t)
		{
			this.m_Value = from + (to - from) * t;
		}
	}
}
