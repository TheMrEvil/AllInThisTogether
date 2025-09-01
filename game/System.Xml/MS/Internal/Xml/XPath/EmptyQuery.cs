using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200062A RID: 1578
	internal sealed class EmptyQuery : Query
	{
		// Token: 0x0600407C RID: 16508 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XPathNavigator Advance()
		{
			return null;
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x00002068 File Offset: 0x00000268
		public override object Evaluate(XPathNodeIterator context)
		{
			return this;
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x0012B969 File Offset: 0x00129B69
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0000B528 File Offset: 0x00009728
		public override void Reset()
		{
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x001648E6 File Offset: 0x00162AE6
		public EmptyQuery()
		{
		}
	}
}
