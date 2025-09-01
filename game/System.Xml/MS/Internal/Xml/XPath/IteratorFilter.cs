using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000637 RID: 1591
	internal class IteratorFilter : XPathNodeIterator
	{
		// Token: 0x060040D2 RID: 16594 RVA: 0x001657C3 File Offset: 0x001639C3
		internal IteratorFilter(XPathNodeIterator innerIterator, string name)
		{
			this._innerIterator = innerIterator;
			this._name = name;
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x001657D9 File Offset: 0x001639D9
		private IteratorFilter(IteratorFilter it)
		{
			this._innerIterator = it._innerIterator.Clone();
			this._name = it._name;
			this._position = it._position;
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0016580A File Offset: 0x00163A0A
		public override XPathNodeIterator Clone()
		{
			return new IteratorFilter(this);
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x00165812 File Offset: 0x00163A12
		public override XPathNavigator Current
		{
			get
			{
				return this._innerIterator.Current;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x0016581F File Offset: 0x00163A1F
		public override int CurrentPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x00165827 File Offset: 0x00163A27
		public override bool MoveNext()
		{
			while (this._innerIterator.MoveNext())
			{
				if (this._innerIterator.Current.LocalName == this._name)
				{
					this._position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002E42 RID: 11842
		private XPathNodeIterator _innerIterator;

		// Token: 0x04002E43 RID: 11843
		private string _name;

		// Token: 0x04002E44 RID: 11844
		private int _position;
	}
}
