using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000059 RID: 89
	public interface IPanel : IDisposable
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001FA RID: 506
		VisualElement visualTree { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001FB RID: 507
		EventDispatcher dispatcher { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001FC RID: 508
		ContextType contextType { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001FD RID: 509
		FocusController focusController { get; }

		// Token: 0x060001FE RID: 510
		VisualElement Pick(Vector2 point);

		// Token: 0x060001FF RID: 511
		VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000200 RID: 512
		ContextualMenuManager contextualMenuManager { get; }
	}
}
