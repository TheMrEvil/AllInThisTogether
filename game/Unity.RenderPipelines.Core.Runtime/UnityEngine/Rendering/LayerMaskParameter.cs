using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C0 RID: 192
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class LayerMaskParameter : VolumeParameter<LayerMask>
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x0001D810 File Offset: 0x0001BA10
		public LayerMaskParameter(LayerMask value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
