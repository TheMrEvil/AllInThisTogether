using System;

namespace System.Xml
{
	// Token: 0x02000074 RID: 116
	internal abstract class BaseTreeIterator
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x00010D75 File Offset: 0x0000EF75
		internal BaseTreeIterator(DataSetMapper mapper)
		{
			this.mapper = mapper;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004CC RID: 1228
		internal abstract XmlNode CurrentNode { get; }

		// Token: 0x060004CD RID: 1229
		internal abstract bool Next();

		// Token: 0x060004CE RID: 1230
		internal abstract bool NextRight();

		// Token: 0x060004CF RID: 1231 RVA: 0x00010D84 File Offset: 0x0000EF84
		internal bool NextRowElement()
		{
			while (this.Next())
			{
				if (this.OnRowElement())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00010D9B File Offset: 0x0000EF9B
		internal bool NextRightRowElement()
		{
			return this.NextRight() && (this.OnRowElement() || this.NextRowElement());
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		internal bool OnRowElement()
		{
			XmlBoundElement xmlBoundElement = this.CurrentNode as XmlBoundElement;
			return xmlBoundElement != null && xmlBoundElement.Row != null;
		}

		// Token: 0x0400062E RID: 1582
		protected DataSetMapper mapper;
	}
}
