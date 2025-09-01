using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D9 RID: 217
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x0001DDDA File Offset: 0x0001BFDA
		public Vector4Parameter(Vector4 value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001DDE4 File Offset: 0x0001BFE4
		public override void Interp(Vector4 from, Vector4 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
			this.m_Value.w = from.w + (to.w - from.w) * t;
		}
	}
}
