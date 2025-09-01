using System;

namespace UnityEngine.UI
{
	// Token: 0x02000005 RID: 5
	public interface ICanvasElement
	{
		// Token: 0x06000013 RID: 19
		void Rebuild(CanvasUpdate executing);

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20
		Transform transform { get; }

		// Token: 0x06000015 RID: 21
		void LayoutComplete();

		// Token: 0x06000016 RID: 22
		void GraphicUpdateComplete();

		// Token: 0x06000017 RID: 23
		bool IsDestroyed();
	}
}
