using System;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000046 RID: 70
	internal interface ITweenValue
	{
		// Token: 0x060004D2 RID: 1234
		void TweenValue(float floatPercentage);

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004D3 RID: 1235
		bool ignoreTimeScale { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004D4 RID: 1236
		float duration { get; }

		// Token: 0x060004D5 RID: 1237
		bool ValidTarget();
	}
}
