using System;
using System.Collections;
using System.Diagnostics;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000659 RID: 1625
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy, nq}")]
	internal class XPathArrayIterator : ResetableIterator
	{
		// Token: 0x060041D5 RID: 16853 RVA: 0x00168952 File Offset: 0x00166B52
		public XPathArrayIterator(IList list)
		{
			this.list = list;
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x00168961 File Offset: 0x00166B61
		public XPathArrayIterator(XPathArrayIterator it)
		{
			this.list = it.list;
			this.index = it.index;
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x00168981 File Offset: 0x00166B81
		public XPathArrayIterator(XPathNodeIterator nodeIterator)
		{
			this.list = new ArrayList();
			while (nodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = nodeIterator.Current;
				this.list.Add(xpathNavigator.Clone());
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x001689B5 File Offset: 0x00166BB5
		public IList AsList
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x001689BD File Offset: 0x00166BBD
		public override XPathNodeIterator Clone()
		{
			return new XPathArrayIterator(this);
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x001689C5 File Offset: 0x00166BC5
		public override XPathNavigator Current
		{
			get
			{
				if (this.index < 1)
				{
					throw new InvalidOperationException(SR.Format("Enumeration has not started. Call MoveNext.", string.Empty));
				}
				return (XPathNavigator)this.list[this.index - 1];
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x001689FD File Offset: 0x00166BFD
		public override int CurrentPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x00168A05 File Offset: 0x00166C05
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00168A12 File Offset: 0x00166C12
		public override bool MoveNext()
		{
			if (this.index == this.list.Count)
			{
				return false;
			}
			this.index++;
			return true;
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x00168A38 File Offset: 0x00166C38
		public override void Reset()
		{
			this.index = 0;
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x00168A41 File Offset: 0x00166C41
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x00168A4E File Offset: 0x00166C4E
		private object debuggerDisplayProxy
		{
			get
			{
				if (this.index >= 1)
				{
					return new XPathNavigator.DebuggerDisplayProxy(this.Current);
				}
				return null;
			}
		}

		// Token: 0x04002EA2 RID: 11938
		protected IList list;

		// Token: 0x04002EA3 RID: 11939
		protected int index;
	}
}
