using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DA RID: 218
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0001DE75 File Offset: 0x0001C075
		public NoInterpVector4Parameter(Vector4 value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
