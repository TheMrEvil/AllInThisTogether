using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000665 RID: 1637
	internal class XPathSingletonIterator : ResetableIterator
	{
		// Token: 0x0600424B RID: 16971 RVA: 0x0016AA2A File Offset: 0x00168C2A
		public XPathSingletonIterator(XPathNavigator nav)
		{
			this._nav = nav;
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x0016AA39 File Offset: 0x00168C39
		public XPathSingletonIterator(XPathNavigator nav, bool moved) : this(nav)
		{
			if (moved)
			{
				this._position = 1;
			}
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x0016AA4C File Offset: 0x00168C4C
		public XPathSingletonIterator(XPathSingletonIterator it)
		{
			this._nav = it._nav.Clone();
			this._position = it._position;
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x0016AA71 File Offset: 0x00168C71
		public override XPathNodeIterator Clone()
		{
			return new XPathSingletonIterator(this);
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x0016AA79 File Offset: 0x00168C79
		public override XPathNavigator Current
		{
			get
			{
				return this._nav;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x0016AA81 File Offset: 0x00168C81
		public override int CurrentPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x0001222F File Offset: 0x0001042F
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x0016AA89 File Offset: 0x00168C89
		public override bool MoveNext()
		{
			if (this._position == 0)
			{
				this._position = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x0016AA9D File Offset: 0x00168C9D
		public override void Reset()
		{
			this._position = 0;
		}

		// Token: 0x04002EEF RID: 12015
		private XPathNavigator _nav;

		// Token: 0x04002EF0 RID: 12016
		private int _position;
	}
}
