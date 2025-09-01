using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012A RID: 298
	public class Box : VisualElement
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x00028601 File Offset: 0x00026801
		public Box()
		{
			base.AddToClassList(Box.ussClassName);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00028617 File Offset: 0x00026817
		// Note: this type is marked as 'beforefieldinit'.
		static Box()
		{
		}

		// Token: 0x0400044D RID: 1101
		public static readonly string ussClassName = "unity-box";

		// Token: 0x0200012B RID: 299
		public new class UxmlFactory : UxmlFactory<Box>
		{
			// Token: 0x06000A17 RID: 2583 RVA: 0x00028623 File Offset: 0x00026823
			public UxmlFactory()
			{
			}
		}
	}
}
