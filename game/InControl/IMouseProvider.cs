using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000043 RID: 67
	public interface IMouseProvider
	{
		// Token: 0x0600035B RID: 859
		void Setup();

		// Token: 0x0600035C RID: 860
		void Reset();

		// Token: 0x0600035D RID: 861
		void Update();

		// Token: 0x0600035E RID: 862
		Vector2 GetPosition();

		// Token: 0x0600035F RID: 863
		float GetDeltaX();

		// Token: 0x06000360 RID: 864
		float GetDeltaY();

		// Token: 0x06000361 RID: 865
		float GetDeltaScroll();

		// Token: 0x06000362 RID: 866
		bool GetButtonIsPressed(Mouse control);

		// Token: 0x06000363 RID: 867
		bool GetButtonWasPressed(Mouse control);

		// Token: 0x06000364 RID: 868
		bool GetButtonWasReleased(Mouse control);

		// Token: 0x06000365 RID: 869
		bool HasMousePresent();
	}
}
