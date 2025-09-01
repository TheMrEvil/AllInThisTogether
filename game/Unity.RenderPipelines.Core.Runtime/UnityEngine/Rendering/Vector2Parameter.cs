using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D5 RID: 213
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
		public Vector2Parameter(Vector2 value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001DCFC File Offset: 0x0001BEFC
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}
	}
}
