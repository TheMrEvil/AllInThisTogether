using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D7 RID: 215
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0001DD55 File Offset: 0x0001BF55
		public Vector3Parameter(Vector3 value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001DD60 File Offset: 0x0001BF60
		public override void Interp(Vector3 from, Vector3 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
		}
	}
}
