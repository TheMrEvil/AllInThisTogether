using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000654 RID: 1620
	internal abstract class ValueQuery : Query
	{
		// Token: 0x060041B8 RID: 16824 RVA: 0x001648E6 File Offset: 0x00162AE6
		public ValueQuery()
		{
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x001686D0 File Offset: 0x001668D0
		protected ValueQuery(ValueQuery other) : base(other)
		{
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x0000B528 File Offset: 0x00009728
		public sealed override void Reset()
		{
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x001686D9 File Offset: 0x001668D9
		public sealed override XPathNavigator Current
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x001686D9 File Offset: 0x001668D9
		public sealed override int CurrentPosition
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x001686D9 File Offset: 0x001668D9
		public sealed override int Count
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x001686D9 File Offset: 0x001668D9
		public sealed override XPathNavigator Advance()
		{
			throw XPathException.Create("Expression must evaluate to a node-set.");
		}
	}
}
