using System;
using System.Text;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000404 RID: 1028
	internal class AttributeSet : ProtoTemplate
	{
		// Token: 0x060028BD RID: 10429 RVA: 0x000F4FED File Offset: 0x000F31ED
		public AttributeSet(QilName name, XslVersion xslVer) : base(XslNodeType.AttributeSet, name, xslVer)
		{
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000F4FF8 File Offset: 0x000F31F8
		public override string GetDebugName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<xsl:attribute-set name=\"");
			stringBuilder.Append(this.Name.QualifiedName);
			stringBuilder.Append("\">");
			return stringBuilder.ToString();
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000F502E File Offset: 0x000F322E
		public new void AddContent(XslNode node)
		{
			base.AddContent(node);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000F5037 File Offset: 0x000F3237
		public void MergeContent(AttributeSet other)
		{
			base.InsertContent(other.Content);
		}

		// Token: 0x04002052 RID: 8274
		public CycleCheck CycleCheck;
	}
}
