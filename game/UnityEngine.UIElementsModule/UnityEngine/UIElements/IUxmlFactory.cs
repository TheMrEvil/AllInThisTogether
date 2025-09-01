using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EC RID: 748
	public interface IUxmlFactory
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060018D5 RID: 6357
		string uxmlName { get; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060018D6 RID: 6358
		string uxmlNamespace { get; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060018D7 RID: 6359
		string uxmlQualifiedName { get; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060018D8 RID: 6360
		bool canHaveAnyAttribute { get; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060018D9 RID: 6361
		IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription { get; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060018DA RID: 6362
		IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription { get; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060018DB RID: 6363
		string substituteForTypeName { get; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060018DC RID: 6364
		string substituteForTypeNamespace { get; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060018DD RID: 6365
		string substituteForTypeQualifiedName { get; }

		// Token: 0x060018DE RID: 6366
		bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x060018DF RID: 6367
		VisualElement Create(IUxmlAttributes bag, CreationContext cc);
	}
}
