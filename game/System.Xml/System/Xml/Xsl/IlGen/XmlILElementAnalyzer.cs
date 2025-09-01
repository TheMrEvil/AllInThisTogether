using System;
using System.Collections;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004AA RID: 1194
	internal class XmlILElementAnalyzer : XmlILStateAnalyzer
	{
		// Token: 0x06002EBE RID: 11966 RVA: 0x00110C9C File Offset: 0x0010EE9C
		public XmlILElementAnalyzer(QilFactory fac) : base(fac)
		{
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x00110CBC File Offset: 0x0010EEBC
		public override QilNode Analyze(QilNode ndElem, QilNode ndContent)
		{
			this.parentInfo = XmlILConstructInfo.Write(ndElem);
			this.parentInfo.MightHaveNamespacesAfterAttributes = false;
			this.parentInfo.MightHaveAttributes = false;
			this.parentInfo.MightHaveDuplicateAttributes = false;
			this.parentInfo.MightHaveNamespaces = !this.parentInfo.IsNamespaceInScope;
			this.dupAttrs.Clear();
			return base.Analyze(ndElem, ndContent);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x00110D25 File Offset: 0x0010EF25
		protected override void AnalyzeLoop(QilLoop ndLoop, XmlILConstructInfo info)
		{
			if (ndLoop.XmlType.MaybeMany)
			{
				this.CheckAttributeNamespaceConstruct(ndLoop.XmlType);
			}
			base.AnalyzeLoop(ndLoop, info);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00110D48 File Offset: 0x0010EF48
		protected override void AnalyzeCopy(QilNode ndCopy, XmlILConstructInfo info)
		{
			if (ndCopy.NodeType == QilNodeType.AttributeCtor)
			{
				this.AnalyzeAttributeCtor(ndCopy as QilBinary, info);
			}
			else
			{
				this.CheckAttributeNamespaceConstruct(ndCopy.XmlType);
			}
			base.AnalyzeCopy(ndCopy, info);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00110D78 File Offset: 0x0010EF78
		private void AnalyzeAttributeCtor(QilBinary ndAttr, XmlILConstructInfo info)
		{
			if (ndAttr.Left.NodeType == QilNodeType.LiteralQName)
			{
				QilName qilName = ndAttr.Left as QilName;
				this.parentInfo.MightHaveAttributes = true;
				if (!this.parentInfo.MightHaveDuplicateAttributes)
				{
					XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.attrNames.Add(qilName.LocalName), this.attrNames.Add(qilName.NamespaceUri));
					int i;
					for (i = 0; i < this.dupAttrs.Count; i++)
					{
						XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)this.dupAttrs[i];
						if (xmlQualifiedName2.Name == xmlQualifiedName.Name && xmlQualifiedName2.Namespace == xmlQualifiedName.Namespace)
						{
							this.parentInfo.MightHaveDuplicateAttributes = true;
						}
					}
					if (i >= this.dupAttrs.Count)
					{
						this.dupAttrs.Add(xmlQualifiedName);
					}
				}
				if (!info.IsNamespaceInScope)
				{
					this.parentInfo.MightHaveNamespaces = true;
					return;
				}
			}
			else
			{
				this.CheckAttributeNamespaceConstruct(ndAttr.XmlType);
			}
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x00110E74 File Offset: 0x0010F074
		private void CheckAttributeNamespaceConstruct(XmlQueryType typ)
		{
			if ((typ.NodeKinds & XmlNodeKindFlags.Attribute) != XmlNodeKindFlags.None)
			{
				this.parentInfo.MightHaveAttributes = true;
				this.parentInfo.MightHaveDuplicateAttributes = true;
				this.parentInfo.MightHaveNamespaces = true;
			}
			if ((typ.NodeKinds & XmlNodeKindFlags.Namespace) != XmlNodeKindFlags.None)
			{
				this.parentInfo.MightHaveNamespaces = true;
				if (this.parentInfo.MightHaveAttributes)
				{
					this.parentInfo.MightHaveNamespacesAfterAttributes = true;
				}
			}
		}

		// Token: 0x040024FD RID: 9469
		private NameTable attrNames = new NameTable();

		// Token: 0x040024FE RID: 9470
		private ArrayList dupAttrs = new ArrayList();
	}
}
