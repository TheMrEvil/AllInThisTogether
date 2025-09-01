using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001A5 RID: 421
	internal sealed class DocumentXPathNodeIterator_Empty : XPathNodeIterator
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x00065520 File Offset: 0x00063720
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNavigator nav)
		{
			this.nav = nav.Clone();
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00065534 File Offset: 0x00063734
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNodeIterator_Empty other)
		{
			this.nav = other.nav.Clone();
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0006554D File Offset: 0x0006374D
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_Empty(this);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00065555 File Offset: 0x00063755
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04000FFB RID: 4091
		private XPathNavigator nav;
	}
}
