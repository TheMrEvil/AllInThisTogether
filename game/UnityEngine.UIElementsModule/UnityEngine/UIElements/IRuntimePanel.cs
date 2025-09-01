using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024E RID: 590
	internal interface IRuntimePanel
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060011E9 RID: 4585
		PanelSettings panelSettings { get; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060011EA RID: 4586
		// (set) Token: 0x060011EB RID: 4587
		GameObject selectableGameObject { get; set; }
	}
}
