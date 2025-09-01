using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000641 RID: 1601
	internal sealed class OperandQuery : ValueQuery
	{
		// Token: 0x06004131 RID: 16689 RVA: 0x001668D0 File Offset: 0x00164AD0
		public OperandQuery(object val)
		{
			this.val = val;
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x001668DF File Offset: 0x00164ADF
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.val;
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x001668E7 File Offset: 0x00164AE7
		public override XPathResultType StaticType
		{
			get
			{
				return base.GetXPathType(this.val);
			}
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x04002E58 RID: 11864
		internal object val;
	}
}
