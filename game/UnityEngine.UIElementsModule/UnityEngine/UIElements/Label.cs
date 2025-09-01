using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014E RID: 334
	public class Label : TextElement
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002C3AE File Offset: 0x0002A5AE
		public Label() : this(string.Empty)
		{
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002C3BD File Offset: 0x0002A5BD
		public Label(string text)
		{
			base.AddToClassList(Label.ussClassName);
			this.text = text;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002C3DB File Offset: 0x0002A5DB
		// Note: this type is marked as 'beforefieldinit'.
		static Label()
		{
		}

		// Token: 0x040004C2 RID: 1218
		public new static readonly string ussClassName = "unity-label";

		// Token: 0x0200014F RID: 335
		public new class UxmlFactory : UxmlFactory<Label, Label.UxmlTraits>
		{
			// Token: 0x06000ADA RID: 2778 RVA: 0x0002C3E7 File Offset: 0x0002A5E7
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000150 RID: 336
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x06000ADB RID: 2779 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
			public UxmlTraits()
			{
			}
		}
	}
}
