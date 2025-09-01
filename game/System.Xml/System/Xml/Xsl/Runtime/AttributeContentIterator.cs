using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000435 RID: 1077
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AttributeContentIterator
	{
		// Token: 0x06002AD7 RID: 10967 RVA: 0x001024DF File Offset: 0x001006DF
		public void Create(XPathNavigator context)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.needFirst = true;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x001024FA File Offset: 0x001006FA
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !XmlNavNeverFilter.MoveToFirstAttributeContent(this.navCurrent);
				return !this.needFirst;
			}
			return XmlNavNeverFilter.MoveToNextAttributeContent(this.navCurrent);
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x0010252D File Offset: 0x0010072D
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040021B9 RID: 8633
		private XPathNavigator navCurrent;

		// Token: 0x040021BA RID: 8634
		private bool needFirst;
	}
}
