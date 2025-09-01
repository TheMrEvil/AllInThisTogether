using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200065D RID: 1629
	internal sealed class XPathEmptyIterator : ResetableIterator
	{
		// Token: 0x060041F2 RID: 16882 RVA: 0x00166C66 File Offset: 0x00164E66
		private XPathEmptyIterator()
		{
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x0000B528 File Offset: 0x00009728
		public override void Reset()
		{
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x00168D28 File Offset: 0x00166F28
		// Note: this type is marked as 'beforefieldinit'.
		static XPathEmptyIterator()
		{
		}

		// Token: 0x04002EAC RID: 11948
		public static XPathEmptyIterator Instance = new XPathEmptyIterator();
	}
}
