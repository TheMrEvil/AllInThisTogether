using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000229 RID: 553
	public class CustomStyleResolvedEvent : EventBase<CustomStyleResolvedEvent>
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x00043370 File Offset: 0x00041570
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement visualElement = base.target as VisualElement;
				return (visualElement != null) ? visualElement.customStyle : null;
			}
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00043399 File Offset: 0x00041599
		public CustomStyleResolvedEvent()
		{
		}
	}
}
