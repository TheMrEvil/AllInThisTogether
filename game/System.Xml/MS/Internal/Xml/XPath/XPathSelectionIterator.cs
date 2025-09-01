using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000663 RID: 1635
	internal class XPathSelectionIterator : ResetableIterator
	{
		// Token: 0x0600423F RID: 16959 RVA: 0x0016A911 File Offset: 0x00168B11
		internal XPathSelectionIterator(XPathNavigator nav, Query query)
		{
			this._nav = nav.Clone();
			this._query = query;
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x0016A92C File Offset: 0x00168B2C
		protected XPathSelectionIterator(XPathSelectionIterator it)
		{
			this._nav = it._nav.Clone();
			this._query = (Query)it._query.Clone();
			this._position = it._position;
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x0016A967 File Offset: 0x00168B67
		public override void Reset()
		{
			this._query.Reset();
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x0016A974 File Offset: 0x00168B74
		public override bool MoveNext()
		{
			XPathNavigator xpathNavigator = this._query.Advance();
			if (xpathNavigator != null)
			{
				this._position++;
				if (!this._nav.MoveTo(xpathNavigator))
				{
					this._nav = xpathNavigator.Clone();
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x0016A9BB File Offset: 0x00168BBB
		public override int Count
		{
			get
			{
				return this._query.Count;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x0016A9C8 File Offset: 0x00168BC8
		public override XPathNavigator Current
		{
			get
			{
				return this._nav;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x0016A9D0 File Offset: 0x00168BD0
		public override int CurrentPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x0016A9D8 File Offset: 0x00168BD8
		public override XPathNodeIterator Clone()
		{
			return new XPathSelectionIterator(this);
		}

		// Token: 0x04002EEC RID: 12012
		private XPathNavigator _nav;

		// Token: 0x04002EED RID: 12013
		private Query _query;

		// Token: 0x04002EEE RID: 12014
		private int _position;
	}
}
