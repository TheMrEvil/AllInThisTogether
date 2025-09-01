using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000270 RID: 624
	internal struct ComputedTransitionProperty
	{
		// Token: 0x040008E0 RID: 2272
		public StylePropertyId id;

		// Token: 0x040008E1 RID: 2273
		public int durationMs;

		// Token: 0x040008E2 RID: 2274
		public int delayMs;

		// Token: 0x040008E3 RID: 2275
		public Func<float, float> easingCurve;
	}
}
