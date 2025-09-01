using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D6 RID: 214
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001DD4B File Offset: 0x0001BF4B
		public NoInterpVector2Parameter(Vector2 value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
