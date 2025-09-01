using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D8 RID: 216
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0001DDD0 File Offset: 0x0001BFD0
		public NoInterpVector3Parameter(Vector3 value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
