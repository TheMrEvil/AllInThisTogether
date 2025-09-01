using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000433 RID: 1075
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AttributeIterator
	{
		// Token: 0x06002AD1 RID: 10961 RVA: 0x001023FF File Offset: 0x001005FF
		public void Create(XPathNavigator context)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.needFirst = true;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0010241A File Offset: 0x0010061A
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToFirstAttribute();
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNextAttribute();
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x0010244D File Offset: 0x0010064D
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040021B5 RID: 8629
		private XPathNavigator navCurrent;

		// Token: 0x040021B6 RID: 8630
		private bool needFirst;
	}
}
