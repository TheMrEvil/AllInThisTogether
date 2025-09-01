using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200064D RID: 1613
	internal sealed class ReversePositionQuery : ForwardPositionQuery
	{
		// Token: 0x06004174 RID: 16756 RVA: 0x0016792A File Offset: 0x00165B2A
		public ReversePositionQuery(Query input) : base(input)
		{
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x00167933 File Offset: 0x00165B33
		private ReversePositionQuery(ReversePositionQuery other) : base(other)
		{
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x0016793C File Offset: 0x00165B3C
		public override XPathNodeIterator Clone()
		{
			return new ReversePositionQuery(this);
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x00167944 File Offset: 0x00165B44
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004178 RID: 16760 RVA: 0x0016795A File Offset: 0x00165B5A
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
